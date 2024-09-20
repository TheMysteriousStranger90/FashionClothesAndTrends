import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { FavoritesService } from 'src/app/favorites/favorites.service';
import { ClothingItem } from 'src/app/shared/models/clothing-item';
import { SharedService } from 'src/app/wishlist/shared.service';
import { WishlistService } from 'src/app/wishlist/wishlist.service';

@Component({
  selector: 'app-clothing-item',
  templateUrl: './clothing-item.component.html',
  styleUrls: ['./clothing-item.component.sass']
})
export class ClothingItemComponent implements OnInit {

  @Input() product?: ClothingItem;
  isFavorite: boolean = false;
  defaultWishlistId: string | null = null;

  constructor(
    private basketService: BasketService,
    private favoritesService: FavoritesService,
    private wishlistService: WishlistService,
    private sharedService: SharedService
  ) {}

  ngOnInit(): void {
    if (this.product) {
      this.favoritesService.isFavorite(this.product.id).subscribe({
        next: (isFav) => this.isFavorite = isFav,
        error: (error) => console.error(error)
      });
    }
    
    this.sharedService.defaultWishlistId$.subscribe({
      next: (id) => this.defaultWishlistId = id
    });
  }

  addItemToBasket() {
    this.product && this.basketService.addItemToBasket(this.product);
  }

  toggleFavorite() {
    if (this.product) {
      if (this.isFavorite) {
        this.favoritesService.removeFavorite(this.product.id).subscribe({
          next: () => this.isFavorite = false,
          error: (error) => console.error(error)
        });
      } else {
        this.favoritesService.addFavorite(this.product.id).subscribe({
          next: () => this.isFavorite = true,
          error: (error) => console.error(error)
        });
      }
    }
  }

  addToWishlist() {
    if (this.product && this.defaultWishlistId) {
      this.wishlistService.addItemToWishlist(this.product.id, this.defaultWishlistId).subscribe({
        next: () => console.log('Item added to wishlist'),
        error: (error) => console.error(error)
      });
    }
  }
}