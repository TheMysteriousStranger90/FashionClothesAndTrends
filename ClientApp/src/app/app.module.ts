import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {FormsModule} from '@angular/forms';
import {SharedModule} from './shared/shared.module';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {GalleryModule} from 'ng-gallery';
import {FileUploadModule} from 'ng2-file-upload';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {TimeagoModule} from 'ngx-timeago';
import {CoreModule} from './core/core.module';
import {HomeComponent} from './home/home.component';
import {FavoritesComponent} from './favorites/favorites.component';
import {JwtInterceptor} from './core/interceptors/jwt.interceptor';
import {LoadingInterceptor} from './core/interceptors/loading.interceptor';
import { NotificationsComponent } from './notifications/notifications.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    FavoritesComponent,
    NotificationsComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    GalleryModule,
    HttpClientModule,
    SharedModule,
    CoreModule,
    FormsModule,
    FileUploadModule,
    TimeagoModule.forRoot(),
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
