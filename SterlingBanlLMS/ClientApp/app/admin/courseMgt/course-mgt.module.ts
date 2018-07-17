import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../../shared/shared.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { ToastrModule } from 'ngx-toastr';
import { AddCourseComponent } from './add-course/add-course.component';
import { AdminDashboardComponent } from './dashboard/dashboard.component';

@NgModule({
    imports: [
        SharedModule,
        ReactiveFormsModule,
        NgxPaginationModule,
        ToastrModule.forRoot({
            progressBar: true,
            timeOut: 4000,
            preventDuplicates: true
        }),
        RouterModule.forChild([
            { path: 'admin/dashboard', component: AdminDashboardComponent },
            { path: 'admin/addcourse', component: AddCourseComponent }
        ])
    ],
    declarations: [
        AdminDashboardComponent,
        AddCourseComponent
    ],

    providers: [

    ]
})
export class CourseManagementModule { }
