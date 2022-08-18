import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Role } from 'app/_models/Role';
import { AuthService } from '../_services/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authService: AuthService
  ) { }
//AuthGuard is used to protect the routes from unauthorized access in angular.
// CanActivate is a Interface that a class can implement to be a guard deciding if a route can be activated
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):boolean {
    const currentUser = this.authService.currentUserValue;
    let isLoggedIn = this.authService.isAuthenticated();
    if (currentUser) {
      
      if (currentUser.role.toString() === 'Customer') {
        console.log('Customer logged in');
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
        return false;
      }


      // send the user to login page, if requested endpoint has roles which user does not have
      if (route.data['roles'] && route.data['roles'].indexOf(currentUser.role) === -1)  {
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
        return false;
      }
      else
      // current user exists, meaning user logged in, so return true
      return true;
    }
    else{
    // in general, if not currently logged in, redirect to login page with the return url
    this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    return false;
    }
   

    
    
  }
}