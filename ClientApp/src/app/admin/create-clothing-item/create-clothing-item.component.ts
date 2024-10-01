import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FileUploader } from 'ng2-file-upload';
import { Brand } from 'src/app/shared/models/brand';
import { CreateClothingItem } from 'src/app/shared/models/create-clothing-item';
import { ShopService } from 'src/app/shop/shop.service';
import { environment } from 'src/environments/environment';
import { FileUploadModule } from 'ng2-file-upload';
import { ClothingItem } from 'src/app/shared/models/clothing-item';
@Component({
  selector: 'app-create-clothing-item',
  templateUrl: './create-clothing-item.component.html',
  styleUrls: ['./create-clothing-item.component.sass']
})
export class CreateClothingItemComponent implements OnInit {
  clothingItemForm: FormGroup;
  brands: Brand[] = [];
  clothingItems: ClothingItem[] = [];
  genders = ['Male', 'Female', 'Kids'];
  sizes = ['XS', 'S', 'M', 'L', 'XL', 'XXL'];
  categories = ['Top', 'Bottom', 'Outerwear', 'Accessories', 'Shoes', 'Bags', 'Jewelry'];

  constructor(private fb: FormBuilder, private shopService: ShopService) {
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
      next: (brands) => this.brands = brands,
      error: (error) => console.error('Error loading brands', error)
    });
  }

  loadClothingItems(): void {
    this.shopService.getAllClothingItems().subscribe({
      next: (items) => this.clothingItems = items,
      error: (error) => console.error('Error loading clothing items', error)
    });
  }

  createClothingItem(): void {
    if (this.clothingItemForm.valid) {
      const newClothingItem: CreateClothingItem = this.clothingItemForm.value;
      this.shopService.addClothingItem(newClothingItem).subscribe({
        next: () => {
          console.log('Clothing item created successfully');
          this.clothingItemForm.reset();
          this.loadClothingItems();
        },
        error: (err) => console.error(err)
      });
    }
  }

  deleteClothingItem(id: string): void {
    this.shopService.removeClothingItem(id).subscribe({
      next: () => {
        console.log('Clothing item deleted successfully');
        this.loadClothingItems();
      },
      error: (err) => console.error(err)
    });
  }
}