import { Entity, PrimaryGeneratedColumn, Column } from "typeorm"

@Entity()
export class Product {

    @PrimaryGeneratedColumn()
    id: number

    title: string
    price: number
    description: string
    category: string
    image: string

}
