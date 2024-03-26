import { Specification } from './Specification';

export interface Product {
    id: number;
    imageUrl: URL | null;
    name: string;
    price: number;
    discount: number;
    description: string;
    specifications: Specification[];
}
