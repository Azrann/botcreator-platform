import { Injectable } from '@angular/core';
import { ServiceBotComponent } from './service-bot.component';
import { CanDeactivate } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class CanDeactivateServiceBot implements CanDeactivate<ServiceBotComponent> {
  canDeactivate(component: ServiceBotComponent): boolean {
   
    if(component.hasUnsavedData()){
        if (confirm("You have unsaved changes! If you leave, your changes will be lost.")) {
            return true;
        } else {
            return false;
        }
    }
    return true;
  }
}