export class EventView {
    name: string;
    username: string;
    botId: string;
    dataJson: string;
    dataObj: any;
    clientId: string;
    createdOn: Date;

    get text(): string {
        if (!this.dataObj) {
            return;
        }

        return this.dataObj.Text;
    }
}