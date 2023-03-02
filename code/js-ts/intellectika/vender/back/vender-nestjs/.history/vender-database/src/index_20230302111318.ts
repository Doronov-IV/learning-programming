import { AppDataSource } from "./data-source"
import { Product } from "./entity/Product"
import { Rating } from "./entity/Rating"
import * as fs from 'fs' 
import { EntityManager } from "typeorm"

AppDataSource.initialize().then(async () => {

    

}).catch(error => console.log(error))
