import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
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
    private sharedService: SharedService,
    private toastr: ToastrService
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
    if (!this.product) {
      this.toastr.warning('Product is not available.');
      return;
    }

    if (this.isFavorite) {
      this.favoritesService.removeFavorite(this.product.id).subscribe({
        next: () => {
          this.isFavorite = false;
          this.toastr.success('Removed from favorites');
        },
        error: (error) => {
          console.error(error);
          this.toastr.error('Failed to remove from favorites');
        }
      });
    } else {
      this.favoritesService.addFavorite(this.product.id).subscribe({
        next: () => {
          this.isFavorite = true;
          this.toastr.success('Added to favorites');
        },
        error: (error) => {
          console.error(error);
          this.toastr.error('Failed to add to favorites');
        }
      });
    }
  }

  addToWishlist() {
    if (!this.product) {
      this.toastr.warning('Product is not available.');
      return;
    }

    this.wishlistService.getWishlistsByUserId().subscribe({
      next: (wishlists) => {
        if (wishlists.length === 0) {
          this.toastr.warning('Please create a wishlist first.');
        } else if (!this.defaultWishlistId) {
          this.toastr.warning('Please set a default wishlist.');
        } else {
          this.wishlistService.addItemToWishlist(this.product!.id, this.defaultWishlistId!).subscribe({
            next: () => this.toastr.success('Item added to wishlist'),
            error: (error) => console.error(error)
          });
        }
      },
      error: (error) => console.error(error)
    });
  }
}
