import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { PubNubAngular } from 'pubnub-angular2';
import { ChatBackend } from './chat-backend';
import { first } from 'rxjs/operators';
import { AuthenticationService } from '../data/auth.service';
import { OnlineUser } from 'src/app/core/models/common/chat/online-user';
import { RecentUser } from 'src/app/core/models/common/chat/recent-user';
import { ChatEntity } from 'src/app/core/models/common/chat/chat-entity';
import { PubNubMessage } from 'src/app/core/models/common/chat/pubnub-message';
import { InboundMessage } from 'src/app/core/models/common/chat/inbound-message';
import { ChatMessage } from 'src/app/core/models/common/chat/chat-message';
import { RequestClientConnection } from 'src/app/core/models/common/chat/request-client-connection';

@Injectable({ providedIn: 'root' })
export class ChatEngine {
    inboundChannelName: string;
    authKey: string;
    onlineUsers: OnlineUser[] = [];
    recentUsers: RecentUser[] = [];
    messages: any = {};
    messageLastReadTime: any = {};
    activeChats: ChatEntity[] = [];

    instance: any;
    create: any;
    plugin: any;
    me: any = { state: {} };
    chat: any = {};
    chats: any[] = [];
    selectedUserIdSubject = new BehaviorSubject<any>(null);
    selectedUserId = this.selectedUserIdSubject.asObservable();

    isSelectedUserActiveSubject = new BehaviorSubject<any>(false);
    isSelectedUserActive = this.isSelectedUserActiveSubject.asObservable();

    currentOnlineUsers: any[] = [];
    isInitialized: Boolean = false;

    constructor(private pubnub: PubNubAngular, private chatBackend: ChatBackend,
        private authenticationService: AuthenticationService) {
        pubnub.init({
            publishKey: 'REPLACE_WITH_YOUR_VALUE',//'pub-c-2fa897fe-cb32-4df5-a40e-08e04d312dcf',//
            subscribeKey: 'REPLACE_WITH_YOUR_VALUE',//'sub-c-400456a8-39a2-11e9-b86f-06e8d5f9a4fd'//
        });

        pubnub.addListener({
            status: this.handleStatus.bind(this),
            message: this.handleMessage.bind(this)
        });


        /*this.instance = (ChatEngineCore as any).create(
            {
                publishKey: 'REPLACE_WITH_YOUR_VALUE',
                subscribeKey: 'REPLACE_WITH_YOUR_VALUE'
            },
            {
                debug: true,
                globalChannel: 'chat-engine-angular2-simple'
            });
        this.create = (ChatEngineCore as any).create.bind(this);
        this.plugin = (ChatEngineCore as any).plugin;*/
    }

    connectAgentToClient() {
        this.chatBackend.requestClientConnection(this.authKey)
            .pipe(first())
            .subscribe(data => {

                if(!data)
                    return;
                    
                let usersIntheQueue = this.getOnlineUsers();
                let currentUser = usersIntheQueue.find(u => u.userId == data.userId);
                if (currentUser == null)
                    return;

                let recentUser = this.addOnlineUserToRecent(currentUser, data);
                this.newChat(recentUser);


                //TODO: FOr demo
                /*this.publishMessage(
                    {
                        action: "leave",
                        user: {
                            userId: recentUser.userId,
                        }
                    }, this.inboundChannelName

                )*/


            })
    }

    addOnlineUserToRecent(onlineUser: OnlineUser, connection: RequestClientConnection): any {
        let existingRecentUser = this.recentUsers.find(user => user.userId === onlineUser.userId);

        if (existingRecentUser) {
            return;
        }

        let recentUser: RecentUser =
        {
            avatarUrl: onlineUser.avatarUrl || "https://ptetutorials.com/images/user-profile.png", userId: onlineUser.userId,
            userName: onlineUser.userName, channel: connection.channel
        };
        this.recentUsers.push(recentUser);

        this.registerMessageListeners(recentUser.userId, connection.channel);
        return recentUser;
    }

    registerMessageListeners(userId: string, channel: string): any {
        this.pubnub.subscribe({ channels: [channel] });
    }

    handleMessage(message: PubNubMessage<object>) {
        if (message.channel === this.inboundChannelName) {
            this.handleInboundMessage(message as PubNubMessage<InboundMessage>);
        }
        else {
            let msg = message as PubNubMessage<ChatMessage>;
            msg.message.messageTimeStamp = Math.ceil(+msg.timetoken / 10000);
            msg.message.avatar_url = msg.message.avatar_url || "https://ptetutorials.com/images/user-profile.png";

            let msgs = this.messages[msg.message.userId] || [];
            msgs.push(msg.message);
            msgs.sort((a, b) => a.messageTimeStamp - b.messageTimeStamp);
            this.messages[msg.message.userId] = msgs.slice(0);
            this.updateRecentUser(msg.message.userId, msg.message);


            if (this.getSelectedUserId() === msg.message.userId && this.getIsSelectedUserActive()) {
                this.updateLastReadTime(msg.message.userId);
            }

            if (!this.getIsSelectedUserActive()) {
                this.playSound()
            }
        }
    }

    playSound() {
        let sound = new Audio("../../assets/sounds/icq_message_sound.mp3");
        sound.play();
    }

    updateRecentUser(userId: string, msg: ChatMessage): any {
        let recentUser = this.recentUsers.find(s => s.userId === userId);
        if (!recentUser)
            return;

        recentUser.lastMessage = msg.text;
        recentUser.lastMessageDate = msg.messageTimeStamp;
    }

    updateLastReadTime(userId) {
        this.messageLastReadTime[userId] = new Date().getTime();
    }

    handleInboundMessage(message: PubNubMessage<InboundMessage>): any {
        if (message.message.action === "join") {
            var newOnlineUsers = this.onlineUsers.slice(0);
            newOnlineUsers.push(message.message.user);
            this.onlineUsers = newOnlineUsers;
            //TODO: Implement trackchange function
        } else if (message.message.action === "leave") {
            var newOnlineUSers = [];
            this.onlineUsers.forEach(item => {
                if (item.userId !== message.message.user.userId) {
                    newOnlineUSers.push(item);
                }
            });

            this.onlineUsers = newOnlineUSers;
        }
    }

    handleStatus(st) {
        if (st.category === "PNUnknownCategory") {
            var newState = {
                new: 'error'
            };
            this.pubnub.setState({
                state: newState
            },
                function (status) {
                    console.log(st.errorData.message);
                });
        }
    }

    publishMessage(message, channel: string) {
        this.pubnub.publish(
            {
                message: message,
                channel: channel
            },
            function (status, response) {
                if (status.error) {
                    console.log(status);
                } else {
                    console.log("message Published w/ timetoken", response.timetoken);
                }
            }
        );
    }

    initializeOnlineUsers() {
        if (this.isInitialized)
            return;

        this.chatBackend.getInboundBotChannelInfo()
            .pipe(first())
            .subscribe(
                data => {
                    this.authKey = data.authKey;
                    this.inboundChannelName = data.channel

                    this.pubnub.setAuthKey(this.authKey);
                    this.pubnub.subscribe({
                        channels: [this.inboundChannelName]
                    });
                },
                error => {
                    console.error(error);
                });

        var t = setTimeout(() => {

            /*this.pubnub.publish(
                {
                    message: {
                        action: 'join',
                        user: {
                            userId: "111",
                            avatarUrl: "https://ptetutorials.com/images/user-profile.png",
                            userName: 'REPLACE_WITH_YOUR_VALUE',
                            channel: "111"
                        }
                    },
                    channel: this.inboundChannelName
                },
                function (status, response) {
                    if (status.error) {
                        console.log(status);
                    } else {
                        console.log("message Published w/ timetoken", response.timetoken);
                    }
                }
            );

            this.pubnub.publish(
                {
                    message: {
                        action: 'join',
                        user: {
                            userId: "222", avatarUrl: "https://ptetutorials.com/images/user-profile.png",
                            userName: 'REPLACE_WITH_YOUR_VALUE',
                            channel: "222"
                        }
                    },
                    channel: this.inboundChannelName
                },
                function (status, response) {
                    if (status.error) {
                        console.log(status);
                    } else {
                        console.log("message Published w/ timetoken", response.timetoken);
                    }
                }
            );*/

            clearTimeout(t);
        }, 3000)

        this.isInitialized = true;
    }

    clearOnlineUsers() {
        //TODO
    }

    userHasUnreadMessage(recentUser: RecentUser): Boolean {
        if (!recentUser)
            return false;

        let userMessages = this.messages[recentUser.userId] as ChatMessage[];
        if (!userMessages || !userMessages.length)
            return false;
        var lastMessage = userMessages[userMessages.length - 1];

        let lastMessageReadTime = this.messageLastReadTime[recentUser.userId];
        if (!lastMessageReadTime)
            return true;

        return lastMessage.messageTimeStamp > lastMessageReadTime;
    }

    calculateUsersCountHasUnreadMessages(): Number {
        if (this.recentUsers == null || this.recentUsers.length == 0) {
            return 0;
        }

        return this.recentUsers.map<number>(recentUser => this.userHasUnreadMessage(recentUser) ? 1 : 0)
            .reduce((prev, current) => prev + current);
    }

    newChat(user: RecentUser) {
        // Could be needed to register listeners here also
        if (!user) {
            return;
        }

        var existingChat = this.activeChats.find(c => c.peerUserId === user.userId);

        if (existingChat) {
            this.setSelectedUserId(user.userId);
            this.setIsSelectedUserActive(true);
            this.updateLastReadTime(user.userId);
            return;
        }

        let newChatEntity: ChatEntity = {
            channel: user.channel,
            peerUserId: user.userId
        };

        this.activeChats.push(newChatEntity);

        this.setSelectedUserId(user.userId);
        this.updateLastReadTime(user.userId);
    }

    getSelectedUserId(): string {
        return this.selectedUserIdSubject.value;
    }

    getIsSelectedUserActive(): string {
        return this.isSelectedUserActiveSubject.value;
    }

    setSelectedUserId(userId) {
        this.selectedUserIdSubject.next(userId);
    }

    setIsSelectedUserActive(status: boolean) {
        this.isSelectedUserActiveSubject.next(status);
    }

    getRecentUsers(): RecentUser[] {
        return this.recentUsers;
    }

    getOnlineUsers(): OnlineUser[] {
        return this.onlineUsers;

        /*let onlineUsers: OnlineUser[] = [
            { userId: "111", avatarUrl: "https://ptetutorials.com/images/user-profile.png", userName: 'REPLACE_WITH_YOUR_VALUE' },
            { userId: "222", avatarUrl: "https://ptetutorials.com/images/user-profile.png", userName: 'REPLACE_WITH_YOUR_VALUE' },
            { userId: "333", avatarUrl: "https://ptetutorials.com/images/user-profile.png", userName: 'REPLACE_WITH_YOUR_VALUE' }

        ]

        return onlineUsers;*/
        /*
        if (!onlineUsers) {
            this.currentOnlineUsers = [];
            return this.currentOnlineUsers;
        }

        let users: any = [];
        Object.keys(onlineUsers).forEach((key) => {
            if (this.me.uuid !== key) {
                let o = onlineUsers[key];
                o["avatar"] = "https://ptetutorials.com/images/user-profile.png";
                o["alt"] = "yo";
                o["lastMsg"] = "No Messages";
                o["msgDate"] = "";
                users.push(o);
            }
        });

        if (!users || users.length === 0) {
            this.currentOnlineUsers = [];
            return this.currentOnlineUsers;
        }*/

        /*let finalUsers = [];
        users.forEach((user) => {
            var userToAdd = user;

            var findExistingUser = this.currentOnlineUsers.find(u => u.uuid === user.uuid);
            if (findExistingUser) {
                userToAdd = findExistingUser;
            }

            finalUsers.push(userToAdd);
        });

        this.currentOnlineUsers = finalUsers;*/
    }
}