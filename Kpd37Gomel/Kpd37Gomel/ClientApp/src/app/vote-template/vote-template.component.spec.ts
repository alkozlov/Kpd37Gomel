import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VoteTemplateComponent } from './vote-template.component';

describe('VoteTemplateComponent', () => {
  let component: VoteTemplateComponent;
  let fixture: ComponentFixture<VoteTemplateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VoteTemplateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VoteTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
