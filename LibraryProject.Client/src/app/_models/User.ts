import { Role } from "./Role";

export interface User
{
    id: number;
    firstName: string;
    middleName: string;
    lastName: string;
    email: string;
    password: string;
    role: Role;
}