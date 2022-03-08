import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(private _http: HttpClient) { }


  setHeader(){
    const headers = { 'content-type': 'application/json'} ;
    return headers;
  }

  getList(numpage: string): Observable<any> {
    return this._http.get('http://localhost:5000/api/student/list/page=' + numpage).pipe(
      map(data => {
        return data;
      }),
    );
  }

  getByID(id: string): Observable<any> {
    return this._http.get('http://localhost:5000/api/student/' + id).pipe(
      map(data => {
        return data;
      }),
    );
  }

  delete(id: string): Observable<any> {
    return this._http.delete('http://localhost:5000/api/student/delete/' + id).pipe(
      map(data => {
        return data;
      }),
    );
  }
  
  add(obj: any): Observable<any> {
    const body=JSON.stringify(obj);
    return this._http.post('http://localhost:5000/api/student/add',body,{'headers':this.setHeader()}).pipe(
      map(data => {
        return data;
      }),
    );
  }

  put(obj: any,id:string): Observable<any> {
    const body=JSON.stringify(obj);
    return this._http.put('http://localhost:5000/api/student/update/'+id,body,{'headers':this.setHeader()}).pipe(
      map(data => {
        return data;
      }),
    );
  }
}
