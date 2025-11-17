export interface DatatableLazy {
  page: number;            // Número da página atual
  pageSize: number;        // Quantidade por página
  sortField: string | null;      // Nome da coluna que está ordenando
  sortDirection: 'asc' | 'desc'; // Direção
  filters?: { [key: string]: any }; // (Opcional) filtros enviados pelo PrimeNG
}
