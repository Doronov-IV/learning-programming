import { AppDataSource } from "./data-source"
import { Product } from "./entity/Product"

AppDataSource.initialize().then(async () => {

    const ratingRepository = MyData

}).catch(error => console.log(error))
