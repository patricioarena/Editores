  export class EscritoTexto {
    Titulo: String;
    Texto: String;

    constructor(objeto?: any) {
      this.Titulo = objeto && objeto.Archivo || '';
      this.Texto = objeto && objeto.Extension || '';
    }
  }
