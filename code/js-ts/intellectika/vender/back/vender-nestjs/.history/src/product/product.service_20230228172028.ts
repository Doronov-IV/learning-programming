import { Injectable } from '@nestjs/common';
import { IProduct } from 'src/iproduct/iproduct.interface';

@Injectable()
export class ProductService {

    private productOne = <IProduct> {
        title: 'Slim Shady',
        price: 7.62,
        description: 'Rap God',
        category: 'Esoteric',
        image: 'https://imgur.com/t/space/NdvowU6'
    }


    public getProduct(): IProduct {
        
    }

}
