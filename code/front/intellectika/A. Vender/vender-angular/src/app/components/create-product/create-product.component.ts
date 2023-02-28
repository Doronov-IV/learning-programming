import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormControlDirective } from '@angular/forms'

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

  constructor() {}

  ngOnInit(): void {
      
  }

  get title(): FormControl {
    return this.form.controls.title
  }

  submit() {
    
  }

}
