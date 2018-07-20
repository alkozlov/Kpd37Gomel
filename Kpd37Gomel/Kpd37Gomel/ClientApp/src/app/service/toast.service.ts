import { Injectable, NgZone } from '@angular/core';
import notify from 'devextreme/ui/notify';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class ToastService {
  private toastMessageDisplayTime: number = 5000;

  constructor(private snackBar: MatSnackBar, private zone: NgZone) { }

  public showSuccessToast(message: string) {
    notify(message, 'success', this.toastMessageDisplayTime);
  }

  public showErrorToast(message: string) {
    notify(message, 'error', this.toastMessageDisplayTime);
  }

  public openSnackBar(errorText: string): void {
    this.zone.run(() => {
      const snackBar = this.snackBar.open(errorText, 'OK', {
        verticalPosition: 'bottom',
        horizontalPosition: 'center',
        duration: 10000
      });
      snackBar.onAction().subscribe(() => {
        snackBar.dismiss();
      });
    });
  }
}
