import { TestBed } from '@angular/core/testing';

import { RequestleaveService } from './requestleave.service';

describe('RequestleaveService', () => {
  let service: RequestleaveService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RequestleaveService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
