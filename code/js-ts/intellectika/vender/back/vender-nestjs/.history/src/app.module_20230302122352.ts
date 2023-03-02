import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { ProductModule } from './modules/product.module';
import { DatabaseService } from './services/database/database.service';

@Module({
  imports: [ProductModule],
  controllers: [AppController],
  providers: [AppService, Produc DatabaseService],
})
export class AppModule {}
