import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RequestLeaveComponent } from './request-leave/request-leave.component';
import { UserManagementComponent } from './user-management/user-management.component';

const routes: Routes = [
  {path:"login",component:LoginComponent},
  {path:"user-management",component:UserManagementComponent},
  {path:"request-leave",component:RequestLeaveComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
