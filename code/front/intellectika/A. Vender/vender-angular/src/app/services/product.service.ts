import { Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http'
import { catchError, delay, Observable, retry, throwError } from "rxjs";
import { IProduct } from "../models/product";
import { ErrorService } from "./error.service";



@Injectable({
    providedIn: 'root'
})
export class ProductService {

    readonly delayTime = 2000


    constructor(
        private http: HttpClient,
        private errorService: ErrorService) {
    }

    getAll(): Observable<IProduct[]> {
        return this.http.get<IProduct[]>('https://fakestoreapi.com/products', {
            params: new HttpParams({
                fromObject: {limit: 5}
            })
        }).pipe(delay(this.delayTime),
        retry(2),
        catchError(this.handleError.bind(this))
        )
    }


    private handleError(error: HttpErrorResponse) {
        this.errorService.handle(error.message)
        return throwError(() => error.message)
    }


}