<?php 
if($_POST['botcreator_hidden'] == 'Y') {
	$botcreator_bot_name = $_POST['botcreator_bot_name'];
	update_option('botcreator_bot_name', $botcreator_bot_name);

	$botcreator_bot_name = $_POST['botcreator_bot_name'];
	update_option('botcreator_bot_name', $botcreator_bot_name);

	$botcreator_bot_url = $_POST['botcreator_bot_url'];
	update_option('botcreator_bot_url', $botcreator_bot_url);

	$botcreator_bot_avatar_url = $_POST['botcreator_bot_avatar_url'];
	update_option('botcreator_bot_avatar_url', $botcreator_bot_avatar_url);

	$botcreator_bot_id = $_POST['botcreator_bot_id'];
	update_option('botcreator_bot_id', $botcreator_bot_id);

	$botcreator_user_avatar_url = $_POST['botcreator_user_avatar_url'];
	update_option('botcreator_user_avatar_url', $botcreator_user_avatar_url);

	$botcreator_head_title = $_POST['botcreator_head_title'];
	update_option('botcreator_head_title', $botcreator_head_title);

	$botcreator_first_message_text = $_POST['botcreator_first_message_text'];
	update_option('botcreator_first_message_text', $botcreator_first_message_text);

	$botcreator_description = $_POST['botcreator_description'];
	update_option('botcreator_description', $botcreator_description);

	$botcreator_buttons_chart_now_btn = $_POST['botcreator_buttons_chart_now_btn'];
	update_option('botcreator_buttons_chart_now_btn', $botcreator_buttons_chart_now_btn);

	$botcreator_buttons_register_btn = $_POST['botcreator_buttons_register_btn'];
	update_option('botcreator_buttons_register_btn', $botcreator_buttons_register_btn);

	$botcreator_form_name_title = $_POST['botcreator_form_name_title'];
	update_option('botcreator_form_name_title', $botcreator_form_name_title);

	$botcreator_form_name_placeholder = $_POST['botcreator_form_name_placeholder'];
	update_option('botcreator_form_name_placeholder', $botcreator_form_name_placeholder);

	$botcreator_form_email_title = $_POST['botcreator_form_email_title'];
	update_option('botcreator_form_email_title', $botcreator_form_email_title);

	$botcreator_form_email_placeholder = $_POST['botcreator_form_email_placeholder'];
	update_option('botcreator_form_email_placeholder', $botcreator_form_email_placeholder);

	$botcreator_styles_widget_fontfamily = $_POST['botcreator_styles_widget_fontfamily'];
	update_option('botcreator_styles_widget_fontfamily', $botcreator_styles_widget_fontfamily);

	$botcreator_styles_widget_width = $_POST['botcreator_styles_widget_width'];
	update_option('botcreator_styles_widget_width', $botcreator_styles_widget_width);

	$botcreator_styles_widget_right = $_POST['botcreator_styles_widget_right'];
	update_option('botcreator_styles_widget_right', $botcreator_styles_widget_right);

	$botcreator_styles_widget_bottom = $_POST['botcreator_styles_widget_bottom'];
	update_option('botcreator_styles_widget_bottom', $botcreator_styles_widget_bottom);

	$botcreator_modal_head_backgroundcolor = $_POST['botcreator_modal_head_backgroundcolor'];
	update_option('botcreator_modal_head_backgroundcolor', $botcreator_modal_head_backgroundcolor);

	$botcreator_modal_body_backgroundcolor = $_POST['botcreator_modal_body_backgroundcolor'];
	update_option('botcreator_modal_body_backgroundcolor', $botcreator_modal_body_backgroundcolor);

	$botcreator_buttons_fontfamily = $_POST['botcreator_buttons_fontfamily'];
	update_option('botcreator_buttons_fontfamily', $botcreator_buttons_fontfamily);

	$botcreator_buttons_backgroundcolor = $_POST['botcreator_buttons_backgroundcolor'];
	update_option('botcreator_buttons_backgroundcolor', $botcreator_buttons_backgroundcolor);

	$botcreator_buttons_color = $_POST['botcreator_buttons_color'];
	update_option('botcreator_buttons_color', $botcreator_buttons_color);

	$botcreator_buttons_minheight = $_POST['botcreator_buttons_minheight'];
	update_option('botcreator_buttons_minheight', $botcreator_buttons_minheight);

	$botcreator_message_backgroundcolor = $_POST['botcreator_message_backgroundcolor'];
	update_option('botcreator_message_backgroundcolor', $botcreator_message_backgroundcolor);

	$botcreator_message_boxshadow = $_POST['botcreator_message_boxshadow'];
	update_option('botcreator_message_boxshadow', $botcreator_message_boxshadow);
?>
	<div class="updated"><p><strong><?php _e('Options Saved.' ); ?></strong></p></div>
<?php

} else {
	$botcreator_bot_name = get_option('botcreator_bot_name');
	$botcreator_bot_url = get_option('botcreator_bot_url');
	$botcreator_bot_avatar_url = get_option('botcreator_bot_avatar_url');
	$botcreator_bot_id = get_option('botcreator_bot_id');
	$botcreator_user_avatar_url = get_option('botcreator_user_avatar_url');
	$botcreator_head_title = get_option('botcreator_head_title');
	$botcreator_first_message_text = get_option('botcreator_first_message_text');
	$botcreator_description = get_option('botcreator_description');
	$botcreator_buttons_chart_now_btn = get_option('botcreator_buttons_chart_now_btn');
	$botcreator_buttons_register_btn = get_option('botcreator_buttons_register_btn');
	$botcreator_form_name_title = get_option('botcreator_form_name_title');
	$botcreator_form_name_placeholder = get_option('botcreator_form_name_placeholder');
	$botcreator_form_email_title = get_option('botcreator_form_email_title');
	$botcreator_form_email_placeholder = get_option('botcreator_form_email_placeholder');
	$botcreator_styles_widget_fontfamily = get_option('botcreator_styles_widget_fontfamily');
	$botcreator_styles_widget_width = get_option('botcreator_styles_widget_width');
	$botcreator_styles_widget_right = get_option('botcreator_styles_widget_right');
	$botcreator_styles_widget_bottom = get_option('botcreator_styles_widget_bottom');
	$botcreator_modal_head_backgroundcolor = get_option('botcreator_modal_head_backgroundcolor');
	$botcreator_modal_body_backgroundcolor = get_option('botcreator_modal_body_backgroundcolor');
	$botcreator_buttons_fontfamily = get_option('botcreator_buttons_fontfamily');
	$botcreator_buttons_backgroundcolor = get_option('botcreator_buttons_backgroundcolor');
	$botcreator_buttons_color = get_option('botcreator_buttons_color');
	$botcreator_buttons_minheight = get_option('botcreator_buttons_minheight');
	$botcreator_message_backgroundcolor = get_option('botcreator_message_backgroundcolor');
	$botcreator_message_boxshadow = get_option('botcreator_message_boxshadow');
}
?>

<div class="wrap">
    <?php    echo "<h2>" . __( 'BotCreator Options' ) . "</h2>"; ?>
     
    <form name="form" method="post" action="<?php echo str_replace( '%7E', '~', $_SERVER['REQUEST_URI']); ?>">
        <input type="hidden" name="botcreator_hidden" value="Y">
        <p><?php _e("Bot Name: "); ?><input type="text" name="botcreator_bot_name" value="<?php echo $botcreator_bot_name; ?>" placeholder="Bot Name"></p>
        <p><?php _e("Bot URL: "); ?><input type="text" name="botcreator_bot_url" value="<?php echo $botcreator_bot_url; ?>" placeholder="Bot URL"></p>
        <p><?php _e("Bot Avatar URL: "); ?><input type="text" name="botcreator_bot_avatar_url" value="<?php echo $botcreator_bot_avatar_url; ?>" placeholder="Bot Avatar URL"></p>
        <p><?php _e("Bot ID: "); ?><input type="text" name="botcreator_bot_id" value="<?php echo $botcreator_bot_id; ?>" placeholder="Bot ID"></p>
        <p><?php _e("User Avatar URL: "); ?><input type="text" name="botcreator_user_avatar_url" value="<?php echo $botcreator_user_avatar_url; ?>" placeholder="User Avatar URL"></p>
		<p><?php _e("Head Title: "); ?><input type="text" name="botcreator_head_title" value="<?php echo $botcreator_head_title; ?>" placeholder="Chat with us!"></p>
        <p><?php _e("First Message Text: "); ?><input type="text" name="botcreator_first_message_text" value="<?php echo $botcreator_first_message_text; ?>" placeholder="Let me help you :)"></p>
        <p><?php _e("Description: "); ?><input type="text" name="botcreator_description" value="<?php echo $botcreator_description; ?>" placeholder="I am the Bot agent, I can assist with frequently asked questions"></p>
        <p><?php _e("Buttons - Chart Now: "); ?><input type="text" name="botcreator_buttons_chart_now_btn" value="<?php echo $botcreator_buttons_chart_now_btn; ?>" placeholder="Chat now"></p>
        <p><?php _e("Buttons - Register: "); ?><input type="text" name="botcreator_buttons_register_btn" value="<?php echo $botcreator_buttons_register_btn; ?>" placeholder="Start"></p>
        <p><?php _e("Form - Name -> Title: "); ?><input type="text" name="botcreator_form_name_title" value="<?php echo $botcreator_form_name_title; ?>" placeholder="Your name:"></p>
		<p><?php _e("Form - Name -> Placeholder: "); ?><input type="text" name="botcreator_form_name_placeholder" value="<?php echo $botcreator_form_name_placeholder; ?>" placeholder="Type your name..."></p>
		<p><?php _e("Form - Email -> Title: "); ?><input type="text" name="botcreator_form_email_title" value="<?php echo $botcreator_form_email_title; ?>" placeholder="E-mail:"></p>
		<p><?php _e("Form - Email -> Placeholder: "); ?><input type="text" name="botcreator_form_email_placeholder" value="<?php echo $botcreator_form_email_placeholder; ?>" placeholder="Type your email..."></p>
        <p><?php _e("Styles - Widget -> Font Family: "); ?><input type="text" name="botcreator_styles_widget_fontfamily" value="<?php echo $botcreator_styles_widget_fontfamily; ?>" placeholder="Open Sans"></p>
		<p><?php _e("Styles - Widget -> Width: "); ?><input type="text" name="botcreator_styles_widget_width" value="<?php echo $botcreator_styles_widget_width; ?>" placeholder="350px"></p>
		<p><?php _e("Styles - Widget -> Right: "); ?><input type="text" name="botcreator_styles_widget_right" value="<?php echo $botcreator_styles_widget_right; ?>" placeholder="1em"></p>
		<p><?php _e("Styles - Widget -> Bottom: "); ?><input type="text" name="botcreator_styles_widget_bottom" value="<?php echo $botcreator_styles_widget_bottom; ?>" placeholder="1em"></p>
		<p><?php _e("Modal - Head Background Color: "); ?><input type="text" name="botcreator_modal_head_backgroundcolor" value="<?php echo $botcreator_modal_head_backgroundcolor; ?>" placeholder="#ffffff"></p>
		<p><?php _e("Modal - Body Background Color: "); ?><input type="text" name="botcreator_modal_body_backgroundcolor" value="<?php echo $botcreator_modal_body_backgroundcolor; ?>" placeholder="#E9EEF4"></p>
		<p><?php _e("Buttons - Font Family: "); ?><input type="text" name="botcreator_buttons_fontfamily" value="<?php echo $botcreator_buttons_fontfamily; ?>" placeholder="Open Sans"></p>
		<p><?php _e("Buttons - Background Color: "); ?><input type="text" name="botcreator_buttons_backgroundcolor" value="<?php echo $botcreator_buttons_backgroundcolor; ?>" placeholder="#2ecc71"></p>
		<p><?php _e("Buttons - Color: "); ?><input type="text" name="botcreator_buttons_color" value="<?php echo $botcreator_buttons_color; ?>" placeholder="#ffffff"></p>
		<p><?php _e("Buttons - Min Height: "); ?><input type="text" name="botcreator_buttons_minheight" value="<?php echo $botcreator_buttons_minheight; ?>" placeholder="45px"></p>
		<p><?php _e("Message - Background Color: "); ?><input type="text" name="botcreator_message_backgroundcolor" value="<?php echo $botcreator_message_backgroundcolor; ?>" placeholder="#ffffff"></p>
		<p><?php _e("Message - Box Shadow: "); ?><input type="text" name="botcreator_message_boxshadow" value="<?php echo $botcreator_message_boxshadow; ?>" placeholder="0 1px 15px rgba(0,0,0,0.5)"></p>
     
        <p class="submit">
        <input type="submit" name="Submit" value="<?php _e('Update Options' ) ?>" />
        </p>
    </form>
</div>