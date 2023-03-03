import { AppDataSource } from "./data-source"
import { Product } from "./entity/Product"
import { Rating } from "./entity/Rating"
import * as fs from 'fs' 
import { EntityManager } from "typeorm"
import { IProduct } from "../../src/iproduct/iproduct.interface"

export class StoreDatabaseManager {

    constructor() {
        if (!AppDataSource.isInitialized) AppDataSource.initialize()
        this.productRepository = AppDataSource.getRepository(Product)
        this.ratingRepository = AppDataSource.getRepository(Rating)
    }

    readonly productRepository
    readonly ratingRepository


    public addProduct(product: Product) {
        this.performOperation(async () => {

            // let existingRating = await this.ratingRepository.findOneBy({
            //     rate: product.rating.rate,
            //     count: product.rating.count
            // })


            // if (existingRating !== null && existingRating !== undefined) {
            //     console.log('[manual] existing rating was already present')
            //     let ratingProducts = existingRating.products

            //     if (ratingProducts === null || ratingProducts === undefined) {
            //         ratingProducts = []
            //     }
            //     ratingProducts.push(product)
            // }

            // else {
            //     console.log('[manual] existing rating was falsy. creating')
            //     let newRating: Rating = product.rating
            //     newRating.products = [product]
            //     this.addRating(newRating)
            // }

            this.ratingRepository.save(product.rating)
            this.productRepository.save(product)
            this.ratingRepository.save(product.rating)
        })
        this.printReport('add-product')  
    }


    public addRating(rating: Rating) {
        this.performOperation(() => {
            this.ratingRepository.save(rating)
            this.printReport('add-rating')  
        })
    }


    public async getAllProducts(): Promise<IProduct[]> {
        let repositoryCount: number = await this.productRepository.count()
        console.log('get all products count returned: ' + repositoryCount)
        if (repositoryCount === 0){
            this.fillDatabase()
        }
         let products: IProduct[] = []

        products = await this.productRepository.find()

        return products
    }



    public clearTables() {
        this.performOperation(async () => {
            let manager: EntityManager = new EntityManager(AppDataSource)
            await manager.query('USE StoreDatabase; DELETE FROM rating; DBCC CHECKIDENT (rating, RESEED, 4747030)')
            await manager.query('USE StoreDatabase; DELETE FROM product; DBCC CHECKIDENT (product, RESEED, 2)')
            this.printReport('clear')  
        })
    }


    private printReport(methodName: string): void {
        console.log('[manual] ' + methodName + '-execution-finished')
    }


    private performOperation(callback) {
        callback()
    }


    public fillDatabase() {
        let rawProductsJson = fs.readFileSync('data/json/products.json', 'utf-8')
        let arr: Product[] = JSON.parse(rawProductsJson)
        console.log('product array count: ' + arr.length)
        
        for (let item of arr) {
            this.addProduct(item)
        }
        
    }

}