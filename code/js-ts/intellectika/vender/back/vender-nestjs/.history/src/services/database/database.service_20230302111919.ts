import { Injectable } from '@nestjs/common';
import {Store}
 
@Injectable()
export class DatabaseService {

    constructor(private dbManager: StoreDatabaseManager)

}
