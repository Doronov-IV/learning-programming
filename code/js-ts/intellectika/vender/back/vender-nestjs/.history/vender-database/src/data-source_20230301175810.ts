import "reflect-metadata"
import { DataSource } from "typeorm"
import { User } from "./entity/User"

export const AppDataSource = new DataSource({
    type: "mssql",
    host: "s-dev-2\\doronoviv",
    port: 1433,
    username: "sa",
    password: "123456",
    database: "master",
    ssl: true
})
