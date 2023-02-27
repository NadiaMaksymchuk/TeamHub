export interface UpdateTask {
    id: number;
    projectId: number;
    performerId: number;
    name: string;
    description: string;
    state: string;
    finishedAt: Date;
}