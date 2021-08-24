import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
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
  selectedChallenge?: Challenge;
  challenge: Challenge;
  loading = false;


  constructor(private challengeService: ChallengeService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadChallenges();
  }

  loadChallenges() {
    this.loading = true;
    this.challengeService.getChallenges(this.pageNumber, this.pageSize, this.container).subscribe(response => {
      this.challenges = response.result;
      this.pagination = response.pagination;
      this.loading = false;
    })
  }
  pageChanged(event: any) {
    if (this.pageNumber != event.page) {
      this.pageNumber = event.page;
      console.log(this.pageNumber);
      this.loadChallenges();
    }
  }

  
  updateChallenge(id: number, answer: boolean) {
    var challenge ;
    this.challenges.forEach(c => {
      if(c.id = id){
        challenge = c;
      }
    });
    challenge.answer = answer;
    this.challengeService.updateChallenge(challenge).subscribe(() => {
      this.toastr.success('Challenge answered');
      this.loadChallenges();
    })
  }

  deleteChallenge(id: number) {
    this.challengeService.deleteChallenge(id).subscribe(() => {
      this.challenges.splice(this.challenges.findIndex(m => m.id === id), 1);
    })
  }



}
