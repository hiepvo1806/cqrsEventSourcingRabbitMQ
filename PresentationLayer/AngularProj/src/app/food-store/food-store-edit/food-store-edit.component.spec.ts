import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FoodStoreEditComponent } from './food-store-edit.component';

describe('FoodStoreEditComponent', () => {
  let component: FoodStoreEditComponent;
  let fixture: ComponentFixture<FoodStoreEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FoodStoreEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FoodStoreEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
