import { GetArquivoDatatableDto } from "../dtos/get-arquivo-datatable.dto";

export class GetArquivoDatatableResponse {
    data?: GetArquivoDatatableDto[];
    totalRecords?: number;
    page?:number;
    pageSize?:number;
}