import { Injectable } from '@nestjs/common';
import { IProduct } from 'src/iproduct/iproduct.interface';

@Injectable()
export class ProductService {

    private productOne = <IProduct> {
        title: 'Slim Shady',
        price: 7.62,
        description: 'Rap God',
        category: 'Esoteric',
        image: 'https://i.imgur.com/NdvowU6.jpeg'
    }


    
    public getProduct(): IProduct {
        return this.productOne
    }

}
