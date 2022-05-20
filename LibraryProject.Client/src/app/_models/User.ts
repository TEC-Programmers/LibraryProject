import { Role } from "./Role";

export interface User
{
    id: Number;
    firstName: string;
    middleName: string;
    lastName: string;
    email: string;
    password: string;
    role: Role;
}