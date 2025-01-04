import { InvoiceItem } from "./invoiceItem";
import { Member } from "./member";
import { WorkOrder } from "./workorder";

export interface Invoice {
  id: number;
  workorderId: number;
  workorder: WorkOrder;
  contractorId: number;
  contractor: Member;
  amount: number;
  createdDate: Date;
  dueDate?: Date;
  datePaid?: Date;
  invoiceItems: InvoiceItem[];
}

