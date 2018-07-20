import { OverlayRef } from '@angular/cdk/overlay';

export class LoaderOverlayRef {
  constructor(private overlayRef: OverlayRef) { }

  public close(): void {
    this.overlayRef.dispose();
  }
}
