import { type ISpecification } from './ISpecification';

export interface IProduct {
  id: number;
  name: string;
  price: number;
  discount: number;
  unitMeasure: string;

  category: ICategory | null;
  description: string;
  specifications: ISpecification[];
  imagePaths: string[];
}

export interface IProductResponse {
  id: number;
  name: string;
  price: number;
  discount: number;
  unitMeasure: string;

  categoryName: string;
  categoryId: number;
  description: string;
  specifications: ISpecification[];
  imagePaths: string[];
}

export interface ICategory {
  id: number;
  name: string;
  items: never[] | IProduct[];
}

export interface ICategoryResponse {
  id: number;
  name: string;
  products: never[] | IProductResponse[];
}
