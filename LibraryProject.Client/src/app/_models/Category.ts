import { Author } from "./Author";
import { Publisher } from "./Publisher";

export interface Category
{
  Id: number;
  CategoryName: string;
  Books: _Book[];
}

export interface _Book
{
    Id: Number;
    Title: string;
    Language: string;
    Description: string;
    PublishYear: Number;
    CategoryId: Number;
    Category: Category;
    AuthorId: Number;
    Author:Author;
    PublisherId: Number;
    Publisher: Publisher
}
