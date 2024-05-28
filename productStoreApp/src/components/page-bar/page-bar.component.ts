import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  Input,
  OnInit,
} from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import { Observable, tap } from 'rxjs';

const SHOWN_PAGES_LIMIT = 5;

@UntilDestroy()
@Component({
  selector: 'app-page-bar',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './page-bar.component.html',
  styleUrl: './page-bar.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PageBarComponent implements OnInit {
  protected pagesArray: number[] = [];

  @Input() currentPage$: Observable<number> | null = null;

  currentPage: number | null = null;

  @Input() pagesCount$: Observable<number | null> | null = null;

  pagesCount: number | null = null;

  @Input() countQuery$: Observable<number> | null = null;

  countQuery: number | null = null;

  constructor(
    protected readonly cdr: ChangeDetectorRef,
    private readonly router: Router
  ) {}

  ngOnInit(): void {
    this.pagesCount$
      ?.pipe(
        untilDestroyed(this),
        tap((count) => {
          this.pagesCount = count ?? 1;
          this.updateArray();
          this.cdr.markForCheck();
        })
      )
      .subscribe();

    this.currentPage$
      ?.pipe(
        untilDestroyed(this),
        tap((page) => {
          this.currentPage = page;
          this.updateArray();
          this.cdr.markForCheck();
        })
      )
      .subscribe();

    this.countQuery$
      ?.pipe(
        untilDestroyed(this),
        tap((count) => {
          this.countQuery = count;
          this.updateArray();
          this.cdr.markForCheck();
        })
      )
      .subscribe();
  }

  previousButtonClick(): void {
    if (this.currentPage === null || this.currentPage <= 1) {
      return;
    }

    this.router.navigate([], {
      queryParams: { page: +this.currentPage - 1, count: this.countQuery },
    });
  }

  nextButtonClick(): void {
    if (
      this.currentPage === null ||
      this.pagesCount === null ||
      this.currentPage >= this.pagesCount
    ) {
      return;
    }

    this.router.navigate([], {
      queryParams: { page: +this.currentPage + 1, count: this.countQuery },
    });
  }

  protected updateArray(): void {
    if (this.currentPage === null || this.pagesCount === null) {
      return;
    }

    // Define boundaries of visible pages
    let leftPage = +this.currentPage - Math.floor(SHOWN_PAGES_LIMIT / 2);
    let rightPage;

    // Adjust the right page index if first first page is visible
    if (leftPage < 1) {
      leftPage = 1;
      rightPage = SHOWN_PAGES_LIMIT;
    } else {
      rightPage = +this.currentPage + Math.floor(SHOWN_PAGES_LIMIT / 2);
    }

    // Adjust the left page index if last page is visible
    if (rightPage > this.pagesCount) {
      leftPage = this.pagesCount - SHOWN_PAGES_LIMIT + 1;

      if (leftPage < 1) {
        leftPage = 1;
      }
    }

    // Clear array
    this.pagesArray = [];

    // Fill the pagesArray
    for (let i = leftPage; i <= rightPage && i <= this.pagesCount; i++) {
      this.pagesArray.push(i);
    }
  }

  protected increaseArray(): void {
    if (this.pagesCount === null) {
      return;
    }

    const firstPage = this.pagesArray[this.pagesArray.length - 1] + 1;
    const lastPage = firstPage + SHOWN_PAGES_LIMIT;

    for (let i = firstPage; i <= lastPage && i <= this.pagesCount; i++) {
      this.pagesArray.push(i);
    }
  }
}
