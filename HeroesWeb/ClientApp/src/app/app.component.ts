import { Component } from '@angular/core';
import { HubConnection } from '@aspnet/signalr';
import * as signalR from '@aspnet/signalr';

import { Message } from 'primeng/api';
import { Hero } from './hero';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Tour of Heroes';
  private _hubConnection: HubConnection;
  msgs: Message[] = [];

  constructor() { }

  ngOnInit(): void {
    this._hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5002/heroes')
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this._hubConnection.start()
      .then(() => console.log('Connection started!'))
      .catch(err => console.log('Error while establishing connection :('));
      
    this._hubConnection.on('BroadcastMessage', (type: string, payload: string) => {
      this.msgs.push({ severity: type, summary: payload });
    });

    this._hubConnection.on('AddHeroes', (item: Hero) => {
      this.msgs.push({ severity: 'success', summary: 'Hero addes:' + item.name });
    });
    
    this._hubConnection.on('UpdateHeroes', (item: Hero) => {
      this.msgs.push({ severity: 'success', summary: 'Hero updated:' + item.name });
    });
    
    this._hubConnection.on('DelteHeroes', (id: string) => {
      this.msgs.push({ severity: 'success', summary: 'Hero deleted:' + id });
    });
  }
}

