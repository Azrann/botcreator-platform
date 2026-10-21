pnWrapper = {

    token: "",
    channelId: "",
    publishChannel: "",
    userId: "",
    chatMode: false,
    webUserId: "",

    sendInitialize: function (name1, email1, botId1) {
        mymessage.BotId = botId1;
        mymessage.WebUserId = "testPubNub.webUserId";
        var params = {
            name: name1,
            email: email1,
            botId: botId1,
            clientId: "REPLACE_WITH_YOUR_VALUE"
        };
        var ajax = new XMLHttpRequest();
        //ajax.open("POST", "http://localhost:8443//api/chat", true);
        ajax.open("POST", "https://botengineapi-deve.azurewebsites.net/api/chat", true);
       
        ajax.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        ajax.send(
            JSON.stringify(params)
        );
        ajax.onreadystatechange = function() {
            if (ajax.readyState === 4 && ajax.status === 200) {
                //var data = JSON.parse(ajax.responseText);
                //console.log(data);
                data = {"token":"REPLACE_WITH_YOUR_VALUE","channelId":"86213674","publishChannel":"bc-message1","userId":"adbd0047-844d-4735-8189-e6185a7aebf2"};
                pnWrapper.token = data.token;
                pnWrapper.userId = data.userId;
                pnWrapper.channelId = data.channelId;
                pnWrapper.publishChannel = data.publishChannel;
                pubnub.setAuthKey(data.token);
                // Gerenate the error
                // pubnub.setAuthKey(data.token+'e');
                pnWrapper.addListeners();
                pnWrapper.subscribe();
                pnWrapper.sendMessage('menu', data.token);
            }
        }
    },

    changeChannelId: function (channelId) {
        pnWrapper.channelId = channelId;
        pnWrapper.publishChannel = channelId;
        pnWrapper.subscribe();
    },

    addListeners: function () {
        pubnub.addListener({
            status: function (statusEvent) {
            },
            message: function (message) {
                console.log(JSON.stringify(message))
                //pnWrapper.processMessage(message);
            }
        })
    },
    sendCallback: function (text) {
        mymessage.Type = "callback";
        mymessage.Text = text;
        mymessage.ChatId = pnWrapper.channelId;
        mymessage.FromId = pnWrapper.channelId;
        mymessage.WebUserId = "REPLACE_WITH_YOUR_VALUE";
        pubnub.publish(
            {
                message: mymessage,
                channel: this.publishChannel,
                sendByPost: false,
                storeInHistory: false,
            },
            function (status, response) {
                if (status.error) {
                    // handle error
                    console.log(status)
                } else {
                    console.log("message Published w/ timetoken", response.timetoken)
                }
            }
        );
    },

    sendChatMessage: function (text) {
        myChatMessage.text = text;
        myChatMessage.userId = pnWrapper.userId;

        pubnub.publish(
            {
                message: myChatMessage,
                channel: this.publishChannel,
                sendByPost: false,
                storeInHistory: false,
            },
            function (status, response) {
                if (status.error) {
                    // handle error
                    console.log(status)
                } else {
                    console.log("message Published w/ timetoken", response.timetoken)
                }
            }
        );
    },

    processMessage: function (message) {
        if (pnWrapper.chatMode)
        {
            if (message.message.sender == "client")
            {
                return;
            }
            if (message.message.message == "[ping]")
            {
                pnWrapper.sendChatMessage("[pong]");
                return;
            }
            if (message.message.message == "[pong]")
            {
                return;
            }
        }

        if (message.message.buttons) {
            // help_widget.widget.create_message(true, message.message.text, true, true);
            help_widget.widget.create_message(true, message.message.buttons, true);
        }
        else if (message.message.carrousel) {
            help_widget.widget.create_message(true, message.message.text, true);
            help_widget.widget.create_message(true, message.message.carrousel, true);
        }
        else if (message.message.Command)
        {
            var channelId = message.message.Command.split(" ")[1];
            pnWrapper.changeChannelId(channelId);
            pnWrapper.chatMode = true;
        }
        else {
            if (message.message.from !== help_widget.widget.settings.bot.name) {
                help_widget.widget.setBotName(message.message.from);
                // help_widget.widget.settings.bot.name = message.message.name;
            }
            if (message.message.avarat_url)
            {
                help_widget.widget.settings.bot.avarat_url = message.message.avarat_url;
            }
            help_widget.widget.create_message(true, message.message.text, true, true);
        }
    },

    sendMessage: function (text, token) {
        mymessage.Type = "message";
        mymessage.Text = text;
        mymessage.ChatId = pnWrapper.channelId;
        mymessage.FromId = pnWrapper.channelId;
        mymessage.WebUserId = "REPLACE_WITH_YOUR_VALUE";
        pubnub.publish(
            {
                message: mymessage,
                channel: this.publishChannel,
                sendByPost: false,
                storeInHistory: false,
            },
            function (status, response) {
                if (status.error) {
                    // handle error
                    pnWrapper.errorHandler(status, function () {
                        setTimeout(function () {
                            var settings = help_widget.widget.settings;
                            console.clear();
                            pnWrapper.sendInitialize(settings.user.name, settings.user.email, settings.bot.id);
                            /*pubnub.setAuthKey(token);
                            pnWrapper.subscribe();
                            pnWrapper.sendMessage(text, token);*/
                        }, 1000);
                    });
                } else {
                    console.log("message Published w/ timetoken", response.timetoken)
                }
            }
        );
    },

    errorHandler: function(status, callback) {
        console.log('-------------+++' + status.statusCode + '+++---------------');

        typeof callback === 'function' && callback();
    },

    subscribe: function () {
        pubnub.subscribe({
            channels: [pnWrapper.channelId],
        });
    },

    unsubscribe: function () {
        // console.log(pnWrapper);
        pubnub.unsubscribe({
            channels: [pnWrapper.channelId],
        });
    }
};

var pubnub = new PubNub({
    subscribeKey: "REPLACE_WITH_YOUR_VALUE",
    publishKey: "REPLACE_WITH_YOUR_VALUE",

    logVerbosity: true,
    uuid: "myUniqueUUID",
    presenceTimeout: 600
});

var mymessage = {
    "Type": "message",
    "ChatId": "",
    "FromId": "",
    "Text": "nothing",
    "ReplyToMessage": "",
    "FirstName": "Alberto",
    "LastName": "",
    "BotId": "",
    "WebUserId": "REPLACE_WITH_YOUR_VALUE" 
};

var myChatMessage = {
    "userId": "",
    "text": "",
    "sender": "client" // agent, client
};