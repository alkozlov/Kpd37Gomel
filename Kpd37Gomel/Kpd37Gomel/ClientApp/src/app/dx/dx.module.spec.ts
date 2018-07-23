import { DxModule } from './dx.module';

describe('DxModule', () => {
  let dxModule: DxModule;

  beforeEach(() => {
    dxModule = new DxModule();
  });

  it('should create an instance', () => {
    expect(dxModule).toBeTruthy();
  });
});
