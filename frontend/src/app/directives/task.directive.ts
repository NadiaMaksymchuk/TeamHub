import {Directive, ElementRef, OnInit } from '@angular/core';

@Directive({
  selector:'[taskDirective]'
})
export class TaskDirective{
  constructor(private elementRef :ElementRef){}

  public state: string;
  background: any;

  ngOnInit() {

    if(this.elementRef.nativeElement.classList.value == "InProgress") {
      this.elementRef.nativeElement.style.background = "yellow";
    }
    
    if(this.elementRef.nativeElement.classList.value == "ToDo") {
      this.elementRef.nativeElement.style.background = "red";
    }

    if(this.elementRef.nativeElement.classList.value == "Done") {
      this.elementRef.nativeElement.style.background = "green";
    }

    if(this.elementRef.nativeElement.classList.value == "Cancelled" || this.elementRef.nativeElement.classList.value == "4" || this.elementRef.nativeElement.classList.value == "Canceled") {
      this.elementRef.nativeElement.classList.value = "Cancelled";
      this.elementRef.nativeElement.style.backgroundColor = "pink";
    }

    console.log(this.elementRef.nativeElement.classList.value)
  }

}