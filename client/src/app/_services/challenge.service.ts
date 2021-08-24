import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Challenge } from '../_models/challenge';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelpers';

@Injectable({
  providedIn: 'root'
})
export class ChallengeService {
  baseUrl = environment.apiUrl;
  challenge: Challenge;

  constructor(private http: HttpClient) { }

  getChallenges(pageNumber, pageSize, container) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('Container', container);
    return getPaginatedResult<Challenge[]>(this.baseUrl + 'challenges', params, this.http);
  }

  getChallengesThread(username: string) {
    return this.http.get<Challenge[]>(this.baseUrl + 'challenges/thread/' + username);
  }

  sendChallenge(username: string, location: string, sport: string, eventDate: Date){
    return this.http.post<Challenge>(this.baseUrl + 'challenges', {
      recipientUsername: username,
      location,
      sport,
      eventDate
    })
  }


  updateChallenge(challenge: Challenge){
    var id = challenge.id;
    return this.http.put(this.baseUrl + 'challenges/' + id, challenge);
  }

}
