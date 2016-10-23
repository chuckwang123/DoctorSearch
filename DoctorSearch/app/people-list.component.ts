import { Component } from '@angular/core';
import { IPerson } from "./IPerson";
import {Column} from './column';
import {Appservice} from './app.service';

@Component({
    selector: 'people-list',
    templateUrl: 'app/Grid.html',
    providers: [Appservice]
})
export class PeopleListComponent{
    people: IPerson[];
    errorMessage: string;
    mode = 'Observable';

    constructor(private _appservice: Appservice) {
        this._appservice = _appservice;
        this.getDoctors();
    }

    columns: Array<Column> = [
            new Column('Name', 'name'),
            new Column('Birth', 'Birth'),
            new Column('Sex', 'Sex'),
            new Column('TEL', 'Tel'),
            new Column('AreaCode', 'Area Code'),
            new Column('Score', 'Score')
        ];

    getDoctors() {
        this._appservice.getDoctor()
            .then(
                data => this.people = data);
        console.log(this.people);
    };
} 