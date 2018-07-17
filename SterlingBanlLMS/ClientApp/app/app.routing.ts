import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CertificateComponent } from "./employee/certificate/cetificate.component"
import { SupportComponent } from "./employee/support/support.componet";

const appRoutes: Routes = [
    { path: 'support', component: SupportComponent },
    { path: 'certificate', component: CertificateComponent },
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: '**', redirectTo: 'home', pathMatch: 'full' }
];

export const routing: ModuleWithProviders =
    RouterModule.forRoot(appRoutes, { useHash: true });