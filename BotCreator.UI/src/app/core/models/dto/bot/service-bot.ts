export interface ServiceBot {
    id?: number;
    name: string;
    buttonText1: string;
    resultText1: string;
    buttonText2: string;
    resultText2: string;
    buttonText3: string;
    resultText3: string;
    whatsappChecked: boolean;
    whatsappNumber: string;
    chatOnWeb: boolean;
    fbChecked: boolean;
    fbUserName: string;
    talkToMeChecked: boolean;
    talkToAgent: string;
    callMeEmail: string;
    callMeChecked: boolean;
}
