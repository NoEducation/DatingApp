export class UserToRegister{
    public username: string;
    public password: string;
    public confirmPassword : string;
    public gender : string;
    public dateOfBirth : Date;
    public city : string;
    public knowAs : string;

    constructor(username : string,
        password : string,
        confirmPassword : string,
        gender : string,
        dateOfBirth : Date,
        city  : string,
        knowAs : string)
    {
        this.username = username,
        this.password = password,
        this.confirmPassword = confirmPassword,
        this.gender =gender,
        this.dateOfBirth = this.dateOfBirth,
        this.city = city;
        this.knowAs = knowAs;
    }
}