import { Injectable } from '@angular/core';
import notify from 'devextreme/ui/notify';

@Injectable()
export class ToastService {

  constructor() { }

  public showSuccessToast(message: string) {
    notify(message, 'success', 2000);
  }

  public showErrorToast(message: string) {
    notify(message, 'error', 2000);
  }
}
