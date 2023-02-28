import { Controller } from '@nestjs/common';
import { Controller } from '../iproduct';

@Controller('product')
export class ProductController {

    @Get()
    getProducts() {

        productOne : IProduct

        return [
            
        ]
    }

}