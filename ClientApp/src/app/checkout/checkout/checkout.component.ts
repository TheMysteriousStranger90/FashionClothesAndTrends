import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AccountService} from 'src/app/account/account.service';
import {BasketService} from 'src/app/basket/basket.service';
import {UsersService} from 'src/app/users/users.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.sass']
})
export class CheckoutComponent implements OnInit {

  constructor(private fb: FormBuilder, private accountService: AccountService, private usersService: UsersService,
              private basketService: BasketService) { }

  ngOnInit(): void {
    this.getAddressFormValues();
    this.getDeliveryMethodValue();
  }

  checkoutForm = this.fb.group({
    addressForm: this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      country: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      addressLine: ['', Validators.required],
      postalCode: ['', Validators.required],
    }),
    deliveryForm: this.fb.group({
      deliveryMethod: ['', Validators.required]
    }),
    paymentForm: this.fb.group({
      nameOnCard: ['', Validators.required]
    })
  });

  getAddressFormValues() {
    this.usersService.getUserAddress().subscribe({
      next: address => {
        address && this.checkoutForm.get('addressForm')?.patchValue(address);
      }
    });
  }

  getDeliveryMethodValue() {
    const basket = this.basketService.getCurrentBasketValue();
    if (basket && basket.deliveryMethodId) {
      this.checkoutForm.get('deliveryForm')?.get('deliveryMethod')?.patchValue(basket.deliveryMethodId.toString());
    }
  }

  get addressForm(): FormGroup {
    return this.checkoutForm.get('addressForm') as FormGroup;
  }

  get deliveryForm(): FormGroup {
    return this.checkoutForm.get('deliveryForm') as FormGroup;
  }

  get paymentForm(): FormGroup {
    return this.checkoutForm.get('paymentForm') as FormGroup;
  }
}
