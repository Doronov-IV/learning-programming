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

        if (this.getAppStatus() === "auto") {
            this.products = await databaseService.getAll()
        }
        else {
            await databaseService.performSomeActions()
        }


        return this.products
    }

    private getAppStatus(): string {
        let rawJson = fs.readFileSync('config/app.json','utf-8')
        let config = JSON.parse(rawJson)
        return config.status
    }

}
