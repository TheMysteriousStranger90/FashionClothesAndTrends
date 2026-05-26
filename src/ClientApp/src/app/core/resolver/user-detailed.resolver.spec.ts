import { TestBed } from '@angular/core/testing';
import { ResolveFn } from '@angular/router';

import { userDetailedResolver } from './user-detailed.resolver';

describe('userDetailedResolver', () => {
  const executeResolver: ResolveFn<boolean> = (...resolverParameters) => 
      TestBed.runInInjectionContext(() => userDetailedResolver(...resolverParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeResolver).toBeTruthy();
  });
});
