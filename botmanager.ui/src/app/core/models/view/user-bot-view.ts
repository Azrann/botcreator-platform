export interface UserBotView {
    id: number;
    name: string;
    template: string;
    status: string,
    deployDisabled: boolean,
    editDisabled: boolean,
    removeDisabled: boolean,
    isActive: boolean
    isActivating?: boolean
}