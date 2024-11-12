import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);  // Inject the Router to navigate
  const activatedRoute = inject(ActivatedRoute);  // Inject ActivatedRoute to access route data

  // Get the saved token and user role from localStorage
  const savedToken = localStorage.getItem("sddToken");
  const sddUserRole = localStorage.getItem("sddUserRole");  // Assumes you saved the user role in localStorage

  // Access the required role from the activated route's data
  const requiredRole = activatedRoute.snapshot.data['userrole'];  // Use snapshot to access route data
debugger;
  if (savedToken) {
    // Check if the user has the required role
    //if (requiredRole && requiredRole.toString() === sddUserRole) { check why requiredRole is coming as undefined
    //   if (sddUserRole == "1") {
    //   return true;  // Allow access if the role matches
    // } else {
    //   alert('You are not authorized to view this screen.');
    //   //router.navigateByUrl('/unauthorized');  // Redirect to an unauthorized page or any other page
    //   return false;
    
    //}    
      return true;
  } else {
    // If no token is found, redirect to the login page
    router.navigateByUrl("/login");
    return false;
  }
};