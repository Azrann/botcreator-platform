import { Component, OnInit } from '@angular/core';
import { IOption } from 'projects/coda/src/app/ui/interfaces/option';
import { Router, ActivatedRoute } from '@angular/router';
import { CompleteServiceBot } from 'src/app/core/models/dto/bot/complete-service-bot';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { PageLoaderService } from 'src/app/core/services/notification/page-loader.service';
import { BotService } from 'src/app/core/services/data/bot.service';
import { delay, first } from 'rxjs/operators';
import { AlertService } from 'src/app/core/services/notification/alert.service';

@Component({
  selector: 'app-service-bot',
  templateUrl: './service-bot.component.html',
  styleUrls: ['./service-bot.component.scss']
})
export class ServiceBotComponent implements OnInit {

  allLanguages: IOption[] = [
    { label: "English", value: "en" },
    { label: "Spanish", value: "es" },
  ];

  serviceBot: CompleteServiceBot;
  customerServiceBotForm: FormGroup;
  isEditMode: boolean;
  isSubmitted: boolean = false;


  constructor(private route: ActivatedRoute,
    private router: Router, private formBuilder: FormBuilder,
    private pageLoaderService: PageLoaderService,
    private botService: BotService,
    private alertService: AlertService) { }

  ngOnInit() {
    let fv = this.formDefaultValue;
    this.customerServiceBotForm = this.formBuilder.group({
      name: [fv.name, Validators.required],
      buttonText1: [fv.buttonText1, Validators.required],
      resultText1: [fv.resultText1, Validators.required],
      buttonText2: [fv.buttonText2, Validators.required],
      resultText2: [fv.resultText2, Validators.required],
      buttonText3: fv.buttonText3,
      resultText3: fv.resultText3,
      whatsappChecked: fv.whatsappChecked,
      whatsappNumber: fv.whatsappNumber,
      chatOnWeb: fv.chatOnWeb,
      fbChecked: fv.fbChecked,
      fbUserName: fv.fbUserName,
      talkToMeChecked: fv.talkToMeChecked,
      talkToAgent: fv.talkToAgent,
      callMeChecked: fv.callMeChecked,
      callMeEmail: [fv.callMeEmail, Validators.email],
      language: [fv.language],
      htmlContent: fv.htmlContent,
      bubbleMessage: fv.bubbleMessage,
      chatNow: fv.chatNow,
      description: fv.description,
      primaryColor: fv.primaryColor,
      secondaryColor: fv.secondaryColor,
      active: fv.active
    });


    this.route.data.subscribe(data => {
      const resolvedData: CompleteServiceBot = data['resolvedData'];
      //this.errorMessage = resolvedData.error;
      this.onServiceBotRetrieved(resolvedData);
    });
  }
  get formCtrls() { return this.customerServiceBotForm.controls; }

  onServiceBotRetrieved(serviceBot: CompleteServiceBot) {

    if (!serviceBot) {
      //this.customerServiceBotForm.reset();
      this.isEditMode = false;
      return;
    }
    this.isEditMode = true;
    this.serviceBot = serviceBot;
    let bot = this.serviceBot.bot;
    let settings = this.serviceBot.settings;
    let html = this.serviceBot.widgetHtml;

    this.customerServiceBotForm.setValue({
      name: bot.name,
      buttonText1: bot.buttonText1,
      resultText1: bot.resultText1,
      buttonText2: bot.buttonText2,
      resultText2: bot.resultText2,
      buttonText3: bot.buttonText3,
      resultText3: bot.resultText3,
      whatsappChecked: bot.whatsappChecked,
      whatsappNumber: bot.whatsappNumber,
      chatOnWeb: bot.chatOnWeb,
      fbChecked: bot.fbChecked,
      fbUserName: bot.fbUserName,
      talkToMeChecked: bot.talkToMeChecked || false,
      talkToAgent: bot.talkToAgent,
      callMeChecked: bot.callMeChecked,
      callMeEmail: bot.callMeEmail,
      language: settings.language,
      htmlContent: html,
      bubbleMessage: settings.bubbleMessage,
      chatNow: settings.chatNow,
      description: settings.description,
      primaryColor: settings.primaryColor,
      secondaryColor: settings.secondaryColor,
      active: settings.active
    });
  }

  get formDefaultValue(): any {
    return {
      name: '',
      buttonText1: '',
      resultText1: '',
      buttonText2: '',
      resultText2: '',
      buttonText3: '',
      resultText3: '',
      whatsappChecked: false,
      whatsappNumber: '',
      chatOnWeb: false,
      fbChecked: false,
      fbUserName: '',
      talkToMeChecked: false,
      talkToAgent: '',
      callMeChecked: false,
      callMeEmail: '',
      language: 'en',
      htmlContent: '',
      bubbleMessage: '',
      chatNow: '',
      description: '',
      primaryColor: '',
      secondaryColor: '',
      active: true
    }
  }

  setProgress(page: number): number {
    if (!page || page < 0) {
      return 0;
    } else {
      return (page / 4) * 100;
    }
  }

  onSubmit() {
    if (this.customerServiceBotForm.invalid) {
      return;
    }
    this.pageLoaderService.startLoading();

    let sbToSave: CompleteServiceBot = {
      bot: {
        id: this.serviceBot && this.serviceBot.bot.id,
        buttonText1: this.formCtrls.buttonText1.value,
        buttonText2: this.formCtrls.buttonText2.value,
        buttonText3: this.formCtrls.buttonText3.value,
        callMeChecked: this.formCtrls.callMeChecked.value,
        callMeEmail: this.formCtrls.callMeEmail.value,
        chatOnWeb: this.formCtrls.chatOnWeb.value,
        fbChecked: this.formCtrls.fbChecked.value,
        fbUserName: this.formCtrls.fbUserName.value,
        name: this.formCtrls.name.value,
        whatsappChecked: this.formCtrls.whatsappChecked.value,
        whatsappNumber: this.formCtrls.whatsappNumber.value,
        talkToMeChecked: this.formCtrls.talkToMeChecked.value,
        talkToAgent: this.formCtrls.talkToAgent.value,
        resultText1: this.formCtrls.resultText1.value,
        resultText2: this.formCtrls.resultText2.value,
        resultText3: this.formCtrls.resultText3.value
      },
      settings: {
        active: this.formCtrls.active.value,
        bubbleMessage: this.formCtrls.bubbleMessage.value,
        chatNow: this.formCtrls.chatNow.value,
        description: this.formCtrls.description.value,
        language: this.formCtrls.language.value,
        primaryColor: this.formCtrls.primaryColor.value,
        secondaryColor: this.formCtrls.secondaryColor.value,
        botId: this.serviceBot && this.serviceBot.bot.id
      }
    };

    this.botService.saveCompleteServiceBot(sbToSave)
      .pipe(first())
      .subscribe(
        data => {
          this.isSubmitted =true;
          this.pageLoaderService.stopLoading();
          this.alertService.success('Bot saved successful', true);
          this.router.navigate(['/vertical/bot/list']);
        },
        error => {
          var errMsg = error.error.error.errorMessage;
          this.alertService.error(errMsg);
          this.pageLoaderService.stopLoading();
        });
  }

  hasUnsavedData() {
    if(this.isSubmitted) {
      return false;
    }

    return !this.customerServiceBotForm.pristine;
  }

}
