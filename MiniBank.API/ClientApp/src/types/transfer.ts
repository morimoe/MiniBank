export interface TransferRequest{
    fromAccountNumber: string;
    toAccountNumber: string;
    amount: number;
    description: string;
}

export interface TransferResult{
    success: boolean;
    errorMessage: string;
}