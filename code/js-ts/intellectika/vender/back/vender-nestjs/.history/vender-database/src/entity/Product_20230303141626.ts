import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from "typeorm"
import { Rating } from "./Rating"
import {IProduct} from '../../../src/iproduct/iproduct.interface'

@Entity()
export class Product implements IProduct {

    @PrimaryGeneratedColumn()
    id: number

    @Column()
    title: string

    @Column("float")
    price: number

    @Column({ type: "nvarchar", length: "MAX" })
    description: string

    @Column()
    category: string

    @Column()
    image: string

    @ManyToOne(() => Rating, (rating) => rating.products)
    rating?: Rating

    @Column()
    ratingId?: number

}
