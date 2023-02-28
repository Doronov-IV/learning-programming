import { Controller } from '@nestjs/common';
import { IPr } from '../iproduct';

@Controller('product')
export class ProductController {

    @Get()
    getProducts() {

        productOne : IProduct

        return [
            
        ]
    }

}