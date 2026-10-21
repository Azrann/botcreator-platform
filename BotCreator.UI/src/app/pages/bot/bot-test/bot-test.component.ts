import { Component, OnInit, OnDestroy, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { BotTestData } from './bot-test-data';

@Component({
  selector: 'app-bot-test',
  templateUrl: './bot-test.component.html',
  styleUrls: ['./bot-test.component.scss']
})
export class BotTestComponent implements OnInit, OnDestroy {

  constructor(private route: ActivatedRoute) { }

  private appendedNodes: HTMLElement[];

  public botName: string;
  public widgetHtml: string;

  @ViewChild('container', { static: true })
  containerElem: ElementRef

  ngOnInit() {
    this.route.data.subscribe(data => {
      let botTestData: BotTestData = data['resolvedData'];
      this.botName = botTestData.botName;
      this.initWidget(botTestData.widgetHtml);
      this.widgetHtml = botTestData.widgetHtml;
    });
  }

  ngOnDestroy() {
    let widgetElem = document.getElementById("helpWidget");
    if (widgetElem) {
      widgetElem.remove();
    }
  }

  initWidget(widgetHtml: string) {
    if (!widgetHtml) {
      console.error("widget html is empty");
      return;
    }
    var nodes = this.extractNodes(widgetHtml);
    this.appendedNodes = nodes;
    nodes.forEach(node => {
      console.log(JSON.stringify(node));
      this.containerElem.nativeElement.appendChild(node);
    });

  }

  extractNodes(widgetHtml: string): HTMLElement[] {
    var result: HTMLElement[] = [];
    var wrapper = document.createElement('div');
    wrapper.innerHTML = widgetHtml;

    wrapper.childNodes.forEach(node => {
      if (node.nodeName === "LINK") {
        result.push(this.constructLinkNode(node as HTMLLinkElement));
      } else if (node.nodeName === "SCRIPT") {
        result.push(this.constructScriptNode(node as HTMLScriptElement));
      }
    });

    return result;
  }

  constructLinkNode(node: HTMLLinkElement) {
    var link: HTMLLinkElement = document.createElement("link");
    link.href = node.href;

    return link;
  }

  constructScriptNode(node: HTMLScriptElement) {
    var script: HTMLScriptElement = document.createElement("script");
    if (node.src) {
      script.src = node.src;
    }

    if (node.text) {
      script.text = node.text;
    }

    return script;
  }

}
