import {Component, OnInit} from '@angular/core';
import {MatDialog, MatDialogRef} from '@angular/material/dialog';
import {RolesModalComponent} from 'src/app/core/modals/roles-modal/roles-modal.component';
import {User} from 'src/app/shared/models/user';
import {AdminService} from '../admin.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.sass']
})
export class UserManagementComponent implements OnInit {
  users: User[] = [];
  dialogRef: MatDialogRef<RolesModalComponent> | undefined;
  availableRoles = [
    'Administrator',
    'Buyer',
  ];

  constructor(private adminService: AdminService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.getUsersWithRoles();
  }

  getUsersWithRoles() {
    this.adminService.getUsersWithRoles().subscribe({
      next: users => this.users = users,
      error: error => console.error(error)
    });
  }

  addRole(roleName: string): void {
    this.adminService.addRole(roleName).subscribe({
      next: result => console.log(result),
      error: error => console.error(error)
    });
  }

  deleteRole(roleName: string): void {
    this.adminService.deleteRole(roleName).subscribe({
      next: result => console.log(result),
      error: error => console.error(error)
    });
  }

  openRolesModal(user: User) {
    this.dialogRef = this.dialog.open(RolesModalComponent, {
      data: {
        username: user.username,
        availableRoles: this.availableRoles,
        selectedRoles: [...user.roles]
      }
    });

    this.dialogRef.afterClosed().subscribe(selectedRoles => {
      if (selectedRoles && !this.arrayEqual(selectedRoles, user.roles)) {
        this.adminService.updateUserRoles(user.username, selectedRoles).subscribe({
          next: roles => user.roles = roles,
          error: error => console.error(error)
        });
      }
    });
  }

  private arrayEqual(arr1: any[], arr2: any[]) {
    return JSON.stringify(arr1.sort()) === JSON.stringify(arr2.sort());
  }
}
