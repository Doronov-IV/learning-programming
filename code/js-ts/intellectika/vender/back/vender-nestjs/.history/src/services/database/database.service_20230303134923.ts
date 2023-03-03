import { Injectable } from '@nestjs/common';
import {StoreDatabaseManager} from '../../../vender-database/src/store-database-manager'
import { IProduct } from 'src/iproduct/iproduct.interface';
 
@Injectable()
export class DatabaseService {

    readonly dbManager: StoreDatabaseManager = new StoreDatabaseManager()


    public async getAll() : Promise<IProduct[]> {
        return await this.dbManager.getAllProducts()
    }

    public async performSomeActions(): Promise<void> {
        
    }


}
