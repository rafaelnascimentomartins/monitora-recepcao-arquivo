import { Injectable } from "@angular/core";
import { BaseService } from "../../../core/services/base.service";
import { environment } from "../../../environment/environment";
import { HttpClient, HttpParams } from "@angular/common/http";
import { GetArquivoDatatableRequest } from "../models/requests/get-arquivo-datatable-request.model";
import { Observable } from "rxjs";
import { GetArquivoDatatableResponse } from "../models/responses/get-arquivo-datatable-response.model";
import { GetArquivoDashResumoStatusResponse } from "../models/responses/get-arquivo-dash-resumo-status-response.model";
import { ToastService } from "../../../core/services/toast.service";

@Injectable({
  providedIn: 'root' // ou omitido se você quiser scoped service na feature
})
export class ArquivoService extends BaseService { 
    private baseUrl = `${environment.domain}/arquivo`;

    constructor(
        private http: HttpClient,
        protected override toastService: ToastService
    ) { super(toastService); }

    getDatatable(request: GetArquivoDatatableRequest): Observable<GetArquivoDatatableResponse> {
        let params = new HttpParams();
    
        // Converte cada propriedade do objeto em query string
        Object.keys(request).forEach(key => {
            const value = request[key as keyof GetArquivoDatatableRequest];
            if (value !== undefined && value !== null) {
                params = params.set(key, value.toString());
            }
        });
    
        return this.handleRequest(
            this.http.get<GetArquivoDatatableResponse>(`${this.baseUrl}/GetDatatable`, {params})
        );
    }

    getDashResumoStatus() : Observable<GetArquivoDashResumoStatusResponse> {
        return this.handleRequest(
            this.http.get<GetArquivoDashResumoStatusResponse>(`${this.baseUrl}/GetDashResumoStatus`)
        );
    }
}