import { Injectable } from "@angular/core";
import { IUser } from "../models/user";
import { ToastrService } from "ngx-toastr";

@Injectable()
export class AuthService {
    currentUser: IUser;
    redirectUrl: string;

    constructor(private toastr: ToastrService) { }
    isLoggedIn(): boolean {
        return !!this.currentUser;
    }

    login(email: string, password: string): void {
        this.currentUser = {
            id: 2,
            email: email,
            password: password
        };
        console.log(`User: ${this.currentUser.email} logged in`);
    }
}