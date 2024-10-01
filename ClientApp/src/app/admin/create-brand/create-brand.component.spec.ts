import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBrandComponent } from './create-brand.component';

describe('CreateBrandComponent', () => {
  let component: CreateBrandComponent;
  let fixture: ComponentFixture<CreateBrandComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateBrandComponent]
    });
    fixture = TestBed.createComponent(CreateBrandComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
