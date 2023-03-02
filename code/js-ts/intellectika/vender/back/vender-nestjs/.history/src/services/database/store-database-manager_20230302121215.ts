import { AppDataSource } from "../../../vender-database/src/data-source"
import { Product } from "../../../vender-database/src/entity/Product"
import { Rating } from "../../../vender-database/src/entity/Rating"
import * as fs from 'fs' 
import { EntityManager } from "typeorm"
import { IProduct } from "src/iproduct/iproduct.interface"
import { getConnectionManager } from 'typeorm';

export class StoreDatabaseManager {

    constructor() {
        AppDataSource.initialize()
        this.productRepository = AppDataSource.getRepository(Product)
        this.ratingRepository = AppDataSource.getRepository(Rating)
    }

    readonly productRepository
    readonly ratingRepository


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


    public getAllProducts(): IProduct[] {
        let products: IProduct[] = []
        this.performOperation(async () => {
            for (let i = 0, iSize = await this.productRepository.count(); i < iSize; i++) {
                let item = await this.productRepository.find({
                    where: {
                        id: i
                    }
                }) as unknown as IProduct
                products.push(item)
            }
        })
        return products
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
        callback(
    }

}