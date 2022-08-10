import { LiveAnnouncer } from '@angular/cdk/a11y';
import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort, Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Methods } from 'src/app/methods/methods';
import { IReservation } from 'src/app/models/reservation';
import { ApiService } from 'src/app/services/api.service';
import { EditReservationComponent } from '../edit-reservation/edit-reservation.component';

@Component({
  selector: 'app-reservations',
  templateUrl: './reservations.component.html',
  styleUrls: ['./reservations.component.css']
})
export class ReservationsComponent implements OnInit, AfterViewInit {

  reservations!: Observable<IReservation[]>;
  currentDate = new Date();
  displayedColumns = ['checkin', 'checkout', 'property', 'price', 'guests', 'buttons']

  dialogConfig = new MatDialogConfig();
  modalDialog!: MatDialogRef<EditReservationComponent, any>;

  dataSource!: MatTableDataSource<IReservation>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  
  constructor(
    private api: ApiService,
    private matDialog: MatDialog, 
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

  openModal(reservationId: number) {
    this.dialogConfig.id = "projects-modal-component";
    this.dialogConfig.height = "300px";
    this.dialogConfig.width = "620px";
    this.modalDialog = this.matDialog.open(EditReservationComponent, this.dialogConfig);
    this.modalDialog.componentInstance.reservationId = reservationId;
    this.modalDialog.afterClosed().subscribe(res => {
      this.api.getReservations().subscribe(res => {
        this.dataSource = new MatTableDataSource<IReservation>(res);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.dataSource.sortingDataAccessor = (item, prop) => {
          switch (prop) {
            case 'checkin':
              return <string>this.date.transform(item.checkinDate, 'yyyy-MM-dd');
            case 'checkout':
              return <string>this.date.transform(item.checkoutDate, 'yyyy-MM-dd');
            case 'property':
              return item.property.name;
            case 'price':
              return item.totalPrice;
            case 'guests':
              return item.guestsNumber;
            default:
              return item.property.name;
          }
        }
      });
    });
  }

  ngOnInit(): void {
    this.reservations = this.api.getReservations();
    this.api.getReservations().subscribe(res => {
      this.dataSource = new MatTableDataSource<IReservation>(res);
      this.dataSource.sortingDataAccessor = (item, prop) => {
        switch (prop) {
          case 'checkin':
            return <string>this.date.transform(item.checkinDate, 'yyyy-MM-dd');
          case 'checkout':
            return <string>this.date.transform(item.checkoutDate, 'yyyy-MM-dd');
          case 'property':
            return item.property.name;
          case 'price':
            return item.totalPrice;
          case 'guests':
            return item.guestsNumber;
          default:
            return item.property.name;
        }
      };
    });
  }

  formatDate(d: Date): Date {
    return Methods.formatDate(d);
  }

  checkDate(d: Date): boolean {
    if (Methods.formatDate(d).getDate() === this.currentDate.getDate())
      return false;

    return (Methods.formatDate(d) < this.currentDate)  
  }

  delete(reservationId: number): void {
    this.api.deleteReservation(reservationId).subscribe(res => {
      this.api.getReservations().subscribe(res => {
        this.dataSource = new MatTableDataSource<IReservation>(res);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.dataSource.sortingDataAccessor = (item, prop) => {
          switch (prop) {
            case 'checkin':
              return <string>this.date.transform(item.checkinDate, 'yyyy-MM-dd');
            case 'checkout':
              return <string>this.date.transform(item.checkoutDate, 'yyyy-MM-dd');
            case 'property':
              return item.property.name;
            case 'price':
              return item.totalPrice;
            case 'guests':
              return item.guestsNumber;
            default:
              return item.property.name;
          }
        }
      });
    });
  }
}
