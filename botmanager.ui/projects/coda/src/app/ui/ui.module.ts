import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DragulaModule  } from 'ng2-dragula';

import { TCAutocompleteDirective } from './directives/autocomplete/autocomplete.directive';
import { TCBorderColorDirective } from './directives/border-color/border-color.directive';
import { TCFilterTableDirective } from './directives/filter-table/filter-table.directive';
import { TCBorderStyleDirective } from './directives/border-style/border-style.directive';
import { TCSortTableDirective } from './directives/sort-table/sort-table.directive';
import { TCBgColorDirective } from './directives/bg-color/bg-color.directive';
import { TCGradientDirective } from './directives/gradient/gradient.directive';
import { TCColorDirective } from './directives/color/color.directive';
import { TCShapeDirective } from './directives/shape/shape.directive';
import { TCFocusDirective } from './directives/focus/focus.directive';

import { TCListItemComponent } from './components/list/list-item';
import { TCPanelComponent } from './components/accordion/panel';
import { TCColumnComponent } from './components/table/column/column.component';
import { TCProgressComponent } from './components/progress';
import { TCAccordionComponent } from './components/accordion';
import { TCButtonComponent } from './components/button';
import { TCTabComponent } from './components/tabs/tab';
import { TCTableComponent } from './components/table';
import { TCListComponent } from './components/list';
import { TCCardComponent } from './components/card';
import { TCTabsComponent } from './components/tabs';
import { TCInputComponent } from './components/input';
import { TCFormDescriptionComponent } from './components/form-description';
import { TCFormGroupComponent } from './components/form-group';
import { TCFormLabelComponent } from './components/form-label';
import { TCPaginationComponent } from './components/pagination';
import { TCToDoListComponent } from './components/to-do-list';
import { TCCheckboxComponent } from './components/checkbox';
import { TCInboxComponent } from './components/inbox';
import { TCTextareaComponent } from './components/textarea';
import { TCUsersComponent } from './components/users';
import { TCContactComponent } from './components/contact';
import { TCTaskboardComponent } from './components/taskboard';
import { TCSwitcherComponent } from './components/switcher';
import { HightlightPipe } from './pipes/hightlight/hightlight.pipe';
import { TCAutocompleteComponent } from './components/autocomplete';
import { TСChatComponent } from './components/chat';
import { TCPricingComponent } from './components/pricing';
import { TCRadioComponent, TCRadioOptionComponent } from './components/radio';
import { TCBadgeComponent } from './components/badge';
import { TCDropdownComponent, TCDropdownButtonComponent, TCDropdownContentComponent } from './components/dropdown';
import { TCIconComponent } from './components/icon';
import { TCFontSizeDirective } from './directives/font-size/font-size.directive';
import { TCVTimelineComponent } from './components/v-timeline';
import { TCSelectComponent } from './components/select';
import { TCSliderComponent, TCSliderHandlerComponent } from './components/slider';
import { TCNotificationComponent, TCNotificationLayoutComponent } from './components/notification';
import { TCAlertComponent } from './components/alert';
import { TCRatingComponent } from './components/rating';
import { ColorPickerModule } from 'ngx-color-picker';

@NgModule({
  imports: [
    CommonModule,
		FormsModule,
    ReactiveFormsModule,    
    DragulaModule.forRoot(),
    ColorPickerModule
  ],
  declarations: [
		TCAutocompleteDirective,
    TCButtonComponent,
    TCBgColorDirective,
    TCBorderColorDirective,
    TCBorderStyleDirective,
		TCColorDirective,
		TCFocusDirective,
    TCGradientDirective,
		TCShapeDirective,
    TCFontSizeDirective,
		TCCardComponent,
		TCTabsComponent,
		TCTabComponent,
		TCAccordionComponent,
		TCPanelComponent,
		TCTableComponent,
		TCColumnComponent,
		TCSortTableDirective,
		TCFilterTableDirective,
		TCProgressComponent,
		TCListComponent,
		TCListItemComponent,
    TCInputComponent,
    TCTextareaComponent,
    TCFormDescriptionComponent,
    TCFormGroupComponent,
    TCFormLabelComponent,
    TCPaginationComponent,
    TCToDoListComponent,
    TCCheckboxComponent,
    TCInboxComponent,
    TCUsersComponent,
    TCContactComponent,
    TCTaskboardComponent,
    TCSwitcherComponent,
    HightlightPipe,
    TCAutocompleteComponent,
    TСChatComponent,
    TCPricingComponent,
    TCRadioComponent,
    TCRadioOptionComponent,
    TCBadgeComponent,
    TCDropdownComponent,
    TCDropdownButtonComponent,
    TCDropdownContentComponent,
    TCIconComponent,
    TCVTimelineComponent,
    TCSelectComponent,
    TCSliderComponent,
    TCSliderHandlerComponent,
    TCNotificationComponent,
    TCNotificationLayoutComponent,
    TCAlertComponent,
    TCRatingComponent,
  ],
  exports: [
		TCAutocompleteDirective,
    TCButtonComponent,
    TCBgColorDirective,
    TCBorderColorDirective,
    TCBorderStyleDirective,
    TCColorDirective,
    TCGradientDirective,
		TCShapeDirective,
    TCFontSizeDirective,
		TCCardComponent,
		TCTabsComponent,
		TCTabComponent,
		TCAccordionComponent,
		TCPanelComponent,
		TCTableComponent,
		TCSortTableDirective,
		TCProgressComponent,
		TCListComponent,
		TCListItemComponent,
		TCColumnComponent,
    TCInputComponent,
    TCTextareaComponent,
    TCFormDescriptionComponent,
    TCFormGroupComponent,
    TCFormLabelComponent,
    TCToDoListComponent,
    TCCheckboxComponent,
		TCInboxComponent,
		TCUsersComponent,
		TCContactComponent,
		TCTaskboardComponent,
		TCSwitcherComponent,
		HightlightPipe,
		TCAutocompleteComponent,
		TCFocusDirective,
		TСChatComponent,
		TCPricingComponent,
		TCPricingComponent,
    TCRadioComponent,
		TCRadioOptionComponent,
		TCBadgeComponent,
    TCDropdownComponent,
    TCDropdownButtonComponent,
    TCDropdownContentComponent,
    TCIconComponent,
		TCVTimelineComponent,
		TCSelectComponent,
		TCSliderComponent,
		TCNotificationComponent,
		TCNotificationLayoutComponent,
		TCAlertComponent,
		TCRatingComponent
  ]
})
export class UIModule { }
