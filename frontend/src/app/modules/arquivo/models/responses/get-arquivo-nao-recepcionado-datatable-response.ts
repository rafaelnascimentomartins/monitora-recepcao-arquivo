import { GetArquivoNaoRecepcionadoDatatableDto } from "../dtos/get-arquivo-nao-recepcionado-datatable.dto";

export class GetArquivoNaoRecepcionadoDatatableResponse {
    data?: GetArquivoNaoRecepcionadoDatatableDto[];
    totalRecords?: number;
    page?:number;
    pageSize?:number;
}