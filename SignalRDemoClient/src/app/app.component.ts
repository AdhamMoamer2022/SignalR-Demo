import { Component, OnDestroy, OnInit } from '@angular/core';
import { SignalRService } from './signal-r.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit, OnDestroy {
  constructor(private signalRService: SignalRService) {}

  ngOnInit(): void {
    this.signalRService.startConnection();
    // setTimeout(() => {
    //   this.signalRService.sendMessage();
    //   this.signalRService.sendMessageListener();
    // }, 2000);
  }

  ngOnDestroy(): void {
    this.signalRService.hubConnection?.off('ReceiveMessage');
  }

  title = 'SignalRDemoClient';
}
