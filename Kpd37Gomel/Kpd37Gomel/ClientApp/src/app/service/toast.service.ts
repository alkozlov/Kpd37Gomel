import { Injectable } from '@angular/core';
import notify from 'devextreme/ui/notify';

@Injectable()
export class ToastService {
  private toastMessageDisplayTime: number = 5000;

  constructor() { }

  public showSuccessToast(message: string) {
    notify(message, 'success', this.toastMessageDisplayTime);
  }

  public showErrorToast(message: string) {
    notify(message, 'error', this.toastMessageDisplayTime);
  }
}
