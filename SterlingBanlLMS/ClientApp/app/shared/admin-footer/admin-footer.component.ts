import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute, NavigationEnd } from "@angular/router";
import * as $ from 'jquery';

@Component({
    selector: 'admin-footer',
    templateUrl: "admin-footer.component.html",
    styleUrls: ['./admin-footer.component.css']
})

export class AdminFooterComponent implements OnInit {
    year: any;

    ngOnInit(): void {

        this.year = new Date().getFullYear();
    }

}