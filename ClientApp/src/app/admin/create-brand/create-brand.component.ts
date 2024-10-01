import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Brand } from 'src/app/shared/models/brand';
import { CreateBrand } from 'src/app/shared/models/create-brand';
import { ShopService } from 'src/app/shop/shop.service';

@Component({
  selector: 'app-create-brand',
  templateUrl: './create-brand.component.html',
  styleUrls: ['./create-brand.component.sass']
})
export class CreateBrandComponent implements OnInit {
  brandForm: FormGroup;
  brands: Brand[] = [];

  constructor(private fb: FormBuilder, private shopService: ShopService) {
    this.brandForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadBrands();
  }

  loadBrands(): void {
    this.shopService.getBrands().subscribe({
      next: (brands) => this.brands = brands,
      error: (error) => console.error('Error loading brands', error)
    });
  }

  createBrand(): void {
    if (this.brandForm.valid) {
      const newBrand: CreateBrand = this.brandForm.value;
      this.shopService.addClothingBrand(newBrand).subscribe({
        next: () => {
          this.loadBrands();
          this.brandForm.reset();
        },
        error: (err) => console.error(err)
      });
    }
  }
}
