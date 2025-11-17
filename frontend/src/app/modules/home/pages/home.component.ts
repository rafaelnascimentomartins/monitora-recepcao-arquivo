import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { MegaMenuItem } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { AvatarModule } from 'primeng/avatar';
import { MegaMenu } from 'primeng/megamenu';
import { CardModule } from 'primeng/card';
import { RoutesEnum } from '../../../core/enums/routes.enum';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    CommonModule, 
    BreadcrumbModule ,
    CardModule
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  
    quickMenu = [
    {
      title: 'Arquivos Recebidos',
      desc: 'Consultar todos os arquivos recebidos.',
      icon: 'pi pi-folder',
      routerLink: RoutesEnum.LISTA_RECEPCOES_ARQUIVOS
    },
    {
      title: 'Dashboards de Status',
      desc: 'Indicadores de recebimento e processamento.',
      icon: 'pi pi-chart-bar',
      routerLink: RoutesEnum.DASHBOARDS_ARQUIVO
    }
  ];

  constructor(private router: Router) { }

  ngOnInit() {
  }

  go(route: string) {
    this.router.navigateByUrl(route);
  }
}