import { Component, ViewChild } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { User } from '../../models/user';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-users-table',
  imports: [MatTableModule, MatPaginatorModule],
  templateUrl: './users-table.component.html',
  styleUrl: './users-table.component.css',
})
export class UsersTableComponent {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  dataSource = new MatTableDataSource<User>();
  displayedColumns: string[] = ['id', 'username', 'email', 'role', 'actions'];

  constructor(private userService: UserService) {}

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  ngOnInit() {
    this.userService.getAll().subscribe((data: User[]) => {
      this.dataSource.data = data;
    });
  }
}
