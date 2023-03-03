import { Injectable } from '@nestjs/common';
import { IProduct } from 'src/iproduct/iproduct.interface';
import * as fs from 'fs';
import { DatabaseService } from './database/database.service';
import { StoreDatabaseManager } from '../../vender-database/src/store-database-manager';

@Injectable()
export class ProductService {

    private products: IProduct[] = []

    private appStatus : string

    public async getProducts(): Promise<IProduct[]> {
        return await this.readProducts()
    }


    private async readProducts() : Promise<IProduct[]> {
        let databaseService: DatabaseService = new DatabaseService()
        this.products = await databaseService.getAll()
        console.log(this.products.length)
        return this.products
    }

    private getAppStatus

}
