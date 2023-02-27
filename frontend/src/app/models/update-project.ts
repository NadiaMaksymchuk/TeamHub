export interface UpdateProject {
    id: number;
    authorId: number;
    teamId: number | null;
    name: string;
    description: string;
    deadline: Date;
}