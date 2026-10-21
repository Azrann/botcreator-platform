import { Component, OnInit, Input } from '@angular/core';
import { AlertService } from 'src/app/core/services/notification/alert.service';
import { ClipboardService } from 'ngx-clipboard';

@Component({
    selector: 'app-bot-htmlcontent',
    templateUrl: './bot-htmlcontent.component.html',
    styleUrls: ['./bot-htmlcontent.component.scss']
})

export class BotHtmlcontentComponent implements OnInit {

    constructor(private alertService: AlertService,
        private clipboardService: ClipboardService) { }

    @Input() htmlContent: string;

    ngOnInit() {
    }

    onCopyButtonClick() {
        var copyText = this.htmlContent;
        this.clipboardService.copyFromContent(copyText);
        this.alertService.success("Text Copied!");
    }

}
