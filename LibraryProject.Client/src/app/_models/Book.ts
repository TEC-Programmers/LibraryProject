import { Author } from "./Author";
import { Category } from "./Category";
import { Publisher } from "./Publisher";

export interface Book
{
    id: Number;
    title: string;
    language: string;
    description: string;
    publishYear: Number;
    categoryId: Number;
    category: Category;
    authorId: Number;
    author:Author;
    publisherId: Number;
    publisher: Publisher
}
