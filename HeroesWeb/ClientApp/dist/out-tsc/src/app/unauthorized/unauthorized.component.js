var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { Location } from '@angular/common';
import { AuthService } from '../services/auth.service';
var UnauthorizedComponent = /** @class */ (function () {
    function UnauthorizedComponent(location, service) {
        this.location = location;
        this.service = service;
    }
    UnauthorizedComponent.prototype.ngOnInit = function () {
    };
    UnauthorizedComponent.prototype.login = function () {
        this.service.startSigninMainWindow();
    };
    UnauthorizedComponent.prototype.goback = function () {
        this.location.back();
    };
    UnauthorizedComponent = __decorate([
        Component({
            selector: 'app-unauthorized',
            templateUrl: 'unauthorized.component.html'
        }),
        __metadata("design:paramtypes", [Location, AuthService])
    ], UnauthorizedComponent);
    return UnauthorizedComponent;
}());
export { UnauthorizedComponent };
//# sourceMappingURL=unauthorized.component.js.map