import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import {
  MsalInterceptor,
  MsalModule,
  MsalService,
  MSAL_INSTANCE,
  MSAL_INTERCEPTOR_CONFIG,
} from "@azure/msal-angular";

import { AppComponent } from "./app.component";
import { MSALInstanceFactory, MSALInterceptorConfigFactory } from "./msal";

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule, 
    MsalModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true,
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MSALInterceptorConfigFactory,
    },
    MsalService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
