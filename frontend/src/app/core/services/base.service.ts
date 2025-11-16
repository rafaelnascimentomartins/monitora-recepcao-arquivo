import { Injectable } from "@angular/core";
import { Observable, catchError, map, throwError } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class BaseService {

    constructor() {}

  // Função genérica para tratar o observable HTTP
  protected handleRequest<T>(observable: Observable<T>): Observable<T> {
    return observable.pipe(
      map((res: T) => {
        // Aqui você pode adicionar manipulação de dados antes de retornar
        return res; 
      }),
      catchError((error) => {
        //FLUENT VALIDATION
        const fluentValidation = error.error?.errors;
        if(error.status === 400 && fluentValidation) {
            const messages: string[] = [];
            fluentValidation.forEach((fv:any) => {
                messages.push(fv.errorMessage);
            });

            console.log(messages);
            //this.dialogService.validation(messages);
        }

        //ADICIONAR LOG?
        console.log('Erro na requisição:', error); // log global
        return throwError(() => error); // propaga o erro para o component
      })
    );
  }
}