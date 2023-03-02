import { Injectable } from '@nestjs/common';
import {StoreDatabaseManager } from '../database/sto'
 
@Injectable()
export class DatabaseService {

    constructor(private dbManager: StoreDatabaseManager)

}
