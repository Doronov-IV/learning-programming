import { Entity, PrimaryGeneratedColumn, Column } from "typeorm"

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
    image: string
    rating?: {
        rate: number
        count: number
    }

}
