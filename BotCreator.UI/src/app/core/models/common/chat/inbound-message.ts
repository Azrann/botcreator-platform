import { OnlineUser } from './online-user';

export interface InboundMessage {
    action: string;
    user: OnlineUser
}