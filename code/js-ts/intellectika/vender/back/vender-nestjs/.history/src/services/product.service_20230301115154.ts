import { Injectable } from '@nestjs/common';
import { IProduct } from 'src/iproduct/iproduct.interface';
import * as fs from 'fs';

@Injectable()
export class ProductService {

    private products: IProduct[] = []


    public getProducts(): IProduct[] {
        this.readProducts
        return this.products
    }


    public readProducts() : void{
        if (this.products.length === 0) {
            let jsonFileContents = fs.readFileSync('data\\json\\products.json', 'utf-8');
            this.products = JSON.parse(jsonFileContents);
        }
    }

}
