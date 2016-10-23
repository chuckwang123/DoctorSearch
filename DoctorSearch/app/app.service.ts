import {Injectable} from "@angular/core"
import { IPerson } from "./IPerson";
import {Http, Response} from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class Appservice {
    endpointUrl: string = "/api/Doctors";
    constructor(private http: Http) {
    }
    getDoctor(){
        return this.http
            .get(this.endpointUrl)
            .toPromise()
            .then(response => response.json());
    }
    private extractData(res: Response) {
        let body = res.json();
        return body.data || {};
    }
    private handleError(error: any) {
         //In a real world app, we might use a remote logging infrastructure
         //We'd also dig deeper into the error to get a better message
        let errMsg = (error.message) ? error.message :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        console.error(errMsg);  //log to console instead
        return Observable.throw(errMsg);
    }
}