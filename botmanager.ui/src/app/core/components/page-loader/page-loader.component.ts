import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { PageLoaderService } from 'src/app/core/services/notification/page-loader.service';

@Component({
  selector: 'app-page-loader',
  templateUrl: './page-loader.component.html',
  styleUrls: ['./page-loader.component.scss']
})
export class PageLoaderComponent implements OnInit {

  constructor(public pageLoader: PageLoaderService) {
    this.pageLoader.loadingStatus$.subscribe((status) => {
      if (status) {
        window.scrollTo(0, 0);
      }
    })
  }

  ngOnInit() {
  }

}
