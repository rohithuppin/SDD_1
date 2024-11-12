import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { LayoutComponent } from './pages/layout/layout.component';
import { UserListComponent } from './pages/user-list/user-list.component';
import { CreateUserComponent } from './pages/create-user/create-user.component';
import { authGuard } from './guard/auth.guard';

export const routes: Routes = [
    {
        path:'',
        redirectTo: 'login',
        pathMatch: 'full'
    },
    {
        path: 'login',
        component:LoginComponent
    },
    {
        path:'',
        component:LayoutComponent,
        children: [
            {
                path:'user-list',
                component:UserListComponent,
                canActivate:[authGuard],
                data: { userrole: 1 } 
            }, 
            {
                path:'createUser/:id=0',
                component:CreateUserComponent,
                canActivate:[authGuard]
            }, 
            {
                path:'editUser/:id',
                component:CreateUserComponent,
                canActivate:[authGuard]
            }
        ]
    },
    { 
        path: '**', 
        redirectTo: '/login' 
    } 
];
