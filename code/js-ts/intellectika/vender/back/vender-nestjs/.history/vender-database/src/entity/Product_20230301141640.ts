import { Entity, PrimaryGeneratedColumn, Column } from "typeorm"

@Entity()
export class Product {

    @PrimaryGeneratedColumn()
    id: number

    @Column()
    title: string

    @Column("d")
    price: number

    @Column()
    description: string

    @Column()
    category: string

    @Column()
    image: string

    @Column()
    ratingId: number

}
