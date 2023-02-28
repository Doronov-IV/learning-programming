import { Controller } from '@nestjs/common';
import { IProduct } from '../iproduct/iproduct.interface';

@Controller('product')
export class ProductController {

    @Get()
    getProduct() {

        let productOne = <IProduct> {
            title: 'Slim Shady',
            price: 7.62,
            description: 'Rap God',
            category: 'Esoteric',
            image: 'https://imgur.com/t/space/NdvowU6'
        }

        return 
    }

}