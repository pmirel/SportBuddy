import { Component, OnInit } from '@angular/core';
import { Challenge } from '../_models/challenge';
import { Pagination } from '../_models/pagination';
import { ChallengeService } from '../_services/challenge.service';

@Component({
  selector: 'app-challenges',
  templateUrl: './challenges.component.html',
  styleUrls: ['./challenges.component.css']
})
export class ChallengesComponent implements OnInit {
  challenges: Challenge[];
  pagination: Pagination;
  container = "New";
  pageNumber = 1;
  pageSize = 5;
  dropDownValue = "";
  selectedChallenge?: Challenge;


  constructor(private challengeService: ChallengeService) { }

  ngOnInit(): void {
    this.loadChallenges();
  }

  loadChallenges() {
    this.challengeService.getChallenges(this.pageNumber, this.pageSize, this.container).subscribe(response => {
      this.challenges = response.result;
      this.pagination = response.pagination;
    })
  }
  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.loadChallenges();
  }

  updateChallenge(event: any) {
    console.log(this.challenges);
  }


  SetDropDownValue(drpValue: any) {
    this.dropDownValue = drpValue.target.value;
  }

  onSelect(challenge: Challenge) {
    console.log(this.selectedChallenge = challenge);
  }

}
