import { animate, style, transition, trigger } from '@angular/animations';
import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import UserService from '../../services/user.service';

interface IDropdownLink {
  text: string;
  link: string;
}

@Component({
  selector: 'app-auth-dropdown',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './auth-dropdown.component.html',
  styleUrl: './auth-dropdown.component.scss',
  animations: [
    trigger('fadeInOutAnimation', [
      transition(':enter', [
        style({ opacity: 0 }),
        animate('0.1s', style({ opacity: 1 })),
      ]),
      transition(':leave', [animate('0.1s', style({ opacity: 0 }))]),
    ]),
  ],
})
export class AuthDropdownComponent {
  isActive: boolean = false;

  dropdownLinks: IDropdownLink[] = [
    {
      text: "My Profile",
      link: "#"
    },
    {
      text: "My Orders",
      link: "#"
    }
  ];

  constructor(private readonly userService: UserService) {}

  switchDropdown(): void {
    this.isActive = !this.isActive;
  }
  
  logoutButtonClick(): void {
    this.userService.logoutSession();
  }
}
