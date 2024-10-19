import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { PaginatedResult } from '../_models/pagination';

// const httpOptions = {
//   headers: new HttpHeaders({
//     Autorosation: 'Bearer' + JSON.parse(localStorage.getItem('user') || '{}').token
//   })
// }

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  baseUrl = environment.apiUrl;
  members: Member[] = [];
  paginatedResult: PaginatedResult<Member[]> = new PaginatedResult<Member[]>();

  constructor(private http: HttpClient ) {

  } 

  getMembers(page? : number, itemsPerPage?: number) {

    let params = new HttpParams();

    if(page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('PageSize', itemsPerPage.toString());
    }

    // if(this.members.length > 0) return of(this.members); 
    return this.http.get<Member[]>(this.baseUrl + 'users', {observe: 'response', params}).pipe(

      map(response => {
        this.paginatedResult.result = response.body;
        if(response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return this.paginatedResult;
      })

      // map(members => {
      //   this.members = members;
      //   return members;
      // })
    );
  }

  getMember(email: string) {
    const member = this.members.find(x => x.email === email);
    if(member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'users/' + email);
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    );
  }

  uploadProfilePhoto(userId: number, image: File) {
    const formData = new FormData();
    formData.append('file', image);
    return this.http.post<Member>(this.baseUrl + 'users/' + userId + '/add-photo', formData);
  }

}
