import { Injectable } from '@nestjs/common';
import {StoreDatabaseManager }
 
@Injectable()
export class DatabaseService {

    constructor(private dbManager: StoreDatabaseManager)

}
