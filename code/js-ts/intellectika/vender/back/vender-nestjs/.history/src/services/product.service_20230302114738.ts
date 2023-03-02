import { Injectable } from '@nestjs/common';
import { IProduct } from 'src/iproduct/iproduct.interface';
import * as fs from 'fs';

@Injectable()
export class ProductService {


    public getProducts(): IProduct[] {
        this.readProducts()
        return this.products
    }

}
