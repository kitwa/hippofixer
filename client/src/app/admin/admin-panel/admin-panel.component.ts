import { Component, OnInit } from '@angular/core';
import { Stats } from 'src/app/_models/stats';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.css']
})
export class AdminPanelComponent implements OnInit {

  stats: Stats

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.getStats();
  }

  getStats(){
    this.adminService.getStats().subscribe(stats => {
      this.stats = stats;
    })
  }

}
