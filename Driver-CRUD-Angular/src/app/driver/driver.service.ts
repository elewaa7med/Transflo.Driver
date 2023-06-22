import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
    
import {  Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
   
import { ListResponseViewMode, GetDriver, PostDriver, UpdateDriver } from './DriverModel';
    
@Injectable({
  providedIn: 'root'
})
export class DriverService {
    
  private apiURL = "https://localhost:44301/api/Driver";
    
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }
  constructor(private httpClient: HttpClient) { }
    
  getAll(): Observable<ListResponseViewMode> {
    return this.httpClient.get<ListResponseViewMode>(this.apiURL + '/GetSortedAlphDriverList')
    .pipe(
      // catchError(this.errorHandler)
    )
  }
    
  create(PostDriver:PostDriver): Observable<GetDriver> {
    return this.httpClient.post<GetDriver>(this.apiURL + '/AddDriver', JSON.stringify(PostDriver), this.httpOptions)
    .pipe(
      // catchError(this.errorHandler)
    )
  }  
  create100(): Observable<string> {
    return this.httpClient.post<string>(this.apiURL + '/Add100RundomDriver',this.httpOptions)
    .pipe(
      // catchError(this.errorHandler)
    )
  } 
    
  find(id:string): Observable<GetDriver> {
    return this.httpClient.get<GetDriver>(this.apiURL + '/GetDriverbyId?id=' + id)
    .pipe(
      // catchError(this.errorHandler)
    )
  }
    
  update(id:string, updateDriver:UpdateDriver): Observable<GetDriver> {
    return this.httpClient.patch<GetDriver>(this.apiURL + '/EditDriver?Id=' + id, JSON.stringify(updateDriver), this.httpOptions)
    .pipe(
      // catchError(this.errorHandler)
    )
  }
    
  delete(id:string){
    return this.httpClient.delete<string>(this.apiURL + '/DeleteDriver?Id=' + id, this.httpOptions)
    .pipe(
      // catchError(this.errorHandler)
    )
  }
     
   
//   errorHandler(error) {
//     let errorMessage = '';
//     if(error.error instanceof ErrorEvent) {
//       errorMessage = error.error.message;
//     } else {
//       errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
//     }
//     return throwError(errorMessage);
//  }
}