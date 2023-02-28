import { Controller } from '@nestjs/common';
import { IProduct } from '../iproduct/iproduct.interface';

@Controller('product')
export class ProductController {

    @Get()
    getProducts() {

        let productOne = <IProduct> {
            title: 'Slim Shady',
            price: 7.62,
            description: 'Rap God',
            category
        }

        return [
            
        ]
    }

}