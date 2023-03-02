import { Product } from "@prisma/client"
import { Entity, PrimaryGeneratedColumn, Column, OneToMany, ManyToOne } from "typeorm"

@Entity()
export class Rating {

    @PrimaryGeneratedColumn()
    id: number

    @Column("double")
    rate: number

    @Column()
    count: number

    @OneToMany(() => Product, (product) => product)
    products: Product[]

}