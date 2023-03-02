import { Injectable } from '@nestjs/common';
import { StoreDatabaseManager } from 
 
@Injectable()
export class DatabaseService {

    constructor(private dbManager: StoreDatabaseManager)

}
