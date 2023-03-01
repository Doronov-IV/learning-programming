import { Entity, PrimaryGeneratedColumn, Column } from "typeorm"

@Entity()
export class Rating {

    @PrimaryGeneratedColumn()
    id: number

    @Column("double")
    rate: number

    @Column()
    count: number
}