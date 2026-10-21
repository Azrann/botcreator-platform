import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, from, forkJoin, pipe } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { RequestService } from '../common/request.service';
import { BotTemplate } from 'src/app/core/models/dto/bot/bot-template';
import { BotTemplateView } from 'src/app/core/models/view/bot-template-view';
import { BotSettingView } from 'src/app/core/models/view/bot-setting-view';
import { BotSetting } from 'src/app/core/models/dto/bot/bot-setting';
import { BotWidgetHtml } from 'src/app/core/models/dto/bot/bot-widget-html';
import { UserBot } from 'src/app/core/models/dto/bot/user-bot';
import { UserBotView } from 'src/app/core/models/view/user-bot-view';
import { ServiceBot } from 'src/app/core/models/dto/bot/service-bot';
import { IMenuItem } from 'projects/coda/src/app/ui/interfaces/menuItem';
import { CompleteServiceBot } from '../../models/dto/bot/complete-service-bot';

@Injectable({ providedIn: 'root' })
export class BotService {

    private menuUrlTemplate: string = '../../../../assets/data/{lang}.menu.json'

    constructor(private http: HttpClient, private request: RequestService) { }

    getMenuUrl(language: string) {
        return this.menuUrlTemplate.replace('{lang}', language)
    }

    getTemplates() {
        return this.request.get<BotTemplate[]>('bot/templates').pipe(map((data: BotTemplate[]): BotTemplateView[] => {
            if (!data)
                return;

            return data.map<BotTemplateView>((botTemplate: BotTemplate): BotTemplateView => {
                return {
                    avatarUrl: 'assets/templates/' + botTemplate.avatar,
                    botConstructUrl: 'create-bot/' + botTemplate.name,
                    botDescription: botTemplate.description,
                    botName: botTemplate.name
                }
            })
        }))
    }

    saveServiceBot(serviceBot: ServiceBot): Observable<number> {
        return this.request.post<any>('bot/service-bot/savedeploy', serviceBot);
    }

    saveBotSettings(setting: BotSetting) {
        return this.request.post<any>('api/WidgetConfig/save', setting);
    }

    saveCompleteServiceBot(bot: CompleteServiceBot) {
        return this.saveServiceBot(bot.bot).pipe(
            switchMap((botId: number) => {
                let settings = bot.settings;
                settings.botId = botId;
                return this.saveBotSettings(bot.settings);
            })
        );
    }

    getBotSetting(botId: string): Observable<BotSettingView> {
        return this.request.get<BotSetting>('api/WidgetConfig/data/' + botId).pipe(map((botSetting: BotSetting): BotSettingView => {
            if (!botSetting)
                return;

            return {
                bubbleMessage: botSetting.bubbleMessage,
                chatNow: botSetting.chatNow,
                description: botSetting.description,
                language: botSetting.language,
                primaryColor: botSetting.primaryColor,
                secondaryColor: botSetting.secondaryColor,
                active: botSetting.active
            };
        }))
    }

    setBotActive(botId: string, isActive: boolean) {
        return this.request.post<any>('api/WidgetConfig/setActive', {
            botId: botId,
            active: isActive
        });
    }

    getServiceBot(botId: string) {
        return this.request.get<ServiceBot>('bot/customer-service/' + botId);
    }

    getCompleteServiceBot(botId: string): Observable<CompleteServiceBot> {
        return forkJoin({
            bot: this.getServiceBot(botId),
            settings: this.getBotSetting(botId),
            widgetHtml: this.getWidgetHtml(botId)
        }).pipe(map((value) => {
            let result: CompleteServiceBot = {
                bot: value.bot,
                settings: value.settings,
                widgetHtml: value.widgetHtml.htmlContent
            };
            return result
        }));
    }

    getWidgetHtml(botId: string): Observable<BotWidgetHtml> {
        return this.request.get<BotWidgetHtml>('bot/service-bot/widget-html/' + botId)
            .pipe(map((botWidgetHtml: BotWidgetHtml): BotWidgetHtml => {
                if (!botWidgetHtml)
                    return;

                return {
                    htmlContent: botWidgetHtml.htmlContent
                };
            }))
    }

    getUserBots() {
        return this.request.get<UserBot[]>('bot/user-bots').pipe(map((data: UserBot[]): UserBotView[] => {
            if (!data)
                return;

            return data.map<UserBotView>((userBot: UserBot): UserBotView => {
                return {
                    id: userBot.id,
                    name: userBot.name,
                    status: userBot.isDeployed ? 'Deployed' : 'Not Deployed',
                    template: userBot.templateName,
                    deployDisabled: userBot.isDeployed,
                    editDisabled: false,
                    removeDisabled: userBot.isDeployed,
                    isActive: userBot.isActive
                }
            })
        }))
    }

    removeBot(botId) {
        return this.request.post<any>('bot/service-bot/remove-bot/' + botId, null);
    }

    deployBot(botId) {
        return this.request.post<any>('bot/service-bot/deploy-bot/' + botId, null);
    }

    getMenuItems(language: string): Observable<IMenuItem[]> {
        return this.request.localGet<IMenuItem[]>(this.getMenuUrl(language));
    }
}