export interface MessageApp {
    id:                number;
    senderId:          number;
    senderEmail:       string;
    senderUsername:    string;
    recipientId:       number;
    recipientEmail:    string;
    recipientUsername: string;
    content:           string;
    dateRead?:         Date;
    messageSent:       Date;
}