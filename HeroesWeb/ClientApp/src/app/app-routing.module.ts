import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DashboardComponent }   from './dashboard/dashboard.component';
import { HeroesComponent }      from './heroes/heroes.component';
import { HeroDetailComponent }  from './hero-detail/hero-detail.component';
import { CallbackComponent } from './callback/callback.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { AuthGuardService } from './services/auth-guard.service';

import { AuthService } from './services/auth.service';
import { GlobalEventsManager } from './services/global.events.manager';


const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'home', component: DashboardComponent },
  { path: 'callback', component: CallbackComponent },
  { path: 'unauthorized', component: UnauthorizedComponent }, 
  { path: 'detail/:id', component: HeroDetailComponent },
  { path: 'heroes', component: HeroesComponent,  canActivate:[AuthGuardService]  }
];

@NgModule({
  imports: [ 
    RouterModule.forRoot(routes)
  ],
  providers: [

      { provide: 'ORIGIN_URL', useValue: location.origin },
      AuthService, 
      AuthGuardService, 
      GlobalEventsManager 
    ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
