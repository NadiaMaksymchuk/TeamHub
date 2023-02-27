export interface User {
    id: number;
    teamId: number | null;
    firstName: string;
    lastName: string;
    email: string;
    registeredAt: Date;
    birthDay: Date;
    updateAt: Date;
}