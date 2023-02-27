import { User } from "./user";

export interface Team {
    id: number;
    name: string;
    createdAt: Date;
    updateAt: Date;
    users: User[];
}