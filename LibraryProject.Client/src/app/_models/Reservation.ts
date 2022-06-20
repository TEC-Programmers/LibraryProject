import { Book } from "./Book";
import { User } from "./User";

export interface Reservation
{
    id: Number;
    userId: Number;
    user: User;
    bookId: Number;
    book:Book;
    reserved_At: string;
    reserved_To: string;
}
