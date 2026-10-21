<?php

/**

 * Plugin Name: BotCreator* Plugin URI: http://www.BotCreator.com/

 * Description: This plugin accepts values in the Admin Panel and substitutes in the help_widget json script code.

 * Version: 1.0

 * Author: Dinesh

 * Author URI: http://www.BotCreator.com/

 */

function botcreator_config()

{

    include('botcreator_config.php');

}



function botcreator_admin_actions()

{

    add_options_page("BotCreator", "BotCreator", 1, "BotCreator", "botcreator_config");

}



add_action('admin_menu', 'botcreator_admin_actions');

function botcreator_output()

{

    $botcreator_bot_name = get_option('botcreator_bot_name');

    if ($botcreator_bot_name == "") $botcreator_bot_name = 'Bot Agent';

    $botcreator_bot_url = get_option('botcreator_bot_url');

    if ($botcreator_bot_url == "") $botcreator_bot_url = 'https://botcreator.online';

    $botcreator_bot_avatar_url = get_option('botcreator_bot_avatar_url');

    if ($botcreator_bot_avatar_url == "") $botcreator_bot_avatar_url = 'https://botcreator.azureedge.net/images/toy-robot-small.jpg';

    $botcreator_bot_id = get_option('botcreator_bot_id');

    if ($botcreator_bot_id == "") $botcreator_bot_id = 'REPLACE_WITH_YOUR_VALUE';

    $botcreator_user_avatar_url = get_option('botcreator_user_avatar_url');

    if ($botcreator_user_avatar_url == "") $botcreator_user_avatar_url = 'https://botcreator.azureedge.net/images/avatar.jpg';

    $botcreator_head_title = get_option('botcreator_head_title');

    if ($botcreator_head_title == "") $botcreator_head_title = 'Chat with us!';

    $botcreator_first_message_text = get_option('botcreator_first_message_text');

    if ($botcreator_first_message_text == "") $botcreator_first_message_text = 'Let me help you :)';

    $botcreator_description = get_option('botcreator_description');

    if ($botcreator_description == "") $botcreator_description = 'I am the Bot agent, I can assist with frequently asked questions';

    $botcreator_buttons_chart_now_btn = get_option('botcreator_buttons_chart_now_btn');

    if ($botcreator_buttons_chart_now_btn == "") $botcreator_buttons_chart_now_btn = 'Chat now';

    $botcreator_buttons_register_btn = get_option('botcreator_buttons_register_btn');

    if ($botcreator_buttons_register_btn == "") $botcreator_buttons_register_btn = 'Start';

    $botcreator_form_name_title = get_option('botcreator_form_name_title');

    if ($botcreator_form_name_title == "") $botcreator_form_name_title = 'Your name:';

    $botcreator_form_name_placeholder = get_option('botcreator_form_name_placeholder');

    if ($botcreator_form_name_placeholder == "") $botcreator_form_name_placeholder = 'Type your name...';

    $botcreator_form_email_title = get_option('botcreator_form_email_title');

    if ($botcreator_form_email_title == "") $botcreator_form_email_title = 'E-mail:';

    $botcreator_form_email_placeholder = get_option('botcreator_form_email_placeholder');

    if ($botcreator_form_email_placeholder == "") $botcreator_form_email_placeholder = 'Type your email...';

    $botcreator_styles_widget_fontfamily = get_option('botcreator_styles_widget_fontfamily');

    if ($botcreator_styles_widget_fontfamily == "") $botcreator_styles_widget_fontfamily = 'Open Sans';

    $botcreator_styles_widget_width = get_option('botcreator_styles_widget_width');

    if ($botcreator_styles_widget_width == "") $botcreator_styles_widget_width = '350px';

    $botcreator_styles_widget_right = get_option('botcreator_styles_widget_right');

    if ($botcreator_styles_widget_right == "") $botcreator_styles_widget_right = '1em';

    $botcreator_styles_widget_bottom = get_option('botcreator_styles_widget_bottom');

    if ($botcreator_styles_widget_bottom == "") $botcreator_styles_widget_bottom = '1em';

    $botcreator_modal_head_backgroundcolor = get_option('botcreator_modal_head_backgroundcolor');

    if ($botcreator_modal_head_backgroundcolor == "") $botcreator_modal_head_backgroundcolor = '#ffffff';

    $botcreator_modal_body_backgroundcolor = get_option('botcreator_modal_body_backgroundcolor');

    if ($botcreator_modal_body_backgroundcolor == "") $botcreator_modal_body_backgroundcolor = '#E9EEF4';

    $botcreator_buttons_fontfamily = get_option('botcreator_buttons_fontfamily');

    if ($botcreator_buttons_fontfamily == "") $botcreator_buttons_fontfamily = 'Open Sans';

    $botcreator_buttons_backgroundcolor = get_option('botcreator_buttons_backgroundcolor');

    if ($botcreator_buttons_backgroundcolor == "") $botcreator_buttons_backgroundcolor = '#2ecc71';

    $botcreator_buttons_color = get_option('botcreator_buttons_color');

    if ($botcreator_buttons_color == "") $botcreator_buttons_color = '#ffffff';

    $botcreator_buttons_minheight = get_option('botcreator_buttons_minheight');

    if ($botcreator_buttons_minheight == "") $botcreator_buttons_minheight = '45px';

    $botcreator_message_backgroundcolor = get_option('botcreator_message_backgroundcolor');

    if ($botcreator_message_backgroundcolor == "") $botcreator_message_backgroundcolor = '#ffffff';

    $botcreator_message_boxshadow = get_option('botcreator_message_boxshadow');

    if ($botcreator_message_boxshadow == "") $botcreator_message_boxshadow = '0 1px 15px rgba(0,0,0,0.5)';

    echo "<link rel='stylesheet' href='https://botcreator.azureedge.net/css/styles.css'>

		<link rel='stylesheet' href='https://botcreator.azureedge.net/css/helpWidget.css'>

        <link rel='stylesheet' type='text/css' href='https://fonts.googleapis.com/css?family=Open+Sans' />

        <script src='https://cdn.pubnub.com/sdk/javascript/pubnub.4.21.7.js'></script>

        <script src='https://botcreator.azureedge.net/javascript/helpWidget.js'></script>

        <script>		

            help_widget.init({

                    bot: {

                        name: '" . $botcreator_bot_name . "',

            			url: '" . $botcreator_bot_url . "',

                        avarat_url: '" . $botcreator_bot_avatar_url . "',

                        id: '" . $botcreator_bot_id . "'			

                        },

                    user: {

                        avarat_url: '" . $botcreator_user_avatar_url . "'

                        },			

                        head_title: '" . $botcreator_head_title . "',

                        first_message_text: '<b>" . $botcreator_first_message_text . "</b>',

                        description: '" . $botcreator_description . "',

                        icons: {},

                        buttons: {				

                            chart_now_btn: '" . $botcreator_buttons_chart_now_btn . "',

                            register_btn: '" . $botcreator_buttons_register_btn . "'

                        },			

                        form: {				

                            name: {					

                                title: '" . $botcreator_form_name_title . "',

                                placeholder: '" . $botcreator_form_name_placeholder . "'

                            },				

                            email: {

                                title: '" . $botcreator_form_email_title . "',					placeholder: '" . $botcreator_form_email_placeholder . "'				}			},			styles: {				widget: {					fontFamily: '" . $botcreator_styles_widget_fontfamily . "',					width: '" . $botcreator_styles_widget_width . "',					right: '" . $botcreator_styles_widget_right . "',					bottom: '" . $botcreator_styles_widget_bottom . "'				},				modal_head: {					backgroundColor: '" . $botcreator_modal_head_backgroundcolor . "',				},				modal_body: {					backgroundColor: '" . $botcreator_modal_body_backgroundcolor . "',				},				btn_images: {},				buttons: {					fontFamily: '" . $botcreator_buttons_fontfamily . "',					backgroundColor: '" . $botcreator_buttons_backgroundcolor . "',					color: '" . $botcreator_buttons_color . "',					minHeight: '" . $botcreator_buttons_minheight . "'				},				message: {					backgroundColor: '" . $botcreator_message_backgroundcolor . "',

            boxShadow: '" . $botcreator_message_boxshadow . "'}}});

		</script>";

}



add_action('wp_footer', 'botcreator_output', 5);