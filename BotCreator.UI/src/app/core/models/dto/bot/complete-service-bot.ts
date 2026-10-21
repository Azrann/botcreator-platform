import { ServiceBot } from './service-bot';
import { BotSetting } from './bot-setting';

export interface CompleteServiceBot {
    bot: ServiceBot,
    settings: BotSetting,
    widgetHtml?: string
}