import { Component, OnInit } from '@angular/core';
import { Pagination } from 'src/app/_models/pagination';
import { Property } from 'src/app/_models/property';
import { PropertiesService } from 'src/app/_services/properties.service';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['./property-list.component.css']
})
export class PropertyListComponent implements OnInit {

  properties: Property[];
  pagination: Pagination;
  pageNumber = 1;
  pageSize = 10;


  constructor(private propertiesService: PropertiesService) { }

  ngOnInit(): void {
    this.loadProperties();
  }

  loadProperties(){
    this.propertiesService.getProperties(this.pageNumber, this.pageSize).subscribe(response => {
      this.properties = response.result;
      this.pagination = response.pagination;

    })
  }
  pageChanged(event: any){
    this.pageNumber = event.page;
    this.loadProperties();
  }

}
