import { Component } from '@angular/core';
import { VideoService } from '../../../../service/video.service';

@Component({
    selector: 'video-progress',
    templateUrl: './progress.component.html',
    styleUrls: []
})
export class ProgressComponent {
    constructor(public videoService: VideoService) { }
}
