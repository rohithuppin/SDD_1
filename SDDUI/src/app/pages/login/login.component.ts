import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../service/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  loginObj: any = {
    "EmailId": "",
    "Password": ""
  }
  userService =  inject(UserService);
  router = inject(Router)

  login() {
    debugger;
    this.userService.onLogin(this.loginObj).subscribe((res:any)=>{
      debugger;
      if(res.user) {
        localStorage.setItem("sddToken",JSON.stringify(res.token));
        localStorage.setItem("sddUserRole",JSON.stringify(res.user.userRole)); // Role used to Role based authentication
        if(res.user.userRole == 1 ) {
          this.router.navigateByUrl("user-list");
        } else {
          this.router.navigateByUrl("editUser/"+ res.user.userId);
        }
        
      } else {
        alert(res.message)
      }
    })
  }
}
