import { BaseFilter } from "../../../../core/models/base-filter.model";

export class GetArquivoDatatableRequest extends BaseFilter {
    empresaId?: string;
    arquivoStatusId?: string;

    constructor(empresaId?: string, arquivoStatusId?: string)
    {
        super();
        empresaId = empresaId;
        arquivoStatusId = arquivoStatusId;
    }
}