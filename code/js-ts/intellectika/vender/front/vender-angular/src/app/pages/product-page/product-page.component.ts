import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import { ModalService } from 'src/app/services/modal.service';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css']
})
export class ProductPageComponent implements OnInit {
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
