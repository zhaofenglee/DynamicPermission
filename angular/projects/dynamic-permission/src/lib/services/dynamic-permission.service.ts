import { Injectable } from '@angular/core';
import { RestService } from '@abp/ng.core';

@Injectable({
  providedIn: 'root',
})
export class DynamicPermissionService {
  apiName = 'DynamicPermission';

  constructor(private restService: RestService) {}

  sample() {
    return this.restService.request<void, any>(
      { method: 'GET', url: '/api/DynamicPermission/sample' },
      { apiName: this.apiName }
    );
  }
}
