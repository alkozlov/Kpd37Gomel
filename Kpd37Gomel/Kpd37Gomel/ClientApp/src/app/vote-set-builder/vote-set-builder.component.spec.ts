import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VoteSetBuilderComponent } from './vote-set-builder.component';

describe('VoteSetBuilderComponent', () => {
  let component: VoteSetBuilderComponent;
  let fixture: ComponentFixture<VoteSetBuilderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VoteSetBuilderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VoteSetBuilderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
