using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotCreator.Core.Common.Helpers
{
    public static class Extensions
    {
        public static void Log(this ILogger logger, LogLevel level, string message, int userId, Exception ex = null)
        {
            var msg = $"UserId: {userId}. {message}";
            logger.Log(level, ex, msg);
        }

        public static void LogInfo(this ILogger logger, string message, int userId)
        {
            logger.Log(LogLevel.Information, message, userId);
        }

        public static void LogErr(this ILogger logger, string message, int userId)
        {
            logger.Log(LogLevel.Error, message, userId);
        }

        public static void LogErr(this ILogger logger, string message, int userId, Exception ex)
        {
            logger.Log(LogLevel.Error, message, userId, ex);
        }

        public static void LogWarn(this ILogger logger, string message, int userId)
        {
            logger.Log(LogLevel.Warning, message, userId);
        }
    }
}
