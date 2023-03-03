import { StoreDatabaseManager } from '../../../vender-database/src/store-database-manager';
import { IProduct } from 'src/iproduct/iproduct.interface';
export declare class DatabaseService {
    readonly dbManager: StoreDatabaseManager;
    getAll(): Promise<IProduct[]>;
    performSomeActions(): Promise<void>;
}
