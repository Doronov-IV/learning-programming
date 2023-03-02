import { Injectable } from '@nestjs/common';
import {StoreDatabase}
 
@Injectable()
export class DatabaseService {

    constructor(private dbManager: StoreDatabaseManager)

}
