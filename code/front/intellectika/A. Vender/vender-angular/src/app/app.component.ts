import { TitleCasePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './models/product';
import { ProductService } from './services/product.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [TitleCasePipe]
})
export class AppComponent implements OnInit {
  title = 'vender'
  products: IProduct[] = []

  constructor(private productService: ProductService) {

  }

  ngOnInit(): void {
    this.productService.getAll().subscribe(products => {
      this.products = products;
    })  
  }
}
