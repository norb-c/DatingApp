import { Resolve, Router } from '@angular/router';
import { User } from '../_models/User';
import { Injectable } from '@angular/core';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ListsResolver implements Resolve<User[]>{
	pageSize = 5;
	pageNumber = 1;
	likesParams = 'likers';

	constructor(private userService: UserService,
		private router: Router, private alertify: AlertifyService) { }

	// for getting parameter from the rul
	resolve(): Observable<User[]> {
		return this.userService.getUsers(this.pageNumber, this.pageSize, null, this.likesParams).pipe(

			catchError(err => {
				this.alertify.error('Problem retrieving data');
				this.router.navigate(['/home']);
				return of(null);
			})
		);
	}

}

