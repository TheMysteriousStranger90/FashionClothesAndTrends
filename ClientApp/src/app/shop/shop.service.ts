import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClothingItem } from '../shared/models/clothing-item';
import { IPagination } from '../shared/models/pagination';
import { ClothingParams } from '../shared/models/clothing-params';
import { Brand } from '../shared/models/brand';
import { environment } from 'src/environments/environment';
import { Guid } from 'guid-typescript';
import { map, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShopService {

  baseUrl = environment.apiUrl;
  products: ClothingItem[] = [];
  brands: Brand[] = [];
  pagination?: IPagination<ClothingItem[]>;
  clothingParams = new ClothingParams();
  clothingCache = new Map<string, IPagination<ClothingItem[]>>();

  constructor(private http: HttpClient) { }

  setShopParams(params: ClothingParams) {
    this.clothingParams = params;
  }

  getShopParams() {
    return this.clothingParams;
  }

  getClothing(id: string) {
    const clothing = [...this.clothingCache.values()]
      .reduce((acc, paginatedResult) => {
        return {...acc, ...paginatedResult.data.find(x => x.id === id)}
      }, {} as ClothingItem)

    if (Object.keys(clothing).length !== 0) return of(clothing);

    return this.http.get<ClothingItem>(this.baseUrl + 'clothing/' + id);
  }

  getBrands() {
    if (this.brands.length > 0) return of(this.brands);

    return this.http.get<Brand[]>(this.baseUrl + 'clothing/brands').pipe(
      map(brands => this.brands = brands)
    );
  }

  getClothingItems(useCache = true): Observable<IPagination<ClothingItem[]>> {
    if (!useCache) this.clothingCache = new Map();

    const cacheKey = Object.values(this.clothingParams).join('-');
    if (this.clothingCache.size > 0 && useCache) {
      if (this.clothingCache.has(cacheKey)) {
        this.pagination = this.clothingCache.get(cacheKey);
        if (this.pagination) return of(this.pagination);
      }
    }

    let params = new HttpParams();

    if (this.clothingParams.clothingBrandId) {
      params = params.append('clothingBrandId', this.clothingParams.clothingBrandId.toString());
    }

    if (this.clothingParams.gender) {
      params = params.append('gender', this.clothingParams.gender);
    }

    if (this.clothingParams.size) {
      params = params.append('size', this.clothingParams.size);
    }

    if (this.clothingParams.category) {
      params = params.append('category', this.clothingParams.category);
    }

    if (this.clothingParams.sort) {
      params = params.append('sort', this.clothingParams.sort);
    }

    if (this.clothingParams.search) {
      params = params.append('search', this.clothingParams.search);
    }

    params = params.append('pageIndex', this.clothingParams.pageIndex.toString());
    params = params.append('pageSize', this.clothingParams.pageSize.toString());

    return this.http.get<IPagination<ClothingItem[]>>(this.baseUrl + 'clothing', { params }).pipe(
      map(response => {
        this.clothingCache.set(cacheKey, response);
        this.pagination = response;
        return response;
      })
    );
  }

  addClothingBrand(clothingBrand: Brand): Observable<void> {
    return this.http.post<void>(this.baseUrl + 'clothing/brands', clothingBrand);
  }

  addClothingItem(clothingItem: ClothingItem): Observable<void> {
    return this.http.post<void>(this.baseUrl + 'clothing/items', clothingItem);
  }

  getAllClothingItems(): Observable<ClothingItem[]> {
    return this.http.get<ClothingItem[]>(this.baseUrl + 'clothing/all');
  }

  clearCache() {
    this.clothingCache.clear();
  }

  updateCache(key: string, data: IPagination<ClothingItem[]>) {
    this.clothingCache.set(key, data);
  }
}
