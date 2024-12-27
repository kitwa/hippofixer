export interface InvoiceItem {
  id: number;
  invoiceId: number;
  description: string;
  price: number;
  quantity: number;
  deleted: boolean;
}
