import { AppDataSource } from "./data-source"
import { Product } from "./entity/Product"

AppDataSource.initialize().then(async () => {

    const ratingRepository = MyDataSource.getRepository

}).catch(error => console.log(error))
