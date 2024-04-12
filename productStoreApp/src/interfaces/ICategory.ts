import { IProduct, adaptProduct } from "./IProduct";

export interface ICategory {
    id: number;
    name: string;

    items: IProduct[];
}

export function adaptCategory(apiCategory: any, imageBasePath: string): ICategory {
    return {
        id: apiCategory.id,
        name: apiCategory.name,
        items: (apiCategory.products as object[] || []).map(product  => adaptProduct(product, imageBasePath)),
    }
}