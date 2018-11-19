import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import { Observable} from "rxjs";

import { Message } from "../messages/message";

@Injectable({
  providedIn: 'root',
})
export class MessagesService 
{
    apipath : string = "http://localhost:55942/api/message";
    constructor(private http: HttpClient){ }

  sendMessage(message : Message) : Observable<any>
  {
     return this.http.post(this.apipath, message);
  }
  
}