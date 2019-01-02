import { Directive, ElementRef, AfterViewInit } from '@angular/core';
import * as $ from 'jquery';
import 'metismenu';

@Directive({
  selector: '[appMetisMenu]'
})
export class MetisMenuDirective implements AfterViewInit {

  constructor(private element: ElementRef) { }

  ngAfterViewInit(): void {
    $(this.element.nativeElement).metisMenu({
      //activeClass: 'open'
      //toggle: false // disable the auto collapse. Default: true.
    });
  }
}
