import { AppDataSource } from "../../../vender-database/src/data-source"
import { Product } from "../../../vender-database/src/entity/Product"
import { Rating } from "../../../vender-database/src/entity/Rating"
import * as fs from 'fs' 
import { EntityManager } from "typeorm"
import { IProduct } from "src/iproduct/iproduct.interface"

export class StoreDatabaseManager {

    readonly productRepository = AppDataSource.getRepository(Product)
    readonly ratingRepository = AppDataSource.getRepository(Rating)


    public addProduct(product: Product) {
        this.performOperation(() => {
            this.productRepository.save(product)
            this.printReport('add-product')  
        })
    }


    public addRating(rating: Rating) {
        this.performOperation(() => {
            this.ratingRepository.save(rating)
            this.printReport('add-rating')  
        })
    }


    public getAllProducts(): Product[] {
        this.performOperation(() => {
            return this.productRepository
        })
    }



    public clearTables() {
        this.performOperation(async () => {
            let manager: EntityManager = new EntityManager(AppDataSource)
            await manager.query('USE StoreDatabase; DELETE FROM rating; DBCC CHECKIDENT (rating, RESEED, 0)')
            this.printReport('clear')  
        })
    }


    private printReport(methodName: string): void {
        console.log('[manual] ' + methodName + '-execution-finished')
    }


    private performOperation(callback) {
        AppDataSource.initialize().then(async () => {
            callback();
        }).catch(error => console.log(error))
    }

}