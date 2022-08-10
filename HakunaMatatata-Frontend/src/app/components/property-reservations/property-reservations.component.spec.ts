import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PropertyReservationsComponent } from './property-reservations.component';

describe('PropertyReservationsComponent', () => {
  let component: PropertyReservationsComponent;
  let fixture: ComponentFixture<PropertyReservationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PropertyReservationsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PropertyReservationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
