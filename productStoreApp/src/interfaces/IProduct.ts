import { IImage, IImageResponse } from './IImage';
import { ISpecification } from './ISpecification';

export interface IProduct {
  id: number;
  name: string;
  price: number;
  discount: number;
  unitMeasure: string;

  category: ICategory | null;
  description: string;
  specifications: ISpecification[];
  imagePaths: IImage[];
}

export interface IProductResponse {
  id: number;
  name: string;
  price: number;
  discount: number;
  description: string;
  unitMeasure: string;

  category: ICategoryResponse;
  specifications: ISpecification[];
  images: IImageResponse[];
}

export interface ICategory {
  id: number;
  name: string;
  items: IProduct[] | null;
}

export interface ICategoryResponse {
  id: number;
  name: string;
  products: IProductResponse[] | null;
}
