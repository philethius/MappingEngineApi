import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { ApplicationComponent } from './components/application/application.component';
import { EditorComponent } from './components/editor/editor.component';
import { EditorPageComponent } from './components/routing-pages/editor-page/editor-page.component';
import { HomePageComponent } from './components/routing-pages/home-page/home-page.component';
import { PresentationPageComponent } from './components/routing-pages/presentation-page/presentation-page.component';
import { ApplicationService } from './services/application.service';

@NgModule({
	declarations: [
		AppComponent,
    	EditorPageComponent,
		HomePageComponent,
    	PresentationPageComponent,
    	ApplicationComponent,
    	EditorComponent
	],
	imports: [
    	AppRoutingModule,
    	//BrowserAnimationsModule,
    	BrowserModule, 
    	HttpClientModule
  	],
  	providers: [
    	ApplicationService
  	],
  	bootstrap: [
    	AppComponent
  	]
})
export class AppModule { }
