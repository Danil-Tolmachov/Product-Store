import { bootstrapApplication } from '@angular/platform-browser';
import { enableProdMode, isDevMode } from '@angular/core';
import appConfig from './app/app.config';
import AppComponent from './app/app.component';

if (!isDevMode()) {
  enableProdMode();
}

bootstrapApplication(AppComponent, appConfig);
