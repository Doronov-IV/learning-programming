import { Injectable } from '@nestjs/common';
import { IProduct } from 'src/iproduct/iproduct.interface';
import * as fs from 'fs';
import { DatabaseService } from './database/database.service';
import { StoreDatabaseManager } from './database/store-database-manager';

@Injectable()
export class ProductService {

    private products: IProduct[] = []

    public getProducts(): IProduct[] {
        this.readProducts()
        return this.products
    }


    private readProducts() : Iproduct[] {
        if (this.products.length === 0) {
            let databaseService: DatabaseService = new DatabaseService()
            this.products = databaseService.getAll()
        }
        else return products
    }

}
