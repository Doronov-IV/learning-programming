import { StoreDatabaseManager } from '../database/store-database-manager';
import { IProduct } from 'src/iproduct/iproduct.interface';
export declare class DatabaseService {
    readonly dbManager: StoreDatabaseManager;
    getAll(): IProduct[];
}
