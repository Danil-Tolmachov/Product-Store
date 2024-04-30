import { CommonModule, NgOptimizedImage } from '@angular/common';
import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'app-image-container',
  standalone: true,
  imports: [CommonModule, NgOptimizedImage],
  templateUrl: './image-container.component.html',
  styleUrl: './image-container.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ImageContainerComponent {
  @Input() imageUrl: string = '';

  @Input() altText: string | null = '';
}
