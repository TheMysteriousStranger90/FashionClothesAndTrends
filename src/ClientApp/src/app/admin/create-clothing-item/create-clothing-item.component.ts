import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormGroupDirective } from '@angular/forms';
import { ClothingItem } from 'src/app/shared/models/clothing-item';
import { CreateClothingItem } from 'src/app/shared/models/create-clothing-item';
import { Brand } from 'src/app/shared/models/brand';
import { ShopService } from 'src/app/shop/shop.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: false,
  selector: 'app-create-clothing-item',
  templateUrl: './create-clothing-item.component.html',
  styleUrls: ['./create-clothing-item.component.sass']
})
export class CreateClothingItemComponent implements OnInit {
  @ViewChild(FormGroupDirective) formDirective!: FormGroupDirective;
  clothingItemForm: FormGroup;
  brands: Brand[] = [];
  clothingItems: ClothingItem[] = [];
  genders = ['Male', 'Female', 'Kids'];
  sizes = ['XS', 'S', 'M', 'L', 'XL', 'XXL'];
  categories = ['Top', 'Bottom', 'Outerwear', 'Accessories', 'Shoes', 'Bags', 'Jewelry'];

  constructor(private fb: FormBuilder, private shopService: ShopService, private cdr: ChangeDetectorRef) {
    this.clothingItemForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required],
      price: ['', Validators.required],
      gender: ['', Validators.required],
      size: ['', Validators.required],
      category: ['', Validators.required],
      brand: ['', Validators.required],
      isInStock: [true, Validators.required],
      pictureUrl: [null]
    });
  }

  ngOnInit(): void {
    this.loadBrands();
    this.loadClothingItems();
  }

  loadBrands(): void {
    this.shopService.getBrands().subscribe({
      next: (brands) => {
        this.brands = brands;
        this.cdr.markForCheck();
      },
      error: (error) => console.error('Error loading brands', error)
    });
  }

  loadClothingItems(): void {
    this.shopService.getAllClothingItems().subscribe({
      next: (items) => {
        this.clothingItems = items;
        this.cdr.markForCheck();
      },
      error: (error) => console.error('Error loading clothing items', error)
    });
  }

  createClothingItem(): void {
    if (this.clothingItemForm.valid) {
      const newClothingItem: CreateClothingItem = this.clothingItemForm.value;
      this.shopService.addClothingItem(newClothingItem).subscribe({
        next: () => {
          this.formDirective.resetForm();
          this.loadClothingItems();
        },
        error: (err) => console.error(err)
      });
    }
  }

  deleteClothingItem(id: string): void {
    this.shopService.removeClothingItem(id).subscribe({
      next: () => {
        this.loadClothingItems();
        this.cdr.markForCheck();
      },
      error: (err) => console.error(err)
    });
  }
}
