import {Component, OnInit} from '@angular/core';
import {FavoriteItemDto} from '../shared/models/favorite-item';
import {FavoritesService} from './favorites.service';
import {Guid} from 'guid-typescript';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.sass']
})
export class FavoritesComponent implements OnInit {
  favoriteItems: FavoriteItemDto[] = [];

  constructor(private favoritesService: FavoritesService) {
  }

  ngOnInit(): void {
    this.loadFavorites();
  }

  loadFavorites() {
    this.favoritesService.getFavoritesByUserId().subscribe({
      next: (favorites) => {
        this.favoriteItems = favorites;
        this.favoriteItems.forEach(item => {
        });
      },
      error: (error) => console.error('Error loading favorites:', error)
    });
  }

  removeFavorite(clothingItemId: string) {
    console.log('Removing favorite with ID:', clothingItemId);
    this.favoritesService.removeFavorite(clothingItemId).subscribe({
      next: () => {
        this.favoriteItems = this.favoriteItems.filter(item => item.clothingItemDtoId !== clothingItemId);
      },
      error: (error) => console.error('Error removing favorite:', error)
    });
  }
}
