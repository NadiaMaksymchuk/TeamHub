import { User } from "./user";
import { TaskState } from "./taskState";

export interface Task {
    id: number;
    projectId: number;
    performerId: number;
    name: string;
    description: string;
    createdAt: Date;
    finishedAt: Date | null;
    updateAt: Date | null; 
    performer: User;
    state: TaskState;
}