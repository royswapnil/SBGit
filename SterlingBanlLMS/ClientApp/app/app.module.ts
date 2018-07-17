import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { APP_BASE_HREF } from '@angular/common';
import { routing } from './app.routing';

import { AppComponent } from './app.component';
import { HttpModule } from '@angular/http';
import { NgProgressModule } from 'ngx-progressbar';
import { MultiTenantModule } from './admin/multiTenant/multi-tenant.module';
import { CourseManagementModule } from './admin/courseMgt/course-mgt.module';
import { CertificateComponent } from './employee/certificate/cetificate.component';
import { SupportComponent } from './employee/support/support.componet';
import { CourseModule } from './employee/course/course.module';
import { SharedModule } from './shared/shared.module';
import { UserModule } from './employee/user/user.module';

@NgModule({
  declarations: [
      AppComponent,
      CertificateComponent,
      SupportComponent,

    ],
  imports: [
      BrowserModule,
      RouterModule,
      HttpModule,
      routing,
      CourseModule,
      SharedModule,
      UserModule,
      NgProgressModule,
      MultiTenantModule,
      CourseManagementModule
  ],
  providers: [{ provide: APP_BASE_HREF, useValue: '/' }],
  bootstrap: [AppComponent]
})
export class AppModule { }
