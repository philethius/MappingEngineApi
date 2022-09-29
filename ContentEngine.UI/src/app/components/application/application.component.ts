import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { Application } from "src/app/models/application";
import { ApplicationService } from "src/app/services/application.service";

@Component({
    selector: 'application',
    templateUrl: './application.component.html'
})
export class ApplicationComponent implements OnInit, OnDestroy {
    app?: Application;

    constructor(private ApplicationService: ApplicationService) { }

    public ngOnInit(): void {
        this.ApplicationService.application.subscribe(application => {
            this.app = application;
        });
    }

    public ngOnDestroy(): void {
        
    }
}