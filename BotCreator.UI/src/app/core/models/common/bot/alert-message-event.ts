export enum AlertType {
    SUCCESS = 0,
    ERROR = 1
}

export interface AlertMessageEvent {
    type: AlertType;
    text: string;
}