import { Component, OnInit, HostBinding, Input, HostListener, ElementRef, ChangeDetectorRef } from '@angular/core';
import { Router } from '@angular/router';

import { delay } from 'rxjs/operators';
import { subAppear } from 'projects/coda/src/app/animations/subApear';
import { IMenuItem } from 'projects/coda/src/app/ui/interfaces/menuItem';
import { BotService } from 'src/app/core/services/data/bot.service';
import { BotTranslateService } from 'src/app/core/services/translate/bot-translate.service';

@Component({
  selector: 'bot-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss'],
  animations: [subAppear]
})
export class MenuComponent implements OnInit {
  @HostBinding('class.main-menu') true;
  @HostBinding('class.horizontal') @Input() horizontal: boolean;

  menuItems: any;
  maxListSize: number;

  @HostListener('window:resize')
  public onResize() {
    this.menuItems.forEach((item: IMenuItem) => {
      item.active = false;
    });
  }

  constructor(
    private router: Router,
    private el: ElementRef,
    private ref: ChangeDetectorRef,
    private botService: BotService,
    private translate: BotTranslateService
  ) {
    this.maxListSize = 10;
    this.horizontal = false;
    this.onLanguageChance = this.onLanguageChance.bind(this);
    this.translate.onLangChange.subscribe(this.onLanguageChance)
  }

  ngOnInit() {
    this.onLanguageChance(this.translate.defaultLanguage);
  }

  onLanguageChance(lng: string) {
    this.botService.getMenuItems(lng).subscribe((menuItems) => {
      this.menuItems = menuItems
    });
  }

  toggle(event: Event, item: IMenuItem) {
    event.preventDefault();

    item.active = !item.active;
  }

  subState(active: boolean) {
    return active ? 'active' : 'inactive';
  }
}
