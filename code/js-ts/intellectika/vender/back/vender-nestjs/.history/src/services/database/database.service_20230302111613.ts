import { Injectable } from '@nestjs/common';
import { StoreData }

@Injectable()
export class DatabaseService {

    constructor(private dbManager: StoreDatabaseManager)

}
