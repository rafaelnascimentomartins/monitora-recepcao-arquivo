export class GetArquivoDatatableDto {
    identificador: string = '';
    dataInsercao?: Date;
    estabelecimento?:string;
    dataProcessamento?: Date;
    periodoInicial?: Date;
    periodoFinal?: Date;
    sequencia?:string;
    empresaDescricao?:string;
    arquivoStatusDescricao?:string;
}