var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable, EventEmitter } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { Router } from "@angular/router";
import { UserManager } from 'oidc-client';
import { GlobalEventsManager } from './global.events.manager';
import { map } from 'rxjs/operators';
var settings = {
    authority: 'http://localhost:5000',
    client_id: 'mvc',
    redirect_uri: 'http://localhost:5002/callback',
    post_logout_redirect_uri: 'http://localhost:5002/signout-callback-oidc',
    response_type: 'id_token token',
    scope: 'openid profile',
    silent_redirect_uri: 'http://localhost:5002/silent-renew.html',
    automaticSilentRenew: true,
    accessTokenExpiringNotificationTime: 4,
    // silentRequestTimeout:10000,
    filterProtocolClaims: true,
    loadUserInfo: true
};
var AuthService = /** @class */ (function () {
    function AuthService(http, _router, _globalEventsManager) {
        var _this = this;
        this.http = http;
        this._router = _router;
        this._globalEventsManager = _globalEventsManager;
        this._loggedIn = false;
        this._userLoadedEvent = new EventEmitter();
        if (typeof window !== 'undefined') {
            //instance needs to be created within the if clause
            //otherwise you'll get a sessionStorage not defined error.
            this._mgr = new UserManager(settings);
            this._mgr
                .getUser()
                .then(function (user) {
                if (user) {
                    _this._currentUser = user;
                    _this._userLoadedEvent.emit(user);
                }
            })
                .catch(function (err) {
                console.log(err);
            });
            this._mgr.events.addUserUnloaded(function (e) {
                //if (!environment.production) {
                console.log("user unloaded");
                //}
            });
        }
    }
    AuthService.prototype.clearState = function () {
        this._mgr.clearStaleState().then(function () {
            console.log("clearStateState success");
        }).catch(function (e) {
            console.log("clearStateState error", e.message);
        });
    };
    AuthService.prototype.getUser = function () {
        var _this = this;
        this._mgr.getUser().then(function (user) {
            console.log("got user");
            _this._userLoadedEvent.emit(user);
        }).catch(function (err) {
            console.log(err);
        });
    };
    AuthService.prototype.removeUser = function () {
        var _this = this;
        this._mgr.removeUser().then(function () {
            _this._userLoadedEvent.emit(null);
            console.log("user removed");
        }).catch(function (err) {
            console.log(err);
        });
    };
    AuthService.prototype.startSigninMainWindow = function () {
        this._mgr.signinRedirect({ data: 'some data' }).then(function () {
            console.log("signinRedirect done");
        }).catch(function (err) {
            console.log(err);
        });
    };
    AuthService.prototype.endSigninMainWindow = function () {
        //TODO: Validate why in a promise a global variable is not accessible,
        //      instead a method scope variable is required so it can be used within
        //      the promise.
        //Answer: the previous code was using function (user) { } instead of just (user) =>
        //        because is a function that only has one parameter (user) that explains
        //        why the other variables were undefined, the fix was to use an anonymous function
        //        a lambda expression.
        var _this = this;
        //TODO: Validate why even though _mgr has already been instantiated, I need to enclose
        //      the call in !== undefined, removing the if clause results in a failure of _mgr
        //      is undefined
        if (typeof window !== 'undefined') {
            this._mgr.signinRedirectCallback().then(function (user) {
                console.log("signed in");
                _this._loggedIn = true;
                _this._globalEventsManager.showNavBar(_this._loggedIn);
                _this._router.navigate(['home']);
            }).catch(function (err) {
                console.log(err);
            });
        }
    };
    AuthService.prototype.startSignoutMainWindow = function () {
        this._mgr.signoutRedirect().then(function (resp) {
            console.log("signed out", resp);
            setTimeout(function () {
                console.log("testing to see if fired...");
            }, 5000);
        }).catch(function (err) {
            console.log(err);
        });
    };
    ;
    AuthService.prototype.endSignoutMainWindow = function () {
        this._mgr.signoutRedirectCallback().then(function (resp) {
            console.log("signed out", resp);
        }).catch(function (err) {
            console.log(err);
        });
    };
    ;
    /**
     * Example of how you can make auth request using angulars http methods.
     * @param options if options are not supplied the default content type is application/json
     */
    AuthService.prototype.AuthGet = function (url, options) {
        if (options) {
            options = this._setRequestOptions(options);
        }
        else {
            options = this._setRequestOptions();
        }
        return this.http.get(url, options).pipe(map(function (r) { return r.json(); }));
    };
    /**
     * @param options if options are not supplied the default content type is application/json
     */
    AuthService.prototype.AuthPut = function (url, data, options) {
        var body = JSON.stringify(data);
        if (options) {
            options = this._setRequestOptions(options);
        }
        else {
            options = this._setRequestOptions();
        }
        return this.http.put(url, body, options).pipe(map(function (r) { return r.json(); }));
    };
    /**
     * @param options if options are not supplied the default content type is application/json
     */
    AuthService.prototype.AuthDelete = function (url, options) {
        if (options) {
            options = this._setRequestOptions(options);
        }
        else {
            options = this._setRequestOptions();
        }
        return this.http.delete(url, options).pipe(map(function (r) { return r.json(); }));
    };
    /**
     * @param options if options are not supplied the default content type is application/json
     */
    AuthService.prototype.AuthPost = function (url, data, options) {
        var body = JSON.stringify(data);
        if (options) {
            options = this._setRequestOptions(options);
        }
        else {
            options = this._setRequestOptions();
        }
        return this.http.post(url, body, options).pipe(map(function (r) { return r.json(); }));
    };
    AuthService.prototype._setAuthHeaders = function (user) {
        this._authHeaders = new Headers();
        if (this._currentUser != undefined) {
            this._authHeaders.append('Authorization', this._currentUser.token_type + " " + this._currentUser.access_token);
        }
        this._authHeaders.append('Content-Type', 'application/json');
    };
    AuthService.prototype._setRequestOptions = function (options) {
        if (options) {
            options.headers.append(this._authHeaders.keys[0], this._authHeaders.values[0]);
        }
        else {
            //setting default authentication headers
            this._setAuthHeaders(this._currentUser);
            options = new RequestOptions({ headers: this._authHeaders });
        }
        return options;
    };
    AuthService = __decorate([
        Injectable(),
        __metadata("design:paramtypes", [Http,
            Router,
            GlobalEventsManager])
    ], AuthService);
    return AuthService;
}());
export { AuthService };
//# sourceMappingURL=auth.service.js.map