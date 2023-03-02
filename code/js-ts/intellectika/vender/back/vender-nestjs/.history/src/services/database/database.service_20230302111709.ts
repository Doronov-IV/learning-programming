import { Injectable } from '@nestjs/common';
import { StoreDatabaseManager } from '../../../vender-database/src/store'
 
@Injectable()
export class DatabaseService {

    constructor(private dbManager: StoreDatabaseManager)

}
