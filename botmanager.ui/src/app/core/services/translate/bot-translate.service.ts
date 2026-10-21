import { Injectable, EventEmitter, ChangeDetectorRef } from '@angular/core';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';

@Injectable({ providedIn: 'root' })
export class BotTranslateService {

    public defaultLanguage: string = 'en';

    private _selectedLanguage: string;

    constructor(private translate: TranslateService) {
        this.translate.setDefaultLang(this.defaultLanguage);
        this.translate.onLangChange.subscribe((language: LangChangeEvent) => {
            this._selectedLanguage = language.lang;
            this.onLangChange.emit(language.lang);
        });
    }

    public get selectedLanguage(): string {
        return this._selectedLanguage
    }

    onLangChange: EventEmitter<string> = new EventEmitter<string>();

    useLanguage(language: string) {
        this.translate.use(language);
        
    }

    useDefaultLanguage() {
        this.translate.use(this.defaultLanguage);
    }
}