import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { SidebarComponent } from './sidebar.component';
import { EmployeeHeaderComponent } from './employee-header/emp-header.component';
import { EmployeeFooterComponent } from './employee-footer/emp-footer.component';
import { RouterModule, Router } from '@angular/router';
import { GeneralHeaderComponent } from './general-header/gen-header.component';
import { AdminFooterComponent } from './admin-footer/admin-footer.component';
import { AdminHeaderComponent } from './admin-header/admin-header.component';
import { SpecFooterComponent } from './spec-footer/spec-footer.component';

@NgModule({
    imports: [CommonModule, RouterModule],
    exports: [
        CommonModule,
        FormsModule,
        SidebarComponent,
        EmployeeHeaderComponent,
        EmployeeFooterComponent,
        AdminFooterComponent,
        AdminHeaderComponent,
        GeneralHeaderComponent,
        SpecFooterComponent

    ],
    declarations: [SidebarComponent, SpecFooterComponent, EmployeeHeaderComponent, EmployeeFooterComponent, AdminFooterComponent, AdminHeaderComponent, GeneralHeaderComponent],
})
export class SharedModule { }
