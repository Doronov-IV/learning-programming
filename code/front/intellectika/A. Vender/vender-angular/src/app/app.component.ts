import { TitleCasePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Observable, Observer, tap } from 'rxjs';
import { IProduct } from './models/product';
import { ProductService } from './services/product.service';
import { FormsModule } from '@angular/forms';
import { FilterProductPipe } from './pipes/filter-product.pipe';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [TitleCasePipe]
})
export class AppComponent implements OnInit {
  title = 'vender'
  isLoading : boolean = false
  term : string = ""
  products$: Observable<IProduct[]>
  

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.isLoading = true
    this.products$ = this.productService.getAll().pipe(tap(() => this.isLoading = false))
  }
}
