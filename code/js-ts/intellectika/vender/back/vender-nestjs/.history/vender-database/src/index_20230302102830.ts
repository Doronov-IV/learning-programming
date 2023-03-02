import { AppDataSource } from "./data-source"
import { Product } from "./entity/Product"
import { Rating } from "./entity/Rating"
import * as fs from 'fs' 

AppDataSource.initialize().then(async () => {

    const ratingRepository = AppDataSource.getRepository(Rating)
    const productRepository = AppDataSource.getRepository(Product)

    jsonString = fs.readFileSync('../data/json','utf-8')
    

}).catch(error => console.log(error))
