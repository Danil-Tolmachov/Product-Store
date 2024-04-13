import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { ICategory, adaptCategory } from "../interfaces/ICategory";


@Injectable({
    providedIn: "root"
})
export class CategoryService {

    constructor(private http: HttpClient) { }

    getCategories(): Observable<ICategory[]> {
        let link = this.getApiUrl() + "/category";

        return this.http.get<ICategory[]>(link).pipe(
            map(response => response.map(category => adaptCategory(category, this.getProductImageApiUrl())))
        );
    }

    getCategory(id: number): Observable<ICategory> {
        let link = this.getApiUrl() + `/category/${id}`;

        return this.http.get<ICategory>(link).pipe(
            map(category => adaptCategory(category, this.getProductImageApiUrl()))
        );
    }

    private getApiUrl(): string {
        return "https://localhost:7048/api";
    }

    private getProductImageApiUrl(): string {
        return "https://localhost:7048/api/image/product";
    }
}