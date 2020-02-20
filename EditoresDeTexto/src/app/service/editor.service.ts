import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse, } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { HttpToolsService } from './http-tools.service';
import { promise } from 'protractor';
import { EscritoTexto } from '../modelos/EscritoTexto';
import { ResponseApi } from '../modelos/ResponseApi';

@Injectable({
  providedIn: 'root'
})
export class EditorService {

  apiurl = environment.API_URL;

  // tslint:disable-next-line: no-shadowed-variable
  constructor(private HttpClient: HttpClient, private HttpToolsService: HttpToolsService) { }

  private extractData(res) {
    const body = res.data;
    return body || {};
  }

  nuevoEscritoTexto(escritoTexto): Observable<EscritoTexto> {
    const url = `${this.apiurl}EscritosTexto/nuevo`;
    return this.HttpClient
      .post<ResponseApi<EscritoTexto>>(url, escritoTexto)
      .pipe(map(res => res.data));
    }

  nuevoEscritoTexto2(escritoTexto): Observable<EscritoTexto> {
    const url = `${this.apiurl}EscritosTexto/nuevo`;
    return this.HttpClient.post( url, escritoTexto, {
      headers: new HttpHeaders()
        .set('Content-Type', 'application/json')
        .append('Access-Control-Allow-Origin', '*'),
      responseType: 'text'
    }).pipe(map(res => res as any));
  }










}
