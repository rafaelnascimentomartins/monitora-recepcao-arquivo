import { GetArquivoDashResumoStatusDto } from "../dtos/get-arquivo-dash-resumo-status.dto";

export class GetArquivoDashResumoStatusResponse {
    data?: GetArquivoDashResumoStatusDto[];
    total?: number;
    dataGeracao?:Date;
}