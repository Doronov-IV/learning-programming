import { IProduct } from '../../iproduct/iproduct.interface';
import { ProductService } from '../../services/product.service';
export declare class ProductController {
    private productService;
    constructor(productService: ProductService);
    getProducts(): Promise<IProduct[]>;
}
