import { Injectable } from '@nestjs/common';
import {StoreDatabaseManager } from '../database/'
 
@Injectable()
export class DatabaseService {

    constructor(private dbManager: StoreDatabaseManager)

}
