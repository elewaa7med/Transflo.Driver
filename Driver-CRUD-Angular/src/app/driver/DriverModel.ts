export interface ListResponseViewMode{
    data : GetDriver[],
    ItemCount : number
}

export interface GetDriver {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
}

export interface PostDriver {
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
}


export interface UpdateDriver {
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
}