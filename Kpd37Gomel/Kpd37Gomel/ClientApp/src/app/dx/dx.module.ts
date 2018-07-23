import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {
  DxDataGridModule,
  DxTemplateModule,
  DxToastModule,
  DxPieChartModule,
  DxRadioGroupModule,
  DxListModule,
  DxLoadIndicatorModule,
  DxLoadPanelModule
  } from 'devextreme-angular';

@NgModule({
  imports: [
    CommonModule,
    DxDataGridModule,
    DxTemplateModule,
    DxToastModule,
    DxPieChartModule,
    DxRadioGroupModule,
    DxListModule,
    DxLoadIndicatorModule,
    DxLoadPanelModule
  ],
  exports: [
    CommonModule,
    DxDataGridModule,
    DxTemplateModule,
    DxToastModule,
    DxPieChartModule,
    DxRadioGroupModule,
    DxListModule,
    DxLoadIndicatorModule,
    DxLoadPanelModule
  ],
  declarations: []
})
export class DxModule { }
