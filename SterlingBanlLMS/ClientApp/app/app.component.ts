import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from "@angular/router";
import { NgProgress } from 'ngx-progressbar';
import { AuthService } from './service/auth.service';


@Component({
  selector: 'lms-app',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.css']
})
export class AppComponent implements OnInit {
    LoggedInUser: string;
    location: any;
    constructor(private router: Router, private authService: AuthService, public ngProgress: NgProgress) {
        router.events.subscribe(e => {
            if (e instanceof NavigationEnd) {
                this.location = e.url;
               // console.log("current url", e.url)
            }
        })
    }
    ngOnInit(): void {
        this.ngProgress.start();

    }
  title = 'app';
}
