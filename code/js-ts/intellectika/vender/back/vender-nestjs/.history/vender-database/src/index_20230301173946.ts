import { createConnection } from "net"

const main = async () => {
    const connection = await createConnection( {
        type: "mssql",
        host: "localhost",
        port: 1433,
        username: "sa",
        password: "123456",
        database: "master"
})
}
