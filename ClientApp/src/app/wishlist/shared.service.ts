import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  private defaultWishlistIdSource = new BehaviorSubject<string | null>(null);
  defaultWishlistId$ = this.defaultWishlistIdSource.asObservable();

  setDefaultWishlistId(id: string | null) {
    this.defaultWishlistIdSource.next(id);
  }

  getDefaultWishlistId(): string | null {
    return this.defaultWishlistIdSource.value;
  }
}
