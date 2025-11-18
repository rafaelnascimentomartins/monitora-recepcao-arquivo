export class Breadcrumb {
    texto: string;
    url: string;
    ativo: boolean;

    constructor(texto?: string, url?: string, ativo?: boolean) {
        this.texto = texto ?? "Início";
        this.url = url ?? "/";
        this.ativo = ativo ?? false;
    }
}