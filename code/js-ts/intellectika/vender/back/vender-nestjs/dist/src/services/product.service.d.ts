import { IProduct } from 'src/iproduct/iproduct.interface';
export declare class ProductService {
    private products;
    getProducts(): Promise<IProduct[]>;
    private readProducts;
}
