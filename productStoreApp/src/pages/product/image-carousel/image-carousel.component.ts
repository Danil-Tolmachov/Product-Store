import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  Input,
} from '@angular/core';
import { IImage } from '../../../interfaces/IImage';
import ImageContainerComponent from '../../../components/image-container/image-container.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-image-carousel',
  standalone: true,
  imports: [CommonModule, ImageContainerComponent],
  templateUrl: './image-carousel.component.html',
  styleUrl: './image-carousel.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ImageCarouselComponent {
  protected selectedImage: number = 0;
  protected showControls: boolean = true;
  @Input() imagePaths: IImage[] = [];

  constructor(private readonly cdr: ChangeDetectorRef) {
    this.showControls = this.imagePaths.length > 1 ? true : false;
  }

  nextImage(): void {
    this.selectedImage++;
    this.cdr.markForCheck();
  }

  previousImage(): void {
    this.selectedImage--;
    this.cdr.markForCheck();
  }
}
