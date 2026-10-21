import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { BotTranslateService } from '../../services/translate/bot-translate.service';

@Component({
  selector: 'app-language-selector',
  templateUrl: './language-selector.component.html',
  styleUrls: ['./language-selector.component.scss']
})
export class LanguageSelectorComponent implements OnInit {

  closeDropdown: EventEmitter<boolean>

  constructor(private translate: BotTranslateService) {
    this.closeDropdown = new EventEmitter<boolean>();
  }

  ngOnInit() {
  }

  onLanguageChange(language: string) {
    this.closeDropdown.emit(true);
    this.translate.useLanguage(language);
  }
}
