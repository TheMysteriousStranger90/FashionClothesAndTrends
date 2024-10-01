import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateClothingItemComponent } from './create-clothing-item.component';

describe('CreateClothingItemComponent', () => {
  let component: CreateClothingItemComponent;
  let fixture: ComponentFixture<CreateClothingItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateClothingItemComponent]
    });
    fixture = TestBed.createComponent(CreateClothingItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
