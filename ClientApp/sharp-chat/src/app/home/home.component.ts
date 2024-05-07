import { NgFor } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    FormsModule,
    NgFor
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  private _webSocket: WebSocket = new WebSocket('ws://localhost:5227/ws');

  public messages: string[] = [];
  public chatboxContent: string = '';

  ngOnInit(): void {
    this._webSocket.onmessage = (event: MessageEvent) => {
      const message = event.data;
      this.messages.push(message);
    };
  }

  public sendMessage() {
    if (this.chatboxContent) {
      this._webSocket.send(this.chatboxContent);
      this.chatboxContent = "";
    }
  }
}
