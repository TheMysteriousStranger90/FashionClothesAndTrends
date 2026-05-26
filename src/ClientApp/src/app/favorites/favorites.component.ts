import {Component, OnInit, ChangeDetectionStrategy, ChangeDetectorRef} from '@angular/core';
import {FavoriteItemDto} from '../shared/models/favorite-item';
import {FavoritesService} from './favorites.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: false,
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.sass']
})
export class FavoritesComponent implements OnInit {
  favoriteItems: FavoriteItemDto[] = [];

  constructor(private favoritesService: FavoritesService, private cdr: ChangeDetectorRef) {
  }

  ngOnInit(): void {
    this.loadFavorites();
  }

  loadFavorites() {
    this.favoritesService.getFavoritesByUserId().subscribe({
      next: (favorites) => {
        this.favoriteItems = favorites;
        this.cdr.markForCheck();
      },
      error: (error) => console.error('Error loading favorites:', error)
    });
  }

  removeFavorite(clothingItemId: string) {
    this.favoritesService.removeFavorite(clothingItemId).subscribe({
      next: () => {
        this.favoriteItems = this.favoriteItems.filter(item => item.clothingItemDtoId !== clothingItemId);
        this.cdr.markForCheck();
      },
      error: (error) => console.error('Error removing favorite:', error)
    });
  }
}
