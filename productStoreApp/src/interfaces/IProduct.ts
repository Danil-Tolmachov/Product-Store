import { ISpecification } from './ISpecification';
import { ICategory } from './ICategory';

export interface IProduct {
    id: number;
    name: string;
    price: number;
    discount: number;
    unitMeasure: string;

    category: ICategory | null;
    description: string;
    specifications: ISpecification[];
    imagePathes: string[];
}

export function adaptProduct(apiProduct: any, imageBasePath: string): IProduct {
    let category: ICategory = {
        id: apiProduct.categoryId,
        name: apiProduct.categoryName,
        items: [],
    }

    return {
        id: apiProduct.id,
        name: apiProduct.name,
        price: apiProduct.price,
        discount: apiProduct.discount,
        unitMeasure: apiProduct.unitMeasure,
        category: category,
        description: apiProduct.description,
        specifications: apiProduct.specifications,
        imagePathes: (apiProduct.imagePathes as string[] || []).map(path => imageBasePath + "/" + path),
    };
}
