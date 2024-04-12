import { Specification } from './Specification';
import { Category } from './Category';

export interface Product {
    id: number;
    imageUrl: URL | null;
    name: string;
    price: number;
    discount: number;
    category: Category | null;
    description: string;
    specifications: Specification[];
}
