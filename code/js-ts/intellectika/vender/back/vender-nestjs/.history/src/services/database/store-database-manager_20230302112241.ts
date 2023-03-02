import { AppDataSource } from "../../../vender-database/src/data-source"
import { Product } from "../../../vender-database/src/entity/Product"
import { Rating } from "../../../vender-database/src/entity/Rating"
import * as fs from 'fs' 
import { EntityManager } from "typeorm"

export class StoreDatabaseManager {


    public addProduct(product: Product) {
        AppDataSource.initialize().then(async () => {
            const productRepository = AppDataSource.getRepository(Rating)
            productRepository.save(product)
            this.printReport('add-product')  
        }).catch(error => console.log(error))
    }


    public addRating(rating: Rating) {
        AppDataSource.initialize().then(async () => {
            const ratingRepository = AppDataSource.getRepository(Rating)
            ratingRepository.save(rating)
            this.printReport('add-rating')  
        }).catch(error => console.log(error))
    }


    public getAllProducts() ]



    public clearTables() {
        AppDataSource.initialize().then(async () => {     
            let manager: EntityManager = new EntityManager(AppDataSource)
            await manager.query('USE StoreDatabase; DELETE FROM rating; DBCC CHECKIDENT (rating, RESEED, 0)')
            this.printReport('clear')    
        }).catch(error => console.log(error))
    }


    private printReport(methodName: string): void {
        console.log('[manual] ' + methodName + '-execution-finished')
    }

}