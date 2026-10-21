import { Component, OnInit, EventEmitter, HostListener, ViewChild, ElementRef } from '@angular/core';
import { AuthenticationService } from 'src/app/core/services/data/auth.service';
import { Router, NavigationStart } from '@angular/router';
import { SideBarComponent } from '../components/side-bar';

@Component({
  selector: 'app-main-layout',
  templateUrl: './main-layout.component.html',
  styleUrls: ['./main-layout.component.scss']
})
export class MainLayoutComponent implements OnInit {
  closeDropdown: EventEmitter<boolean>;
  boxed = null;
  topbarStyle = null;
  pageTitle = null;
  verticalNavStyle = null;
  scrollbarOptions = null;
  openedSidebar: boolean = false;

  @ViewChild(SideBarComponent, { static: true }) sideBar: SideBarComponent;

  constructor(private authService: AuthenticationService,
    private router: Router) {
    this.closeDropdown = new EventEmitter<boolean>();
  }


  @HostListener('window:resize')
  public onResize(event?) {
    if (window.innerWidth < 768) {
      this.closeSideBar();

    }
  }

  ngOnInit() {
    this.closeSideBar();

    this.router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        this.closeSideBar();
      }
    });
  }

  onCloseDropdown() {
    this.closeDropdown.emit(true);
  }

  logOut($event) {
    $event.preventDefault();
    this.onCloseDropdown();

    setTimeout(() => {
      this.authService.logout();
      this.router.navigate(['/extra/login']);
    });

  }

  onUserProfileSelect($event) {
    $event.preventDefault();
    this.onCloseDropdown();

    setTimeout(() => {
      this.router.navigate(['/vertical/user-profile']);
    });
  }

  onUserSettingSelect() {
    this.router.navigate(['/vertical/user-setting']);
  }

  toggleMenu(event: Event, state: boolean) {
    event.preventDefault();
    this.toggleSideBar();
  }

  get userFullName(): string {
    return this.authService.currentUserValue.firstName + ' ' + this.authService.currentUserValue.lastName;
  }

  private toggleSideBar() {
    if (this.openedSidebar) {
      this.closeSideBar();
    } else {
      this.openSideBar()
    }
  }

  private closeSideBar() {
    this.openedSidebar = false;
    this.sideBar.openedSidebar = false;
  }

  private openSideBar() {
    this.openedSidebar = true;
    this.sideBar.openedSidebar = true;
  }

}
