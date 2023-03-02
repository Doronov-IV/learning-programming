import { Injectable } from '@nestjs/common';
import { StoreDatabaseManager } from '../../../vender-database/src/store-database-manager'
 
@Injectable()
export class DatabaseService {

    constructor(private dbManager: StoreDatabaseManager)

}
