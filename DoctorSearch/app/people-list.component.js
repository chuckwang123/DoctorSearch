"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var column_1 = require('./column');
var app_service_1 = require('./app.service');
var PeopleListComponent = (function () {
    function PeopleListComponent(_appservice) {
        this._appservice = _appservice;
        this.mode = 'Observable';
        this.columns = [
            new column_1.Column('Name', 'name'),
            new column_1.Column('Birth', 'Birth'),
            new column_1.Column('Sex', 'Sex'),
            new column_1.Column('TEL', 'Tel'),
            new column_1.Column('AreaCode', 'Area Code'),
            new column_1.Column('Score', 'Score')
        ];
        this._appservice = _appservice;
        this.getDoctors();
    }
    PeopleListComponent.prototype.getDoctors = function () {
        var _this = this;
        this._appservice.getDoctor()
            .then(function (data) { return _this.people = data; });
        console.log(this.people);
    };
    ;
    PeopleListComponent = __decorate([
        core_1.Component({
            selector: 'people-list',
            templateUrl: 'app/Grid.html',
            providers: [app_service_1.Appservice]
        }), 
        __metadata('design:paramtypes', [app_service_1.Appservice])
    ], PeopleListComponent);
    return PeopleListComponent;
}());
exports.PeopleListComponent = PeopleListComponent;
//# sourceMappingURL=people-list.component.js.map