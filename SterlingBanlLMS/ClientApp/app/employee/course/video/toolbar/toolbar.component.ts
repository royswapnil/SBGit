import { Component } from '@angular/core';
import { TimeDisplayPipe } from "../../../../pipes/timedisplay.pipe";
import { VideoService } from '../../../../service/video.service';

@Component({
    selector: 'video-toolbar',
    templateUrl: './toolbar.component.html',
    styleUrls: []
})
export class ToolbarComponent {

    constructor(private videoService: VideoService) { }

}
