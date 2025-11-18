import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, ContentChildren, EventEmitter, Input, Output, QueryList, TemplateRef } from '@angular/core';
import { TableLazyLoadEvent, TableModule, TablePageEvent } from 'primeng/table';
import { DatatableLazy } from '../../interfaces/datatable-lazy.interface';
import { PaginatorModule } from 'primeng/paginator';
import { ButtonModule } from 'primeng/button';
import { DatatableColumn, TagSeverity } from '../../models/datatable-column.model';
import { TagModule } from 'primeng/tag';

@Component({
  selector: 'app-datatable',
  standalone: true,
  imports: [
    CommonModule, 
    TableModule,
    PaginatorModule,
    ButtonModule,
    TagModule
  ],
  templateUrl: './datatable.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DatatableComponent {
  /** Dados a serem exibidos */
  @Input() data: any[] = [];

  /** Total de registros (server-side) */
  @Input() totalRecords = 0;

  /** Loading do datatable */
  @Input() loading = false;

  /** Colunas do datatable: [{ field: 'name', header: 'Nome' }, ...] */
  @Input() columns: DatatableColumn[] = [];

  /** Evento emitido quando a tabela precisa recarregar dados */
  @Output() lazyLoad = new EventEmitter<DatatableLazy>();

  //VARIÁVEIS
  first = 0;
  rows = 10;
  
  @ContentChildren('cellTemplate') cellTemplates!: QueryList<any>;
  constructor() {}

  resolveTagSeverity(col: DatatableColumn, row: any): TagSeverity {
    const value = row[col.field];

    if (col.tagColors) {
      return col.tagColors[value] ?? 'info';
    }

    if (col.tagColorFn) {
      return col.tagColorFn(value, row);
    }

    return 'info';
  }

  onLazyLoad(event: TableLazyLoadEvent) {
    const pageSize = event.rows ?? 10;

    // --- MONTA OBJETO FINAL ---
    const request: DatatableLazy = {
      page: Math.floor((event.first ?? 0) / pageSize) + 1,
      pageSize: pageSize,
      sortField: Array.isArray(event.sortField) ? event.sortField[0] : event.sortField ?? null,
      sortDirection: event.sortOrder === 1 ? 'asc' : 'desc'
    };

    this.lazyLoad.emit(request);
  }
}
