import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VoteSetCreateComponent } from './vote-set-create.component';

describe('VoteSetCreateComponent', () => {
  let component: VoteSetCreateComponent;
  let fixture: ComponentFixture<VoteSetCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VoteSetCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VoteSetCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
