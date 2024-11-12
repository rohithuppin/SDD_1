import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../service/user.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-create-user',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './create-user.component.html',
  styleUrl: './create-user.component.css'
})
export class CreateUserComponent {

  userObj: any = {
    "userId": 0,
    "fullname": "",
    "emailId": "",
    "mobileNo": "",
//    "addressLine1": "",
//    "addressLine2": "",
    "city": "",
//    "profilePicUrl": "",
    "passwordHash": "",
    "userRole": 2
  }
  loggedUserId: number = 0;
  userService = inject(UserService)
  actiavteRoute = inject(ActivatedRoute);
  currentId: number = 0;

  constructor() {
    debugger;       

    this.actiavteRoute.params.subscribe((res:any)=>{
      debugger;
      this.currentId = res.id;  
      if(this.currentId > 0)
        this.getUserById(this.currentId);    
    })
  }

  getUserById(id:number) {
    this.userService.GetUserById(id).subscribe((res:any)=>{
        this.userObj = res;
    })
  }

  onSave() {
    this.userService.createNewUser(this.userObj).subscribe((res:any)=>{
      if(res.newUserId > 0) {        
        alert("User Created Success")
        this.userObj.userId = res.newUserId;
      } else {
        alert(res.message)
      }
    })
  }
  onUpdate() {
    this.userService.updateUser(this.userObj).subscribe((res:any)=>{
      if(res > 0) {
        alert("User Update Success")
      } else {
        alert(res.message)
      }
    })
  }
}
