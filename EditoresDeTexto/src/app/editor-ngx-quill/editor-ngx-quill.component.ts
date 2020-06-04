import { Component, OnInit } from '@angular/core';
import { EscritoTexto } from '../modelos/EscritoTexto';
import { EditorService } from '../service/editor.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { NotificationService } from '../service/notification.service';
import { TitleService } from '../service/title.service';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-editor-ngx-quill',
  templateUrl: './editor-ngx-quill.component.html',
  styleUrls: ['./editor-ngx-quill.component.scss']
})
export class EditorNgxQuillComponent implements OnInit {
  // Editor
  editorStyle = {
    height: '35rem',
  };

  config = {
    toolbar: [
      ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
      ['blockquote', 'code-block'],

      [{ 'header': 1 }, { 'header': 2 }],               // custom button values
      [{ 'list': 'ordered' }, { 'list': 'bullet' }],
      [{ 'script': 'sub' }, { 'script': 'super' }],      // superscript/subscript
      [{ 'indent': '-1' }, { 'indent': '+1' }],          // outdent/indent
      [{ 'direction': 'rtl' }],                         // text direction

      [{ 'size': ['small', false, 'large', 'huge'] }],  // custom dropdown
      [{ 'header': [1, 2, 3, 4, 5, 6, false] }],

      [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme
      [{ 'font': [] }],
      [{ 'align': [] }],

      ['clean'],                                         // remove formatting button

      ['link', 'image', 'video']                         // link and image, video
    ]
  };
  // Editor
  title: String;
  escritoTexto_Titulo: String;
  escritoTexto_Texto: String;
  textPreview: String = '';
  escritoTexto: EscritoTexto;
  isEnabled = false;
  showPreview = false;
  showExample = false;
  fileUrl;
  response;
  baseUrl = environment.BASE_URL;

  // tslint:disable:max-line-length
  example = `<h1 class="ql-align-center">Editor de texto</h1><p><br></p><p>Un&nbsp;<strong>editor de texto&nbsp;
  </strong>es un&nbsp;<a href="https://es.wikipedia.org/wiki/Programa_inform%C3%A1tico" rel="noopener noreferrer"
  target="_blank" style="color: rgb(11, 0, 128);">programa informático</a>&nbsp;que permite crear y modificar&nbsp;
  <a href="https://es.wikipedia.org/wiki/Archivo_(inform%C3%A1tica)" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">
  archivos digitales</a>&nbsp;compuestos únicamente por textos sin formato, conocidos comúnmente como&nbsp;
  <a href="https://es.wikipedia.org/wiki/Archivo_de_texto" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">
  archivos de texto</a>&nbsp;o <span style="background-color: rgb(255, 255, 0);">“texto plano”
  </span>. El programa lee el archivo e interpreta los&nbsp;<a href="https://es.wikipedia.org/wiki/Byte" rel="noopener noreferrer"
  target="_blank" style="color: rgb(11, 0, 128);">bytes</a>&nbsp;leídos según el&nbsp;<a href="https://es.wikipedia.org/wiki/Codificaci%C3%B3n_de_caracteres"
  rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">código de caracteres</a>&nbsp;que usa el editor.
  Es comúnmente de 7- u 8-bits en&nbsp;<a href="https://es.wikipedia.org/wiki/ASCII" rel="noopener noreferrer"
  target="_blank" style="color: rgb(11, 0, 128);">ASCII</a>&nbsp;o&nbsp;<a href="https://es.wikipedia.org/wiki/UTF-8"
  rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">UTF-8</a>, rara vez&nbsp;<a href="https://es.wikipedia.org/wiki/EBCDIC"
   rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">EBCDIC</a>.</p><p><br></p><p>Por ejemplo,
   un editor ASCII de 88 bits que lee el&nbsp;<a href="https://es.wikipedia.org/wiki/N%C3%BAmero_binario" rel="noopener

   noreferrer" target="_blank" style="color: rgb(11, 0, 128);">número binario</a>&nbsp;<code style="color: rgb(0, 0, 0);
   background-color: rgb(248, 249, 250);">0110 0001</code>&nbsp;(<a href="https://es.wikipedia.org/wiki/Sistema_decimal"
   rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">decimal</a>&nbsp;97 o&nbsp;<a href="https://es.wikipedia.org/wiki/Hexadecimal"
    rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">hexadecimal</a>&nbsp;61) en el archivo lo representará en la pantalla por la
    figura&nbsp;<code style="color: rgb(0, 0, 0); background-color: rgb(248, 249, 250);">a</code>, que el usuario reconoce como la letra "a" y ofrecerá al
    usuario las funciones necesarias para cambiar el número binario en el archivo.</p><p><br></p><p>Los editores de texto son incluidos en el&nbsp;
    <a href="https://es.wikipedia.org/wiki/Sistema_operativo" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">sistema operativo
    </a>&nbsp;o en algún&nbsp;<a href="https://es.wikipedia.org/wiki/Paquete_de_software" rel="noopener noreferrer" target="_blank" style="color:
    rgb(11, 0, 128);">paquete de software</a>&nbsp;instalado, y se usan cuando se deben crear o modificar archivos de
    texto como archivos de configuración,&nbsp;<a href="https://es.wikipedia.org/wiki/Lenguaje_de_programaci%C3%B3n_interpretado" rel="noopener noreferrer"
     target="_blank" style="color: rgb(11, 0, 128);">lenguaje de programación interpretado</a>&nbsp;(<a href="https://es.wikipedia.org/wiki/Script"
     rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);"><em>scripts</em></a>) o el&nbsp;<a href="https://es.wikipedia.org/wiki/C%C3%B3digo_fuente"
     rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">código fuente</a>&nbsp;de algún programa.</p><p><br></p><p>El archivo creado por un editor de texto incluye por
     convención en&nbsp;<a href="https://es.wikipedia.org/wiki/DOS" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">DOS</a>&nbsp;y&nbsp;
     <a href="https://es.wikipedia.org/wiki/Microsoft_Windows" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">Microsoft Windows</a>&nbsp;la&nbsp;
     <a href="https://es.wikipedia.org/wiki/Extensi%C3%B3n_de_archivo" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">extensión
     </a>&nbsp;".txt", aunque pueda ser cambiada a cualquier otra con posterioridad. Tanto&nbsp;
     <a href="https://es.wikipedia.org/wiki/Unix" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">
     Unix</a>&nbsp;como&nbsp;<a href="https://es.wikipedia.org/wiki/N%C3%BAcleo_Linux" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">Linux
     </a>&nbsp;dan al usuario total libertad en la denominación de sus archivos.</p><p><br></p><p><span style="background-color: rgb(255, 255, 0);">
     Al trasladar archivos de texto de un sistema operativo a otro se debe considerar que existen al menos dos convenciones diferentes</span> para señalar el&nbsp;
     <a href="https://es.wikipedia.org/wiki/Nueva_l%C3%ADnea" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">término de una línea
     </a>&nbsp;o lo que es lo mismo una&nbsp;<em>nueva línea</em>: Unix y<span style="color: rgb(230, 0, 0);"> Linux</span> usan solo&nbsp;
     <a href="https://es.wikipedia.org/wiki/Retorno_de_carro" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">retorno de carro</a>&nbsp;en cambio
      <span style="color: rgb(230, 0, 0);">Microsoft </span>Windows utiliza retorno de carro y&nbsp;<a href="https://es.wikipedia.org/wiki/Salto_de_l%C3%ADnea" rel="noopener noreferrer"
      target="_blank" style="color: rgb(11, 0, 128);">salto de línea</a>.</p><p><br></p><h2>Diferencia entre texto plano y archivos de texto con diagramación</h2><p>Los editores de textos
      "planos" se distinguen de los&nbsp;<a href="https://es.wikipedia.org/wiki/Procesador_de_texto" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">procesadores
      de texto</a>&nbsp;en que se usan para escribir solo texto, sin formato y sin imágenes, es decir sin&nbsp;<a href="https://es.wikipedia.org/wiki/Diagramaci%C3%B3n" rel="noopener noreferrer"
      target="_blank" style="color: rgb(11, 0, 128);">diagramación</a>.</p><p><br></p><ul><li>El texto plano es representado en el editor mostrando todos los caracteres presentes en el archivo.
       Los únicos caracteres de formato son los&nbsp;<a href="https://es.wikipedia.org/wiki/Car%C3%A1cter_de_control" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">
       caracteres de control</a>&nbsp;del respectivo código de caracteres. En la práctica, estos son: salto de línea, retorno de carro y&nbsp;<a href="https://es.wikipedia.org/wiki/Tabulador"
       rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">tabulación horizontal</a>. El código de caracteres más usado es el ASCII.</li></ul><p><br></p><ul><li>Generalmente,
       los documentos creados por un procesador de texto contienen más caracteres de control para darle al texto un formato o diagramación particular, a menudo protegidos de ser copiados por
       una&nbsp;<a href="https://es.wikipedia.org/wiki/Marca_(registro)" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">marca registrada</a>&nbsp;como por ejemplo
       negrita, cursiva, columnas, tablas, tipografías, etcétera. En un comienzo se utilizaron tales formatos solo en&nbsp;<a href="https://es.wikipedia.org/wiki/Autoedici%C3%B3n" rel="noopener
       noreferrer" target="_blank" style="color: rgb(11, 0, 128);">autoedición</a>, pero hoy se utilizan incluso en el procesador de texto más sencillo.<a href="https://es.wikipedia.org/wiki/Editor_de_texto#cite_note-1"
        rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);"><sup>1</sup></a>​</li></ul><p><br></p><ul><li>En la mayoría de los casos, los procesadores de texto pueden almacenar un texto plano en un archivo de texto plano,
        pero se le debe ordenar explícitamente que se desea esa opción, de otra manera podría guardarlo con algún formato especial.</li></ul><p><br></p><h2>Tipos de editores de texto</h2><p><br>
        </p><p>Hay una gran variedad de editores de texto. Algunos son de uso general,
        mientras que otros están diseñados para escribir o programar en un&nbsp;<a href="https://es.wikipedia.org/wiki/Lenguaje_de_programaci%C3%B3n" rel="noopener noreferrer" target="_blank"
         style="color: rgb(11, 0, 128);">lenguaje</a>. Algunos son muy sencillos, mientras que otros
        tienen implementadas gran cantidad de funciones. El editor de texto debe ser considerado como una herramienta de trabajo del&nbsp;<a href="https://es.wikipedia.org/wiki/Programador"
        rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">programador</a>&nbsp;o administrador de la máquina. Como herramienta permite realizar ciertos trabajos,
         pero también requiere de aprendizaje para que el usuario conozca y obtenga destreza en su uso. La llamada "<a href="https://es.wikipedia.org/wiki/Curva_de_aprendizaje" rel="noopener
         noreferrer" target="_blank" style="color: rgb(11, 0, 128);">curva de aprendizaje</a>" es una representación de la destreza adquirida a lo largo del tiempo de aprendizaje.
          Un editor puede ofrecer muchas funciones, pero si su curva de aprendizaje es muy larga,
        puede desanimar el aprendizaje y terminará siendo dejado de lado. Puede que un editor tenga una curva de aprendizaje muy empinada y corta, pero si no ofrece muchas funciones el
        usuario le reemplazará por otro más productivo. Es decir la elección del editor más apropiado depende de varios factores, alguno de ellos muy subjetivos. Esta coyuntura de intereses
        ha dado lugar a largas discusiones sobre la respuesta a la pregunta: <em>¿cuál es
        el mejor editor de texto?</em> Muchos editores originalmente salidos de Unix o Linux, han sido&nbsp;<a href="https://es.wikipedia.org/wiki/Portabilidad" rel="noopener noreferrer"
        target="_blank" style="color: rgb(11, 0, 128);">portados</a>&nbsp;a otros sistemas operativos, lo que permite trabajar en otros sistemas sin tener que aprender el uso de otro editor.
        </p><p><br></p><blockquote><em class="ql-font-serif">"Algunos editores son sencillos mientras que otros ofrecen una amplia gama de funciones."</em></blockquote><p><br></p><p>Editores
        para profesionales deben ser capaces de leer archivos de gran extensión, mayor que la capacidad de
         la&nbsp;<a href="https://es.wikipedia.org/wiki/Memoria_de_acceso_aleatorio" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">memoria de acceso aleatorio</a>&nbsp;
         de la máquina y
         también arrancar rápidamente, ya que el tiempo de espera disminuye la concentración y disminuye de por sí la productividad. Los editores de texto sirven para muchas cosas porque facilitan
         el trabajo.
         </p><p><br></p><p>Algunos editores de texto incluyen el uso de lenguajes de programación para automatizar engorrosos o repetidos procedimientos a realizar en el texto. Por ejemplo,&nbsp;
         <a href="https://es.wikipedia.org/wiki/Emacs" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">Emacs</a>&nbsp;puede ser adaptado a las necesidades del usuario, incluso
          las combinaciones de teclas para ejecutar funciones
          pueden ser adaptadas y es programable en&nbsp;<a href="https://es.wikipedia.org/wiki/Lisp" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">Lisp</a>.</p><p><br></p><p>
          Muchos editores de texto incluyen&nbsp;<a href="https://es.wikipedia.org/wiki/Coloreado_de_sintaxis"
          rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">coloreado de sintaxis</a>&nbsp;y funciones que ofrecen al usuario completar una palabra iniciada usando para ello la
          configuración.</p><p>Algunas funciones especiales son:</p><p><br></p><ul><li>Editores diseñados para un lenguaje de programación determinado, con coloreado de sintaxis,&nbsp;
          <a href="https://es.wikipedia.org/wiki/Macro"
          rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">macros</a>, completación de palabras, etcétera.</li></ul><p><br></p><ul><li>Editores con regiones plegables.
          A veces no todo el texto es relevante para el usuario. Con este tipo de editores ciertas regiones con texto irrelevante pueden ser plegadas, escondidas, mostrando al usuario solo lo
          importante del texto.</li></ul><p><br></p><ul><li>Un&nbsp;<a href="https://es.wikipedia.org/wiki/Entorno_de_desarrollo_integrado" rel="noopener noreferrer" target="_blank" style="color:
           rgb(11, 0, 128);">entorno de desarrollo integrado</a>&nbsp;es un programa que incluye un editor y otras herramientas de trabajo, como&nbsp;
           <a href="https://es.wikipedia.org/wiki/Compilador" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">compiladores</a>, extractores de&nbsp;
           <a href="https://es.wikipedia.org/wiki/Diff" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">diferencias</a>&nbsp;entre dos textos,&nbsp;
           <a href="https://es.wikipedia.org/wiki/Repositorio" rel="noopener noreferrer" target="_blank" style="color: rgb(11, 0, 128);">repositorios</a>, etcétera, incluidos en un solo programa.
           </li></ul><p><br></p><p><em class="ql-size-small">Fuente: </em><a href="https://es.wikipedia.org/wiki/Editor_de_texto" rel="noopener noreferrer" target="_blank">
           https://es.wikipedia.org/wiki/Editor_de_texto</a></p>`;

  constructor(
    public editorService: EditorService,
    public spinner: NgxSpinnerService,
    private notificationService: NotificationService,
    private titleServive: TitleService
  ) { }

  ngOnInit() {
    this.title = this.titleServive.APP_TITLE;
    this.escritoTexto_Titulo = '';
    this.escritoTexto_Texto = '';
  }

  Guardar() {
    this.isEnabled = false;
    this.showPreview = false;
    this.textPreview = '';
    // console.log("TextArea::text: " + this.text);
    this.escritoTexto = new EscritoTexto;
    this.escritoTexto.Titulo = this.escritoTexto_Titulo;
    this.escritoTexto.Texto = this.escritoTexto_Texto;
    // this.objeto.Extension = '.xml';
    this.editorService.nuevoEscritoTexto(this.escritoTexto).subscribe(
      resp => {
        // if (resp === '-1') {
        //   // this.notificationService.show('Certificado', 'Certificado no valido', 'info');
        // } else if (resp === '-2') {
        //   this.notificationService.showInfo('Certificado', 'Certificado no valido');
        // } else {
        //   this.response = resp;
        //   this.isEnabled = true;
        // }
      }, err => {
        // console.log(JSON.parse(err.error).ExceptionMessage)
        // const message = JSON.parse(err.error).ExceptionMessage;
        // this.notificationService.showError('Error', message);
      });
  }

  preview() {
    this.showPreview = this.showPreview ? false : true;
    this.textPreview = this.escritoTexto_Texto;
  }

  loadExample() {
    this.escritoTexto_Texto = this.example;
    this.escritoTexto_Titulo = 'Ejemplo múltiple';
    this.showExample = true;
  }

  unLoadExample() {
    this.escritoTexto_Texto = '';
    this.escritoTexto_Titulo = '';
    this.showExample = false;
  }

}
