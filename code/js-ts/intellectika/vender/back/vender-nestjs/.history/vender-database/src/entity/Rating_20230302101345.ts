import { Entity, PrimaryGeneratedColumn, Column, OneToMany } from "typeorm"

@Entity()
export class Rating {
    
    @PrimaryGeneratedColumn()
    id: number
}