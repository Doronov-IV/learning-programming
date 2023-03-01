import "reflect-metadata"
import { DataSource } from "typeorm"
import { User } from "./entity/User"

export const AppDataSource = new DataSource({
    type: "mssql",
    host: "S-DEV-2\\DORONOVIV",
    username: "sa",
    password: "123456",
    database: "master"
})
