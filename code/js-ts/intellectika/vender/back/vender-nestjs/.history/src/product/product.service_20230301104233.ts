import { Injectable } from '@nestjs/common';
import { IProduct } from 'src/iproduct/iproduct.interface';
import * as fs from 'fs';

@Injectable()
export class ProductService {

    private products: IProduct[] = [{
        title: 'Slim Shady',
        price: 7.62,
        description: 'Rap God',
        category: 'Esoteric',
        image: 'https://i.imgur.com/NdvowU6.jpeg'
    }]


    public getProducts(): IProduct[] {
        let jsonFileContents = fs.readFileSync('..\\data\\json\\products.json', 'utf-8');
        products = JSON.parse();
        return this.products
    }

}
