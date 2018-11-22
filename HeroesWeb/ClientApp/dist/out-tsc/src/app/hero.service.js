var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { of } from 'rxjs';
import { catchError, map, tap, shareReplay, retry } from 'rxjs/operators';
import { MessageService } from './message.service';
import { AuthService } from './services/auth.service';
var httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
var HeroService = /** @class */ (function () {
    function HeroService(http, auth, messageService) {
        this.http = http;
        this.auth = auth;
        this.messageService = messageService;
        this.heroesUrl = 'api/heroes'; // URL to web api
    }
    /** GET heroes from the server */
    HeroService.prototype.getHeroes = function () {
        var _this = this;
        return this.auth.AuthGet(this.heroesUrl).pipe(tap(function (_) { return _this.log('fetched heroes'); }), catchError(this.handleError('getHeroes', [])), retry(3), shareReplay(1));
    };
    /** GET hero by id. Return `undefined` when id not found */
    HeroService.prototype.getHeroNo404 = function (id) {
        var _this = this;
        var url = this.heroesUrl + "/?id=" + id;
        return this.http.get(url)
            .pipe(map(function (heroes) { return heroes[0]; }), // returns a {0|1} element array
        tap(function (h) {
            var outcome = h ? "fetched" : "did not find";
            _this.log(outcome + " hero id=" + id);
        }), catchError(this.handleError("getHero id=" + id)));
    };
    /** GET hero by id. Will 404 if id not found */
    HeroService.prototype.getHero = function (id) {
        var _this = this;
        var url = this.heroesUrl + "/" + id;
        return this.auth.AuthGet(url).pipe(tap(function (_) { return _this.log("fetched hero id=" + id); }), catchError(this.handleError("getHero id=" + id)));
    };
    /* GET heroes whose name contains search term */
    HeroService.prototype.searchHeroes = function (term) {
        var _this = this;
        if (!term.trim()) {
            // if not search term, return empty hero array.
            return of([]);
        }
        return this.auth.AuthGet(this.heroesUrl + "/?name=" + term).pipe(tap(function (_) { return _this.log("found heroes matching \"" + term + "\""); }), catchError(this.handleError('searchHeroes', [])));
    };
    //////// Save methods //////////
    /** POST: add a new hero to the server */
    HeroService.prototype.addHero = function (hero) {
        var _this = this;
        return this.auth.AuthPost(this.heroesUrl, hero).pipe(tap(function (hero) { return _this.log("added hero w/ id=" + hero.id); }), catchError(this.handleError('addHero')));
    };
    /** DELETE: delete the hero from the server */
    HeroService.prototype.deleteHero = function (hero) {
        var _this = this;
        var id = typeof hero === 'number' ? hero : hero.id;
        var url = this.heroesUrl + "/" + id;
        return this.auth.AuthDelete(url).pipe(tap(function (_) { return _this.log("deleted hero id=" + id); }), catchError(this.handleError('deleteHero')));
    };
    /** PUT: update the hero on the server */
    HeroService.prototype.updateHero = function (hero) {
        var _this = this;
        return this.auth.AuthPut(this.heroesUrl, hero).pipe(tap(function (_) { return _this.log("updated hero id=" + hero.id); }), catchError(this.handleError('updateHero')));
    };
    /**
     * Handle Http operation that failed.
     * Let the app continue.
     * @param operation - name of the operation that failed
     * @param result - optional value to return as the observable result
     */
    HeroService.prototype.handleError = function (operation, result) {
        var _this = this;
        if (operation === void 0) { operation = 'operation'; }
        return function (error) {
            // TODO: send the error to remote logging infrastructure
            console.error(error); // log to console instead
            // TODO: better job of transforming error for user consumption
            _this.log(operation + " failed: " + error.message);
            // Let the app keep running by returning an empty result.
            return of(result);
        };
    };
    /** Log a HeroService message with the MessageService */
    HeroService.prototype.log = function (message) {
        this.messageService.add("HeroService: " + message);
    };
    HeroService = __decorate([
        Injectable({ providedIn: 'root' }),
        __metadata("design:paramtypes", [HttpClient,
            AuthService,
            MessageService])
    ], HeroService);
    return HeroService;
}());
export { HeroService };
//# sourceMappingURL=hero.service.js.map