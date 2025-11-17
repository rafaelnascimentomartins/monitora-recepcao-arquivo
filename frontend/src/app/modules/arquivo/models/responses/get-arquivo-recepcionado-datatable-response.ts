import { GetArquivoRecepcionadoDatatableDto } from "../dtos/get-arquivo-recepcionado-datatable.dto";

export class GetArquivoRecepcionadoDatatableResponse {
    data?: GetArquivoRecepcionadoDatatableDto[];
    totalRecords?: number;
    page?:number;
    pageSize?:number;
}