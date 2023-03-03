"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.StoreDatabaseManager = void 0;
const data_source_1 = require("./data-source");
const Product_1 = require("./entity/Product");
const Rating_1 = require("./entity/Rating");
const fs = require("fs");
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
    async getAllProducts() {
        let repositoryCount = await this.productRepository.count();
        console.log('get all products count returned: ' + repositoryCount);
        if (repositoryCount === 0) {
            this.fillDatabase();
        }
        let products = [];
        products = await this.productRepository.find();
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
    fillDatabase() {
        let rawProductsJson = fs.readFileSync('data/json/products.json', 'utf-8');
        let arr = JSON.parse(rawProductsJson);
        console.log('product array count: ' + arr.length);
        for (let item of arr) {
            this.addProduct(item);
        }
    }
}
exports.StoreDatabaseManager = StoreDatabaseManager;
//# sourceMappingURL=store-database-manager.js.map