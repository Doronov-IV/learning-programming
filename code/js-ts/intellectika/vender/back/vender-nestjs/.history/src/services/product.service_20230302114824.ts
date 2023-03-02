import { Injectable } from '@nestjs/common';
import { IProduct } from 'src/iproduct/iproduct.interface';
import * as fs from 'fs';
import { DatabaseService } from './database/database.service';

@Injectable()
export class ProductService {

    private products: IProduct[] = []

    private database

    public getProducts(): IProduct[] {
        this.readProducts()
        return this.products
    }


    public readProducts() : void{
        if (this.products.length === 0) {
            products =  
        }
    }

}
