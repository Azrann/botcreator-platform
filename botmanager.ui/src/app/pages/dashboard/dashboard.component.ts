import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  chartData1: Array<any>;
  chartData2: Array<any>;
  chartData3: Array<any>;
  barChartOptions: any;
  barChartLabels: Array<string>;
  barChartType: string;
  barChartLegend: boolean;
  barChartColors: any[];
  lineChartOptions: any;
  lineChartLegend: boolean;
  lineChartType: string;
  lineChartLabels: any[];
  lineChartColors: any[];

  constructor() {
    this.barChartOptions = {
      scaleShowVerticalLines: false,
      responsive: true,
      scales: {
        yAxes: [{
          color: '#fff',
          ticks: {
            beginAtZero: true,
            fontColor: '#fff'
          },
        }],
        xAxes: [{
          color: '#fff',
          ticks: {
            fontColor: '#fff'
          },
        }]
      }
    };
    this.barChartLabels = ['2012', '2013', '2014', '2015', '2016', '2017', '2018'];
    this.barChartType = 'bar';
    this.barChartLegend = false;
    this.barChartColors = [
      {
        backgroundColor: '#64b5f6'
      },
      {
        backgroundColor: '#FF8534'
      }
    ];
    this.lineChartLabels = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
    this.lineChartOptions = {
      responsive: true,
      scales: {
        yAxes: [{
          color: '#fff',
          ticks: {
            beginAtZero: true,
            fontColor: '#fff'
          },
        }],
        xAxes: [{
          color: '#fff',
          ticks: {
            fontColor: '#fff'
          },
        }]
      }
    };
    this.lineChartLegend = false;
    this.lineChartType = 'line';
    this.lineChartColors = [
      {
        backgroundColor: 'transparent',
        borderColor: '#e2829c',
        pointBackgroundColor: '#fff',
        pointBorderColor: '#e2829c',
        pointHoverBackgroundColor: '#fff',
        pointHoverBorderColor: '#e2829c'
      },
      {
        backgroundColor: 'transparent',
        borderColor: '#FF8534',
        pointBackgroundColor: '#fff',
        pointBorderColor: '#FF8534',
        pointHoverBackgroundColor: '#fff',
        pointHoverBorderColor: '#FF8534'

      },
      {
        backgroundColor: 'transparent',
        borderColor: '#64b5f6',
        pointBackgroundColor: '#fff',
        pointBorderColor: '#64b5f6',
        pointHoverBackgroundColor: '#fff',
        pointHoverBorderColor: '#64b5f6'
      }
    ];
  }

  ngOnInit() {
    this.chartData1 = [
      { data: [65, 59, 80, 81, 56, 55, 40], label: 'Direct' },
      { data: [28, 48, 40, 19, 86, 27, 90], label: 'Referral' }
    ];
    this.chartData2 = [
      { data: [92, 83, 80, 81, 56, 55, 30], label: 'Site A' },
      { data: [28, 48, 50, 62, 65, 70, 90], label: 'Site B' },
      { data: [18, 48, 77, 9, 100, 27, 40], label: 'Site C' }
    ];
  }

}
