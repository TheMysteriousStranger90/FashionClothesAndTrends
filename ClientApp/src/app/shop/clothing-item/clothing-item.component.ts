import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { FavoritesService } from 'src/app/favorites/favorites.service';
import { ClothingItem } from 'src/app/shared/models/clothing-item';

@Component({
  selector: 'app-clothing-item',
  templateUrl: './clothing-item.component.html',
  styleUrls: ['./clothing-item.component.sass']
})
export class ClothingItemComponent implements OnInit {

  @Input() product?: ClothingItem;
  isFavorite: boolean = false;

  constructor(private basketService: BasketService, private favoritesService: FavoritesService) { }

  ngOnInit(): void {
    if (this.product) {
      this.favoritesService.isFavorite(this.product.id).subscribe({
        next: (isFav) => this.isFavorite = isFav,
        error: (error) => console.error(error)
      });
    }
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
}
