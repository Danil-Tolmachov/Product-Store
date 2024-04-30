import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CheckoutScreenService {
  private isActiveSubject: Subject<null>;

  public isActive: Observable<null>;

  constructor() {
    this.isActiveSubject = new Subject<null>();

    this.isActive = this.isActiveSubject.asObservable();
  }

  /**
   * Shows a message by updating the message subject.
   * @param message The message to be shown.
   */
  showScreen(): void {
    this.isActiveSubject.next(null);
  }
}
