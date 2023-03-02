import { Injectable } from '@nestjs/common';
import { StoreDatabaseManager } from '../../../vender-database/src'
 
@Injectable()
export class DatabaseService {

    constructor(private dbManager: StoreDatabaseManager)

}
