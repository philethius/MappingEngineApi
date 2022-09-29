import { HttpClient } from "@angular/common/http";
import { Injectable, OnDestroy } from "@angular/core";
import { BehaviorSubject, Observable, of } from "rxjs";
import { Application } from "../models/application";

@Injectable()
export class ApplicationService implements OnDestroy {
    private _applicationSubject$ = new BehaviorSubject<Application | undefined>(undefined);
    private _loadFailureMessageSubject$ = new BehaviorSubject<string | undefined>(undefined);
    
    public get application(): Observable<Application | undefined> {
        return this._applicationSubject$.asObservable();
    }

    public get loadFailureMessage(): Observable<string | undefined> {
        return this._loadFailureMessageSubject$.asObservable();
    }

    constructor(private Http: HttpClient) {}

    public startNewApplication(application?: Application): void {
        this._applicationSubject$.next(application ?? new Application());
    }

    public loadApplicationById(id: string): void {
        this.loadApplicationByIdHttp(id).subscribe(
            {
                next: (value: Application) => { 
                    this._applicationSubject$.next(value); 
                },
                error: (error: string) => { 
                    this._applicationSubject$.next(undefined); 
                }
            }
        )
    }

    public loadApplicationByName(name: string): void {
        this.loadApplicationByNameHttp(name).subscribe(
            {
                next: (value: Application) => { 
                    this._applicationSubject$.next(value); 
                },
                error: (error: string) => { 
                    this._applicationSubject$.next(undefined); 
                }
            }
        )
    }

    private loadApplicationByIdHttp(id: string): Observable<Application> {
        const application = new Application();
        application.name = 'LoadedById';
        return of(application);
        //return this.Http.get<Application>('');
    }

    private loadApplicationByNameHttp(name: string): Observable<Application> {
        const application = new Application();
        application.name = name;
        return of(application);
        //return this.Http.get<Application>('');
    }

    ngOnDestroy(): void {
        this._applicationSubject$.next(undefined);
        this._applicationSubject$.complete();
    }
}