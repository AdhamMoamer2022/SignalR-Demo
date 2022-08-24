import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  public hubConnection: signalR.HubConnection | undefined;

  constructor(public toastr: ToastrService) {}

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/toastr', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      })
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('hub Connection started');
        this.sendMessage();
        this.sendMessageListener();
      })
      .catch((err) => console.log('Error while starting connection: ' + err));
  };

  public sendMessage = async () => {
    console.log('sendMessage');
    await this.hubConnection
      ?.invoke('SendMessage', 'Hello from Angular')
      .then(() => console.log('send Message invoked'))
      .catch((err) => console.error(err));
    console.log('sendMessage end');
  };
  public sendMessageListener = () => {
    console.log('sendMessageListener start');
    this.hubConnection?.on('MessageResponse', (data: any) => {
      console.log('send MessageListener invoked');
      this.toastr.success(data);
    });
  };
}
