import { Controller, Get } from '@nestjs/common';
import { IProduct } from '../iproduct/iproduct.interface';

@Controller('product')
export class ProductController {

    @Get()
    getProduct() {



        return productOne
    }

}