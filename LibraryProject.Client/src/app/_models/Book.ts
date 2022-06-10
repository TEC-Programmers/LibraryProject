import { Author } from "./Author";
import { Category } from "./Category";
import { Publisher } from "./Publisher";

export interface Book
{
    id: number;
    title: string;
    language: string;
    description: string;
    publishYear: number;
    categoryId: number;
    category?: Category;
    authorId: number;
    author?: Author;
    publisherId: number;
    publisher?: Publisher
    image?: string;
}
