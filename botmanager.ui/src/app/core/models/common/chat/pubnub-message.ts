export interface PubNubMessage<T> {
    channel: string;
    timetoken: string;
    publisher: string;
    message: T;
}