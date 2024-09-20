import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Wishlist} from 'src/app/shared/models/wishlist';
import {WishlistService} from '../wishlist.service';
import {AccountService} from 'src/app/account/account.service';
import {User} from 'src/app/shared/models/user';
import {map, Observable, of, switchMap, take} from 'rxjs';
import {SharedService} from '../shared.service';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.sass']
})
export class WishlistComponent implements OnInit {
  wishlists: Wishlist[] = [];
  addWishlistForm: FormGroup;
  user: User | undefined;
  defaultWishlistId: string | null = null;

  constructor(
    private wishlistService: WishlistService,
    private fb: FormBuilder,
    private accountService: AccountService,
    private sharedService: SharedService
  ) {
    this.addWishlistForm = this.fb.group({
      name: ['', Validators.required]
    });

    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.user = user;
        }
      }
    });
  }

  ngOnInit(): void {
    if (this.user?.id) {
      this.loadWishlists();
    }

    this.sharedService.defaultWishlistId$.subscribe({
      next: (id) => this.defaultWishlistId = id
    });
  }

  loadWishlists(): void {
    this.wishlistService.getWishlistsByUserId().subscribe({
      next: (wishlists) => {
        this.wishlists = wishlists.map(wishlist => ({
          ...wishlist,
          wishListItems: wishlist.items
        }));
        
        let currentDefaultWishlistId = this.sharedService.getDefaultWishlistId();

        if (!currentDefaultWishlistId) {
          const defaultWishlist = this.wishlists.find(w => w.name === 'Default');
          currentDefaultWishlistId = defaultWishlist?.id || null;
          this.sharedService.setDefaultWishlistId(currentDefaultWishlistId);
        }

        this.defaultWishlistId = currentDefaultWishlistId;
      },
      error: (error) => console.error('Error loading wishlists:', error)
    });
  }

  addWishlist(): void {
    if (this.addWishlistForm.invalid || !this.user?.id) {
      return;
    }

    const wishlistName = this.addWishlistForm.value.name;

    this.wishlistService.createWishlist(wishlistName).subscribe(() => {
      this.loadWishlists();
      
      const currentDefaultWishlistId = this.sharedService.getDefaultWishlistId();
      if (this.defaultWishlistId !== currentDefaultWishlistId) {
        this.defaultWishlistId = currentDefaultWishlistId;
      }

      this.addWishlistForm.reset();
    });
  }

  deleteWishlist(wishlistId: string): void {
    this.wishlistService.deleteWishlist(wishlistId).subscribe(() => {
      this.loadWishlists();
    });
  }

  addItemToWishlist(wishlistId: string, clothingItemId: string): void {
    this.wishlistService.addItemToWishlist(clothingItemId, wishlistId).subscribe(() => {
      this.loadWishlists();
    });
  }

  removeItemFromWishlist(wishlistId: string, itemId: string): void {
    this.wishlistService.removeItemFromWishlist(wishlistId, itemId).subscribe(() => {
      this.loadWishlists();
    });
  }

  setDefaultWishlist(wishlistId: string): void {
    this.sharedService.setDefaultWishlistId(wishlistId);
  }
}
