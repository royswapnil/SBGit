import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute, NavigationEnd } from "@angular/router";
import * as $ from 'jquery';

@Component({
    selector: 'emp-footer',
    templateUrl: "emp-footer.component.html",
    styleUrls: ['emp-footer.component.css']
})

export class EmployeeFooterComponent implements OnInit {
    year: any;
    ngOnInit(): void {

        this.year = new Date().getFullYear();

    }

}