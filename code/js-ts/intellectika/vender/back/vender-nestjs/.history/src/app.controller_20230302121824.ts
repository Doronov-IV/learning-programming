import { Controller, Get } from '@nestjs/common';
import { PrismaClient } from '@prisma/client';
import { AppService } from './app.service';
import { IProduct } from './iproduct/iproduct.interface';
import { ProductService } from './services/product.service';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService) {}

  @Get()
  getProducts(): IProduct[] {
    let s = new ProductService()
    return s.getProducts()
  }
}
