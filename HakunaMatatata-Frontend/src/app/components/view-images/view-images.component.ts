import { Component, Input, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { IProperty } from 'src/app/models/property';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-view-images',
  templateUrl: './view-images.component.html',
  styleUrls: ['./view-images.component.css']
})
export class ViewImagesComponent implements OnInit {

  @Input()
  property!: IProperty;
  isOwner!: boolean;

  constructor(public dialogRef: MatDialogRef<ViewImagesComponent>, private api: ApiService) { }

  ngOnInit(): void {
    this.api.getCurrentUser().subscribe(
      res => {
        if (res.property.propertyId === this.property.propertyId)
          this.isOwner = true;
        else
          this.isOwner = false;
      }, 
      err => this.isOwner = false
    );
  }

  closeModal() {
    this.dialogRef.close();
  }

  delete(id: number) {
    this.api.deleteImage(id).subscribe(
      res => this.api.getPropertyById(this.property.propertyId).subscribe(
        x => this.property = x
      )
    );
  }

}
