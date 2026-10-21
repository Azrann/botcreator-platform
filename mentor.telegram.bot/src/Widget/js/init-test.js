(function get_request_to_url(botId) {
    if (!botId) {
        console.log('Data for bot was not provide!');
        return false;
    }

    var ajax = new XMLHttpRequest();
    ajax.open("get", 'https://botcreatorapptest.azurewebsites.net/api/widgetconfig/' + botId, true);
    ajax.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    ajax.send();
    ajax.onreadystatechange = function() {
        if (ajax.readyState === 4 && ajax.status === 200) {
            var init_data = JSON.parse(ajax.responseText).data;

            !!init_data.active && startMainProcess(init_data);
        }
    };

    var startMainProcess = function (data) {
            typeof data.widgetUrl === 'string' && includeWidgetCDN(data.widgetUrl, function () {
                data.bot.id = botId;
                document.help_widget.widget.init(data);
            });
        },
        includeWidgetCDN = function (url, callback) {
            var newScript = document.createElement("script");

            newScript.src = url;
            // newScript.src = './js/helpWidget.js';
            document.body.appendChild(newScript);
            newScript.onload = function () {
                typeof callback === 'function' && callback();
            }
        }
    ;
}(window.botId && window.botId));
