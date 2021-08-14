import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from '../_models/member';
import { Pagination } from '../_models/pagination';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  members: Member[];
  pagination: Pagination;
  pageNumber: 1;
  pageSize = 5;

  constructor(private memberService: MembersService) { }

  ngOnInit(): void {
    // this.loadMembers();
  }

  // loadMembers() {
  //   this.memberService.getMembers(this.pageNumber, this.pageSize).subscribe(response => {
  //     this.members = response.result;
  //     this.pagination = response.pagination;
  //   })
  // }

}
