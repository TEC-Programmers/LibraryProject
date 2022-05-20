import { Author } from "./Author";
import { Category } from "./Category";
import { Publisher } from "./Publisher";

export interface Book
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