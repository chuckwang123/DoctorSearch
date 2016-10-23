import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule, JsonpModule } from '@angular/http';
import { AppComponent }  from './app.component';
import { PeopleListComponent } from './people-list.component';

@NgModule({
    imports: [BrowserModule,HttpModule,JsonpModule],
    declarations: [AppComponent, PeopleListComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }