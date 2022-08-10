import { TestBed } from '@angular/core/testing';

import { ReservationDetailsTrasporterService } from './reservation-details-trasporter.service';

describe('ReservationDetailsTrasporterService', () => {
  let service: ReservationDetailsTrasporterService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReservationDetailsTrasporterService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
