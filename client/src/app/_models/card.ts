export interface Card {
    id: number;
    cardHolderName: string;
    cardNumber: string;
    cardType: string;
    bankName: string;
    expirationDate: Date;
    cvv: number;
    isDefault?: boolean; // Optional because it's initialized with a default value in C#
  }
  