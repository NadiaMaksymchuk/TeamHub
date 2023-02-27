import { Task } from "./task";
import { User } from "./user";
import { Team } from "./team";

export interface Project {
    id: number;
    name: string;
    description: string;
    deadline: Date;
    createdAt: Date;
    author: User;
    team: Team;
}