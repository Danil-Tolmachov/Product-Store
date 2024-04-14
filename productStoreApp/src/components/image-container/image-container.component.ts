import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-image-container',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './image-container.component.html',
  styleUrl: './image-container.component.scss',
})
export default class ImageContainerComponent {
  @Input() imageUrl: string = '';

  @Input() imageAlt: string = '';
}
