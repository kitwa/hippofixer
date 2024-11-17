import { City } from "./city";
import { Client } from "./client";
import { IssueType } from "./issueType";
import { WorkOrderStatus } from "./workOrderStatus";

export interface Issue {
  id: number;
  clientId?: number;
  clientPhone: string;
  clientEmail: string;
  description: string;
  issueTypeId: number;
  photoUrl: string;
  photoPublicId: string;
  emergency: boolean;
  client: Client;
  cityId: number;
  city: City;
  issueType: IssueType;
  statusId: number;
  status: WorkOrderStatus;
  createdDate: Date;
}
