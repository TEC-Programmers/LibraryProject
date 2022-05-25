import { Author } from "./Author";
import { Publisher } from "./Publisher";

export interface Category
{
  id: number;
  categoryName: string;
  books: Book_[];
}

export interface Book_
{
    id: Number;
    title: string;
    language: string;
    description: string;
    publishYear: Number;
}
