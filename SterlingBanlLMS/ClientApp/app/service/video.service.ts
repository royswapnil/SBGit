import { Injectable } from "@angular/core";
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/map';
import { Course } from "../models/course";
import { Observable } from "rxjs/Observable";

@Injectable()
export class VideoService {
    public videoElement: any;
    public currentPath: string = "";
    public currentTitle: string = "loading ...";
    public currentTime: number = 0;
    public totalTime: number = 0;
    public calculatedWidth: number;
    public calculatedScrubY: number;
    public isMuted: boolean = false;
    public isPlaying: boolean = false;
    public isDragging: boolean = false;
    public showDetails: boolean = false;
    public currentDesc: string = "A very nice video...";
    public videos: Course;
    private infoUrl = "/api/course/getallvideos";

    constructor(private http: Http) { }

    appSetup(v: string) {
        this.videoElement = <HTMLVideoElement>document.getElementById(v);
        this.videoElement.addEventListener("loadedmetadata", this.updateData);
        this.videoElement.addEventListener("timeupdate", this.updateTime);
        window.setInterval(this.timerFired, 500);

    }
    

    getVideos(id: number){
        const url = `${this.infoUrl}?id=${id}`;
        return this.http.get(url)
            .map((course: Response) => this.videos = course.json().Result)
            .subscribe(
            data => {
                this.videos = data;
                this.selectedVideo(0,0);
            })
            
            
    }

    selectedVideo(i: number, n: number) {
        this.currentTitle = this.videos.Modules[i].Topics[n]['TopicName'];
        this.videoElement.src = this.videos.Modules[i].Topics[n]["ContentUrl"];
        this.videoElement.pause();
        this.isPlaying = false;
        console.log(`The current title: ${this.currentTitle}`);
        console.log(`The current src: ${this.videoElement.src}`);
    }

    seekVideo(e: any) {
        var w = document.getElementById("progressMeterFull").offsetWidth;
        var d = this.videoElement.duration;
        var s = Math.round(e.pageX / w * d);
        this.videoElement.currentTime = s;
    }

    dragStart = function (e: any) {
        this.isDragging = true;
    };

    dragMove = function (e: any) {
        if (this.isDragging) {
            this.calculatedWidth = e.x;
        }
    };

    dragStop = function (e: any) {
        if (this.isDragging) {
            this.isDragging = false;
            this.seekVideo();
        }
    }

    playVideo() {
        if (this.videoElement.paused) {
            this.videoElement.play();
            this.isPlaying = true;
        }
        else {
            this.videoElement.pause();
            this.isPlaying = false;
        }
    }
    muteVideo() {
        if (this.videoElement.volume == 0) {
            this.videoElement.volume = 1;
            this.isMuted = false;
        }
        else {
            this.videoElement.volume = 0;
            this.isMuted = true;
        }
    }

    updateData = (e: any) => {
        this.totalTime = this.videoElement.duration;
    };

    updateTime = (e: any) => {
        this.currentTime = this.videoElement.currentTime;
    };

    timerFired = () => {
        if (!this.isDragging) {
            this.calculatedScrubY = this.videoElement.offsetHeight;
            var t = this.videoElement.currentTime;
            var d = this.videoElement.duration;
            this.calculatedWidth = (t / d * this.videoElement.offsetWidth);
        }

    };

    fullScreen() {
        if (this.videoElement.requestFullscreen) {
            this.videoElement.requestFullscreen();
        } else if (this.videoElement.mozRequestFullscreen) {
            this.videoElement.mozRequestFullScreen();
        } else if (this.videoElement.webkitRequestFullscreen) {
            this.videoElement.webkitRequestFullscreen();
        } else if (this.videoElement.msRequestFullscreen) {
            this.videoElement.msRequestFullscreen();
        }
    }

    details() {
        if (this.showDetails == false) {
            this.showDetails = true;
        }
        else {
            this.showDetails = false;
        }
    }
}