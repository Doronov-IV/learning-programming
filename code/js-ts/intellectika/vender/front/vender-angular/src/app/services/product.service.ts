import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http'
import { catchError, delay, Observable, retry, throwError } from "rxjs";
import { IProduct } from "../models/product";
import { ErrorService } from "./error.service";
import { tap } from 'rxjs/operators';



@Injectable({
    providedIn: 'root'
})
export class ProductService {

    readonly delayTime = 1000

    products: IProduct[] = []


    constructor(
        private http: HttpClient,
        private errorService: ErrorService) {
    }

    getAll(): Observable<IProduct[]> {
        return this.http.get<IProduct[]>('http://localhost:3000/product', {
            params: new HttpParams({
                fromObject: {limit: 5}
            })
        }).pipe(
            delay(this.delayTime),
            retry(2),
            tap(products => this.products = products),
            catchError(this.handleError.bind(this))
        )
    }


    create(product: IProduct) : Observable<IProduct> {
        return this.http.post<IProduct>('https://fakestoreapi.com/products', product)
            .pipe(
                tap(prod => this.products.push(prod))
            )
    }


    private handleError(error: HttpErrorResponse) {
        this.errorService.handle(error.message)
        return throwError(() => error.message)
    }


}