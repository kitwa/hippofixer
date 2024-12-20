import { Member } from "./member";
import { WorkOrder } from "./workorder";

export interface Invoice {
  id: number;
  workOrderId: number;
  workorder: WorkOrder;
  contractorId: number;
  contractor: Member;
  amount: number;
  createdDate: Date;
  dueDate?: Date;
  datePaid?: Date;
}

