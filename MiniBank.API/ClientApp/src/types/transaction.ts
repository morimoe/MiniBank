export interface Transaction {
    fromAccountNumber: string;
    toAccountNumber: string;
    amount: number;
    currency: string;
    createdAt: string;
    status: string;
    description: string;
}