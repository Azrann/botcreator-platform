using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using channel.telegram.bot.boundedContext.Services;
using Core.Common;
using Core.Common.Extensions;
using log4net.Util;
using Mentor.telegram.bot.boundedContext.Services;
using Newtonsoft.Json;

namespace Bot.telegram.funnel.Actions
{
    public class CleanUpChannelActionAsyncProcessor : ActionProcessorAsync<CleanUpChannelActionAsync>
    {
        private readonly IChannelStorageService _channelStorageService;
        private readonly ICoinApiStorageService _coinApiStorageService;
        private readonly IChannelService _channelService;
        private readonly IUserInterfaceService _interfaceService;

        public CleanUpChannelActionAsyncProcessor(IChannelStorageService channelStorageService, 
            ICoinApiStorageService coinApiStorageService, IChannelService channelService,
            IUserInterfaceService interfaceService)
        {
            _channelStorageService = channelStorageService;
            _coinApiStorageService = coinApiStorageService;
            _channelService = channelService;
            _interfaceService = interfaceService;
        }

        public override async Task<bool> ExecuteAsync(CleanUpChannelActionAsync actionAsync, Dictionary<string, object> parameters)
        {
            Log.LogInf("CleanUpChannelActionAsyncProcessor executing", actionAsync.CorrelationId);
            var telegramChannel = _channelStorageService.GetById(actionAsync.ChannelId);

            if (telegramChannel.ActionType == ActionType.None)
            {
                return true;
            }

            List<string> paidUsers = new List<string>();
            if (telegramChannel.DataStreamType == DataStreamType.GoogleDrive)
            {
                return true;
            }

            if (telegramChannel.DataStreamType == DataStreamType.Url)
            {
                paidUsers = await GetSubscribersUsernames(actionAsync, parameters, telegramChannel);
                if (paidUsers == null || paidUsers.Count == 0) 
                {
                    Log.LogInf("CleanUpChannelActionAsyncProcessor could not get paid users.", actionAsync.CorrelationId);
                    return false;
                }
                paidUsers = paidUsers.Select(x => x.Replace("@", "")).ToList();
                if (telegramChannel.Excluded != null)
                {
                    paidUsers.AddRange(telegramChannel.Excluded);
                }
            }

            Log.LogInf("CleanUpChannelActionAsyncProcessor got subscribers", actionAsync.CorrelationId);
            Log.LogInf($"CleanUpChannelActionAsyncProcessor paid users = {JsonConvertExt.SerializeObject(paidUsers)}", actionAsync.CorrelationId);
            int i = 0;

            var latestUsers = await GetParticipants(actionAsync, telegramChannel);

            while (latestUsers == null && i < 5)
            {
                i++;
                Thread.Sleep(100);
                latestUsers = await GetParticipants(actionAsync, telegramChannel);
            }

            if (latestUsers == null)
            {
                var user = await _channelService.GetUserByUserName("REPLACE_WITH_YOUR_VALUE", Guid.NewGuid());
                await _interfaceService.SendMessageCommandByUserId("Could not retrieved list of participants for channel.", user.Item1, parameters);
            }

            Log.LogInf($"CleanUpChannelActionAsyncProcessor got {latestUsers.Count} participants", actionAsync.CorrelationId);

            List<GenericUser> usersToBeRemovedOrNotified = GetUsersToBeRemovedOrNotified(latestUsers, paidUsers).Take(5).ToList(); // usersToBeRemovedOrNotified

            if (telegramChannel.ActionType == ActionType.Notify)
            {
                var userList2 = JsonConvertExt.SerializeObject(usersToBeRemovedOrNotified.Select(x =>
                    !string.IsNullOrEmpty(x.Username) ? $"@{x.Username}" : $"FirsName-{x.Firstname}-{x.Lastname}"));
                Log.LogInf(userList2, actionAsync.CorrelationId);
                foreach (var telegramChannelAdmin in telegramChannel.Admins)
                {
                    try
                    {
                        var user = await _channelService.GetUserByUserName(telegramChannelAdmin, Guid.NewGuid());
                        var userList = JsonConvertExt.SerializeObject(usersToBeRemovedOrNotified.Select(x =>
                            !string.IsNullOrEmpty(x.Username) ? $"@{x.Username}" : $"FirsName-{x.Firstname}-{x.Lastname}"));
                        await _interfaceService.SendMessageCommandByUserId(
                            $"The following {usersToBeRemovedOrNotified.Count} users are in the channel {telegramChannel.Name} without an active subscription. \n\n" +
                            $"{userList}", user.Item1, parameters);

                    }
                    catch (Exception e)
                    {
                        Log.LogErr($"CleanUpChannelActionAsyncProcessor Error notifying user {e.Message}",actionAsync.CorrelationId);
                        Log.LogErr($"CleanUpChannelActionAsyncProcessor Error notifying user exception {e.StackTrace}", actionAsync.CorrelationId);
                    }
                }
            }

            if (telegramChannel.ActionType == ActionType.Kick || telegramChannel.ActionType == ActionType.KickAndBan)
            {
                foreach (var telegramChannelAdmin in telegramChannel.Admins)
                {
                    try
                    {
                        var user = await _channelService.GetUserByUserName(telegramChannelAdmin, Guid.NewGuid());
                        var userList = JsonConvertExt.SerializeObject(usersToBeRemovedOrNotified.Select(x =>
                            !string.IsNullOrEmpty(x.Username) ? $"@{x.Username}" : $"FirsName-{x.Firstname}-{x.Lastname}"));
                        await _interfaceService.SendMessageCommandByUserId(
                            $"DELETING The following {usersToBeRemovedOrNotified.Count} users From channel {telegramChannel.Name}. \n\n" +
                            $"{userList}", user.Item1, parameters);

                    }
                    catch (Exception e)
                    {
                        Log.LogErr($"CleanUpChannelActionAsyncProcessor Error notifying user {e.Message}", actionAsync.CorrelationId);
                    }
                    Log.DebugExt($"Deleting users in the channel {telegramChannel.Name} without an active subscription. \n\n" +
                                 $"{JsonConvertExt.SerializeObject(usersToBeRemovedOrNotified)}");
                }

                Log.LogInf("CleanUpChannelActionAsyncProcessor removing users started", actionAsync.CorrelationId);
                foreach (var genericUser in usersToBeRemovedOrNotified)
                {
                    try
                    {
                        await _channelService.BanUserFromChannel(genericUser.Id, telegramChannel.Id,
                        genericUser.Hash, telegramChannel.AccessHash, actionAsync.CorrelationId);
                        
                    }
                    catch (Exception e)
                    {
                        Log.LogErr($"CleanUpChannelActionAsyncProcessor removing user {e.Message}", actionAsync.CorrelationId);
                    }
                }

                if (telegramChannel.ActionType == ActionType.Kick)
                {
                    if (usersToBeRemovedOrNotified.Count > 15)
                    {
                        Thread.Sleep(3000);
                    }

                    foreach (var genericUser in usersToBeRemovedOrNotified)
                    {
                        try
                        {
                            await _channelService.UnBanUserFromChannel(genericUser.Id, telegramChannel.Id,
                                genericUser.Hash, telegramChannel.AccessHash, actionAsync.CorrelationId);

                        }
                        catch (Exception e)
                        {
                            Log.LogErr($"CleanUpChannelActionAsyncProcessor unbanning user {e.Message}",
                                actionAsync.CorrelationId);
                        }
                    }
                }
            }

            return true;
        }

        private async Task<List<GenericUser>> GetParticipants(CleanUpChannelActionAsync actionAsync, TelegramChannelData telegramChannel)
        {
            try
            {
                return await _channelService.GetParticipantsByChannel(actionAsync.ChannelId,
                    telegramChannel.AccessHash, actionAsync.CorrelationId, "sessionIVO1");
            }
            catch (Exception e)
            {
                Log.LogErr($"CleanUpChannelActionAsyncProcessor GetParticipants error {e.Message}", actionAsync.CorrelationId);
                Log.LogErr($"CleanUpChannelActionAsyncProcessor GetParticipants error { e.StackTrace}", actionAsync.CorrelationId);
            }

            return null;
        }

        private List<GenericUser> GetUsersToBeRemovedOrNotified(List<GenericUser> latestUsers, List<string> paidUsers)
        {
            List<GenericUser> result = new List<GenericUser>();
            foreach (var user in latestUsers)
            {
                if (string.IsNullOrEmpty(user.Username))
                {
                    result.Add(user);
                }
                else if(!paidUsers.Select(x => x.ToLower()).Contains(user.Username.ToLower()))
                {
                    result.Add(user);
                }
            }

            return result;
        }

        private async Task<List<string>> GetSubscribersUsernames(CleanUpChannelActionAsync actionAsync, Dictionary<string, object> parameters,
            TelegramChannelData telegramChannel)
        {
            List<string> paidUsers = new List<string>();
            var data = _coinApiStorageService.GetCoinApiDataById(parameters.GetBotId());

            foreach (var url in telegramChannel.DataStreamUrl)
            {
                Log.LogInf($"Retrieving paid users from Url {url} {data.PublicKey}", actionAsync.CorrelationId);

                var result = await
                    HttpClientHelper.GetAsync(url, string.Empty, data.PublicKey);
                Log.LogInf($"HttpClientHelper.GetAsync completed from Url {url}  with status code {result.StatusCode}", actionAsync.CorrelationId);
                if (!result.IsSuccessStatusCode)
                {
                    Log.LogErr(
                        $"Error retrieving paid users from Url {telegramChannel.DataStreamUrl} with status code {result.StatusCode}", actionAsync.CorrelationId);
                    return null; 
                }
                Log.LogInf($"result.Content.ReadAsStringAsync before from Url {url}", actionAsync.CorrelationId);
                var stringResponse = await result.Content.ReadAsStringAsync();
                Log.LogInf($"result.Content.ReadAsStringAsync after from Url with result = {stringResponse.Length}", actionAsync.CorrelationId);
                
                try
                {
                    GetUsernamesResponse response =
                        JsonConvert.DeserializeObject<GetUsernamesResponse>(stringResponse);
                    if (response.Success && response.Payload != null && response.Payload.Count > 0)
                    {
                        Log.LogInf($"GetUsernamesResponse response {response.Payload.Count}", actionAsync.CorrelationId);
                        paidUsers.AddRange(response.Payload);
                    }
                }
                catch (Exception ex)
                {
                    Log.LogErr($"Error retrieving paid users from Url {url} exception {JsonConvertExt.SerializeObject(ex)}", actionAsync.CorrelationId);
                    return null;
                }
            }

            return paidUsers;
        }
    }
}