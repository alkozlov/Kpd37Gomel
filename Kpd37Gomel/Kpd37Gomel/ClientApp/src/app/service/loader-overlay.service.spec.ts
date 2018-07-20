import { TestBed, inject } from '@angular/core/testing';

import { LoaderOverlayService } from './loader-overlay.service';

describe('LoaderOverlayService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LoaderOverlayService]
    });
  });

  it('should be created', inject([LoaderOverlayService], (service: LoaderOverlayService) => {
    expect(service).toBeTruthy();
  }));
});
