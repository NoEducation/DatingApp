import { Photo } from './photo.model';

export class User {
    userId: number;
    name : string;
    knowAs : string;
    age : number;
    gender : string;
    created: Date;
    lastActive : string;
    city : string;
    county : string;
    interests?: string;
    introduction?: string;
    lookingFor?: string;
    photoUrl?: string;
    photos?: Photo[];
}
