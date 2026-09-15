export interface LoginRequest {
    identifier: string;
    password: string;
}

export interface LoginResult {
    success: boolean;
    errorMessage: string | null;
    token: string | null;
}