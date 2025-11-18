
export type TagSeverity =
  | 'success'
  | 'info'
  | 'warn'
  | 'danger'
  | 'secondary'
  | 'contrast';
  
  export class DatatableColumn {
    field!: string;
    title!: string;

    type?: 'text' | 'tag' | 'badge' | 'template'; // default: text


    // TAG CONFIGS
    tagColors?: { [value: string]: TagSeverity }; // mapeamento direto
    tagColorFn?: (value: any, row: any) => TagSeverity; // lógica dinâmica

    //isTag?: boolean = false;
    template?: (row: any) => string | number;
}