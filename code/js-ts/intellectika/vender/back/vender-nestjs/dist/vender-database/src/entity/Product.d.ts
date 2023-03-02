import { Rating } from "./Rating";
import { IProduct } from '../../../src/iproduct/iproduct.interface';
export declare class Product implements IProduct {
    id: number;
    title: string;
    price: number;
    description: string;
    category: string;
    image: string;
    rating?: Rating;
}
