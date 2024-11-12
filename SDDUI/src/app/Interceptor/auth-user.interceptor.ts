import { HttpInterceptorFn } from '@angular/common/http';

export const authUserInterceptor: HttpInterceptorFn = (req, next) => {
  const sddAuthToken = localStorage.getItem("sddToken");
  const clonedReq = req.clone({
    setHeaders:{
      Authorization: `Bearer ${sddAuthToken}`
    }
  });

  return next(clonedReq);
};
