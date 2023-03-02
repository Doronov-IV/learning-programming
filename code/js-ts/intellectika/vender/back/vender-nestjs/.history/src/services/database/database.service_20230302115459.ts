import { Injectable } from '@nestjs/common';
import {StoreDatabaseManager} from '../database/store-database-manager'
import { IProduct } from 'src/iproduct/iproduct.interface';
 
@Injectable()
export class DatabaseService {

    readonly dbManager: StoreDatabaseManager


    public getAll() : IProduct[] {
        return this.dbManager.getAllProducts()
    }


}
