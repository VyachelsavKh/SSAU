import { Component, OnInit } from '@angular/core';
import { UsersService, User } from '../../services/users.service';
import { AuthService }    from '../../services/auth.service';
import { CityService, CityDTO }         from '../../services/Dictionary/city.service';
import { StreetService, StreetDTO }     from '../../services/Dictionary/street.service';
import { DocTypeService, DocumentDTO }  from '../../services/Dictionary/doc-type.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { EditUserRoleModalComponent } from '../edit-user-role-modal/edit-user-role-modal.component'
import { RouterModule, Router } from '@angular/router';

@Component({
  selector: 'app-users-panel',
  templateUrl: './citizens.component.html',
  styleUrl: './citizens.component.css',
  imports: [
    FormsModule,
    CommonModule,
    EditUserRoleModalComponent,
    RouterModule
  ]
})
export class CitizensComponent implements OnInit {
  users: User[] = [];
  filteredUsers: User[] = [];

  cities: CityDTO[] = [];
  streets: StreetDTO[] = [];
  docTypes: DocumentDTO[] = [];

  search = {
    login: '',
    lastName: '',
    firstName: '',
    middleName: '',
    series: '',
    number: '',
    cityId: null as number | null,
    streetId: null as number | null,
    documentTypeId: null as number | null,
  };

  roles: string[] = [];

  constructor(
    private usersService: UsersService,
    private authService: AuthService,
    private cityService: CityService,
    private streetService: StreetService,
    private docTypeService: DocTypeService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.roles = this.authService.getRoles();

    this.usersService.getUsers().subscribe(users => {
      this.users = users;
      this.filteredUsers = [...this.users];
    });

    this.cityService.getAll().subscribe(cities => this.cities = cities);
    this.streetService.getAll().subscribe(streets => this.streets = streets);
    this.docTypeService.getAll().subscribe(docTypes => this.docTypes = docTypes);
  }

  filterUsers(): void {
    this.filteredUsers = this.users.filter(user => {
      const matchesLogin = !this.search.login 
        || user.login.toLowerCase().includes(this.search.login.toLowerCase());
      
      const matchesLastName = !this.search.lastName 
        || user.description?.lastName?.toLowerCase().includes(this.search.lastName.toLowerCase());
        
      const matchesFirstName = !this.search.firstName 
        || user.description?.firstName?.toLowerCase().includes(this.search.firstName.toLowerCase());
        
      const matchesMiddleName = !this.search.middleName 
        || user.description?.middleName?.toLowerCase().includes(this.search.middleName.toLowerCase());
        
      const matchesSeries = !this.search.series 
        || user.document?.series?.toLowerCase().includes(this.search.series.toLowerCase());
        
      const matchesNumber = !this.search.number 
        || user.document?.number?.toLowerCase().includes(this.search.number.toLowerCase());
        
      const matchesCity = !this.search.cityId 
        || user.address?.cityId === this.search.cityId;
        
      const matchesStreet = !this.search.streetId 
        || user.address?.streetId === this.search.streetId;
        
      const matchesDocType = !this.search.documentTypeId 
        || user.document?.documentTypeId === this.search.documentTypeId;

      return matchesLogin && matchesLastName && matchesFirstName && matchesMiddleName && matchesSeries && matchesNumber && matchesCity && matchesStreet && matchesDocType;
    });
  }

  selectedUserId: number | null = null;
  selectedUserLogin: string = '';
  showRoleModal = false;
  
  openRoleModal(user: User): void {
    if (user.login == this.authService.getCredentials()?.login) {
      alert("Вы не можете менять роль самому себе")
      return;
    }
    this.selectedUserId = user.id;
    this.selectedUserLogin = user.login;
    this.showRoleModal = true;
  }
  
  closeRoleModal(): void {
    this.showRoleModal = false;
  }

  hasRole(role: string): boolean {
    return this.authService.getRoles().includes(role);
  }
  
  payments(userId: number): void {
    this.router.navigate(['main', 'citizens', userId, 'payments']);
  }

  getCityName(cityId?: number): string {
    return this.cities.find(city => city.id === cityId)?.name || '';
  }
  
  getStreetName(streetId?: number): string {
    return this.streets.find(street => street.id === streetId)?.name || '';
  }
  
  getDocTypeName(docTypeId?: number): string {
    return this.docTypes.find(docType => docType.id === docTypeId)?.name || '';
  }
}
