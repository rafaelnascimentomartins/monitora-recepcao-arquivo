import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { ArquivoService } from '../../services/arquivo.service';
import { GetArquivoDatatableRequest } from '../../models/requests/get-arquivo-datatable-request.model';
import { Subject, takeUntil } from 'rxjs';
import { LazyLoadEvent, MenuItem } from 'primeng/api';
import { GetArquivoDatatableResponse } from '../../models/responses/get-arquivo-datatable-response.model';
import { GetArquivoDatatableDto } from '../../models/dtos/get-arquivo-datatable.dto';
import { CommonModule } from '@angular/common';
import { DatatableComponent } from '../../../../shared/components/datatable/datatable.component';
import { DatatableLazy } from '../../../../core/interfaces/datatable-lazy.interface';
import { DatatableColumn } from '../../../../core/models/datatable-column.model';
import { Breadcrumb } from 'primeng/breadcrumb';

@Component({
  selector: 'app-lista-arquivo',
  imports: [
    CommonModule, 
    DatatableComponent,
    Breadcrumb
  ],
  templateUrl: './lista-arquivo.component.html',
  styles: `
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
`,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ListaArquivoComponent implements OnInit, OnDestroy  {

  //INTERNOS
  private destroy$ = new Subject<void>();

  //FLAGS
  flagLoadingLista: boolean = false;

  //VARIÁVEIS
  arquivos: GetArquivoDatatableDto[] = [];
  columns: DatatableColumn[] = [
    { field: 'dataInsercao', title: 'Data Importação' },
    { field: 'empresaDescricao', title: 'Empresa' },
    { field: 'arquivoStatusDescricao', title: 'Status', 
      type: 'tag', 
      tagColors: {
        'Recepcionado': 'success',
        'Não Recepcionado': 'info'
      } 
    },
    { field: 'dataProcessamento', title: 'Data proc.' },
    { field: 'estabelecimento', title: 'Estabelecimento' },
    { field: 'sequencia', title: 'Sequencia' },
    { field: 'periodoInicial', title: 'Período inicial' },
    { field: 'periodoFinal', title: 'Período final' },
  ];
  breadcrumbItems: MenuItem[] = [];
  totalRecords: number= 0;

  constructor(
    private cdr: ChangeDetectorRef,
    private arquivoService: ArquivoService
  ) {}

  ngOnInit(): void {
    this.breadcrumbItems = [
      { label: 'Home', routerLink: '/' },
      { label: 'Lista de arquivos', disabled: true }
    ];
  }

  ngOnDestroy(): void {
    // Emite um valor e completa o Subject, cancelando todos os takeUntil
    this.destroy$.next();
    this.destroy$.complete();
  }

  onLazyLoad(event: DatatableLazy) {
    this.flagLoadingLista = true;
    this.cdr.markForCheck();

    // Monta a requisição baseada no LazyLoadEvent
    const request = new GetArquivoDatatableRequest();
    request.page = event.page;
    request.pageSize = event.pageSize;
    request.sortDirection = event.sortDirection;
    request.sortField = event.sortField;

    this.arquivoService.getDatatable(request)
    .pipe(takeUntil(this.destroy$))
    .subscribe({
      next: (res: GetArquivoDatatableResponse) => {
        this.arquivos = [...(res.data ?? [])];
        this.totalRecords = res.totalRecords ?? 0;  // ← AQUI DEFINIMOS O TOTAL
      },
      complete: () => {
        this.flagLoadingLista = false;
        this.cdr.markForCheck();
      }
    });
  }

  // private buscarLista() : void {
  //   this.flagLoadingLista = true;

  //   var request = new GetArquivoDatatableRequest(
      
  //   ); 

  //   console.log(request);

  //   this.arquivoService.getDatatable(request).subscribe({
  //     next: (res : any) => {
  //       console.log(res);
  //     },
  //     complete: () => {
  //       this.flagLoadingLista = false;
  //     }
  //   }) 
  // };
}
