import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ArquivoService } from '../../../arquivo/services/arquivo.service';
import { Subject, takeUntil } from 'rxjs';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { ChartPieComponent } from '../../../../shared/components/charts/chart-pie/chart-pie.component';
import { DividerModule } from 'primeng/divider';
import { ChartPieItem } from '../../../../shared/interfaces/chart-pie-item.interface';
import { Breadcrumb } from 'primeng/breadcrumb';
import { MenuItem } from 'primeng/api';
import { RoutesEnum } from '../../../../core/enums/routes.enum';
import { GetDashArquivoResumoStatusResponse } from '../../models/responses/get-dash-arquivo-resumo-status-response';
import { DashboardService } from '../../services/dashboard.service';

@Component({
  selector: 'app-dashboard-resumo-status',
  imports: [CommonModule, CardModule, DividerModule, Breadcrumb, ChartPieComponent],
  standalone: true,
  templateUrl: './dashboard-resumo-status.component.html',
  styles: `
  :host ::ng-deep .p-breadcrumb {
  background: transparent;
  border: none;
  padding-left: 0;
  padding-right: 0;
}

:host ::ng-deep .p-breadcrumb .p-menuitem-link {
  font-family: "Inter", sans-serif;
  font-size: 0.9rem;
  color: var(--p-primary-600);
  font-weight: 500;
}
  .page-header {
  margin-bottom: 1.5rem;
}

.page-title {
  margin-top: 0.75rem;
  font-size: 1.5rem;
  font-weight: 600;
  color: var(--p-text-color);
  letter-spacing: -0.5px;
  font-family: "Inter", sans-serif;
}
  `,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardResumoStatusComponent implements OnInit {

  //INTERNOS
  private destroy$ = new Subject<void>();

  //VARIAVEIS
  pieData: ChartPieItem[] = [];
  total: number = 0;
  breadcrumbItems: MenuItem[] = [];

  constructor(
    private cdr: ChangeDetectorRef,
    private arquivoService: ArquivoService,
    private dashboardService: DashboardService) {}

  ngOnInit(): void {
    this.breadcrumbItems = [
      { label: 'Inicio', routerLink: RoutesEnum.HOME },
      { label: 'Dashboard Resumo Status', disabled: true }
    ];
    this.buscarResumoStatus();
  }

  ngOnDestroy(): void {
    // Emite um valor e completa o Subject, cancelando todos os takeUntil
    this.destroy$.next();
    this.destroy$.complete();
  }

  getStatusValue(label: string): number {
    return this.pieData.find(x => x.label === label)?.value || 0;
  }

  private buscarResumoStatus() : void {    
    this.dashboardService.getResumoStatus()
        .pipe(takeUntil(this.destroy$))
        .subscribe({
          next: (res: GetDashArquivoResumoStatusResponse) => {

            this.pieData = [
              {
                label: 'Recepcionados',
                value: res.qtdRecepcionados ?? 0
              },
              {
                label: 'Não Recepcionados',
                value: res.qtdNaoRecepcionados ?? 0
              },
            ];
            this.pieData = [...this.pieData]; // necessário para OnPush
            this.total = (res.qtdRecepcionados ?? 0) + (res.qtdNaoRecepcionados ?? 0);
          },
          complete: () => {
            this.cdr.markForCheck();
          }
        });
  }
}
