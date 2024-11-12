import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  private apiURL = "https://localhost:44329/api/Users/";

  onLogin(obj:any) {
    debugger;
    return this.http.post(this.apiURL + "Login",obj);
  }

  getUsers() {
    return this.http.get(this.apiURL);
  }
 
  createNewUser(obj:any) {
    debugger;
    return this.http.post(this.apiURL,obj);
  }
  updateUser(obj:any) {
    debugger;
    return this.http.put(this.apiURL + obj.userId,obj);
  }
  deletUserById(id: number) {
    return this.http.delete(this.apiURL + id)
  }

  GetUserById(id: number) {
    return this.http.get(this.apiURL + id)
  }
}
