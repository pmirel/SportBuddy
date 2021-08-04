import { Injectable } from '@angular/core';
import { NgxSpinnerService } from "ngx-spinner";

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount = 0;
  constructor(private spinnerService: NgxSpinnerService) { }

  // ngOnInit() {
  //   /** spinner starts on init */
  //   this.spinnerService.show();

  //   setTimeout(() => {
  //     /** spinner ends after 5 seconds */
  //     this.spinnerService.hide();
  //   }, 5000);
  // }

  busy() {
    this.busyRequestCount++;
    this.spinnerService.show(undefined, {
      bdColor: "rgba(255,255,255,0)",
      size: "medium",
      color: "#333333",
      type: "ball-spin"
    })
  }

  idle() {
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      setTimeout(() => {
        this.spinnerService.hide();
      }, 0);
      // this.spinnerService.hide;
    }
  }
}
