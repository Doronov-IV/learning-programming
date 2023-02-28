import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ProductComponent } from './components/product/product.component';
import { GlobalErrorComponent } from './components/global-error/global-error.component';

import { FormsModule } from '@angular/forms';
import { FilterProductPipe } from './pipes/filter-product.pipe';
import { ModelComponent } from './components/model/model.component';


@NgModule({
  declarations: [
    AppComponent,
    ProductComponent,
    GlobalErrorComponent,
    FilterProductPipe,
    ModelComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
