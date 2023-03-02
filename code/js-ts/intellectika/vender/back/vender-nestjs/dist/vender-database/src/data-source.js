"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppDataSource = void 0;
require("reflect-metadata");
const typeorm_1 = require("typeorm");
const Product_1 = require("./entity/Product");
const Rating_1 = require("./entity/Rating");
exports.AppDataSource = new typeorm_1.DataSource({
    type: "mssql",
    host: "s-dev-2\\doronoviv",
    port: 1433,
    username: "sa",
    password: "123456",
    database: "StoreDatabase",
    options: { encrypt: false },
    entities: [
        Rating_1.Rating,
        Product_1.Product
    ],
    synchronize: true
});
//# sourceMappingURL=data-source.js.map