import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { ArquivoService } from '../../services/arquivo.service';
import { Subject, takeUntil } from 'rxjs';
import { MenuItem } from 'primeng/api';
import { CommonModule } from '@angular/common';
import { DatatableComponent } from '../../../../shared/components/datatable/datatable.component';
import { DatatableLazy } from '../../../../core/interfaces/datatable-lazy.interface';
import { DatatableColumn } from '../../../../core/models/datatable-column.model';
import { Breadcrumb } from 'primeng/breadcrumb';
import { RoutesEnum } from '../../../../core/enums/routes.enum';
import { GetArquivoRecepcionadoDatatableResponse } from '../../models/responses/get-arquivo-recepcionado-datatable-response';
import { GetArquivoRecepcionadoDatatableRequest } from '../../models/requests/get-arquivo-recepcionado-datatable-request';
import { GetArquivoRecepcionadoDatatableDto } from '../../models/dtos/get-arquivo-recepcionado-datatable.dto';
import { TabsModule } from 'primeng/tabs';
import { GetArquivoNaoRecepcionadoDatatableDto } from '../../models/dtos/get-arquivo-nao-recepcionado-datatable.dto';
import { GetArquivoNaoRecepcionadoDatatableRequest } from '../../models/requests/get-arquivo-nao-recepcionado-datatable-request';
import { GetArquivoNaoRecepcionadoDatatableResponse } from '../../models/responses/get-arquivo-nao-recepcionado-datatable-response';

@Component({
  selector: 'app-lista-arquivo',
  standalone: true,
  imports: [
    CommonModule, 
    DatatableComponent,
    Breadcrumb,
    TabsModule
  ],
  templateUrl: './lista-arquivo.component.html',
  styleUrls: ['./lista-arquivo.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ListaArquivoComponent implements OnInit, OnDestroy  {

  //INTERNOS
  private destroy$ = new Subject<void>();

  //FLAGS
  flagLoadingRecepcionados: boolean = false;
  flagLoadingNaoRecepcionados: boolean = false;

  //VARIÁVEIS
  arquivosRecepcionados: GetArquivoRecepcionadoDatatableDto[] = [];
  arquivosNaoRecepcionados: GetArquivoNaoRecepcionadoDatatableDto[] = [];
  columnsRecepcionados: DatatableColumn[] = [
    { field: 'dataInsercao', title: 'Data Importação' },
    { field: 'empresaDescricao', title: 'Empresa' },
    { field: 'dataProcessamento', title: 'Data proc.' },
    { field: 'estabelecimento', title: 'Estabelecimento' },
    { field: 'sequencia', title: 'Sequencia' },
    { field: 'periodoInicial', title: 'Período inicial' },
    { field: 'periodoFinal', title: 'Período final' },
  ];
   columnsNaoRecepcionados: DatatableColumn[] = [
    { field: 'estruturaImportada', title: 'Arquivo importação' },
    { field: 'motivos', title: 'Motivos' },
  ];
  breadcrumbItems: MenuItem[] = [];
  totalRecepcionadosRecords: number= 0;
  totalNaoRecepcionadosRecords: number= 0;
  activeValue = 'recep';

  constructor(
    private cdr: ChangeDetectorRef,
    private arquivoService: ArquivoService
  ) {}

  ngOnInit(): void {
    this.breadcrumbItems = [
      { label: 'Inicio', routerLink: RoutesEnum.HOME },
      { label: 'Lista de arquivos', disabled: true }
    ];
  }

  ngOnDestroy(): void {
    // Emite um valor e completa o Subject, cancelando todos os takeUntil
    this.destroy$.next();
    this.destroy$.complete();
  }

  onLazyLoadRecepcionados(event: DatatableLazy) {
    this.flagLoadingRecepcionados = true;
    this.cdr.markForCheck();

    // Monta a requisição baseada no LazyLoadEvent
    const request = new GetArquivoRecepcionadoDatatableRequest();
    request.page = event.page;
    request.pageSize = event.pageSize;
    request.sortDirection = event.sortDirection;
    request.sortField = event.sortField;

    this.arquivoService.getRecepcionadosDatatable(request)
    .pipe(takeUntil(this.destroy$))
    .subscribe({
      next: (res: GetArquivoRecepcionadoDatatableResponse) => {
        this.arquivosRecepcionados = [...(res.data ?? [])];
        this.totalRecepcionadosRecords = res.totalRecords ?? 0;  // ← AQUI DEFINIMOS O TOTAL
      },
      complete: () => {
        this.flagLoadingRecepcionados = false;
        this.cdr.markForCheck();
      }
    });
  }

  onLazyLoadNaoRecepcionados(event: DatatableLazy) {
    this.flagLoadingNaoRecepcionados = true;
    this.cdr.markForCheck();

    // Monta a requisição baseada no LazyLoadEvent
    const request = new GetArquivoNaoRecepcionadoDatatableRequest();
    request.page = event.page;
    request.pageSize = event.pageSize;
    request.sortDirection = event.sortDirection;
    request.sortField = event.sortField;

    this.arquivoService.getNaoRecepcionadosDatatable(request)
    .pipe(takeUntil(this.destroy$))
    .subscribe({
      next: (res: GetArquivoNaoRecepcionadoDatatableResponse) => {
        this.arquivosNaoRecepcionados = [...(res.data ?? [])];
        this.totalNaoRecepcionadosRecords = res.totalRecords ?? 0;  // ← AQUI DEFINIMOS O TOTAL
      },
      complete: () => {
        this.flagLoadingNaoRecepcionados = false;
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
