import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodStoreCreateComponent } from './food-store-create.component';

describe('FoodStoreCreateComponent', () => {
  let component: FoodStoreCreateComponent;
  let fixture: ComponentFixture<FoodStoreCreateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FoodStoreCreateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FoodStoreCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
