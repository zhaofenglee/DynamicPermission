import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { DynamicPermissionComponent } from './components/dynamic-permission.component';
import { DynamicPermissionService } from '@j-s.Abp/dynamic-permission';
import { of } from 'rxjs';

describe('DynamicPermissionComponent', () => {
  let component: DynamicPermissionComponent;
  let fixture: ComponentFixture<DynamicPermissionComponent>;
  const mockDynamicPermissionService = jasmine.createSpyObj('DynamicPermissionService', {
    sample: of([]),
  });
  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [DynamicPermissionComponent],
      providers: [
        {
          provide: DynamicPermissionService,
          useValue: mockDynamicPermissionService,
        },
      ],
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DynamicPermissionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
