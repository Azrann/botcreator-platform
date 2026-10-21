export interface ChatMessage {
    userId: string;
    text: string;
    messageTimeStamp?: number;
    avatar_url: string;
    sender: string; // agent, client
    name?: string;
}