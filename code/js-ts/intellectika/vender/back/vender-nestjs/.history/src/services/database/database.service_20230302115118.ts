import { Injectable } from '@nestjs/common';
import {StoreDatabaseManager} from '../database/store-database-manager'
import { IProduct } from 'src/iproduct/iproduct.interface';
 
@Injectable()
export class DatabaseService {

    constructor(private dbManager: StoreDatabaseManager) {}


    getAll() : IProduct[] {
        return this.dbManager.
    }


}
