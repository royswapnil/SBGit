import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute, NavigationEnd } from "@angular/router";
import * as $ from 'jquery';

@Component({
    selector: 'spec-footer',
    templateUrl: "spec-footer.component.html",
})

export class SpecFooterComponent implements OnInit {
    year: any;

    ngOnInit(): void {

        this.year = new Date().getFullYear();
    }

}