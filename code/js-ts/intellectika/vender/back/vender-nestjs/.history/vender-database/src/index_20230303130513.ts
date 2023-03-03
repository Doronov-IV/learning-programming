import { AppDataSource } from "./data-source"
import { Product } from "./entity/Product"
import { Rating } from "./entity/Rating"
import * as fs from 'fs' 
import { EntityManager } from "typeorm"

import { StoreDatabaseManager } from "./store-database-manager"


let sdm = new StoreDatabaseManager()
sdm.clearTables