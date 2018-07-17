export class Course {
    Id: number;
    CourseName: string;
    CourseDescription: string;
    CourseRetakeCount: number;
    IsDeleted: boolean;
    CreatedDate: Date;
    DueDate: Date;
    ImageUrl: string;
    Modules: Array<Module> = new Array<Module>();

}

export class Module {
    Id: number;
    CourseId: number;
    ModuleName: string;
    HierachicalOrder: number;
    Topics: Array<Topic> = new Array<Topic>();
}

export class Topic {
    Id: number;
    ModuleId: number;
    TopicName: string;
    ContentUrl: string;
    ContentType: string;
}