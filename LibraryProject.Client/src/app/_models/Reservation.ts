import { Book } from "./Book";
import { User } from "./User";

export interface Reservation
{
    id: number;
    userId: number;
    bookId: number;
    reserved_At: string;
    reserved_To: string;
}
