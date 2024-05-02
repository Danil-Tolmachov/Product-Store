import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  Input,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { IImage } from '../../../interfaces/IImage';
import ImageContainerComponent from '../../../components/image-container/image-container.component';

@Component({
  selector: 'app-image-carousel',
  standalone: true,
  imports: [CommonModule, ImageContainerComponent],
  templateUrl: './image-carousel.component.html',
  styleUrl: './image-carousel.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export default class ImageCarouselComponent {
  protected selectedImage: number = 0;
  protected showControls: boolean = true;

  @Input() imagePaths: IImage[] = [];

  constructor(private readonly cdr: ChangeDetectorRef) {
    this.showControls = this.imagePaths.length > 1;
  }

  nextImage(): void {
    if (this.selectedImage >= this.imagePaths.length) {
      this.selectedImage = 0;
    } else {
      this.selectedImage++;
    }

    this.cdr.markForCheck();
  }

  previousImage(): void {
    if (this.selectedImage <= 0) {
      this.selectedImage = this.imagePaths.length;
    } else {
      this.selectedImage--;
    }

    this.cdr.markForCheck();
  }
}
