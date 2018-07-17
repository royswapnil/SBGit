"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var platform_browser_1 = require("@angular/platform-browser");
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var common_1 = require("@angular/common");
var app_routing_1 = require("./app.routing");
var app_component_1 = require("./app.component");
var http_1 = require("@angular/http");
var ngx_progressbar_1 = require("ngx-progressbar");
var multi_tenant_module_1 = require("./admin/multiTenant/multi-tenant.module");
var course_mgt_module_1 = require("./admin/courseMgt/course-mgt.module");
var cetificate_component_1 = require("./employee/certificate/cetificate.component");
var support_componet_1 = require("./employee/support/support.componet");
var course_module_1 = require("./employee/course/course.module");
var shared_module_1 = require("./shared/shared.module");
var user_module_1 = require("./employee/user/user.module");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        declarations: [
            app_component_1.AppComponent,
            cetificate_component_1.CertificateComponent,
            support_componet_1.SupportComponent,
        ],
        imports: [
            platform_browser_1.BrowserModule,
            router_1.RouterModule,
            http_1.HttpModule,
            app_routing_1.routing,
            course_module_1.CourseModule,
            shared_module_1.SharedModule,
            user_module_1.UserModule,
            ngx_progressbar_1.NgProgressModule,
            multi_tenant_module_1.MultiTenantModule,
            course_mgt_module_1.CourseManagementModule
        ],
        providers: [{ provide: common_1.APP_BASE_HREF, useValue: '/' }],
        bootstrap: [app_component_1.AppComponent]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map