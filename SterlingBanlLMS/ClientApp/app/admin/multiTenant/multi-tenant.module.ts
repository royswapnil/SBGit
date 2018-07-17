import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../../shared/shared.module';
import { NgxPaginationModule } from 'ngx-pagination';
import { ToastrModule } from 'ngx-toastr';
import { MultiTenantComponent } from './tenant.component';

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
            { path: 'admin/multi-tenant', component: MultiTenantComponent },
            
        ])
    ],
    declarations: [
        MultiTenantComponent
    ],
    providers: [
        
    ]
})
export class MultiTenantModule { }
