import { TestBed } from '@angular/core/testing';

import { MessagesService } from './messages-service.service';

describe('MessagesServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: MessagesService = TestBed.get(MessagesService);
    expect(service).toBeTruthy();
  });
});
