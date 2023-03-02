import { AppDataSource } from "./data-source"
import { Product } from "./entity/Product"
import { Rating } from "./entity/Rating"

AppDataSource.initialize().then(async () => {

    const ratingRepository = AppDataSource.getRepository(Rating)
    const productRepository = AppDataSource.getRepository(Product)

    console.log()

}).catch(error => console.log(error))
