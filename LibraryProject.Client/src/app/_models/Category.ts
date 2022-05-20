import { Author } from "./Author";
import { Book } from "./Book";
import { Publisher } from "./Publisher";

export interface Category
{
  Id: number;
  CategoryName: string;
  Books: Book[];
}