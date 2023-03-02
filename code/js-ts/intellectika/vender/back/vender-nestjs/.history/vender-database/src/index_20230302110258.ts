import { AppDataSource } from "./data-source"
import { Product } from "./entity/Product"
import { Rating } from "./entity/Rating"
import * as fs from 'fs' 
import { EntityManager } from "typeorm"

AppDataSource.initialize().then(async () => {

    const ratingRepository = AppDataSource.getRepository(Rating)
    const productRepository = AppDataSource.getRepository(Product)

    let jsonString = fs.readFileSync('../data/json/products.json','utf-8')
    let products: Product[] = JSON.parse(jsonString)


    let manager: EntityManager = new EntityManager(AppDataSource)
    await manager.query('USE StoreDatabase; DELETE FROM product; DBCC CHECKIDENT (products, RESEED, 0)')

    console.log('main script finished')
    

}).catch(error => console.log(error))
