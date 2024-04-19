import { TestBed } from '@angular/core/testing';
import { DynamicPermissionService } from './services/dynamic-permission.service';
import { RestService } from '@abp/ng.core';

describe('DynamicPermissionService', () => {
  let service: DynamicPermissionService;
  const mockRestService = jasmine.createSpyObj('RestService', ['request']);
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: RestService,
          useValue: mockRestService,
        },
      ],
    });
    service = TestBed.inject(DynamicPermissionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
