import { Component, OnInit } from '@angular/core';
import { DynamicPermissionService } from '../services/dynamic-permission.service';

@Component({
  selector: 'lib-dynamic-permission',
  template: ` <p>dynamic-permission works!</p> `,
  styles: [],
})
export class DynamicPermissionComponent implements OnInit {
  constructor(private service: DynamicPermissionService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
