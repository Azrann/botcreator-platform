import { Injectable } from '@angular/core';
import { of, Observable } from 'rxjs';
import { RequestService } from '../common/request.service';
import { InboundBotChannelInfo } from 'src/app/core/models/common/chat/inbound-bot-channel-info';
import { RequestClientConnection } from 'src/app/core/models/common/chat/request-client-connection';

@Injectable({ providedIn: 'root' })
export class ChatBackend {
    private first: number = 0;
    private mock: boolean = false;

    constructor(private request: RequestService) {

    }

    getInboundBotChannelInfo(): Observable<InboundBotChannelInfo> {
        //      return of<InboundBotChannelInfo>( { channel: 'my-test-channel', authKey: '' })
        return this.request.post<InboundBotChannelInfo>("bot/service-bot/inbound-channel", null);
    }

    requestClientConnection(authKey: string) {
        if (this.mock) {
            this.first += 111;
            return of<RequestClientConnection>({ channel: this.first.toString(), userId: this.first.toString() })
        }
        //service-bot/find-peer
        return this.request.post<RequestClientConnection>("bot/service-bot/find-peer", {
            authKey: authKey
        });
    }
}