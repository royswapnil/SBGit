var LessonType = function (type) {
    this.className = '';
    
    switch (type) {
        case 1: this.className = "fa fa-text"
        case 2: this.className = "fa fa-video"
        case 3: this.className = "fa fa-file"
        case 5: this.className = "fa fa-question"
        default: this.className = "fa fa-file"
    }
        
        
}

