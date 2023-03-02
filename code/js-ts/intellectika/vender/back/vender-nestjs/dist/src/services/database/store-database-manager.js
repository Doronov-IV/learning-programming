"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.StoreDatabaseManager = void 0;
const data_source_1 = require("../../../vender-database/src/data-source");
const Product_1 = require("../../../vender-database/src/entity/Product");
const Rating_1 = require("../../../vender-database/src/entity/Rating");
const typeorm_1 = require("typeorm");
class StoreDatabaseManager {
    constructor() {
        if (!data_source_1.AppDataSource.isInitialized)
            data_source_1.AppDataSource.initialize();
        this.productRepository = data_source_1.AppDataSource.getRepository(Product_1.Product);
        this.ratingRepository = data_source_1.AppDataSource.getRepository(Rating_1.Rating);
    }
    addProduct(product) {
        this.performOperation(() => {
            this.productRepository.save(product);
            this.printReport('add-product');
        });
    }
    addRating(rating) {
        this.performOperation(() => {
            this.ratingRepository.save(rating);
            this.printReport('add-rating');
        });
    }
    getAllProducts() {
        let products = [];
        this.performOperation(async () => {
            for (let i = 0, iSize = await this.productRepository.count(); i < iSize; i++) {
                let item = await this.productRepository.createQueryBuilder("product").where("product.id = :id", { id: i }).getOne();
                products.push(item);
                console.log(item.title);
            }
        });
        return products;
    }
    clearTables() {
        this.performOperation(async () => {
            let manager = new typeorm_1.EntityManager(data_source_1.AppDataSource);
            await manager.query('USE StoreDatabase; DELETE FROM rating; DBCC CHECKIDENT (rating, RESEED, 0)');
            this.printReport('clear');
        });
    }
    printReport(methodName) {
        console.log('[manual] ' + methodName + '-execution-finished');
    }
    performOperation(callback) {
        callback();
    }
}
exports.StoreDatabaseManager = StoreDatabaseManager;
//# sourceMappingURL=store-database-manager.js.map