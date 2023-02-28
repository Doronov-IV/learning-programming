import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormControlDirective } from '@angular/forms'
import { ModelService } from 'src/app/services/model.service';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css']
})
export class CreateProductComponent implements OnInit {

  form = new FormGroup( {
    title: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(6)
    ])
  })

  constructor(
      private productService: ProductService, 
      private modelService: ModelService
    ) {}

  ngOnInit(): void {
      
  }

  get title(): FormControl {
    return this.form.controls.title
  }

  submit() {
    this.productService.create(
      {
        title: this.form.value.title as string,
        price: 13.5,
        description: 'lorem ipsum set',
        image: 'https://i.pravatar.cc',
        category: 'electronic',
        rating: {
          rate: 42,
          count: 1
      }
    }).subscribe(() => {
      this.modelService.close()
    })
  }

}
