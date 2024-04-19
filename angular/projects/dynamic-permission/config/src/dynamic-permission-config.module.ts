import { ModuleWithProviders, NgModule } from '@angular/core';
import { DYNAMIC_PERMISSION_ROUTE_PROVIDERS } from './providers/route.provider';

@NgModule()
export class DynamicPermissionConfigModule {
  static forRoot(): ModuleWithProviders<DynamicPermissionConfigModule> {
    return {
      ngModule: DynamicPermissionConfigModule,
      providers: [DYNAMIC_PERMISSION_ROUTE_PROVIDERS],
    };
  }
}
