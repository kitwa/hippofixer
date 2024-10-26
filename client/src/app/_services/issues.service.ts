import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Issue } from '../_models/issue';
import { PaginatedResult } from '../_models/pagination';
import { IssueType } from '../_models/issueType';
import { City } from '../_models/city';


@Injectable({
  providedIn: 'root'
})
export class IssuesService {

  baseUrl = environment.apiUrl;
  issues: Issue[] = [];
  paginatedResult: PaginatedResult<Issue[]> = new PaginatedResult<Issue[]>();

  constructor(private http: HttpClient ) {

  } 

  getIssues(page? : number, itemsPerPage?: number) {

    let params = new HttpParams();

    if(page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('PageSize', itemsPerPage.toString());
    }

    // if(this.issues.length > 0) return of(this.issues); 
    return this.http.get<Issue[]>(this.baseUrl + 'issues', {observe: 'response', params}).pipe(

      map(response => {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedResult;
      })

    );
  }


  getIssue(id: number) {
    const issue = this.issues.find(x => x.id === id);
    if(issue !== undefined) return of(issue);
    return this.http.get<Issue>(this.baseUrl + 'issues/' + id);
  }

  updateIssue(issueId: number, issue: Issue) {
    return this.http.put(this.baseUrl + 'issues/' + issueId, issue).pipe(
      map(() => {
        const index = this.issues.indexOf(issue);
        this.issues[index] = issue;
      })
    );
  }

  addIssue(issue: Issue) {
    return this.http.post<Issue>(this.baseUrl + 'issues', issue);
  }

  deleteIssue(issueId: Number) {
    return this.http.put(this.baseUrl + 'issues/' + issueId + '/delete', {}).pipe();
  }

  maskAsSoldIssue(issueId: Number) {
    return this.http.put(this.baseUrl + 'issues/' + issueId + '/mask-as-sold', {}).pipe();
  }

  setMainPhoto(issueId: Number, photoId: Number) {
    return this.http.put(this.baseUrl + 'issues/' + issueId + '/set-main-photo/' + photoId, {});
  }

  deletePhoto(issueId: Number, photoId: Number) {
    return this.http.delete(this.baseUrl + 'issues/' + issueId + '/delete-photo/' + photoId);
  }

  getIssueTypes() {
    return this.http.get<IssueType[]>(this.baseUrl + 'issues/issue-types');
  }

  uploadPhoto(issueId: number, photo: File) {
    const formData = new FormData();
    formData.append('file', photo);
    return this.http.post<any>(this.baseUrl + 'issues/' + issueId + '/add-photo', formData);
  }

  acceptIssue(issueId: Number) {
    return this.http.put(this.baseUrl + 'issues/' + issueId + '/accept', {}).pipe();
  }

  getCities() {
    return this.http.get<City[]>(this.baseUrl + 'issues/cities');
  }

}
