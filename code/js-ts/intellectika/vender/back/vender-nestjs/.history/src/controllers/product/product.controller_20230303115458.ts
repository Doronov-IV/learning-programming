import { Controller, Get } from '@nestjs/common';
import { IProduct } from '../../iproduct/iproduct.interface';
import { ProductService } from '../../services/product.service';

@Controller('product')
export class ProductController {

    constructor(private productService: ProductService) {}

    @Get()
    async getProducts() : Promise<IProduct[]> {
        return await this.productService.getProducts()
    }

}