export interface Challenge {
  id: number;
  senderId: number;
  senderUsername: string;
  senderPhotoUrl: string;
  recipientId: number;
  recipientUsername: string;
  recipientPhotoUrl: string;
  location: string;
  sport: string;
  eventDate: Date;
  answer?: boolean;
  challengeSent: Date;
}