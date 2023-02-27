export interface NewProject {
    authorId: number;
    teamId: number | null;
    name: string;
    description: string;
    deadline: Date;
}