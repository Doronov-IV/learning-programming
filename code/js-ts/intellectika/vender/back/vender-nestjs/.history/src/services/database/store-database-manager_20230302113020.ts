import { AppDataSource } from "../../../vender-database/src/data-source"
import { Product } from "../../../vender-database/src/entity/Product"
import { Rating } from "../../../vender-database/src/entity/Rating"
import * as fs from 'fs' 
import { EntityManager } from "typeorm"
import { IProduct } from "src/iproduct/iproduct.interface"

export class StoreDatabaseManager {


    public addProduct(product: Product) {
        this.performOperation(() => {
            const productRepository = AppDataSource.getRepository(Rating)
            productRepository.save(product)
            this.printReport('add-product')  
        })
    }


    public addRating(rating: Rating) {
        this.performOperation(() => {
            const ratingRepository = AppDataSource.getRepository(Rating)
            ratingRepository.save(rating)
            this.printReport('add-rating')  
        })
    }


    public getAllProducts(): Product[] {
        
    }



    public clearTables() {
        this.performOperation
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