import { User } from "./user";
import { TaskState } from "./taskState";

export interface Task {
    id: number;
    name: string;
    description: string;
    createdAt: Date;
    finishedAt: Date | null; 
    performer: User;
    state: string;
}