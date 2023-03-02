import { AppDataSource } from "./data-source"
import { Product } from "./entity/Product"
import { Rating } from "./entity/Rating"

AppDataSource.initialize().then(async () => {

    const ratingRepository = MyDataSource.getRepository(Rating)

}).catch(error => console.log(error))
