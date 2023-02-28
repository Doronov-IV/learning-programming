import { TitleCasePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Observable, Observer, tap } from 'rxjs';
import { IProduct } from './models/product';
import { ProductService } from './services/product.service';
import { FormsModule } from '@angular/forms';
import { FilterProductPipe } from './pipes/filter-product.pipe';
import { ModalService } from './services/modal.service';

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
  // products$: Observable<IProduct[]>
  

  constructor(
    public productService: ProductService,
    public modelService: ModalService
    ) {}

  ngOnInit(): void {
    this.isLoading = true
    // this.products$ = this.productService.getAll().pipe(tap(() => this.isLoading = false))
    this.productService.getAll().subscribe(() => {
      this.isLoading = false
    })
  }
}
