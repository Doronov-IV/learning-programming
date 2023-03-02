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

    @Column()
    image: string

    @Column()
    rating?: {
        rate: number
        count: number
    }

}
