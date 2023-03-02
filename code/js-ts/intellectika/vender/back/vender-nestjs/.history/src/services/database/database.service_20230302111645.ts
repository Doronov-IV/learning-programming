import { Injectable } from '@nestjs/common';
import { StoreDatabaseManager } from '../../../vender-database/'
 
@Injectable()
export class DatabaseService {

    constructor(private dbManager: StoreDatabaseManager)

}
