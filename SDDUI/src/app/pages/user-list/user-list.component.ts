import { Component, OnInit, inject } from '@angular/core';
import { UserService } from '../../service/user.service';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { FilterPipe } from '../../pipe/filter.pipe';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [RouterLink,FormsModule,CommonModule,FilterPipe],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit {

  userService = inject(UserService);
  router =  inject(Router)
  userList: any[]=[];
  searchtext:any;

  currentPage: number = 1;  // Track current page
  pageSize: number = 5;     // Default page size
  totalItems: number = 0; 

  get paginatedUsers() {
    const start = (this.currentPage - 1) * this.pageSize;
    return this.userList.slice(start, start + this.pageSize);
  }

  onPageChange(page: number) {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
    }
  }

  onPageSizeChange(size: number) {
    this.pageSize = size;
    this.currentPage = 1;  // Reset to first page when page size changes
  }


  // Define a setter for searchtext to reset the page on change
  setSearchtext(value: string) {
    this.searchtext = value;
    // Reset currentPage to 1 when search text changes
    this.currentPage = 1;
  }

  ngOnInit(): void {
    this.loadUsers()
  }

  get totalPages() {
    return Math.ceil(this.totalItems / this.pageSize);
  }

  loadUsers() {
    this.userService.getUsers().subscribe((res:any)=>{
      this.userList = res;
      this.totalItems = res.length;
    })
  }

  onDelete(id: number) {
    const isDelete = confirm("Are you sure want to Delete");
    if(isDelete) {
      this.userService.deletUserById(id).subscribe((res:any)=>{
        if(res > 0) {
          this.loadUsers()
        } else {
          alert(res.message)
        }
      })
    }
  }
  onEdit(id: number) {
    this.router.navigate(['/editUser',id])
  }
  onCreate() {
    this.router.navigate(['/editUser/id=0',])
  }
}
