import {Component, OnInit} from '@angular/core';
import {FileUploader} from 'ng2-file-upload';
import {take} from 'rxjs';
import {AccountService} from 'src/app/account/account.service';
import {PhotosService} from 'src/app/core/services/photos.service';
import {User} from 'src/app/shared/models/user';
import {UserPhoto} from 'src/app/shared/models/user-photo';
import {environment} from 'src/environments/environment';

@Component({
  selector: 'app-user-editor',
  templateUrl: './user-editor.component.html',
  styleUrls: ['./user-editor.component.sass']
})
export class UserEditorComponent implements OnInit {
  uploader: FileUploader | undefined;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl;
  user: User | undefined;

  constructor(
    private photosService: PhotosService,
    private accountService: AccountService
  ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: (user) => {
        if (user) {
          this.user = user;
          
          console.log('User photos:', this.user.userPhotos);
          
          console.log('User token:', this.user.token);
          console.log('User main photoUrl:', this.user.photoUrl);
          

          this.uploader = new FileUploader({
            url: this.baseUrl + 'photos/user',
            authToken: 'Bearer ' + this.user.token,
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
              const photo: UserPhoto = JSON.parse(response);


              if (this.user) {
                if (!this.user.userPhotos) {
                  this.user.userPhotos = [];
                }

                this.user.userPhotos.push(photo);

                if (!this.user.photoUrl) {
                  this.user.photoUrl = photo.url;
                }
              }
            }
          };

        }
      },
      error: (err) => console.error('Error fetching user', err)
    });
  }

  ngOnInit(): void {
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  setMainPhoto(photo: UserPhoto): void {
    this.photosService.setMainUserPhoto(photo.id).subscribe({
      next: () => {
        if (this.user) {
          this.user.photoUrl = photo.url;
          this.user.userPhotos.forEach((p) => {
            p.isMain = p.id === photo.id;
          });
        }
      },
      error: (err) => console.error('Error setting main photo', err)
    });
  }

  deletePhoto(photoId: string): void {
    this.photosService.deleteUserPhoto(photoId).subscribe({
      next: () => {
        if (this.user) {
          this.user.userPhotos = this.user.userPhotos.filter((p) => p.id !== photoId);

          if (this.user.photoUrl === photoId) {
            this.user.photoUrl = this.user.userPhotos.find((p) => p.isMain)?.url || '';
          }
        }
      },
      error: (err) => console.error('Error deleting photo', err)
    });
  }
}
