import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { IProduct, adaptProduct } from "../interfaces/IProduct";

@Injectable({
    providedIn: "root"
})
export class ProductService {

    constructor(private http: HttpClient) { }

    getProducts(): Observable<IProduct[]> {
        let link = this.getApiUrl() + "/product";

        return this.http.get<IProduct[]>(link).pipe(
            map(response => response.map(product => adaptProduct(product, this.getProductImageApiUrl())))
        );
    }

    getProduct(id: number): Observable<IProduct> {
        let link = this.getApiUrl() + `/product/${id}`;

        return this.http.get<IProduct>(link).pipe(
            map(product => adaptProduct(product, this.getProductImageApiUrl()))
        );
    }

    private getApiUrl(): string {
        return "https://localhost:7048/api";
    }

    private getProductImageApiUrl(): string {
        return "https://localhost:7048/api/image/product";
    }
}