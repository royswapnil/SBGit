"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var ngx_progressbar_1 = require("ngx-progressbar");
var auth_service_1 = require("./service/auth.service");
var AppComponent = /** @class */ (function () {
    function AppComponent(router, authService, ngProgress) {
        var _this = this;
        this.router = router;
        this.authService = authService;
        this.ngProgress = ngProgress;
        this.title = 'app';
        router.events.subscribe(function (e) {
            if (e instanceof router_1.NavigationEnd) {
                _this.location = e.url;
                // console.log("current url", e.url)
            }
        });
    }
    AppComponent.prototype.ngOnInit = function () {
        this.ngProgress.start();
    };
    AppComponent = __decorate([
        core_1.Component({
            selector: 'lms-app',
            templateUrl: 'app.component.html',
            styleUrls: ['app.component.css']
        }),
        __metadata("design:paramtypes", [router_1.Router, auth_service_1.AuthService, ngx_progressbar_1.NgProgress])
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.component.js.map