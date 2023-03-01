import { Module } from '@nestjs/common';
import { ProductController } from '../controllers//productproduct.controller';
import { ProductService } from '../services/product.service';

@Module({
  controllers: [ProductController],
  providers: [ProductService]
})
export class ProductModule {}
