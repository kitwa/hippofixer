import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { RolesModalComponent } from 'src/app/modals/roles-modal/roles-modal.component';
import { User } from 'src/app/_models/user';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
  users: Partial<User[]>;
  bsModalRef: BsModalRef;

  constructor(private adminService: AdminService, private modalService: BsModalService) { }

  ngOnInit(): void {
    this.getUsersWithRoles();
  }

  getUsersWithRoles() {
    this.adminService.getUsersWithRoles().subscribe(users => {
      this.users = users;
    })
  }

  openRolesModal(user: User) {
    const config = {
      class:'modal-dialog-center',
      initialState: {
        user,
        roles : this.getRolesArray(user)
      }
    }
    this.bsModalRef = this.modalService.show(RolesModalComponent, config);
    this.bsModalRef.content.updateSelectedRoles.subscribe(values => {
      const rolesToUpdate = {
        roles: [...values.filter(el => el.checked === true).map(el => el.name)]
      };
      if(rolesToUpdate) {
        this.adminService.updateUserRoles(user.email, rolesToUpdate.roles).subscribe(() => {
          user.roles = [...rolesToUpdate.roles]
        })
      }
    });
  }

  private getRolesArray(user){
    const roles = [];
    const userRoles = user.roles;
    const availableRoles: any[] = [
      {name: 'SuperAdmin', value: 'SuperAdmin'},
      {name: 'Admin', value: 'Admin'},
      {name: 'PropertyManager', value: 'PropertyManager'},
      {name: 'Tenant', value: 'Tenant'},
      {name: 'Contractor', value: 'Contractor'},
      {name: 'Owner', value: 'Owner'}
    ];

    availableRoles.forEach(role => {
      let isMatch = false;
      for(const userRole of userRoles) {
        if(role.name === userRole) {
          isMatch = true;
          role.checked = true;
          roles.push(role);
          break;
        }
      }

      if(!isMatch) {
        role.checked = false;
        roles.push(role);
      }
    })
    return roles;
  }

}
