import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { PhotosService } from 'src/app/core/services/photos.service';
import { ClothingItem } from 'src/app/shared/models/clothing-item';
import { ClothingPhoto } from 'src/app/shared/models/clothing-photo';
import { ShopService } from 'src/app/shop/shop.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-add-photo-to-clothing-item',
  templateUrl: './add-photo-to-clothing-item.component.html',
  styleUrls: ['./add-photo-to-clothing-item.component.sass']
})
export class AddPhotoToClothingItemComponent implements OnInit {
  clothingItems: ClothingItem[] = [];
  selectedClothingItem: ClothingItem | null = null;
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;

  constructor(private shopService: ShopService, private photosService: PhotosService) {
    this.uploader = new FileUploader({
      url: '',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const photo = JSON.parse(response);
        if (this.selectedClothingItem) {
          this.selectedClothingItem.pictureUrl = photo.url;
          this.selectedClothingItem.clothingItemPhotos.push(photo);
        }
      }
    };
  }

  ngOnInit(): void {
    this.loadClothingItems();
  }

  loadClothingItems(): void {
    this.shopService.getAllClothingItems().subscribe({
      next: (items) => this.clothingItems = items,
      error: (error) => console.error('Error loading clothing items', error)
    });
  }

  selectClothingItem(item: ClothingItem): void {
    this.selectedClothingItem = item;
    this.uploader.setOptions({ url: `${this.baseUrl}photos/clothing-item/${item.id}` });
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  setMainPhoto(photo: ClothingPhoto): void {
    if (!this.selectedClothingItem) return;

    this.photosService.setMainClothingItemPhoto(this.selectedClothingItem.id, photo.id).subscribe({
      next: () => {
        if (this.selectedClothingItem) {
          this.selectedClothingItem.pictureUrl = photo.url;
          this.selectedClothingItem.clothingItemPhotos.forEach(p => {
            p.isMain = p.id === photo.id;
          });
        }
      },
      error: (err) => console.error('Error setting main photo', err)
    });
  }

  deletePhoto(photoId: string): void {
    if (!this.selectedClothingItem) return;

    this.photosService.deleteClothingItemPhoto(this.selectedClothingItem.id, photoId).subscribe({
      next: () => {
        if (this.selectedClothingItem) {
          this.selectedClothingItem.clothingItemPhotos = this.selectedClothingItem.clothingItemPhotos.filter(x => x.id !== photoId);
        }
      },
      error: (err) => console.error('Error deleting photo', err)
    });
  }
}
