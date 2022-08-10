import { LiveAnnouncer } from '@angular/cdk/a11y';
import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Methods } from 'src/app/methods/methods';
import { IPropertyReservation } from 'src/app/models/propertyReservation';
import { IReservation } from 'src/app/models/reservation';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-property-reservations',
  templateUrl: './property-reservations.component.html',
  styleUrls: ['./property-reservations.component.css']
})
export class PropertyReservationsComponent implements OnInit, AfterViewInit {

  @Input()
  reservations!: IPropertyReservation[];
  @Input()
  propertyId!: number;
  displayedColumns = ['email', 'checkin', 'checkout', 'buttons'];
  
  dataSource!: MatTableDataSource<IPropertyReservation>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(
    public dialogRef: MatDialogRef<PropertyReservationsComponent>, 
    private api: ApiService,
    private _liveAnnouncer: LiveAnnouncer, 
    private date: DatePipe) { }

  ngAfterViewInit(): void {
    setTimeout(() => {
      this.dataSource.paginator = this.paginator;
      this.dataSource.sort = this.sort;
    });
  }

  announceSortChange(sortState: Sort) {
    if (sortState.direction) {
      this._liveAnnouncer.announce(`Sorted ${sortState.direction}ending`);
    } else {
      this._liveAnnouncer.announce('Sorting cleared');
    }
  }

  ngOnInit(): void {
    this.dataSource = new MatTableDataSource<IPropertyReservation>(this.reservations);
      this.dataSource.sortingDataAccessor = (item, prop) => {
        switch (prop) {
          case 'checkin':
            return <string>this.date.transform(item.checkinDate, 'yyyy-MM-dd');
          case 'checkout':
            return <string>this.date.transform(item.checkoutDate, 'yyyy-MM-dd');
          case 'email':
            return item.email;
          default:
            return item.email;
        }
      };
  }

  closeModal() {
    this.dialogRef.close();
  }

  formatDate(d: Date): Date {
    return Methods.formatDate(d);
  }

  checkDate(d: Date): boolean {
    return (Methods.formatDate(d) < new Date());
  }

  delete(reservationId: number): void {
    this.api.deleteReservationByOwner(reservationId).subscribe(res => {
      this.api.getReservationsByPropertyId(this.propertyId).subscribe(x => {
        this.reservations = x;
        this.dataSource = new MatTableDataSource<IPropertyReservation>(this.reservations);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.dataSource.sortingDataAccessor = (item, prop) => {
          switch (prop) {
            case 'checkin':
              return <string>this.date.transform(item.checkinDate, 'yyyy-MM-dd');
            case 'checkout':
              return <string>this.date.transform(item.checkoutDate, 'yyyy-MM-dd');
            case 'email':
              return item.email;
            default:
              return item.email;
          }
        };
      });
    });
  }
}
