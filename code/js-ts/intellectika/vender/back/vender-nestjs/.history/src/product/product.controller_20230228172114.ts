import { Controller, Get } from '@nestjs/common';
import { IProduct } from '../iproduct/iproduct.interface';
import { ProductService } from './product.service';

@Controller('product')
export class ProductController {

    constructor(private productService: ProductService)

    @Get()
    getProduct() {



        return 
    }

}