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
  imports: [RouterLink],
  templateUrl: './page-bar.component.html',
  styleUrl: './page-bar.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PageBarComponent implements OnInit {
  protected pagesArray: number[] = [];

  @Input() currentPage$: Observable<number> | null = null;
  currentPage: number = 1;

  @Input() pagesCount$: Observable<number | null> | null = null;
  pagesCount: number = 1;

  @Input() countQuery$: Observable<number> | null = null;
  countQuery: number = 10;

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
    if (this.currentPage <= 1) {
      return;
    }

    this.router.navigate([], {
      queryParams: { page: +this.currentPage - 1, count: this.countQuery },
    });
  }

  nextButtonClick(): void {
    if (this.currentPage >= this.pagesCount) {
      return;
    }

    this.router.navigate([], {
      queryParams: { page: +this.currentPage + 1, count: this.countQuery },
    });
  }

  protected updateArray(): void {
    let firstPage = +this.currentPage - Math.floor(SHOWN_PAGES_LIMIT / 2);
    let lastPage;

    if (firstPage < 1) {
      firstPage = 1;
      lastPage = SHOWN_PAGES_LIMIT;
    } else {
      lastPage = +this.currentPage + Math.floor(SHOWN_PAGES_LIMIT / 2);
    }

    // Clear array
    this.pagesArray = [];

    // Fill the pagesArray
    for (let i = firstPage; i <= lastPage && i <= this.pagesCount; i++) {
      this.pagesArray.push(i);
    }
  }

  protected increaseArray(): void {
    const firstPage = this.pagesArray[this.pagesArray.length - 1] + 1;
    const lastPage = firstPage + SHOWN_PAGES_LIMIT;

    for (let i = firstPage; i <= lastPage && i <= this.pagesCount; i++) {
      this.pagesArray.push(i);
    }
  }
}
