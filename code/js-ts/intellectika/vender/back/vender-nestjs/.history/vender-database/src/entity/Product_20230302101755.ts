import { Entity, PrimaryGeneratedColumn, Column, OneToMany, ManyToOne } from "typeorm"
import { Rating } from "./Rating"

@Entity()
export class User {

    @PrimaryGeneratedColumn()
    id: number

    @Column()
    title: string

    @Column("double")
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
