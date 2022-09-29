import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Application } from "src/app/models/application";
import { ApplicationService } from "src/app/services/application.service";

@Component({
    selector: 'presentation-page',
    templateUrl: './presentation-page.component.html'
})
export class PresentationPageComponent implements OnInit {
    //application?: Application;
    loadingFailureMessage?: string;

    constructor(
        private Route: ActivatedRoute, 
        private ApplicationService: ApplicationService) { }

    ngOnInit(): void {
        this.ApplicationService.loadFailureMessage.subscribe(msg => {
            this.loadingFailureMessage = msg;
        });

        const idRoute = this.Route.snapshot.paramMap.get('id');
        const nameRoute = this.Route.snapshot.paramMap.get('name');

        if (idRoute != null && idRoute.trim() !== '') {
            this.ApplicationService.loadApplicationById(idRoute);
            return;
        }

        if (nameRoute != null && nameRoute.trim() !== '') {
            this.ApplicationService.loadApplicationByName(nameRoute);
            return;
        }
    }
}