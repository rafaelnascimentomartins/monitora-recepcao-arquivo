import { Injectable } from "@angular/core";
import { BaseService } from "../../../core/services/base.service";
import { environment } from "../../../environment/environment";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { ToastService } from "../../../core/services/toast.service";
import { GetArquivoRecepcionadoDatatableRequest } from "../models/requests/get-arquivo-recepcionado-datatable-request";
import { GetArquivoRecepcionadoDatatableDto } from "../models/dtos/get-arquivo-recepcionado-datatable.dto";
import { GetArquivoRecepcionadoDatatableResponse } from "../models/responses/get-arquivo-recepcionado-datatable-response";
import { GetArquivoNaoRecepcionadoDatatableRequest } from "../models/requests/get-arquivo-nao-recepcionado-datatable-request";
import { GetArquivoNaoRecepcionadoDatatableResponse } from "../models/responses/get-arquivo-nao-recepcionado-datatable-response";
import { GetDashArquivoResumoStatusResponse } from "../models/responses/get-dash-arquivo-resumo-status-response";

@Injectable({
  providedIn: 'root' // ou omitido se você quiser scoped service na feature
})
export class DashboardService extends BaseService { 
    private baseUrl = `${environment.domain}/dashboard`;

    constructor(
        private http: HttpClient,
        protected override toastService: ToastService
    ) { super(toastService); }

        getResumoStatus() : Observable<GetDashArquivoResumoStatusResponse> {
        return this.handleRequest(
            this.http.get<GetDashArquivoResumoStatusResponse>(`${this.baseUrl}/GetResumoStatus`)
        );
    }
}