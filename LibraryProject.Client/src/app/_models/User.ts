export interface User
{
    Id: Number;
    FirstName: string;
    MiddleName: string;
    LastName: string;
    Email: string;
    Password: string;
    Role?: Role;
    token?:string;
}

export enum Role {
    Customer = 'Customer',
    Admin = 'Administrator'
}