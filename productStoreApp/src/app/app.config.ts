import { type ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { provideClientHydration } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient } from '@angular/common/http';
import routes from './app.routes';
import { authInterceptorProvider } from '../interceptors/auth.interceptor';
import { refreshInterceptorProvider } from '../interceptors/refresh.interceptor';

const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideClientHydration(),
    provideAnimations(),
    provideHttpClient(),
    refreshInterceptorProvider,
    authInterceptorProvider,
  ],
};

export default appConfig;
