import { createConnection } from "net"

const main = async () => {
    const connection = await createConnection( {
        type: "mssql",
        host
    })
}