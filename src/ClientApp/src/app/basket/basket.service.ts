import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {BehaviorSubject, map} from 'rxjs';
import {Basket, BasketItem, BasketTotals} from '../shared/models/basket';
import {environment} from 'src/environments/environment';
import {DeliveryMethod} from '../shared/models/delivery-method';
import {ClothingItem} from '../shared/models/clothing-item';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  baseUrl = environment.apiUrl;

  private basketSource = new BehaviorSubject<Basket | null>(null);
  basketSource$ = this.basketSource.asObservable();
  private basketTotalSource = new BehaviorSubject<BasketTotals | null>(null);
  basketTotalSource$ = this.basketTotalSource.asObservable();

  constructor(private http: HttpClient) {
  }

  createPaymentIntent() {
    return this.http.post<Basket>(this.baseUrl + 'payments/' + this.getCurrentBasketValue()?.id, {})
      .pipe(
        map(basket => {
          this.basketSource.next(basket);
        })
      )
  }

  setShippingPrice(deliveryMethod: DeliveryMethod) {
    this.calculateTotals(deliveryMethod.price);
  }

  getBasket(id: string) {
    return this.http.get<Basket>(this.baseUrl + 'basket?id=' + id).subscribe({
      next: basket => {
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    })
  }

  setBasket(basket: Basket) {
    return this.http.post<Basket>(this.baseUrl + 'basket', basket).subscribe({
      next: basket => {
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    })
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  addItemToBasket(item: ClothingItem | BasketItem, quantity = 1) {
    if (this.isProduct(item)) item = this.mapProductItemToBasketItem(item);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.items = this.addOrUpdateItem(basket.items, item, quantity);
    this.setBasket(basket);
  }

  removeItemFromBasket(id: string, quantity = 1) {
    const basket = this.getCurrentBasketValue();
    if (!basket) return;
    const item = basket.items.find(x => x.id === id);
    if (item) {
      item.quantity -= quantity;
      if (item.quantity === 0) {
        basket.items = basket.items.filter(x => x.id !== id);
      }
      if (basket.items.length > 0) this.setBasket(basket);
      else this.deleteBasket(basket);
    }
  }

  deleteBasket(basket: Basket) {
    return this.http.delete(this.baseUrl + 'basket?id=' + basket.id).subscribe({
      next: () => {
        this.deleteLocalBasket()
      }
    })
  }

  deleteLocalBasket() {
    this.basketSource.next(null);
    this.basketTotalSource.next(null);
    localStorage.removeItem('basket_id');
  }

  private addOrUpdateItem(items: BasketItem[], itemToAdd: BasketItem, quantity: number): BasketItem[] {
    const item = items.find(x => x.id === itemToAdd.id);
    if (item) item.quantity += quantity;
    else {
      itemToAdd.quantity = quantity;
      items.push(itemToAdd);
    }
    return items;
  }

  private createBasket(): Basket {
    const basket = new Basket();
    localStorage.setItem('basket_id', basket.id);
    return basket;
  }

  private mapProductItemToBasketItem(item: ClothingItem): BasketItem {
    return {
      id: item.id,
      clothingName: item.name,
      price: item.price,
      quantity: 0,
      pictureUrl: item.pictureUrl,
      brand: item.brand,
      discount: item.discount
    };
  }

  private calculateTotals(shipping = 0) {
    const basket = this.getCurrentBasketValue();
    if (!basket) return;

    const subtotal = basket.items.reduce((a, b) => {
      const finalPrice = b.discount ? (b.price - (b.price * b.discount / 100)) : b.price;
      return a + (finalPrice * b.quantity);
    }, 0);

    const total = subtotal + shipping;
    console.log(`Subtotal: ${subtotal}, Shipping: ${shipping}, Total: ${total}`);
    this.basketTotalSource.next({ shipping, total, subtotal });
  }

  private isProduct(item: ClothingItem | BasketItem): item is ClothingItem {
    return (item as ClothingItem).brand !== undefined;
  }

  isBasketEmpty(): boolean {
    const basket = this.getCurrentBasketValue();
    return !basket || basket.items.length === 0;
  }
}
