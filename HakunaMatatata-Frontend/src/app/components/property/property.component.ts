import { HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig, MatDialogRef } from '@angular/material/dialog';
import { IProperty } from 'src/app/models/property';
import { IPropertyCreate } from 'src/app/models/propertyCreate';
import { IPropertyReservation } from 'src/app/models/propertyReservation';
import { IUser } from 'src/app/models/user';
import { ApiService } from 'src/app/services/api.service';
import { PropertyReservationsComponent } from '../property-reservations/property-reservations.component';
import { ViewImagesComponent } from '../view-images/view-images.component';

@Component({
  selector: 'app-property',
  templateUrl: './property.component.html',
  styleUrls: ['./property.component.css']
})
export class PropertyComponent implements OnInit {

  currentUser!: IUser;
  currentProperty!: IProperty | null;
  propertyViewModel = new FormGroup({
    name: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    address: new FormControl('', [Validators.required]),
    maxGuests: new FormControl('', [Validators.required]),
    price: new FormControl('', Validators.required)
  });
  message!: string;

  dialogConfig = new MatDialogConfig();
  modalDialog!: MatDialogRef<any, any>;
  
  progress!: number;
  @Output() public onUploadFinished = new EventEmitter();
  
  constructor(private api: ApiService, private matDialog: MatDialog) { }

  ngOnInit(): void {
    this.api.getCurrentUser().subscribe(x => {
      this.currentProperty = x.property;

      if (this.currentProperty != null) {
        this.propertyViewModel.controls['name'].setValue(this.currentProperty.name);
        this.propertyViewModel.controls['description'].setValue(this.currentProperty.description);
        this.propertyViewModel.controls['address'].setValue(this.currentProperty.address);
        this.propertyViewModel.controls['maxGuests'].setValue(this.currentProperty.maxGuests);
        this.propertyViewModel.controls['price'].setValue(this.currentProperty.price);
      } else {
        this.propertyViewModel.reset();
      }
    });
  }

  onSubmit(): void {
    let createdProperty: IPropertyCreate = {
      name: this.propertyViewModel.get('name')?.value,
      description: this.propertyViewModel.get('description')?.value,
      address: this.propertyViewModel.get('address')?.value,
      maxGuests: this.propertyViewModel.get('maxGuests')?.value,
      price: this.propertyViewModel.get('price')?.value,
    };

    if (this.currentProperty == null) {
      this.api.addProperty(createdProperty).subscribe(res => this.ngOnInit(), err => console.log('wrong'));
    } else {
      this.api.updateProperty(createdProperty).subscribe(res => this.ngOnInit(), err => console.log(err.error));
    }
  }

  deleteProperty(): void {
    this.api.deleteProperty().subscribe(res => this.ngOnInit(), err => console.log('wrong'));
  }

  uploadFile = (files: FileList | null) => {
    if (files === null)
      return;
    if (files.length === 0) {
      return;
    }

    const formData = new FormData();
    for (let i = 0; i < files.length; i++) {
      let fileToUpload = <File>files.item(i);
      formData.append('file[]', fileToUpload, fileToUpload.name);
    }
    
    this.api.uploadImage(formData)
      .subscribe({
        next: (event: any) => {
        if (event.type === HttpEventType.UploadProgress)
          this.progress = Math.round(100 * event.loaded / event.total);
        else if (event.type === HttpEventType.Response) {
          this.message = 'Upload success.';
          this.onUploadFinished.emit(event.body);
        }
      },
      error: (err: HttpErrorResponse) => console.log(err)
    });
  }

  openImagesModal() {
    this.api.getCurrentUser().subscribe(x => {
      this.currentProperty = x.property;
      this.dialogConfig.id = "projects-modal-component";
      this.dialogConfig.height = "80%"
      this.dialogConfig.width = "80%";
      this.modalDialog = this.matDialog.open(ViewImagesComponent, this.dialogConfig);
      this.modalDialog.componentInstance.property = <IProperty>this.currentProperty;
      this.modalDialog.afterClosed().subscribe(res => this.ngOnInit());
    });
  }

  openReservationsModal() {
    this.api.getCurrentUser().subscribe(x => {
      this.currentProperty = x.property;
      if (this.currentProperty != null) {
        this.api.getReservationsByPropertyId(this.currentProperty.propertyId).subscribe(
          res => {
            this.dialogConfig.id = "projects-modal-component";
            this.dialogConfig.height = "80%"
            this.dialogConfig.width = "80%";
            this.modalDialog = this.matDialog.open(PropertyReservationsComponent, this.dialogConfig);
            this.modalDialog.componentInstance.reservations = <IPropertyReservation[]>res;
            this.modalDialog.componentInstance.propertyId = this.currentProperty?.propertyId;
            this.modalDialog.afterClosed().subscribe(res => this.ngOnInit());
          }
        )
      }
    });
  }

  viewReservation(): void {
    this.openReservationsModal();
  }

  viewImages(): void {
    this.openImagesModal();
  }
}
