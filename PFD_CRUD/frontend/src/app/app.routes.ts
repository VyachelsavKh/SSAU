import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { MainComponent } from './pages/main/main.component';
import { MyPaymentsComponent } from './pages/my-payments/my-payments.component';
import { PersonalDataComponent } from './pages/personal-data/personal-data.component';
import { IdentityDocumentComponent } from './pages/identity-document/identity-document.component';
import { AddressComponent } from './pages/address/address.component';
import { CitizensComponent } from './pages/citizens/citizens.component';
import { DictionariesComponent } from './pages/dictionaries/dictionaries.component';
import { AuthGuard } from './auth.guard'
import { ForbiddenComponent } from './pages/forbidden/forbidden.component';
import { PaymentsComponent } from './pages/payments/payments.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'login' },
  { path: 'login', component: LoginComponent },
  { 
    path: 'main', 
    component: MainComponent,
    children: [
      { 
        path: 'my-payments', 
        component: MyPaymentsComponent,
        canActivate: [AuthGuard],
        data: { roles: ['ROLE_USER', 'ROLE_WORKER', 'ROLE_ADMIN'] }
      },
      { 
        path: 'personal-data', 
        component: PersonalDataComponent,
        canActivate: [AuthGuard],
        data: { roles: ['ROLE_USER', 'ROLE_WORKER', 'ROLE_ADMIN'] }
      },
      { 
        path: 'identity-document', 
        component: IdentityDocumentComponent,
        canActivate: [AuthGuard],
        data: { roles: ['ROLE_USER', 'ROLE_WORKER', 'ROLE_ADMIN'] }
      },
      { 
        path: 'address', 
        component: AddressComponent,
        canActivate: [AuthGuard],
        data: { roles: ['ROLE_USER', 'ROLE_WORKER', 'ROLE_ADMIN'] }
      },
      { 
        path: 'citizens', 
        component: CitizensComponent,
        canActivate: [AuthGuard],
        data: { roles: ['ROLE_WORKER', 'ROLE_ADMIN'] }
      },
      { 
        path: 'citizens/:id/payments', 
        component: PaymentsComponent,
        canActivate: [AuthGuard],
        data: { roles: ['ROLE_WORKER', 'ROLE_ADMIN'] }
      },
      { 
        path: 'dictionaries', 
        component: DictionariesComponent,
        canActivate: [AuthGuard],
        data: { roles: ['ROLE_ADMIN'] }
      },
    ]
  },
  {
    path: 'forbidden',
    component: ForbiddenComponent
  }
];
