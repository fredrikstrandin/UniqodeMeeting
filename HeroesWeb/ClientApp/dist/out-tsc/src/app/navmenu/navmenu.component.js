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
import { AuthService } from '../services/auth.service';
import { GlobalEventsManager } from '../services/global.events.manager';
var NavMenuComponent = /** @class */ (function () {
    function NavMenuComponent(_authService, _globalEventsManager) {
        var _this = this;
        this._authService = _authService;
        this._globalEventsManager = _globalEventsManager;
        this._loggedIn = false;
        _globalEventsManager.showNavBarEmitter.subscribe(function (mode) {
            // mode will be null the first time it is created, so you need to igonore it when null
            if (mode !== null) {
                console.log("Global Event, sent: " + mode);
                _this._loggedIn = mode;
            }
        });
    }
    NavMenuComponent.prototype.login = function () {
        this._authService.startSigninMainWindow();
    };
    NavMenuComponent.prototype.logout = function () {
        this._authService.startSignoutMainWindow();
    };
    NavMenuComponent = __decorate([
        Component({
            selector: 'nav-menu',
            templateUrl: './navmenu.component.html',
            styleUrls: ['./navmenu.component.css']
        }),
        __metadata("design:paramtypes", [AuthService,
            GlobalEventsManager])
    ], NavMenuComponent);
    return NavMenuComponent;
}());
export { NavMenuComponent };
//# sourceMappingURL=navmenu.component.js.map