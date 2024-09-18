import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {BasketService} from 'src/app/basket/basket.service';
import {BreadcrumbService} from 'xng-breadcrumb';
import {ShopService} from '../shop.service';
import {ClothingItem} from 'src/app/shared/models/clothing-item';
import {take} from 'rxjs';
import {AccountService} from 'src/app/account/account.service';
import {User} from 'src/app/shared/models/user';
import {Rating} from 'src/app/shared/models/rating';
import {RatingService} from 'src/app/shared/rating/rating.service';
import {CommentService} from 'src/app/core/services/comment.service';
import {FormBuilder, FormGroup} from '@angular/forms';
import {Comment} from '../../shared/models/comment';
import {LikeService} from 'src/app/core/services/like.service';
import {LikeDislike} from 'src/app/shared/models/like-dislike';

@Component({
  selector: 'app-clothing-details',
  templateUrl: './clothing-details.component.html',
  styleUrls: ['./clothing-details.component.sass']
})
export class ClothingDetailsComponent implements OnInit {
  product?: ClothingItem;
  quantity = 1;
  quantityInBasket = 0;
  user?: User;

  averageRating: number | undefined;
  userRating: number | undefined;

  commentForm: FormGroup;
  comments: Comment[] = [];

  constructor(private accountService: AccountService, private ratingService: RatingService, private shopService: ShopService, private activatedRoute: ActivatedRoute, private bcService: BreadcrumbService, private basketService: BasketService, private commentService: CommentService, private likeService: LikeService, private fb: FormBuilder,) {
    this.bcService.set('@productDetails', ' ')

    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) this.user = user;
      }
    })

    this.commentForm = this.fb.group({
      text: ['']
    });
  }

  ngOnInit(): void {
    this.loadProduct();
    this.loadComments();
    this.loadRatings()
  }

  loadProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      this.shopService.getClothing(id).subscribe({
        next: product => {
          this.product = product;
          this.bcService.set('@productDetails', product.name);
          this.basketService.basketSource$.pipe(take(1)).subscribe({
            next: basket => {
              const item = basket?.items.find(x => x.id === id);
              if (item) {
                this.quantity = item.quantity;
                this.quantityInBasket = item.quantity;
              }
            }
          });
          this.loadRatings();
        },
        error: error => console.log(error)
      });
    }
  }

  incrementQuantity() {
    this.quantity++;
  }

  decrementQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  updateBasket() {
    if (this.product) {
      if (this.quantity > this.quantityInBasket) {
        const itemsToAdd = this.quantity - this.quantityInBasket;
        this.quantityInBasket += itemsToAdd;
        this.basketService.addItemToBasket(this.product, itemsToAdd);
      } else {
        const itemsToRemove = this.quantityInBasket - this.quantity;
        this.quantityInBasket -= itemsToRemove;
        this.basketService.removeItemFromBasket(this.product.id, itemsToRemove);
      }
    }
  }

  get buttonText() {
    return this.quantityInBasket === 0 ? 'Add to basket' : 'Update basket';
  }

  loadRatings() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) {
      console.error('Product ID is undefined');
      return;
    }

    this.ratingService.getAverageRating(id).subscribe({
      next: averageRating => {
        this.averageRating = averageRating;
      },
      error: error => console.log(error)
    });

    if (this.user) {
      this.ratingService.getUserRating(this.user.id, id).subscribe({
        next: userRating => {
          this.userRating = userRating ? userRating.score : undefined;
        },
        error: error => console.log(error)
      });
    }
  }

  onRating(rating: number) {
    if (this.product && this.user) {
      const newRating: Rating = {
        userId: this.user.id,
        username: this.user.username,
        clothingItemId: this.product.id,
        score: rating
      };

      this.ratingService.addRating(newRating).subscribe({
        next: () => {
          console.log('Rating added/updated successfully');
          this.loadRatings();
        },
        error: error => console.error('Error adding/updating rating:', error)
      });
    }
  }

  loadComments(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) {
      this.commentService.getCommentsForClothingItem(id).subscribe(
        (comments) => {
          this.comments = comments;
          this.comments.forEach(comment => {
            this.likeService.getLikesCount(comment.id).subscribe({
              next: (count) => comment.likesCount = count,
              error: (error) => console.error('Error fetching likes count', error)
            });
            this.likeService.getDislikesCount(comment.id).subscribe({
              next: (count) => comment.dislikesCount = count,
              error: (error) => console.error('Error fetching dislikes count', error)
            });
          });
        },
        (error) => {
          console.error('Error fetching comments', error);
        }
      );
    }
  }

  addComment(): void {
    if (!this.commentForm.valid) {
      return;
    }

    const comment = this.commentForm.value;
    comment.clothingItemId = this.product?.id;
    comment.userId = this.user?.id;
    comment.username = this.user?.username;

    this.commentForm.reset();

    this.commentService.addComment(comment).subscribe(
      (c) => {
        this.comments.unshift(c);
        this.loadComments();
      },
      (error) => {
        console.error('Error adding comment', error);
      }
    );
  }

  removeComment(commentId: string): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) {
      console.error('Product ID is undefined');
      return;
    }

    this.commentService.removeComment(commentId).subscribe(
      () => {
        this.commentService.getCommentsForClothingItem(id).subscribe(
          (comments) => {
            this.comments = comments;
            this.comments.reverse();
            this.loadComments();
          },
          (error) => {
            console.error('Error fetching comments', error);
          }
        );
      },
      (error) => {
        console.error('Error removing comment', error);
      }
    );
  }

  likeComment(commentId: string): void {
    if (this.user) {
      const likeDislike: LikeDislike = {
        isLike: true,
        commentId: commentId,
        userId: this.user.id,
        username: this.user?.username,
        comment: this.comments.find(c => c.id === commentId)!
      };

      this.likeService.addLikeDislike(likeDislike).subscribe({
        next: () => {
          this.loadComments();
        },
        error: error => console.error('Error liking comment', error)
      });
    }
  }

  dislikeComment(commentId: string): void {
    if (this.user) {
      const likeDislike: LikeDislike = {
        isLike: false,
        commentId: commentId,
        userId: this.user.id,
        username: this.user?.username,
        comment: this.comments.find(c => c.id === commentId)!
      };

      this.likeService.addLikeDislike(likeDislike).subscribe({
        next: () => {
          this.loadComments();
        },
        error: error => console.error('Error disliking comment', error)
      });
    }
  }
}
