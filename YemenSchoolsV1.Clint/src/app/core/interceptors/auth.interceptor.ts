import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const clonedRequste = req.clone({
    withCredentials: true
  })
  return next(clonedRequste);
};
