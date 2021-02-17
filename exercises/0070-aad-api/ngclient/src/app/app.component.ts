import { HttpClient, HttpParams } from "@angular/common/http";
import { Component, OnInit } from "@angular/core";
import { MsalService } from "@azure/msal-angular";
import { AuthenticationResult } from "@azure/msal-browser";
import * as MicrosoftGraph from "@microsoft/microsoft-graph-types";

interface IODataResult<T> {
  value: T;
}

interface IOrder {
  id: number;
  customer: string;
  revenue: number;
}

@Component({
  selector: "app-root",
  templateUrl: "app.component.html",
  styles: [],
})
export class AppComponent implements OnInit {
  loggedIn = false;
  profile?: MicrosoftGraph.User;
  users?: MicrosoftGraph.User[];
  orders?: IOrder[];
  userNameFilter: string = "";

  constructor(private authService: MsalService, private client: HttpClient) {}

  ngOnInit(): void {
    this.checkAccount();
  }

  checkAccount() {
    this.loggedIn = this.authService.instance.getAllAccounts().length > 0;
  }

  login() {
    this.authService
      .loginPopup()
      .subscribe((response: AuthenticationResult) => {
        this.authService.instance.setActiveAccount(response.account);
        this.checkAccount();
      });
  }

  logout() {
    this.authService.logout();
  }

  getProfile() {
    this.client
      .get<MicrosoftGraph.User>("https://graph.microsoft.com/v1.0/me")
      .subscribe((profile) => (this.profile = profile));
  }

  getUsers() {
    let params = new HttpParams().set("$top", "10");
    if (this.userNameFilter) {
      params = params.set(
        "$filter",
        `startsWith(displayName, '${this.userNameFilter}')`
      );
    }
    let url = `https://graph.microsoft.com/v1.0/users?${params.toString()}`;
    this.client
      .get<IODataResult<MicrosoftGraph.User[]>>(url)
      .subscribe((users) => (this.users = users.value));
  }

  getOrders() {
    this.client
      .get<IOrder[]>("http://localhost:5000/api/orders")
      .subscribe((orders) => (this.orders = orders));
  }
}
