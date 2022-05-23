import { Author } from "./Author";
import { Category } from "./Category";
import { Publisher } from "./Publisher";

export interface Book
{
    id: Number;
    title: string;
    language: string;
    image: string;
    description: string;
    publishYear: Number;
    categoryId: Number;
   
    authorId: Number;
   
    publisherId: Number;
}