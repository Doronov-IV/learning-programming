import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from "typeorm"
import { Rating } from "./Rating"

@Entity()
export class Product {

    @PrimaryGeneratedColumn()
    id: number

    @Column()
    title: string

    @Column("")
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
