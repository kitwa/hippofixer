import { Member } from './member';

export interface BlogPost {
    id: number;
    title: string;
    content: string;
    photo: File;
    photoUrl: string;
    photoPublicId : string;
    appUserId?: number;
    appUser: Member;
    createdDate: Date;
}