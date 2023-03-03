import { Product } from "./Product"
import { Entity, PrimaryGeneratedColumn, Column, OneToMany, ManyToOne } from "typeorm"

@Entity()
export class Rating {

    @PrimaryGeneratedColumn()
    id: number

    @Column("float")
    rate: number

    @Column()
    count: number

    @OneToMany(() => Product, (product) => product.rating, {
        cascade: true,
        
    })
    products: Product[]

}