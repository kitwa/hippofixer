import { IssueType } from "./issueType";
import { Member } from "./member";
import { WorkOrderStatus } from "./workOrderStatus";

export interface Issue {
  id: number;
  userId: number;
  contractorId?: number;
  title: string;
  description: string;
  createdDate: Date; // Use string if you're getting date as ISO format
  statusId: number;
  issueTypeId: number;
  user: Member; // Ensure AppUser is also defined
  contractor: Member; // Ensure AppUser is also defined
  status: WorkOrderStatus; // Ensure WorkOrderStatus is also defined
  issueType: IssueType;
  photoUrl: string;
}