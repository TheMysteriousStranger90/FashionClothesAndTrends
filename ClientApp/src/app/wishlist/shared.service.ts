import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private defaultWishlistIdSource = new BehaviorSubject<string | null>(this.getStoredDefaultWishlistId());
  defaultWishlistId$ = this.defaultWishlistIdSource.asObservable();

  setDefaultWishlistId(id: string | null) {
    this.defaultWishlistIdSource.next(id);
    this.storeDefaultWishlistId(id);
  }

  getDefaultWishlistId(): string | null {
    return this.defaultWishlistIdSource.value;
  }

  private storeDefaultWishlistId(id: string | null) {
    if (id) {
      localStorage.setItem('defaultWishlistId', id);
    } else {
      localStorage.removeItem('defaultWishlistId');
    }
  }

  private getStoredDefaultWishlistId(): string | null {
    return localStorage.getItem('defaultWishlistId');
  }
}
