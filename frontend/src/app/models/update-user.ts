export interface UpdateUser {
    id: number;
    teamId: number | null;
    firstName: string;
    lastName: string;
    email: string;
    birthDay: Date;
}