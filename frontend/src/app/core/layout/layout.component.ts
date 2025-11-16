import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { DrawerModule } from 'primeng/drawer';
import { MenubarModule } from 'primeng/menubar';
import { PanelMenuModule } from 'primeng/panelmenu';
import { RoutesEnum } from '../enums/routes.enum';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-layout',
  imports: [CommonModule, RouterOutlet, MenubarModule, DrawerModule, PanelMenuModule, ButtonModule],
  standalone: true,
  templateUrl: './layout.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LayoutComponent implements OnInit {

   drawerVisible = false;

  get isMobile() {
    return window.innerWidth <= 991;
  }

  menuItems: MenuItem[] = [];
    
ngOnInit(): void {
  this.menuItems= [
    {
      label: 'Início',
      icon: 'pi pi-home',
      routerLink: [RoutesEnum.HOME]
    },
    {
      label: 'Arquivos recepcionados',
      icon: 'pi pi-folder',
      routerLink: [RoutesEnum.LISTA_RECEPCOES_ARQUIVOS]
    },
    {
      label: 'Dashboard',
      icon: 'pi pi-chart-bar',
      routerLink: [RoutesEnum.DASHBOARDS_ARQUIVO]
    },
  ];
}

  toggleDrawer() {
    this.drawerVisible = !this.drawerVisible;
  }
}
