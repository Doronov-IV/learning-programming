import { Product } from '../product/product.controller';

describe('Product', () => {
  it('should be defined', () => {
    expect(new Product()).toBeDefined();
  });
});
