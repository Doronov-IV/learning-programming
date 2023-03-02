import { AppDataSource } from "./data-source"
import { Product } from "./entity/Product"
import { Rating } from "./entity/Rating"
import * as fs from 'fs' 

AppDataSource.initialize().then(async () => {

    const ratingRepository = AppDataSource.getRepository(Rating)
    const productRepository = AppDataSource.getRepository(Product)

    let jsonString = fs.readFileSync('../data/json/products.json','utf-8')
    let products: Product[] = JSON.parse(jsonString)

    for(let item of products) {
        if (product)
    }

    console.log('main script finished')
    

}).catch(error => console.log(error))
