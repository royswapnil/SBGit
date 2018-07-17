import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { HomeComponent } from '../home/home.component';
import { NgxPaginationModule } from "ngx-pagination";
import { ToastrModule } from "ngx-toastr";
import { SharedModule } from '../../shared/shared.module';
import { CourseDetailComponent } from './course-detail/course-detail.component';
import { CourseCatalogueComponent } from './course-catalogue/course-catalogue.component';
import { VideoCatalogueComponent } from './video-catalogue/video-catalogue.component';
import { VideoComponent } from './video/video.component';
import { ToolbarComponent } from './video/toolbar/toolbar.component';
import { ProgressComponent } from './video/progress/progress.component';
import { AuthGuard } from '../../guard/auth-guard.service';
import { TimeDisplayPipe } from '../../pipes/timedisplay.pipe';
import { CourseService } from '../../service/course.service';
import { VideoService } from '../../service/video.service';

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
            { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
            { path: 'course/:id', component: CourseDetailComponent },
            { path: 'course/:id/module', component: VideoComponent },
            { path: 'course', component: CourseCatalogueComponent, /*canActivate: [AuthGuard]*/ },
            { path: 'all_videos', component: VideoCatalogueComponent }

        ])
    ],
    declarations: [
        CourseDetailComponent,
        VideoComponent,
        HomeComponent,
        CourseCatalogueComponent,
        VideoCatalogueComponent,
        ToolbarComponent,
        ProgressComponent,
        TimeDisplayPipe
    ],
    providers: [
        CourseService,
        VideoService
    ]
})
export class CourseModule { }
