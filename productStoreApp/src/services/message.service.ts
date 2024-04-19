import { Injectable } from '@angular/core';
import { BehaviorSubject, type Observable } from 'rxjs';
import IMessageModel from '../interfaces/models/IMessageModel';

@Injectable({
  providedIn: 'root',
})
export default class MessageService {
  private messageSubject: BehaviorSubject<IMessageModel>;
  message: Observable<IMessageModel>;

  constructor() {
    this.messageSubject = new BehaviorSubject<IMessageModel>({
      header: '',
      message: [],
    });
    this.message = this.messageSubject.asObservable();
  }

  showMessage(message: IMessageModel): void {
    this.messageSubject.next(message);
  }
}
