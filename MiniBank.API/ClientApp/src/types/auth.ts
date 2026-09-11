export interface LoginRequest {
    email: string;
    password: string;
}

export interface LoginResult {
    success: boolean;
    errorMessage: string | null;
    token: string | null;
}