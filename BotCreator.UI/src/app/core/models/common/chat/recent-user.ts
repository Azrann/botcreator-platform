export interface RecentUser {
    userId: string;
    userName: string;
    avatarUrl: string;
    lastMessageDate?: number;
    lastMessage?: string;
    channel?: string;
}