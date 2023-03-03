import { Product } from "./entity/Product";
import { Rating } from "./entity/Rating";
import { IProduct } from "../../src/iproduct/iproduct.interface";
export declare class StoreDatabaseManager {
    constructor();
    readonly productRepository: any;
    readonly ratingRepository: any;
    addProduct(product: Product): void;
    addRating(rating: Rating): void;
    getAllProducts(): Promise<IProduct[]>;
    clearTables(): void;
    private printReport;
    private performOperation;
    private fillDatabase;
}
