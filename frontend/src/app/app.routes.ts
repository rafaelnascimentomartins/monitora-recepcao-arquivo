import { Routes } from '@angular/router';
import { RoutesEnum } from './core/enums/routes.enum';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./core/layout/layout.component').then((m) => m.LayoutComponent),
      children: [
        { path: '', redirectTo: RoutesEnum.HOME, pathMatch: 'full' },
        {
          path: RoutesEnum.HOME,
          loadComponent: () =>
            import(
              './modules/home/pages/home.component'
            ).then((m) => m.HomeComponent),
        },
        {
          path: RoutesEnum.LISTA_RECEPCOES_ARQUIVOS,
          loadComponent: () =>
            import(
              './modules/arquivo/pages/lista-arquivo/lista-arquivo.component'
            ).then((m) => m.ListaArquivoComponent),
        }
    ],
  },

  // Rota 404
  { path: '**', redirectTo: '' },
];
