import { TestBed } from '@angular/core/testing';

import { BookListItemService } from './book-list-item.service';

describe('BookListItemService', () => {
  let service: BookListItemService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BookListItemService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
