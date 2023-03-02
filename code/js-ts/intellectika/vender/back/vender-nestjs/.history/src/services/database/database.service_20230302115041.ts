import { Injectable } from '@nestjs/common';
import {StoreDatabaseManager} as sdm from '../database/store-database-manager'
 
@Injectable()
export class DatabaseService {

    constructor(private dbManager: StoreDatabaseManager) {}


    getAll() {

    }


}
