import { Injectable } from '@angular/core';
import { BehaviorSubject, type Observable } from 'rxjs';
import IMessageModel from '../interfaces/models/IMessageModel';

@Injectable({
  providedIn: 'root',
})
export default class MessageService {
  private messageSubject = new BehaviorSubject<IMessageModel>({
    header: '',
    message: [],
  });

  public message: Observable<IMessageModel> =
    this.messageSubject.asObservable();

  /**
   * Shows a message by updating the message subject.
   * @param message The message to be shown.
   */
  showMessage(message: IMessageModel): void {
    this.messageSubject.next(message);
  }
}
