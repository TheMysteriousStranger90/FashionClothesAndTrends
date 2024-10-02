import {Component, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ToastrService} from 'ngx-toastr';
import {CheckoutService} from 'src/app/checkout/checkout.service';
import {OrdersService} from 'src/app/orders/orders.service';
import {DeliveryMethod} from 'src/app/shared/models/delivery-method';
import {Order, OrderItem, OrderUpdate} from 'src/app/shared/models/order';

@Component({
  selector: 'app-edit-order',
  templateUrl: './edit-order.component.html',
  styleUrls: ['./edit-order.component.sass']
})
export class EditOrderComponent implements OnInit {
  orders: Order[] = [];
  selectedOrder: Order | null = null;
  editOrderForm: FormGroup;
  displayedColumns: string[] = ['order', 'email', 'date', 'total', 'status', 'actions'];
  deliveryMethods: DeliveryMethod[] = [];
  initialSubtotal: number = 0

  constructor(
    private ordersService: OrdersService,
    private fb: FormBuilder,
    private checkoutService: CheckoutService,
    private toastr: ToastrService
  ) {
    this.editOrderForm = this.fb.group({
      deliveryMethodId: ['', Validators.required],
      shipToAddress: this.fb.group({
        firstName: ['', Validators.required],
        lastName: ['', Validators.required],
        country: ['', Validators.required],
        city: ['', Validators.required],
        state: ['', Validators.required],
        addressLine: ['', Validators.required],
        postalCode: ['', Validators.required]
      }),
      orderItems: this.fb.array([]),
      status: ['', Validators.required],
      subtotal: [0, Validators.required]
    });
  }

  ngOnInit(): void {
    this.loadAllOrders();
    this.loadDeliveryMethods();
  }

  loadAllOrders(): void {
    this.ordersService.getAllOrders().subscribe({
      next: (orders) => this.orders = orders,
      error: (err) => console.error('Error loading all orders', err)
    });

    this.editOrderForm.get('deliveryMethodId')!.valueChanges.subscribe(() => {
      this.updateSubtotal();
    });
  }

  updateSubtotal(): void {
    const orderItems = this.orderItems.controls;
    let subtotal = 0;

    orderItems.forEach((item) => {
      const price = item.get('price')!.value;
      const quantity = item.get('quantity')!.value;
      if (price && quantity) {
        subtotal += price * quantity;
      }
    });

    const selectedDeliveryMethodId = this.editOrderForm.get('deliveryMethodId')!.value;
    const selectedDeliveryMethod = this.deliveryMethods.find(
      (method) => method.id === selectedDeliveryMethodId
    );

    if (selectedDeliveryMethod) {
      subtotal += selectedDeliveryMethod.price;
    }

    this.editOrderForm.get('subtotal')!.setValue(subtotal, { emitEvent: false });
  }

  get orderItems(): FormArray {
    return this.editOrderForm.get('orderItems') as FormArray;
  }

  loadDeliveryMethods(): void {
    this.checkoutService.getDeliveryMethods().subscribe({
      next: (methods) => this.deliveryMethods = methods,
      error: (err) => console.error('Error loading delivery methods', err)
    });
  }

  selectOrder(order: Order): void {
    this.selectedOrder = order;
    const deliveryMethod = this.deliveryMethods.find(dm => dm.shortName === order.deliveryMethod);
    const deliveryMethodId = deliveryMethod ? deliveryMethod.id : '';

    this.initialSubtotal = order.subtotal;

    this.editOrderForm.patchValue({
      ...order,
      deliveryMethodId
    });

    this.setOrderItems(order.orderItems);
  }

  setOrderItems(orderItems: OrderItem[]): void {
    const orderItemsFormArray = this.editOrderForm.get('orderItems') as FormArray;

    while (orderItemsFormArray.length) {
      orderItemsFormArray.removeAt(0);
    }

    orderItems.forEach((item) => {
      orderItemsFormArray.push(
        this.fb.group({
          clothingItemId: [item.clothingItemId, Validators.required],
          clothingItemName: [item.clothingItemName, Validators.required],
          price: [item.price, Validators.required],
          quantity: [item.quantity, Validators.required],
        })
      );
    });

    this.updateSubtotal();
  }

  updateOrder(): void {
    if (this.selectedOrder && this.editOrderForm.valid) {
      const updatedOrder: OrderUpdate = this.editOrderForm.value;

      console.log('Updated Order:', updatedOrder);

      this.ordersService.updateUserOrder(this.selectedOrder.id, updatedOrder).subscribe({
        next: (order) => {
          this.toastr.success('Order updated successfully');
          this.loadAllOrders();
          this.selectedOrder = null;
        },
        error: (err) => {
          this.toastr.error('Error updating order');
          console.error('Error updating order', err);
        }
      });
    }
  }

  cancelEdit(): void {
    this.selectedOrder = null;
    this.editOrderForm.reset();
  }
}
