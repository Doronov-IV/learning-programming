import { Injectable } from '@nestjs/common';

@Injectable()
export class ProductService {

    private productOne = <IProduct> {
        title: 'Slim Shady',
        price: 7.62,
        description: 'Rap God',
        category: 'Esoteric',
        image: 'https://imgur.com/t/space/NdvowU6'
    }

}
