import { Author } from "./Author";
import { Category } from "./Category";
import { Publisher } from "./Publisher";

export interface Book
{
    id: number;
    title: string;
    language: string;
    image: string;
    description: string;
    publishYear: number;
    categoryId: number;
    category: Category[];
    authorId: number;
    //author:{id: number, firstName:string, lastName:string};
    publisherId: number;
    //publisher:{id:number, name:string};
}

// fjern {author} & {publisher} objects