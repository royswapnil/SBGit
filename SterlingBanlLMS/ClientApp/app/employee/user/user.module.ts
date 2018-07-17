import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedModule } from '../../shared/shared.module';
import { ToastrModule } from "ngx-toastr";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ProfileComponent } from './profile/profile.component';
import { AuthGuard } from '../../guard/auth-guard.service';
import { LoginComponent } from '../../authentication/login/login.component';
import { AuthService } from '../../service/auth.service';


@NgModule({
    imports: [
        SharedModule,
        ReactiveFormsModule,
        FormsModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot({
            progressBar: true,
            timeOut: 4000,
            preventDuplicates: true
        }),
        RouterModule.forChild([
            { path: 'profile', component: ProfileComponent },
            { path: 'login', component: LoginComponent }
            

        ])
    ],
    declarations: [
        ProfileComponent,
        LoginComponent
    ],
    providers: [
        AuthGuard,
        AuthService
    ]
})
export class UserModule { }
