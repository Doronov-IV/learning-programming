import { Product } from "./"
import { Entity, PrimaryGeneratedColumn, Column, OneToMany, ManyToOne } from "typeorm"

@Entity()
export class Rating {

    @PrimaryGeneratedColumn()
    id: number

    @Column("double")
    rate: number

    @Column()
    count: number

    @OneToMany(() => Product, (product) => product.rating)
    products: Product[]

}