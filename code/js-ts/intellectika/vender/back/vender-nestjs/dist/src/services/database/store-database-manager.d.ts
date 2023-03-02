import { Product } from "../../../vender-database/src/entity/Product";
import { Rating } from "../../../vender-database/src/entity/Rating";
import { IProduct } from "src/iproduct/iproduct.interface";
export declare class StoreDatabaseManager {
    constructor();
    readonly productRepository: any;
    readonly ratingRepository: any;
    addProduct(product: Product): void;
    addRating(rating: Rating): void;
    getAllProducts(): IProduct[];
    clearTables(): void;
    private printReport;
    private performOperation;
}
