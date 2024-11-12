import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { authUserInterceptor } from './Interceptor/auth-user.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [provideHttpClient(withInterceptors([authUserInterceptor])), provideZoneChangeDetection({ eventCoalescing: true }), provideRouter(routes)]
};
