import { Book } from "./Book";
import { User } from "./User";

export interface Reservation
{
    Id: Number;
    userId: Number;
    User: User;
    bookId: Number;
    Book:Book;
    reserved_At: string;
    reserved_To: string;
}