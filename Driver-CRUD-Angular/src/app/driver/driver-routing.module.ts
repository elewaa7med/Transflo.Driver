import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './index/index.component';
import { ViewComponent } from './view/view.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { DeleteComponent } from './delete/delete.component';
   
const routes: Routes = [
  { path: 'driver', redirectTo: 'driver/index', pathMatch: 'full'},
  { path: 'driver/index', component: IndexComponent },
  { path: 'driver/:Id/view', component: ViewComponent },
  { path: 'driver/create', component: CreateComponent },
  { path: 'driver/:Id/edit', component: EditComponent } ,
  { path: 'driver/:Id/delete', component: DeleteComponent } 
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DriverRoutingModule { }
