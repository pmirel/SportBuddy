import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { moment, MomentAll } from 'ngx-bootstrap/chronos/test/chain';
import { Challenge } from 'src/app/_models/challenge';
import { ChallengeService } from 'src/app/_services/challenge.service';


@Component({
  selector: 'app-member-challenges',
  templateUrl: './member-challenges.component.html',
  styleUrls: ['./member-challenges.component.css']
})
export class MemberChallengesComponent implements OnInit {
  @ViewChild('challengeForm') challengeForm: NgForm;
  @Input() challenge: Challenge[];
  @Input() username: string;
  challenges: Challenge[];
  challengeLocation: string;
  challengeSport: string;
  challengeEventDate: Date;


  constructor(private challengeService: ChallengeService) { }

  ngOnInit(): void {
    this.loadChallenges();
    let minDateStr = new Date().toISOString();
    minDateStr = minDateStr.slice(0, 16);
    document.getElementById("daTi").setAttribute("min", minDateStr);
  }

  loadChallenges() {
    this.challengeService.getChallengesThread(this.username).subscribe(challenges => {
      this.challenges = challenges
    })
  }

  sendChallenge() {
    this.challengeService.sendChallenge(this.username, this.challengeLocation, this.challengeSport, this.challengeEventDate)
      .subscribe(challenge => {
        this.challenges.push(challenge);
        this.challengeForm.reset();
        this.loadChallenges();
      })
  }


}
