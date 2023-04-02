import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { IndexComponent } from './stores/index/index.component';
import { StoresModule } from './stores/stores.module';

@NgModule({
  declarations: [
    AppComponent,
    IndexComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    StoresModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
