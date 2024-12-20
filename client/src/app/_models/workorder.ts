
// import { Invoice } from './invoice';

import { Invoice } from "./invoice";
import { Issue } from "./issue";
import { Member } from "./member";
import { WorkOrderStatus } from "./workOrderStatus";

export interface WorkOrder {
    id: number;
    cost?: number;
    cancelledNote: string;
    issueId: number;
    issue: Issue;
    contractorId: number;
    contractor: Member;
    statusId: number;
    status: WorkOrderStatus;
    invoices: Invoice;
    createdDate: Date;
    dateCompleted?: Date;
    deleted: boolean;
}
