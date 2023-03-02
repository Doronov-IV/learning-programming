import { Injectable } from '@nestjs/common';
import {StoreDatabaseManager} from '../database/store-database-manager'
import {IPro}
 
@Injectable()
export class DatabaseService {

    constructor(private dbManager: StoreDatabaseManager) {}


    getAll() : IProduct[] {

    }


}
