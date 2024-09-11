import { Component, OnInit } from '@angular/core';
import { FavoriteItem } from '../shared/models/favorite-item';
import { FavoritesService } from './favorites.service';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.sass']
})
export class FavoritesComponent implements OnInit {
  favoriteItems: FavoriteItem[] = [];

  constructor(private favoritesService: FavoritesService) { }

  ngOnInit(): void {
    this.loadFavorites();
  }

  loadFavorites() {
    this.favoritesService.getFavoritesByUserId().subscribe({
      next: (favorites) => this.favoriteItems = favorites,
      error: (error) => console.error(error)
    });
  }

  removeFavorite(clothingItemId: string) {
    this.favoritesService.removeFavorite(clothingItemId).subscribe({
      next: () => this.favoriteItems = this.favoriteItems.filter(item => item.clothingItemId !== clothingItemId),
      error: (error) => console.error(error)
    });
  }
}
