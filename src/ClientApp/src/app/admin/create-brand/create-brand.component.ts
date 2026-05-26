import { Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormGroupDirective } from '@angular/forms';
import { Brand } from 'src/app/shared/models/brand';
import { CreateBrand } from 'src/app/shared/models/create-brand';
import { ShopService } from 'src/app/shop/shop.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: false,
  selector: 'app-create-brand',
  templateUrl: './create-brand.component.html',
  styleUrls: ['./create-brand.component.sass']
})
export class CreateBrandComponent implements OnInit {
  @ViewChild(FormGroupDirective) formDirective!: FormGroupDirective;
  brandForm: FormGroup;
  brands: Brand[] = [];

  constructor(
    private fb: FormBuilder,
    private shopService: ShopService,
    private cdr: ChangeDetectorRef
  ) {
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
      next: (brands) => {
        this.brands = brands;
        this.cdr.markForCheck();
      },
      error: (error) => console.error('Error loading brands', error)
    });
  }

  createBrand(): void {
    if (this.brandForm.valid) {
      const newBrand: CreateBrand = this.brandForm.value;
      this.shopService.addClothingBrand(newBrand).subscribe({
        next: () => {
          this.formDirective.resetForm();
          this.loadBrands();
        },
        error: (err) => console.error(err)
      });
    }
  }
}
