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
export class ArquivoService extends BaseService { 
    private baseUrl = `${environment.domain}/arquivo`;

    constructor(
        private http: HttpClient,
        protected override toastService: ToastService
    ) { super(toastService); }

    getRecepcionadosDatatable(request: GetArquivoRecepcionadoDatatableRequest): Observable<GetArquivoRecepcionadoDatatableResponse> {
        let params = new HttpParams();
    
        // Converte cada propriedade do objeto em query string
        Object.keys(request).forEach(key => {
            const value = request[key as keyof GetArquivoRecepcionadoDatatableRequest];
            if (value !== undefined && value !== null) {
                params = params.set(key, value.toString());
            }
        });
    
        return this.handleRequest(
            this.http.get<GetArquivoRecepcionadoDatatableResponse>(`${this.baseUrl}/GetRecepcionadoDatatable`, {params})
        );
    }

    getNaoRecepcionadosDatatable(request: GetArquivoNaoRecepcionadoDatatableRequest): Observable<GetArquivoNaoRecepcionadoDatatableResponse> {
        let params = new HttpParams();
    
        // Converte cada propriedade do objeto em query string
        Object.keys(request).forEach(key => {
            const value = request[key as keyof GetArquivoNaoRecepcionadoDatatableRequest];
            if (value !== undefined && value !== null) {
                params = params.set(key, value.toString());
            }
        });
    
        return this.handleRequest(
            this.http.get<GetArquivoNaoRecepcionadoDatatableResponse>(`${this.baseUrl}/GetNaoRecepcionadoDatatable`, {params})
        );
    }
}