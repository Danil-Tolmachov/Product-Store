import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export default class CheckoutScreenService {
  private isActiveSubject: Subject<null>;

  public isActive: Observable<null>;

  constructor() {
    this.isActiveSubject = new Subject<null>();

    this.isActive = this.isActiveSubject.asObservable();
  }

  /**
   * Activates the checkout screen by emitting a value through the isActiveSubject.
   */
  showScreen(): void {
    this.isActiveSubject.next(null);
  }
}
