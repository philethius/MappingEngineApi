import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EditorPageComponent } from './components/routing-pages/editor-page/editor-page.component';
import { HomePageComponent } from './components/routing-pages/home-page/home-page.component';
import { PresentationPageComponent } from './components/routing-pages/presentation-page/presentation-page.component';
//import { BrowserUtils } from '@azure/msal-browser';
//import { HomePageComponent } from '@app/components/pages/home-page/home-page.component';

const routes: Routes = [
  {
    path: 'editor',
    component: EditorPageComponent
  },
  {
    path: 'app/:id',
    component: PresentationPageComponent
  },
  {
    path: 'app-name/:name',
    component: PresentationPageComponent
  },
  {
    path: '',
    component: HomePageComponent
  }
];

//const initialNavigation = !BrowserUtils.isInIframe() && !BrowserUtils.isInPopup() ? 'enabled' : 'disabled';

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    // Don't perform initial navigation in iframes or popups
    //initialNavigation: initialNavigation
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }