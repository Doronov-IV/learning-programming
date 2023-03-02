import "reflect-metadata"
import { DataSource } from "typeorm"
import { Product } from "./entity/Product"
import { Rating } from "./entity/Rating"

export const AppDataSource = new DataSource({
    type: "mssql",
    host: "s-dev-2\\doronoviv",
    port: 1433,
    username: "sa",
    password: "123456",
    database: "StoreDatabase",
    options: { encrypt: false },
    entities: [
        Rating,
        Product
    ],
    synchronize: true
})
