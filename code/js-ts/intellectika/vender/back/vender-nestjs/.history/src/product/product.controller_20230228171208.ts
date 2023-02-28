import { Controller } from '@nestjs/common';
import { IProduct } from '../iproduct';

@Controller('product')
export class ProductController {

    @Get()
    getProducts() {

        productOne : IProduct

        return [
            
        ]
    }

}