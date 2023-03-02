import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from "typeorm"
import { Rating } from "./Rating"
import {IProduct} from '../../../'

@Entity()
export class Product implements IProduct {

    @PrimaryGeneratedColumn()
    id: number

    @Column()
    title: string

    @Column("float")
    price: number

    @Column()
    description: string

    @Column()
    category: string

    @Column()
    image: string

    @ManyToOne(() => Rating, (rating) => rating.products)
    rating?: Rating

}
