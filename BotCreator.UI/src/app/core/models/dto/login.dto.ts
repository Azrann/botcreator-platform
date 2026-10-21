export interface LoginDto {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    token: string;
    refreshToken: string;
}