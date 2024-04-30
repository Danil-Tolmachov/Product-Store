import { Component, Input } from '@angular/core';
import { ISpecification } from '../../../interfaces/ISpecification';

@Component({
  selector: 'app-specifications-container',
  standalone: true,
  imports: [],
  templateUrl: './specifications-container.component.html',
  styleUrl: './specifications-container.component.scss',
})
export class SpecificationsContainerComponent {
  @Input() specifications: ISpecification[] = [];
}
