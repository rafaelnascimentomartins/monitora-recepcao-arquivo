import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';



import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeuix/themes/aura'; // ou Material, Lara, Nora
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    provideAnimationsAsync(),
    providePrimeNG({
       theme: {
        preset: Aura,
        options: {
          prefix: 'p',
          darkModeSelector: 'html.dark-mode', // permite dark mode
          cssLayer: {
            name: 'primeng',
            order: 'defaults, primeng, utilities'
          }
        },
      },
      ripple: true
    })

  ]
};
