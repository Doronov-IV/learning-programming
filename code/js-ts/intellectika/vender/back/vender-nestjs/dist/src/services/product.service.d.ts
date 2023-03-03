import { IProduct } from 'src/iproduct/iproduct.interface';
export declare class ProductService {
    private products;
    private appStatus;
    getProducts(): Promise<IProduct[]>;
    private readProducts;
    private getAppStatus;
}
