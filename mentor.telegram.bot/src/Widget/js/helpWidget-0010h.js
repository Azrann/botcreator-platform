cookieService = {

    webUserId: "",

    uuidv4: function() {
          return ([1e7]+-1e3+-4e3+-8e3+-1e11).replace(/[018]/g, c =>
            (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
          )
    },

    getCookie: function(name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for(var i=0;i < ca.length;i++) {
            var c = ca[i];
            while (c.charAt(0)==' ') c = c.substring(1,c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
        }
        return null;
    },

    setCookie: function(name,value,days) {
        var expires = "";
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days*24*60*60*1000));
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "")  + expires + "; path=/";
    },

    eraseCookie: function(name) {   
        document.cookie = name+'=; Max-Age=-99999999;';  
    },

    initialize: function(name)
    {
        var mycookie = cookieService.getCookie('botcreatoruid');

        if (mycookie == null)
        {
            mycookie = cookieService.uuidv4();
            cookieService.setCookie('botcreatoruid',mycookie,7);
        }

        cookieService.webUserId = mycookie;
        return mycookie;
    }
};

cookieService.initialize();

document.help_widget = help_widget = {
    applyToggle: function(el){
        var me = el,
            items = me.parentNode.parentNode.children
        ;

        if(me.parentNode.classList.contains('active')){
            me.parentNode.classList.remove("active");
            return false;
        }

        for(var item of items){
            item.classList.remove("active");
        }
        me.parentNode.className += ' active';
    },
    dom: {
        modal_body: {
        },
        views: {
            chat: {
                template: {},
                can_be_remove: {gif_message: [], other: []}
            },
            form: {template: {}}
        },
        modal_head: {
            close_btn: {}
        }
    },
    styles: {
        ' ': {
            "position" : "fixed",
            "right" : "1em",
            "bottom" : "1em",
            "display" : "block",
            "max-width" : "calc(100% - 2em)",
            "z-index": "REPLACE_WITH_YOUR_VALUE",
            "width" : "100%",
            "font-size" : "14px",
            "margin" : "0",
            "padding" : "0"
        },
        '*': {
            "letter-spacing" : "initial",
            "font-size" : "14px",
            "font-style" : "normal",
            "line-height" : "1.5"
        },
        '*:after': {
            "box-sizing" : "border-box",
        },
        '*:before': {
            "box-sizing" : "border-box",
        },
        'p': {
            "font-size" : "14px!important",
            "margin" : "0",
            "padding" : "0"
        },
        '.grey_svg': {
            "fill" : "#424D57"
        },
        '.grey_svg:hover': {
            "fill" : "#858585"
        },
        '.close_widget > svg': {
            "fill" : "#000000"
        },
        'button': {
            "border" : "none",
            "outline" : "none",
            "cursor" : "pointer",
            "background-color" : "transparent",
            "border-radius" : "5px",
            "font-family" : "'Arial', serif",
            "display" : "inline-block",
            "text-transform" : "inherit",
            "font-size" : "12px",
            "letter-spacing" : "initial",
            "text-align" : "center",
            "margin" : "0",
            "padding" : "0"
        },
        '.helpWidget_main_avatar': {
            "display" : "flex"
        },
        '.helpWidget_main_avatar_wrapper': {
            "margin-right" : "5px"
        },
        '.personal_credentials': {
            "font-size" : "11px!important",
            "margin-top": "5px",
            "display": "none"
        },
        '.personal_credentials .time': {
            "font-size" : "11px!important"
        },
        '.helpWidget_feedback_buttons': {
            "align-self" : "center"
        },
        '.helpWidget_feedback_buttons button': {
            "padding" : "0.25em 0.5em!important"
        },
        '.helpWidget_modal_head__bottom': {
            "padding" : "0.5em 0",
            "display" : "flex",
            "justify-content" : "space-between"
        },
        '.helpWidget_main_avatar_info': {
            "flex-direction" : "column",
            "display" : "flex",
            "align-items" : "baseline"
        },
        '.helpWidget_bot_change_message': {
            "width" : "100%",
            "font-size": "12px",
            "margin": "5px 0",
            "text-align": "center",
            "padding": "0 5px",
            "box-sizing": "border-box"
        },
        '.avatar': {
            "max-width" : "40px",
            "max-height" : "40px",
            "border-radius" : "50%"
        },
        '.helpWidget_toggler': {
            "z-index" : "2",
            "cursor" : "pointer",
            "position" : "absolute",
            "right" : "10px",
            "text-align" : "right",
            "bottom" : "10px"
        },
        '.helpWidget_toggler button': {
            "background-color" : "grey",
            "width" : "65px",
            "height" : "65px",
            "padding" : "0",
            "overflow" : "hidden",
            "color" : "white",
            "position" : "relative",
            "box-shadow" : "0 0 5px rgba(0, 0, 0, .5)",
            "border-radius" : "50%"
        },
        '.helpWidget_toggler img': {
            "max-width" : "100%",
            "position" : "relative"
        },
        '.helpWidget_modal': {
            "overflow" : "hidden",
            "padding" : "1em"
        },
        '.helpWidget_modal_inner': {
            "overflow" : "hidden",
            "flex-direction" : "column",
            "min-width" : "200px",
            "min-height" : "250px",
            "box-sizing" : "border-box",
            "border" : "1px solid lightgray",
            "border-radius" : "15px",
            "box-shadow" : "0 0 10px rgba(0, 0, 0, .5)",
            "display" : "flex"
        },
        '.head_btn': {
            "width" : "25px",
            "height" : "25px",
            "padding" : "0;",
            "margin" : "0;",
            "display" : "inline-block"
        },
        '.helpWidget_modal_head': {
            "background-color" : "#ffffff",
            "flex-grow" : "0",
            "text-align" : "center",
            "padding" : "0.5rem 1rem 0",
            "line-height" : "1.5"
        },
        '.helpWidget_modal_head__top': {
            "border-bottom" : "1px solid lightgray",
            "display" : "flex",
            "justify-content" : "space-between",
            "align-items" : "center"
        },
        '.helpWidget_modal_body': {
            "background-color" : "#E9EEF4",
            "flex-basis" : "300px",
            "flex-grow" : "1",
            "padding" : "0 0 100px",
            "position" : "relative"
        },
        '.helpWidget_modal_body_btn_wrap': {
            "width" : "100%",
            "bottom" : "0",
            "left" : "0",
            "position" : "absolute",
            "padding" : "0 0.75em 1em",
            "box-sizing" : "border-box"
        },
        '.helpWidget_modal_body_btn_wrap .helpWidget_main_avatar_wrapper': {
            'padding-top': '8px'
        },
        'button.helpWidget_custom-button': {
            "width" : "100%",
            "padding" : "0.5em",
            "min-height" : "40px",
            "background-color" : "blue",
            "color" : "white",
            "border-radius" : "5px"
        },
        'button.helpWidget_custom-button:hover': {
            'box-shadow': '1px 1px 5px rgba(150, 150, 150, 0.7)'
        },
        '.helpWidget_person': {
            "display" : "flex",
            "padding" : "5px 10px 0px 3px",
            "text-align" : "-webkit-left"
        },
        '.helpWidget_person[data-bot=false]': {
            "flex-direction" : "row-reverse",
            "text-align" : "-webkit-right"
        },
        '.helpWidget_person[data-bot=false] .personal_credentials': {
            'text-align': 'right',
            'display': 'block'
        },
        '.helpWidget_person[data-bot=false] .helpWidget_person_message': {
            'background-color': '#ffffff'
        },
        '.helpWidget_person_avatar_wrapper': {
            "width" : "30px",
            "height" : "30px",
            "border-radius" : "50%",
            "background" : "white",
            "overflow" : "hidden"
        },
        '.helpWidget_person_avatar_wrapper .avatar': {
            "max-width" : "100%",
            "max-height" : "100%"
        },
        '.helpWidget_person_avatar': {
            "display" : "flex",
            "flex-direction" : "column",
            "-webkit-box-align" : "center",
            "align-items" : "center",
            "text-align" : "center",
            "font-size" : "0.7em",
            "line-height" : "1.6em",
            "-webkit-box-flex" : "0",
            "flex-grow" : "0",
            "margin" : "0 0.3em"
        },
        '.helpWidget_person_messages': {
            "padding-top" : "0",
            "-webkit-box-flex": "1",
            "flex-grow:": "1",
            "box-sizing" : "border-box",
            "flex-shrink" : "1"
        },
        '.helpWidget_person_message': {
            "box-sizing" : "border-box",
            "text-align" : "left",
            "background-color" : "#fff",
            "box-shadow" : "0 1px 15px rgba(0, 0, 0, 0.5)",
            "border-radius" : "5px",
            "padding" : "0",
            "margin" : "0.5em 0.25em 0.5em",
            "word-break" : "break-word",
            "font-size: " : "14px",
            "line-height" : "1.5",
            "color" : "black",
            "font-weight" : "400",
            "font-style" : "normal",
            "width": "fit-content"
        },
        '.helpWidget_person_message.btn_wrapper': {},
        '.helpWidget_clear_message': {
            "padding" : "0.5em 1em"
        },
        '.helpWidget_clear_message a': {
            "color" : "black",
            "font-weight": "bold",
            "text-decoration": "none"
        },
        '.helpWidget_clear_message a:visited, .helpWidget_clear_message a:focus, .helpWidget_clear_message a:active': {
            "text-decoration": "none"
        },
        '.helpWidget_clear_message a:hover': {
            "color" : "darkgray"
        },
        '.helpWidget_person_message b': {},
        '.helpWidget_person_gif_message': {
            "position" : "relative",
            "max-width" : "60px",
            "line-height" : "0"
        },
        '.helpWidget_person_gif_message img': {
            "max-width" : "100%"
        },
        '.helpWidget_message_view': {
            "overflow-y" : "auto",
            "overflow-x" : "hidden",
            "max-height" : "400px"
        },
        '.helpWidget_register_form': {
            "background-color" : "#ffffff",
            "border-radius" : "5px",
            "box-shadow" : "0 0 10px rgba(0, 0, 0, 0.5)",
            "padding" : "1em 0.5em"
        },
        '.helpWidget_register_form label': {
            "color" : "grey",
            "width" : "100%",
            "margin-bottom" : "0.75em",
            "display" : "block"
        },
        '.helpWidget_register_form input': {
            "margin-top" : "0.25em",
            "margin-left" : "0",
            "margin-right": "0",
            "background-color" : "transparent",
            "width" : "100%",
            "border" : "1px solid lightgray",
            "line-height" : "40px",
            "padding" : "0.5em",
            "border-radius" : "5px",
            "box-sizing" : "border-box",
            "height" : "40px"
        },
        '.helpWidget_message_wrapper': {
            "display" : "flex",
            "flex-basis" : "100%"
        },
        '.helpWidget_message_wrapper textarea': {
            "-webkit-border-radius" : "5px 0 0 5px",
            "-moz-border-radius" : "5px 0 0 5px",
            "border-radius" : "5px 0 0 5px",
            "margin-right" : "-1px",
            "border-right" : "none"
        },
        '.helpWidget_message_wrapper button': {
            "width" : "30px",
            "border-radius" : "0 5px 5px 0",
            "border-left" : "none",
            "margin" : "0!important",
            "padding" : "0.6em"
        },
        '.helpWidget_message_wrapper button > img': {
            "max-width" : "100%",
            "position" : "relative"
        },
        '.helpWidget_chat_field .chat_top_side': {
            "display" : "flex",
            "justify-content" : "left"
        },
        '.helpWidget_chat_field .helpWidget_chat_bottom_side': {
            "text-align" : "right"
        },
        '.btn_column': {
            "display" : "flex",
            "justify-content" : "space-between",
            "margin-bottom" : "5px"
        },
        '.btn_column button': {
            "flex-grow" : "1",
            "margin" : "0.25em"
        },
        '.btn-simple': {
            "padding" : "5px 2.5px",
            "flex-grow": 1,
            "text-align": "center",
            "font-size": "12px",
            "min-height" : "20px",
            "line-height": "20px",
            "background-color" : "white",
            "color" : "black",
            "border": "1px solid rgba(155, 155, 155, 0.5)",
            "margin" : "2.5px!important",
            "border-radius": '5px',
            "text-decoration": 'none'
        },
        '.icon-link' : {
            'padding': '0 5px'
        },
        '.icon-link i' : {
            'font-size': '24px'
        },
        '.icon-link i.fa-whatsapp' : {
            'color': '#01E675'
        },
        '.btn-simple:hover': {
            "background-color" : "rgba(222, 222, 222, 0.5)"
        },
        '.helpWidget_chat_field .helpWidget_chat_bottom_side .btn-simple': {
            "width" : "100%"
        },
        'textarea': {
            "width": "100%",
            "resize" : "none",
            "min-height" : "50px",
            "margin-bottom" : "0",
            "box-sizing" : "border-box",
            "background" : "white",
            "height" : "auto",
            "z-index" : "auto",
            "position" : "relative",
            "border" : "1px solid #ced4da",
            " border-radius" : ".25rem",
            "padding" : ".375rem .75rem",
            "color" : "#495057"
        },
        /*Carousel*/
        '.helpWidget_csscarousel': {
            "box-sizing" : "border-box",
            "display" : "inline-block",
            "text-align" : "left",
            "position" : "relative",
            "width" : "calc( 100% - 23px )",
            "margin-left" : "10px"
        },
        '.helpWidget_csscarousel > input': {
            "display" : "none"
        },
        '.helpWidget_csscarousel > input:nth-of-type(10):checked ~ ul li:first-of-type': {
            "margin-left" : "-900%"
        },
        '.helpWidget_csscarousel > input:nth-of-type(9):checked ~ ul li:first-of-type': {
            "margin-left" : "-800%"
        },
        '.helpWidget_csscarousel > input:nth-of-type(8):checked ~ ul li:first-of-type': {
            "margin-left" : "-700%"
        },
        '.helpWidget_csscarousel > input:nth-of-type(7):checked ~ ul li:first-of-type': {
            "margin-left" : "-600%"
        },
        '.helpWidget_csscarousel > input:nth-of-type(6):checked ~ ul li:first-of-type': {
            "margin-left" : "-500%"
        },
        '.helpWidget_csscarousel > input:nth-of-type(5):checked ~ ul li:first-of-type': {
            "margin-left" : "-400%"
        },
        '.helpWidget_csscarousel > input:nth-of-type(4):checked ~ ul li:first-of-type': {
            "margin-left" : "-300%"
        },
        '.helpWidget_csscarousel > input:nth-of-type(3):checked ~ ul li:first-of-type': {
            "margin-left" : "-200%"
        },
        '.helpWidget_csscarousel > input:nth-of-type(2):checked ~ ul li:first-of-type': {
            "margin-left" : "-100%"
        },
        '.helpWidget_csscarousel > input:nth-of-type(1):checked ~ ul li:first-of-type': {
            "margin-left" : "0"
        },
        '.helpWidget_csscarousel > ul': {
            "position" : "relative",
            "width" : "100%",
            "min-height" : "75px",
            "z-index" : "3",
            "font-size" : "0",
            "line-height" : "0",
            "background" : "transparent",
            "margin" : "0 auto",
            "padding" : "0",
            "overflow" : "hidden",
            "white-space" : "nowrap",
            "-moz-box-sizing" : "border-box",
            "-webkit-box-sizing" : "border-box",
            "box-sizing" : "border-box"
        },
        '.helpWidget_csscarousel > ul > li': {
            "position" : "relative",
            "width" : "100%",
            "display" : "inline-block",
            "height" : "100%",
            "overflow" : "hidden",
            "font-size" : "initial",
            "line-height" : "normal",
            "-moz-transition" : "all 0.5s cubic-bezier(0.4, 1.3, 0.65, 1)",
            "-o-transition" : " all 0.5s ease-out",
            "-webkit-transition" : "all 0.5s cubic-bezier(0.4, 1.3, 0.65, 1)",
            "transition" : "all 0.5s cubic-bezier(0.4, 1.3, 0.65, 1)",
            "vertical-align" : "top",
            "-moz-box-sizing" : "border-box",
            "-webkit-box-sizing" : " border-box",
            "box-sizing" : "border-box",
            "white-space" : "normal"
        },
        '.helpWidget_csscarousel > ul > li img': {
            "max-width" : "100%"
        },
        '.helpWidget_csscarousel > .arrows': {
            "-webkit-touch-callout" : "none",
            "-webkit-user-select" : "none",
            "-khtml-user-select" : "none",
            "-moz-user-select" : "none",
            "-ms-user-select" : "none",
            "user-select" : "none",
            "position" : "absolute",
            "top" : "50%",
            "left" : "-10px",
            "width" : "100%",
            "height" : "26px",
            "padding" : "0 10px",
            "z-index" : "2",
            "-moz-box-sizing" : "content-box",
            "-webkit-box-sizing" : "content-box",
            "box-sizing" : "content-box"
        },
        '.helpWidget_csscarousel > .arrows label': {
            "display" : "none",
            "position" : "absolute",
            "top" : "-50%",
            "padding" : "6.5px",
            "box-shadow" : "inset 2px -2px 0 1px #424D57",
            "cursor" : "pointer",
            "-moz-transition" : "box-shadow 0.15s, margin 0.15s",
            "-o-transition" : "box-shadow 0.15s, margin 0.15s",
            "-webkit-transition" : "box-shadow 0.15s, margin 0.15s",
            "transition" : "box-shadow 0.15s, margin 0.15s"
        },
        '.helpWidget_csscarousel > .arrows label:hover': {
            "box-shadow" : "inset 2px -2px 0 1px #858585"
        },
        '.helpWidget_csscarousel > .arrows label:before': {
            "content" : "",
            "position" : "absolute",
            "top" : "-100%",
            "left" : "-100%",
            "height" : "300%",
            "width" : "300%",
        },
        '.helpWidget_csscarousel.infinity > input:first-of-type:checked ~ .arrows label.goto-last': {
            "display" : "block",
            "left" : "0",
            "right" : "auto",
            "-moz-transform" : "rotate(45deg)",
            "-ms-transform" : "rotate(45deg)",
            "-o-transform" : "rotate(45deg)",
            "-webkit-transform" : "rotate(45deg)",
            "transform" : "rotate(45deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(1):checked ~ .arrows > label:nth-of-type(0)': {
            "display" : "block",
            "left" : "0",
            "right" : "auto",
            "-moz-transform" : "rotate(45deg)",
            "-ms-transform" : "rotate(45deg)",
            "-o-transform" : "rotate(45deg)",
            "-webkit-transform" : "rotate(45deg)",
            "transform" : "rotate(45deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(2):checked ~ .arrows > label:nth-of-type(1)': {
            "display" : "block",
            "left" : "0",
            "right" : "auto",
            "-moz-transform" : "rotate(45deg)",
            "-ms-transform" : "rotate(45deg)",
            "-o-transform" : "rotate(45deg)",
            "-webkit-transform" : "rotate(45deg)",
            "transform" : "rotate(45deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(3):checked ~ .arrows > label:nth-of-type(2)': {
            "display" : "block",
            "left" : "0",
            "right" : "auto",
            "-moz-transform" : "rotate(45deg)",
            "-ms-transform" : "rotate(45deg)",
            "-o-transform" : "rotate(45deg)",
            "-webkit-transform" : "rotate(45deg)",
            "transform" : "rotate(45deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(4):checked ~ .arrows > label:nth-of-type(3)': {
            "display" : "block",
            "left" : "0",
            "right" : "auto",
            "-moz-transform" : "rotate(45deg)",
            "-ms-transform" : "rotate(45deg)",
            "-o-transform" : "rotate(45deg)",
            "-webkit-transform" : "rotate(45deg)",
            "transform" : "rotate(45deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(5):checked ~ .arrows > label:nth-of-type(4)': {
            "display" : "block",
            "left" : "0",
            "right" : "auto",
            "-moz-transform" : "rotate(45deg)",
            "-ms-transform" : "rotate(45deg)",
            "-o-transform" : "rotate(45deg)",
            "-webkit-transform" : "rotate(45deg)",
            "transform" : "rotate(45deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(6):checked ~ .arrows > label:nth-of-type(5)': {
            "display" : "block",
            "left" : "0",
            "right" : "auto",
            "-moz-transform" : "rotate(45deg)",
            "-ms-transform" : "rotate(45deg)",
            "-o-transform" : "rotate(45deg)",
            "-webkit-transform" : "rotate(45deg)",
            "transform" : "rotate(45deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(7):checked ~ .arrows > label:nth-of-type(6)': {
            "display" : "block",
            "left" : "0",
            "right" : "auto",
            "-moz-transform" : "rotate(45deg)",
            "-ms-transform" : "rotate(45deg)",
            "-o-transform" : "rotate(45deg)",
            "-webkit-transform" : "rotate(45deg)",
            "transform" : "rotate(45deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(8):checked ~ .arrows > label:nth-of-type(7)': {
            "display" : "block",
            "left" : "0",
            "right" : "auto",
            "-moz-transform" : "rotate(45deg)",
            "-ms-transform" : "rotate(45deg)",
            "-o-transform" : "rotate(45deg)",
            "-webkit-transform" : "rotate(45deg)",
            "transform" : "rotate(45deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(9):checked ~ .arrows > label:nth-of-type(8)': {
            "display" : "block",
            "left" : "0",
            "right" : "auto",
            "-moz-transform" : "rotate(45deg)",
            "-ms-transform" : "rotate(45deg)",
            "-o-transform" : "rotate(45deg)",
            "-webkit-transform" : "rotate(45deg)",
            "transform" : "rotate(45deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(10):checked ~ .arrows > label:nth-of-type(9)': {
            "display" : "block",
            "left" : "0",
            "right" : "auto",
            "-moz-transform" : "rotate(45deg)",
            "-ms-transform" : "rotate(45deg)",
            "-o-transform" : "rotate(45deg)",
            "-webkit-transform" : "rotate(45deg)",
            "transform" : "rotate(45deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(11):checked ~ .arrows > label:nth-of-type(10)': {
            "display" : "block",
            "left" : "0",
            "right" : "auto",
            "-moz-transform" : "rotate(45deg)",
            "-ms-transform" : "rotate(45deg)",
            "-o-transform" : "rotate(45deg)",
            "-webkit-transform" : "rotate(45deg)",
            "transform" : "rotate(45deg)",
        },
        '.helpWidget_csscarousel.infinity > input:last-of-type:checked ~ .arrows label.goto-first': {
            "display" : "block",
            "right" : "0",
            "left" : "auto",
            "-moz-transform" : "rotate(225deg)",
            "-ms-transform" : "rotate(225deg)",
            "-o-transform" : "rotate(225deg)",
            "-webkit-transform" : "rotate(225deg)",
            "transform" : "rotate(225deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(1):checked ~ .arrows > label:nth-of-type(2)': {
            "display" : "block",
            "right" : "0",
            "left" : "auto",
            "-moz-transform" : "rotate(225deg)",
            "-ms-transform" : "rotate(225deg)",
            "-o-transform" : "rotate(225deg)",
            "-webkit-transform" : "rotate(225deg)",
            "transform" : "rotate(225deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(2):checked ~ .arrows > label:nth-of-type(3)': {
            "display" : "block",
            "right" : "0",
            "left" : "auto",
            "-moz-transform" : "rotate(225deg)",
            "-ms-transform" : "rotate(225deg)",
            "-o-transform" : "rotate(225deg)",
            "-webkit-transform" : "rotate(225deg)",
            "transform" : "rotate(225deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(3):checked ~ .arrows > label:nth-of-type(4)': {
            "display" : "block",
            "right" : "0",
            "left" : "auto",
            "-moz-transform" : "rotate(225deg)",
            "-ms-transform" : "rotate(225deg)",
            "-o-transform" : "rotate(225deg)",
            "-webkit-transform" : "rotate(225deg)",
            "transform" : "rotate(225deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(4):checked ~ .arrows > label:nth-of-type(5)': {
            "display" : "block",
            "right" : "0",
            "left" : "auto",
            "-moz-transform" : "rotate(225deg)",
            "-ms-transform" : "rotate(225deg)",
            "-o-transform" : "rotate(225deg)",
            "-webkit-transform" : "rotate(225deg)",
            "transform" : "rotate(225deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(5):checked ~ .arrows > label:nth-of-type(6)': {
            "display" : "block",
            "right" : "0",
            "left" : "auto",
            "-moz-transform" : "rotate(225deg)",
            "-ms-transform" : "rotate(225deg)",
            "-o-transform" : "rotate(225deg)",
            "-webkit-transform" : "rotate(225deg)",
            "transform" : "rotate(225deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(6):checked ~ .arrows > label:nth-of-type(7)': {
            "display" : "block",
            "right" : "0",
            "left" : "auto",
            "-moz-transform" : "rotate(225deg)",
            "-ms-transform" : "rotate(225deg)",
            "-o-transform" : "rotate(225deg)",
            "-webkit-transform" : "rotate(225deg)",
            "transform" : "rotate(225deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(7):checked ~ .arrows > label:nth-of-type(8)': {
            "display" : "block",
            "right" : "0",
            "left" : "auto",
            "-moz-transform" : "rotate(225deg)",
            "-ms-transform" : "rotate(225deg)",
            "-o-transform" : "rotate(225deg)",
            "-webkit-transform" : "rotate(225deg)",
            "transform" : "rotate(225deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(8):checked ~ .arrows > label:nth-of-type(9)': {
            "display" : "block",
            "right" : "0",
            "left" : "auto",
            "-moz-transform" : "rotate(225deg)",
            "-ms-transform" : "rotate(225deg)",
            "-o-transform" : "rotate(225deg)",
            "-webkit-transform" : "rotate(225deg)",
            "transform" : "rotate(225deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(9):checked ~ .arrows > label:nth-of-type(10)': {
            "display" : "block",
            "right" : "0",
            "left" : "auto",
            "-moz-transform" : "rotate(225deg)",
            "-ms-transform" : "rotate(225deg)",
            "-o-transform" : "rotate(225deg)",
            "-webkit-transform" : "rotate(225deg)",
            "transform" : "rotate(225deg)",
        },
        '.helpWidget_csscarousel > input:nth-of-type(10):checked ~ .arrows > label:nth-of-type(11)': {
            "display" : "block",
            "right" : "0",
            "left" : "auto",
            "-moz-transform" : "rotate(225deg)",
            "-ms-transform" : "rotate(225deg)",
            "-o-transform" : "rotate(225deg)",
            "-webkit-transform" : "rotate(225deg)",
            "transform" : "rotate(225deg)",
        },
        '.helpWidget_csscarousel .carousel-item .carousel-item-image': {
            "border" : "1px solid #e9e9e9",
            "box-sizing" : "border-box",
            "line-height" : "0",
            "border-bottom" : "none"
        },
        '.helpWidget_csscarousel .carousel-item .carousel-item-desc': {
            "border" : "1px solid #e9e9e9",
            "box-sizing" : "border-box",
            "padding" : "0.5em",
            "border-bottom" : "none"
        },
        '.helpWidget_csscarousel .carousel-item-desc-head': {
            "margin" : "0.25rem 0",
            "color" : "black",
            "font-weight" : "bold"
        },
        '.helpWidget_csscarousel .carousel-item-desc-body': {
            "color" : "#c4c4c4",
            "font-weight" : "normal",
            "font-size" : "90%"
        },
        '.helpWidget_csscarousel .carousel-item .carousel-item-buttons': {
            "border" : "1px solid #e9e9e9",
            "box-sizing" : "border-box",
            "padding" : "0.5em 0.25em"
        },
        /*Carousel*/

        /*Accordion*/
        '.helpWidget_accordion': {
            "width" : "100%"
        },
        '.helpWidget_accordion_body': {
            "width" : "100%",
            "text-align" : "left",
            'font-size': '12px',
            "padding" : "0 5px",
            "box-sizing" : "border-box",
            "height" : "0",
            "margin-top" : "-1px",
            "overflow" : "hidden"
        },
        '.helpWidget_accordion_item': {
            'border': '1px solid rgba(205, 155, 155, 0.5)',
            'border-radius': '5px',
            'min-width': '100px',
            'margin': '2.5px 0'
        },
        '.helpWidget_accordion_head': {
            'padding': '3px 0',
            'box-sizing': 'border-box',
            'cursor': 'pointer',
            'width': '100%',
            'border-bottom': '1px solid rgba(155, 155, 155, 0.5)',
            'border-radius': '5px',
            'text-align': 'center'
        },

        '.helpWidget_accordion_item.active > .helpWidget_accordion_body': {
            "height" : "auto"
        },
        '.helpWidget_accordion_item.active > .helpWidget_accordion_head': {
            "background-color" : "rgba(222, 222, 222, 0.5)"
        },
        '.helpWidget_accordion_item:hover > .helpWidget_accordion_head': {
            "background-color" : "rgba(222, 222, 222, 0.5)"
        }
    }
};


var HelpWidget = /** @class */ (function () {
    var prefix = 'helpWidget',
        remove_msg_indexes = {
            loading: 'gif_message',
            other: 'other'
        },
        bot_name_was_change = false
    ;

    function HelpWidget() {
        this.settings = {
            removeMessages: true,
            APIUrl: 'https://botcreatorapptest.azurewebsites.net/api/widgetconfig/',
            bot: {
                name: 'chatBot',
                message_bot_change: 'The BOT was changed! You communicate with',
                url: 'chatBot.com',
                avarat_url: 'https://media.chat.io/bot/bot.png',
                isBot: true
            },
            user: {
                name: null,
                email: null,
                avarat_url: 'https://www.thecatbehaviorclinic.com/wp-content/themes/tribe/img/avatar.jpeg',
                isBot: false
            },
            targetScroll: null,
            message_icon: 'https://botcreator.azureedge.net/css/loader.gif',
            head_title: 'chat with us!',
            first_message_text: 'Hey, we are line 24/7. Let us know if you have any questions:)',
            description: 'Some description',
            stylesheet: help_widget.styles,
            initial_styles: {
                widget: {},
                modal_head: {},
                modal_body: {},
                buttons: {},
                message: {},
                message_bot_change: {}
            },
            slider: {
                arrows: {
                    left: null,
                    right: null
                }
            },
            buttonsText: {
                chat_now_btn: 'Chat now',
                register_btn: 'Start now'
            },
            form: {
                name: {
                    title: 'Your name:',
                    placeholder: 'Type your name...'
                },
                email: {
                    title: 'E-mail:',
                    placeholder: 'Type your email...'
                }
            },
            btn_images: {
                send_message_btn: '<img src="http://cdn.onlinewebfonts.com/svg/img_85498.png"/>',
                head_settings_btn: '<svg height="4px" version="1.1" viewBox="0 0 16 4" width="16px" class="grey_svg" \n' +
                    '                    xmlns="http://www.w3.org/2000/svg">\n' +
                    '                   <g stroke="none" stroke-width="1">\n' +
                    '                       <g id="menu-vertical">\n' +
                    '                           <path d="M2,0 C0.9,0 0,0.9 0,2 C0,3.1 0.9,4 2,4 C3.1,4 4,3.1 4,2 C4,0.9 3.1,0 2,0 Z M14,0 C12.9,0 12,0.9 12,2 C12,3.1 12.9,4 14,4 C15.1,4 16,3.1 16,2 C16,0.9 15.1,0 14,0 Z M8,0 C6.9,0 6,0.9 6,2 C6,3.1 6.9,4 8,4 C9.1,4 10,3.1 10,2 C10,0.9 9.1,0 8,0 Z"\n' +
                    '                                 id="Shape"></path>\n' +
                    '                       </g>\n' +
                    '                   </g>\n' +
                    '               </svg>',
                head_close_btn: '<svg fill="none" height="25" viewBox="0 0 15 15" width="25" xmlns="http://www.w3.org/2000/svg"\n' +
                    '                 class="grey_svg">\n' +
                    '                <path class="stroked" fill="none" stroke="black" stroke-linecap="square" d="M 4.5 4.5 L 11.5 11.5 M 4.5 11.5 L 11.5 4.5"/>\n' +
                    '            </svg>',
                goodRate_btn: '<svg width="14px" height="19px" viewBox="0 0 14 19" version="1.1"\n' +
                    '               xmlns="http://www.w3.org/2000/svg"\n' +
                    '               class="grey_svg"><title>RateGood</title>\n' +
                    '              <g id="LC2" stroke="none" stroke-width="1" fill-rule="evenodd">\n' +
                    '                  <g transform="translate(-1047.000000, -247.000000)"\n' +
                    '                     fill-rule="nonzero">\n' +
                    '                      <g id="Group" transform="translate(832.000000, 237.000000)">\n' +
                    '                          <path d="M217.011431,10.0079649 C219.301872,10.0254688 221.149331,11.871012 221.149331,14.141574 L221.149331,15.006151 C221.149331,15.7362453 221.746389,16.3281036 222.482897,16.3281036 L225.483677,16.3281036 C225.679987,16.3281036 225.875953,16.3444026 226.069519,16.3768295 C227.984075,16.6975635 229.27384,18.4961084 228.950288,20.3939919 L228.161331,25.0218442 C227.786728,27.2191809 225.866945,28.8276198 223.618875,28.8276198 L220.455497,28.8276198 C217.442509,28.8276198 215,26.406381 215,23.4196316 L215,12.0040832 C215,10.9016253 215.893719,10.0079066 216.996177,10.0079066 C217.001262,10.0079066 217.006347,10.007926 217.011431,10.0079649 Z M223.206897,24.4827922 L227.086361,24.4827922 L227.086361,25.4442124 L224.168317,25.4442124 C223.637339,25.4442124 223.206897,25.0137699 223.206897,24.4827922 Z M223.206897,22.0689991 L228.056228,22.0689991 L228.056228,23.0304193 L224.168317,23.0304193 C223.637339,23.0304193 223.206897,22.5999768 223.206897,22.0689991 Z M223.206897,19.655206 L228.541161,19.655206 L228.541161,20.6166262 L224.168317,20.6166262 C223.637339,20.6166262 223.206897,20.1861837 223.206897,19.655206 Z M216.939732,23.4196316 C216.939732,25.3444256 218.513794,26.9047795 220.455497,26.9047795 L223.618875,26.9047795 C224.920389,26.9047795 226.031842,25.9735781 226.248717,24.7014358 L227.037675,20.0735835 C227.182715,19.2228081 226.604545,18.4165638 225.746296,18.2727865 C225.659525,18.2582503 225.571678,18.2509439 225.483677,18.2509439 L222.482897,18.2509439 C220.675104,18.2509439 219.209598,16.7982006 219.209598,15.006151 L219.209598,14.141574 C219.209598,12.9271831 218.221501,11.9401098 216.996478,11.930748 L216.939732,11.9303143 L216.939732,23.4196316 Z"\n' +
                    '                                ></path>\n' +
                    '                      </g>\n' +
                    '                  </g>\n' +
                    '              </g>\n' +
                    '          </svg>',
                badRate: '<svg width="14px" height="19px" viewBox="0 0 14 19" version="1.1"\n' +
                    '           xmlns="http://www.w3.org/2000/svg"' +
                    '           class="grey_svg"><title>RateBad</title>\n' +
                    '          <g id="LC2" stroke="none" stroke-width="1" fill-rule="evenodd">\n' +
                    '              <g transform="translate(-1092.000000, -247.000000)"\n' +
                    '                 fill-rule="nonzero">\n' +
                    '                  <g transform="translate(832.000000, 237.000000)">\n' +
                    '                      <path d="M262.011431,10.0079649 C264.301872,10.0254688 266.149331,11.871012 266.149331,14.141574 L266.149331,15.006151 C266.149331,15.7362453 266.746389,16.3281036 267.482897,16.3281036 L270.483677,16.3281036 C270.679987,16.3281036 270.875953,16.3444026 271.069519,16.3768295 C272.984075,16.6975635 274.27384,18.4961084 273.950288,20.3939919 L273.161331,25.0218442 C272.786728,27.2191809 270.866945,28.8276198 268.618875,28.8276198 L265.455497,28.8276198 C262.442509,28.8276198 260,26.406381 260,23.4196316 L260,12.0040832 C260,10.9016253 260.893719,10.0079066 261.996177,10.0079066 C262.001262,10.0079066 262.006347,10.007926 262.011431,10.0079649 Z M268.206897,24.4827922 L272.086361,24.4827922 L272.086361,25.4442124 L269.168317,25.4442124 C268.637339,25.4442124 268.206897,25.0137699 268.206897,24.4827922 Z M268.206897,22.0689991 L273.056228,22.0689991 L273.056228,23.0304193 L269.168317,23.0304193 C268.637339,23.0304193 268.206897,22.5999768 268.206897,22.0689991 Z M268.206897,19.655206 L273.541161,19.655206 L273.541161,20.6166262 L269.168317,20.6166262 C268.637339,20.6166262 268.206897,20.1861837 268.206897,19.655206 Z M261.939732,23.4196316 C261.939732,25.3444256 263.513794,26.9047795 265.455497,26.9047795 L268.618875,26.9047795 C269.920389,26.9047795 271.031842,25.9735781 271.248717,24.7014358 L272.037675,20.0735835 C272.182715,19.2228081 271.604545,18.4165638 270.746296,18.2727865 C270.659525,18.2582503 270.571678,18.2509439 270.483677,18.2509439 L267.482897,18.2509439 C265.675104,18.2509439 264.209598,16.7982006 264.209598,15.006151 L264.209598,14.141574 C264.209598,12.9271831 263.221501,11.9401098 261.996478,11.930748 L261.939732,11.9303143 L261.939732,23.4196316 Z"\n' +
                    '                            transform="translate(267.000000, 19.413810) scale(-1, -1) translate(-267.000000, -19.413810) "></path>\n' +
                    '                  </g>\n' +
                    '              </g>\n' +
                    '          </g>\n' +
                    '      </svg>'

            }
        };
        this.date = new Date();
    }

    /*if isBot == true this is Bot, else this is user click*/
    HelpWidget.prototype.create_message =  function (isBot, message, add_to_remove = false, start_removing = false) {

        if (start_removing) {
            clear_remove_messages_array(remove_msg_indexes.other);
        }

        if (is_the_last_me(isBot) && !bot_name_was_change)
            add_new_message(message, add_to_remove);
        else
        {
            bot_name_was_change = false;
            create_first_message(isBot ? this.settings.bot : this.settings.user, message, add_to_remove);
        }
    };

    function extend(obj1, obj2) {
        var keys = Object.keys(obj2);
        for (var i = 0; i < keys.length; i += 1) {
            var val = obj2[keys[i]];
            obj1[keys[i]] = ['string', 'number', 'array', 'boolean'].indexOf(typeof val) === -1 ? extend(obj1[keys[i]] || {}, val) : val;
        }
        return obj1;
    }

    function jsonp(url, callback) {
        var callbackName = 'jsonp_callback_' + Math.round(100000 * Math.random());
        window[callbackName] = function(data) {
            delete window[callbackName];
            document.body.removeChild(script);
            callback(data);
        };

        var script = document.createElement('script');
        script.src = url + (url.indexOf('?') >= 0 ? '&' : '?') + 'callback=' + callbackName;
        document.body.appendChild(script);
    }

    function uuidv4() {
          return ([1e7]+-1e3+-4e3+-8e3+-1e11).replace(/[018]/g, c =>
            (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
          )
    }

    function sendNavigationData(ipData) {
        var params = { 
            Id: uuidv4(),
            WebUserId : cookieService.webUserId,
            Cookies: navigator.cookieEnabled,
            BrowserLanguage: navigator.browserLanguage,
            Language: navigator.language,
            Platform: navigator.platform,
            Connection: navigator.connectionSpeed,
            UserAgent: navigator.userAgent,
            Webdriver: navigator.webdriver,
            CurrentUrl: window.location.href,
            BotId: help_widget.widget.settings.bot.id,
            CreatedDate: "1/2/1981",
            Ip : ipData.ip,
            City: ipData.city,
            Region : ipData.region,
            Country : ipData.country,
            Loc : ipData.loc,
            Postal: ipData.postal,
            Referrer: document.referrer
        };
        var ajax = new XMLHttpRequest();
        ajax.open("POST", "https://botdata.azurewebsites.net/api/Metric", true);
        ajax.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        ajax.send(
            JSON.stringify(params)
        );
    }

    function passNavigatorData() {
        jsonp('https://ipinfo.io', function(response) {
               sendNavigationData(response);       
        });
    }

    function after_widget_constructor(that) {
        widget_constructor(that.settings, function (widget) {
            document.body.appendChild(widget);

            closeWidget();

            document.help_widget.dom.toggler.onclick = function(){
                show_chat_view(that);
                openWidget();
            };

            document.help_widget.dom.modal_head.template.getElementsByClassName('close_widget')[0].onclick = function(){
                pnWrapper.unsubscribe();
                closeWidget();
            };

            document.help_widget.dom.views.chat.btn_submit.onclick = function(){
                show_register_view();
            };

            document.help_widget.dom.views.form.template.onsubmit = function(e){
                e.preventDefault();
                var data = this.getElementsByTagName('input');

                that.login({
                    name: data.name.value,
                    email: data.email.value
                });
                pnWrapper.sendInitialize(that.settings.user.name, that.settings.user.email, that.settings.bot.id);
            };

            that.create_message(
                that.settings.bot.isBot,
                that.settings.first_message_text
            );
            that.create_message(
                that.settings.bot.isBot,
                that.settings.description
            );

            create_main_styles(that.settings.stylesheet);
            applyStyles(that.settings.initial_styles);
            /*
            if (that.settings.styles.widget.fontFamily){
                that.settings.stylesheet['*'] = extend(that.settings.stylesheet['*'], {'font-family': that.settings.styles.widget.fontFamily});
                create_main_styles(that.settings.stylesheet);
            }*/
            passNavigatorData();
        });
    }

    HelpWidget.prototype.init = function (config) {
        this.settings = extend(this.settings, config);
        // var that = this;
        /*if(typeof this.settings.APIUrl === 'string')
            get_request_to_url(this.settings, function (callback) {
                after_widget_constructor(that);
                typeof callback === 'function' && callback();
            });
        else*/

        after_widget_constructor(this);
    };

    var getTime = function () {
        return new Date().getHours() + ':' + (new Date().getMinutes()<10?'0':'') + new Date().getMinutes()
    };

    HelpWidget.prototype.login = function (data) {
        Object.assign(this.settings.user, data);
        show_chat_view(this);
    };

    HelpWidget.prototype.clear_previous = function (callback) {
        if (help_widget.dom.messages_area.children.length === 1)
            return false;

        help_widget.dom.messages_area.innerHTML = '';
        this.create_message(true, this.settings.first_message_text);
        typeof callback === 'function' && callback();
    };

    HelpWidget.prototype.send_the_message = function () {
        var message = help_widget.dom.views.chat.chat_field.value;
        if (!message.length)
            return false;

        if (pnWrapper.chatMode)
        {
            pnWrapper.sendChatMessage(message);
        }else{
            pnWrapper.sendMessage(message);
        }

        this.create_message(false, message);

        help_widget.dom.views.chat.chat_field.value = '';
    };

    HelpWidget.prototype.setBotName = function(name) {
        this.settings.bot.name = name;
        help_widget.dom.messages_area.appendChild(create_element(
            'div',
            {class: prefix+'_bot_change_message'},
            this.settings.bot.message_bot_change +' '+ name + '!'
        ));
        bot_name_was_change = true;
    };

    HelpWidget.prototype.isUserLogin = function () {

        if (!this.settings.user.name || !this.settings.user.email)
            return false;

        help_widget.dom.views.chat.btn_submit.style.display = 'none';

        if (help_widget.dom.views.chat.btn_send)
            return true;

        help_widget.dom.views.chat.template
            .getElementsByClassName('helpWidget_modal_body_btn_wrap')[0]
            .appendChild(create_chating_field(this.settings));

        return true;
    };

    function create_element (tagName, properties, text, callback) {
        var el = document.createElement(tagName, 'widget');
        for (var prop in properties) {
            switch (prop) {
                case 'class':
                    el.className = properties[prop];
                    break;
                case 'id':
                    el.id = properties[prop];
                    break;
                case 'attr':
                    for (var i in properties[prop]){
                        el.setAttribute(i, properties[prop][i]);
                    }
                    break;
                case 'type':
                    el.type = properties[prop];

                    break;
                default:
                    console.log(prop, 'default');
                    break;
            }
        }
        el.innerHTML = text ? text : '';

        if (typeof callback === 'function')
            return callback(el);
        else
            return el;
    }

    function widget_constructor(settings, callback) {

        var $widget_toggler = help_widget.dom.toggler = create_element('div', {class:  prefix + '_toggler'}, '', function (el) {
                el.appendChild(
                    create_element( 'div', {class: prefix + '_person_message'}, '', function (el) {
                        el.innerHTML = '<div class="'+prefix+'_clear_message">'+settings.first_message_text+'</div>'
                        return el;
                    })
                );
                el.appendChild(
                    create_element('button',{}, '', function (button) {
                        button.innerHTML = '<img src="' + settings.bot.avarat_url + '" alt="image">';
                        return button;
                    })
                );
                return el;
            }),
            $widget_modal_head_top= create_element('div', {class: prefix + '_modal_head__top'}, '', function (el) {
                el.appendChild(
                    create_element('button', {class: 'head_btn settings'}, '', function (el) {
                        el.innerHTML = settings.btn_images.head_settings_btn;
                        return el;
                    })
                );
                el.appendChild(
                    create_element('strong', {}, settings.head_title)
                );
                document.help_widget.dom.modal_head.close_btn = create_element('button', {class: 'head_btn close_widget'}, '', function (el) {
                    el.innerHTML = settings.btn_images.head_close_btn;
                    return el;
                });
                el.appendChild(document.help_widget.dom.modal_head.close_btn);

                return el;
            }),
            $widget_modal_head_bottom = create_element('div', {class: prefix + '_modal_head__bottom'}, '', function (el) {
                el.innerHTML = '<div class="' + prefix + '_main_avatar">\n' +
                    '                        <div class="' + prefix + '_main_avatar_wrapper">\n' +
                    '                            <img src="' + settings.bot.avarat_url + '" alt="" class="avatar">\n' +
                    '                        </div>\n' +
                    '                        <div class="' + prefix + '_main_avatar_info">\n' +
                    '                            <strong>' + settings.bot.name + '</strong>\n' +
                    '                            <p>' + settings.bot.url + '</p>\n' +
                    '                        </div>\n' +
                    '                    </div>\n' +
                    '                    <div class="' + prefix + '_feedback_buttons">\n' +
                    '                        <button class="' + prefix + '_feedback_button_like">\n' +
                    settings.btn_images.goodRate_btn +
                    '                        </button>\n' +
                    '                        <button class="' + prefix + '_feedback_button_dislike">\n' +
                    settings.btn_images.badRate +
                    '                        </button>\n' +
                    '                    </div>';
                return el;
            }),
            $widget_modal_head =  help_widget.dom.modal_head.template = create_element('div', {class: prefix + '_modal_head'}, '', function (el) {
                el.appendChild($widget_modal_head_top);
                el.appendChild($widget_modal_head_bottom);
                return el;
            }),
            $message_view = help_widget.dom.messages_area = create_element('div', {class: prefix + '_message_view'}),
            $form_btn_chat = help_widget.dom.views.chat.btn_submit = create_element('button', {class: prefix + '_custom-button', type: 'submit'} , '', function (btn) {
                btn.innerHTML = settings.buttonsText.chat_now_btn;
                return btn;
            }),
            $messages_footer = create_element('div', {class: prefix + '_modal_body_btn_wrap'}, '', function (el) {
                el.appendChild($form_btn_chat);
                return el;
            }),
            $chat_view = help_widget.dom.views.chat.template = create_element('div', {class: prefix + '_chat_view'}, '', function (el) {
                el.appendChild($message_view);
                el.appendChild($messages_footer);
                return el;
            }),
            $form_btn_start = help_widget.dom.views.form.btn_submit = create_element('button', {class: prefix + '_custom-button', type: 'submit'} , '', function (btn) {
                btn.innerHTML = settings.buttonsText.register_btn;
                return btn;
            }),
            $form_view = help_widget.dom.views.form.template = create_element('form', {class: prefix + '_register_form'}, '', function (form) {
                form.innerHTML = '<label for="name">' +settings.form.name.title +
                    '<input id="name" type="text" placeholder="'
                    + settings.form.name.placeholder + '" required></label>\n' +
                    '<label for="email">\n' +
                    settings.form.email.title +
                    '<input id="email" type="email" placeholder="' + settings.form.email.placeholder + '" required>\n' +
                    '</label>';
                form.appendChild($form_btn_start);
                return form;
            }),

            $widget_modal_body = help_widget.dom.modal_body.template = create_element('div', {class: prefix + '_modal_body'}, '', function (el) {
                el.appendChild($chat_view);
                el.appendChild($form_view);
                return el;
            }),
            $widget_modal_inner = create_element('div', {class: prefix + '_modal_inner'}, '', function (el) {
                el.appendChild($widget_modal_head);
                el.appendChild($widget_modal_body);
                return el;
            }),

            $widget_modal = create_element('div', {class: prefix + '_modal'}, '', function (modal) {
                modal.appendChild($widget_modal_inner);
                return modal;
            }),
            $widget = create_element('div', {id: prefix}, '', function (widget) {
                widget.appendChild($widget_toggler);
                widget.appendChild($widget_modal);
                return widget;
            })
        ;

        help_widget.dom.widget_modal = $widget_modal;
        help_widget.dom.widget = $widget;

        typeof callback === 'function' && callback($widget);
    }

    function create_chating_field(settings) {
        var $chat_field = help_widget.dom.views.chat.chat_field = create_element('textarea'),
            $btn_send = help_widget.dom.views.chat.btn_send = create_element('button', {class: 'btn-simple'}, '', function (btn) {
                btn.innerHTML = settings.btn_images.send_message_btn;
                return btn;
            }),
            $message_wrapper = help_widget.dom.views.chat.message_wrapper =
                create_element('div', {class: prefix + '_message_wrapper'}, '', function (el) {
                    el.appendChild($chat_field);
                    el.appendChild($btn_send);
                    return el;
                }),
            $top_side = create_element('div', {class: prefix + '_main_avatar'}, '', function (el) {
                el.appendChild(create_element('div', {class: prefix + '_main_avatar_wrapper'}, '',
                    function (el) {
                        el.innerHTML = '<img src="' + settings.user.avarat_url + '" class="avatar" alt="User avatar"/>';
                        return el;
                    }));
                el.appendChild($message_wrapper);
                return el;
            })
        ;

        return create_element('div', {class: prefix + '_chat_field'}, '', function (el) {
            el.appendChild($top_side);
            return  el;
        });
    }

    function setStyles(el, styles) {
        for (var prop in styles){
            el.setStyle(prop, styles[prop]);
        }
    }

    function applyStyles(styles) {
        setStyles(help_widget.styles['.helpWidget_bot_change_message'], styles.message_bot_change);
        setStyles(help_widget.styles['.helpWidget_person_message'], styles.message);
        setStyles(help_widget.styles['.helpWidget_modal_body'], styles.modal_body);
        setStyles(help_widget.styles['.helpWidget_modal_head'], styles.modal_head);
        setStyles(help_widget.styles['button.helpWidget_custom-button'], styles.buttons);
        setStyles(help_widget.styles['.helpWidget_modal'], styles.widget);
    }

    function openWidget() {
        help_widget.dom.widget_modal.style.display = 'block';
        help_widget.dom.toggler.style.display = 'none';
    }

    function closeWidget() {
        help_widget.dom.widget_modal.style.display = 'none';
        help_widget.dom.toggler.style.display = 'block';
    }

    function show_register_view() {
        help_widget.dom.views.chat.template.style.display = 'none';
        help_widget.dom.views.form.template.style.display = 'block';
    }

    function show_chat_view(plugin) {
        help_widget.dom.views.chat.template.style.display = 'block';
        help_widget.dom.views.form.template.style.display = 'none';

        plugin.isUserLogin() && apply_chating_functional(plugin)
    }

    function is_the_last_me(me) {
        if (!help_widget.dom.message_wrapper) return false;
        return me.toString() === help_widget.dom.message_wrapper.parentElement.getAttribute('data-bot')
    }

    function clear_remove_messages_array(id) {
        var els = help_widget.dom.views.chat.can_be_remove[id];

        if (help_widget.widget.settings.removeMessages === false) {
            if(id === 'other')
                return false;
        }
        for (var i in els) {
            els[i] && els[i].remove();
        }

        help_widget.dom.views.chat.can_be_remove[id] = [];
    }

    function create_first_message(config, message, add_to_remove) {
        var $personal_info = create_element('div', {class: 'personal_credentials'}, '', function (el) {
                el.innerHTML = config.name + ' <span class="time"> ' + getTime() + '</span>'
                return el;
            }),
            $messages = help_widget.dom.message_wrapper = create_element('div', {class: prefix + '_person_messages'}, '', function (el) {
                el.appendChild($personal_info);
                return el;
            }),
            $messages_view = create_element('div', {class: prefix + '_person', attr: {'data-bot':config.isBot}}, '', function (el) {
                el.innerHTML = '<div class="' + prefix + '_person_avatar">' +
                    '<div class="' + prefix + '_person_avatar_wrapper"><img src="' + config.avarat_url + '" alt="avatar" class="avatar"></div>\n' +
                    '</div>';
                el.appendChild($messages);
                return el;
            })
        ;

        help_widget.dom.messages_area.appendChild($messages_view);
        // console.log(help_widget.dom.messages_area, $messages);
        add_new_message(message, add_to_remove);
    }

    function make_clear_message(text) {
        return '<div class="'+prefix+'_clear_message">'+text+'</div>'
    }

    function check_the_message(data) {
        // console.log(data);
        if (Array.isArray(data)) {
            clear_all_before();
            if(Array.isArray(data[0]))
                return make_the_buttons(data);
            else
                return make_the_accordion(data);
        }
        else if (typeof data === 'object') {
            clear_all_before();
            return make_the_carousel(data);
        }
        else
            return make_clear_message(data);
    }

    function make_accordion_item(item_data) {
        var innerBody = typeof item_data.body === 'object' ? check_the_message(item_data.body) : item_data.body,
            head = '<div class="' + prefix + '_accordion_head" onclick="widget.applyToggle(this)">' + item_data.header + '</div>',
            body = '<div class="' + prefix + '_accordion_body">' + innerBody + '</div>'
        ;

        return '<div class="' + prefix + '_accordion_item">' + head + body + '</div>';
    }
    function make_the_accordion(data) {
        var accordion = '<div class="'+ prefix + '_accordion">';
        var item = '';
        for(var i in data) {
            accordion += make_accordion_item(data[i]);
        }
        return accordion + '</div>';
    }

    function make_the_buttons(data, element_id) {

        var btns = '';
        element_id = element_id ? '-'+element_id : '';

        for(var i in data) {
            btns += '<div class="btn_column">';
            for(var item in data[i]) {
                var entity = data[i][item];
                if (typeof entity === 'object') {
                    var text = entity.Icon ? '<i class="fa '+entity.Icon+'" aria-hidden="true"></i>' : entity.Text,
                        linkClasses = entity.Icon ? 'icon-link btn-simple' : 'btn-simple';
                    if (entity.Icon && entity.Text && entity.Text != '')
                    {
                        text += ' ' + entity.Text;
                    }
                    if (entity.Link) {
                        btns += '<button class="'+linkClasses+'" onclick=\"pnWrapper.sendCallback(\''+ entity.Icon + '\');window.open(\''+ entity.Link + '\',\'_blank\')\">' + text + '</button>';
                    } else
                        btns += '<button class="'+linkClasses+'" onclick=\"pnWrapper.sendCallback(\''+ entity.Text.toString() + '\')\">' + text + '</button>';

                }else
                    btns += '<button class="btn-simple" onclick=\"pnWrapper.sendCallback(\''+ entity.Text.toString() + '\')\">' + entity + '</button>';
            }
            btns += '</div>';
        }

        return {
            type: 'buttons',
            html: btns
        };
    }

    function make_the_buttons_carrousel(data, element_id) {

        var btns = '';
        element_id = element_id ? '-'+element_id : '';

        for(var i in data) {
            btns += '<div class="btn_column">';
            for(var item in data[i]) {
                btns += '<button class="btn-simple" onclick="pnWrapper.sendCallback(\''+data[i][item]+element_id+'\');">' + data[i][item] + '</button>';
            }
            btns += '</div>';
        }
        return btns;
    }

    function make_the_carousel(data) {
        var carousel = '<div class="'+ prefix +'_csscarousel">',
            slides = '<ul>',
            navigation = '',
            arrows = '<div class="arrows">',
            unique = new Date().getTime()
        ;
        for(var i in data) {
            if (i === Object.keys(data)[0])
                navigation += '<input type="radio" name="slides' + unique + '" checked="checked" id="slide-' + unique + data[i].id + '"/>';
            else
                navigation += '<input type="radio" name="slides' + unique + '" id="slide-' + unique + data[i].id + '"/>';
            arrows += '<label for="slide-' + unique + data[i].id + '"></label>';

            slides += '<li class="carousel-item">' +
                '<div class="carousel-item-image"><img src="' + data[i].image_url + '" alt="image"></div>' +
                '<div class="carousel-item-desc">' +
                '<div class="carousel-item-desc-head">' + data[i].title + '</div>' +
                '<div class="carousel-item-desc-body">' + data[i].description + '</div>' +
                '</div>' +
                '<div class="carousel-item-buttons">' + make_the_buttons_carrousel(data[i].buttons, data[i].id) + '</div>' +
                '</li>'
            ;
        }

        arrows += '</div>';
        slides += '</ul>';

        return carousel + navigation + slides + arrows + '</div>';
    }

    function add_loading_message (plugin) {
        if (!plugin.isUserLogin())
            return false;

        var $message_with_loader = create_element('div', {class: prefix + '_person_message ' + prefix + '_person_gif_message'}, '' ,function (el) {
            el.innerHTML = '<img src="'+ plugin.settings.message_icon +'" alt="loader-icon">';
            return el;
        });

        help_widget.dom.message_wrapper.appendChild($message_with_loader);
        help_widget.dom.views.chat.can_be_remove[remove_msg_indexes.loading].push($message_with_loader);
    }

    function add_new_message(data, add_to_remove = false) {
        // Example how to use for carousel
        /*
                 var data = {
                     slide_1: {
                         id: 1,
                         image_url: 'https://via.placeholder.com/150',
                         title: 'Slide 1',
                         description: 'this is description for first slide, thanks',
                         buttons: [
                             ['Button 1'],
                             ['Button 2']
                         ]
                     },
                     slide_2: {
                         id: 2,
                         image_url: 'https://via.placeholder.com/150',
                         title: 'Slide 2',
                         description: 'This is description for second slide, thanks',
                         buttons: [
                             ['Button 2'],
                             ['Button 3']
                         ]
                     }
                 };*/

        // Simple accordion
        /*
                var data = [
                    {
                        "header" : "header 1",
                        "body" : "Ad asperiores at consequatur deserunt dicta dolore, esse ex excepturi harum incidunt obcaecati perferendis perspiciatis?"
                    },
                    {
                        "header" : "header 2",
                        "body" : "body 2"
                    },
                    {
                        "header" : "header 3",
                        "body" : "body 3"
                    }
                ];*/



        // Multilevel accordion


        /*var data = [{
            "header" : "header 1",
            "body" :
                [
                    {
                        "header" : "subheader 1",
                        "body" : "body 1 sda d sad asdh adhaskjdhaskjh dkjashdkjsahdksahd kjsahd kjsad hsad hsadsakjd hsakjdhsakdh sad" +
                            "asdsad sad sad asd as"
                    },
                    {
                        "header" : "subheader 2",
                        "body" : "body 2"
                    },
                    {
                        "header" : "subheader 3",
                        "body" : "body 3"
                    }
                ]
        },
            {
                "header" : "header 1",
                "body" : "body 1"
            },
            {
                "header" : "header 2",
                "body" : "body 2"
            },
            {
                "header" : "header 1",
                "body" : [['Top1','Top2'],['Mid1','Mid2','Mid3']]
            }];*/
        /*var data = [[{ icon: "fa-whatsapp" , text: "WhatsApp" , link: "https://api.whatsapp.com/send?phone=11223344&text=Hi"},{ text: "WhatsApp", link: "https://api.whatsapp.com/send?phone=11223344&text=Hi"}]
            ,[{ icon: "fa-instagram" , link: "", text: 'Instagram'},{ icon: "fa-instagram" , link: "https://www.instagram.com"},'Mid3']];
*/
        if (!data)
            return false;


        var type = 'default',
            inner_content = check_the_message(data),
            classes = prefix + '_person_message'
        ;

        if (typeof inner_content == 'object') {
            type = inner_content.type;
            inner_content = inner_content.html;
            classes += ' btn_wrapper'
        }

        var $message = create_element('div', {class: classes}, '', function (el) {
            el.innerHTML = inner_content;
            if (type === 'buttons') {
                el.addEventListener("click", function (e){handlerClick(e, $message, el)});
            }
            return el;
        });

        clear_remove_messages_array(remove_msg_indexes.loading);

        help_widget.dom.message_wrapper.appendChild($message);
        if (add_to_remove) help_widget.dom.views.chat.can_be_remove[remove_msg_indexes.other].push($message);
    }

    function animate(elem, style, unit, from, to, time, prop, callback) {
        // debugger
        if (!elem) {
            return;
        }
        var start = new Date().getTime(),
            timer = setInterval(function () {
                var step = Math.min(1, (new Date().getTime() - start) / time);
                if (prop) {
                    elem[style] = (from + step * (to - from))+unit;
                } else {
                    elem.style[style] = (from + step * (to - from))+unit;
                }
                if (step === 1) {
                    clearInterval(timer);
                    typeof callback === 'function' && callback();
                }
            }, 15);
        if (prop) {
            elem[style] = from+unit;
        } else {
            elem.style[style] = from+unit;
        }
    }

    function handlerClick(e, $message, elms) {
        var
            // innerHeight = 0,
            lineOfNewMsgs = elms.offsetTop
        ;
        // from = e.target.offsetTop + e.target.offsetHeight;
        /*help_widget.dom.messages_area.childNodes.forEach(function(value){
            innerHeight += value.offsetHeight;
        });*/

        elms.remove($message);
        if (help_widget.widget.settings.removeMessages !== false) {
            e.target.removeEventListener(e.type, arguments.callee);
        } else {
            if (help_widget.widget.settings.targetScroll === null) {
                help_widget.widget.settings.targetScroll = $message;
                setTimeout(function(){
                    animate(
                        help_widget.dom.messages_area,
                        "scrollTop",
                        0, 0,
                        lineOfNewMsgs,
                        1000,
                        true,
                        function () {
                            help_widget.widget.settings.targetScroll = null;
                        });
                },1000);
            }
        }
    }

    function apply_chating_functional(plugin) {
        if (help_widget.dom.views.chat.active)
            return false;

        help_widget.dom.views.chat.active = true;
        help_widget.dom.views.chat.btn_send.onclick = function (e){plugin.send_the_message()};
        help_widget.dom.views.chat.chat_field.onkeydown = function (e) {
            if (e.which === 13) {
                e.preventDefault();
                plugin.send_the_message();
            }
        };
        add_loading_message(plugin);
    }


    function clear_all_before() {
        var $item_before_clear = {};
        for(var i of help_widget.dom.messages_area.children){
            var $item = i;
            if ($item.getElementsByClassName('btn_wrapper').length || $item.getElementsByClassName(prefix +'_csscarousel').length )
                $item_before_clear = $item.getElementsByClassName(prefix +'_csscarousel') || $item.getElementsByClassName('btn_wrapper');
        }

        if ($item_before_clear.length) {
            for(var i of $item_before_clear) {
                var person_message = i.parentNode.previousSibling;
                if(person_message.classList.contains('helpWidget_person_message')) {
                    person_message.remove();
                }
                i.parentNode.remove();
            }
        }
    }

    function createStyle(j, props) {
        var selector = '#'+prefix +' '+j+'{';
        for (var i in props) {
            if (typeof props[i] !== 'function')
                selector += i + ': ' + props[i] + ';'
        }
        selector += '}\n';
        return selector;
    }

    function create_main_styles(conf) {
        help_widget.dom.widget.prepend(create_element('div', {id: prefix + '_styleSheet'}, '', function (el) {

            for (var j in conf) {
                el.appendChild(create_element('style', {attr: {'data-id': j}}, '', function (style) {
                    var style_props = conf[j],
                        item = j;
                    style.innerHTML = createStyle(item, style_props);
                    style_props.setStyle = function (prop, value) {
                        if (Object.keys(this).indexOf(prop) !== -1 && this[prop] === value)
                            return false;

                        this[prop] = value;
                        style.innerHTML = createStyle(item, this);
                    };
                    return style;
                }));

            }
            return el;
        }));
    }

    return HelpWidget;
}());

help_widget.widget = new HelpWidget;


pnWrapper = {

    token: "",
    channelId: "",
    publishChannel: "",
    userId: "",
    chatMode: false,
    webUserId: "",

    sendInitialize: function (name1, email1, botId1) {
        mymessage.BotId = botId1;
        mymessage.WebUserId = cookieService.webUserId;
        var params = {
            name: name1,
            email: email1,
            botId: botId1,
            clientId:mymessage.WebUserId
        };
        var ajax = new XMLHttpRequest();
        ajax.open("POST", "https://botengineapi-prod.azurewebsites.net/api/chat", true);
        ajax.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
        ajax.send(
            JSON.stringify(params)
        );
        ajax.onreadystatechange = function() {
            if (ajax.readyState === 4 && ajax.status === 200) {
                var data = JSON.parse(ajax.responseText);
                // console.log(data);

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
                pnWrapper.processMessage(message);
            }
        })
    },
    sendCallback: function (text) {
        mymessage.Type = "callback";
        mymessage.Text = text;
        mymessage.ChatId = pnWrapper.channelId;
        mymessage.FromId = pnWrapper.channelId;
        mymessage.WebUserId = cookieService.webUserId;
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
        mymessage.WebUserId = cookieService.webUserId;
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
    "WebUserId": cookieService.webUserId  
};

var myChatMessage = {
    "userId": "",
    "text": "",
    "sender": "client" // agent, client
};