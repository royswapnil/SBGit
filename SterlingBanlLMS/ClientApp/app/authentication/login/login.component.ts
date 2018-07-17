import { FormBuilder, FormGroup, FormControl, FormArray, Validators, FormControlName, AbstractControl, NgForm } from '@angular/forms';
import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import "rxjs/add/operator/debounceTime";
import { ToastrService } from 'ngx-toastr';
import { IUser } from '../../models/user';
import { GenericValidator } from '../../shared/generic-validator';
import { AuthService } from '../../service/auth.service';

@Component({
    templateUrl: "login.component.html",
    styleUrls: ['login.component.css']
})

export class LoginComponent implements OnInit {
    errorMessage: string;
    emailMessage: string;
    user: IUser;
    loginFrm: FormGroup;
    displayMessage: { [key: string]: string } = {};
    //private validationMessages: { [key: string]: { [key: string]: string } };
    private genericValidator: GenericValidator;
    post: any;
    email: string = '';
    password: string = '';
    private validationMessages = {

        required: 'Email Address is required',
        pattern: 'Please enter a valid email address'


    }
        //this.genericValidator = new GenericValidator(this.validationMessages);

    constructor(private authService: AuthService, private router: Router, private fb: FormBuilder, private toastr: ToastrService) {
        //Define all valiations for the form.
        
    }


    ngOnInit(): void {
        this.loginFrm = this.fb.group({
            email: ['', [Validators.required, Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+')]],
            password: ['', Validators.required],
            remember:''
        });

        const emailControl = this.loginFrm.get('email');
        emailControl.valueChanges.debounceTime(1000).subscribe(value => this.setMessage(emailControl))
    }

    login(post): void {
        this.email = post.email;
        this.password = post.password;

        this.authService.login(this.email, this.password)
        if (this.authService.redirectUrl) {
            this.router.navigateByUrl(this.authService.redirectUrl);
        }
        
        this.router.navigate(['/home']);
        this.toastr.success("Successfully logged in as " + this.email);
    }
    setMessage(c: AbstractControl): void {
        this.emailMessage = '';
        if ((c.touched || c.dirty) && c.errors) {
            this.emailMessage = Object.keys(c.errors).map(key => this.validationMessages[key]).join(' ');
        }
    }

    get Email() { return this.loginFrm.get('emial') };
    get Password(){ return this.loginFrm.get('password') };
}