import { NgModule, NgModuleFactory, ModuleWithProviders } from '@angular/core';
import { CoreModule, LazyModuleFactory } from '@abp/ng.core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { DynamicPermissionComponent } from './components/dynamic-permission.component';
import { DynamicPermissionRoutingModule } from './dynamic-permission-routing.module';

@NgModule({
  declarations: [DynamicPermissionComponent],
  imports: [CoreModule, ThemeSharedModule, DynamicPermissionRoutingModule],
  exports: [DynamicPermissionComponent],
})
export class DynamicPermissionModule {
  static forChild(): ModuleWithProviders<DynamicPermissionModule> {
    return {
      ngModule: DynamicPermissionModule,
      providers: [],
    };
  }

  static forLazy(): NgModuleFactory<DynamicPermissionModule> {
    return new LazyModuleFactory(DynamicPermissionModule.forChild());
  }
}
