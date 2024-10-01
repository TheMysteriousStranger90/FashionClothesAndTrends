import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPhotoToClothingItemComponent } from './add-photo-to-clothing-item.component';

describe('AddPhotoToClothingItemComponent', () => {
  let component: AddPhotoToClothingItemComponent;
  let fixture: ComponentFixture<AddPhotoToClothingItemComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddPhotoToClothingItemComponent]
    });
    fixture = TestBed.createComponent(AddPhotoToClothingItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
