import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { HttpClient } from '@angular/common/http';
import { Component, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatVerticalStepper } from '@angular/material/stepper';

import * as moment from 'moment';
import { environment } from 'src/environments/environment';

interface IGetRegistrationResult {
  id: number;
  ssn: number;
  firstName: string;
  lastName: string;
}

interface IVaccinationResponse {
  id: number;
  registrationId: number;
  vaccinationDate: moment.Moment;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public step1Form: FormGroup;
  public step2Form: FormGroup;
  public step3Form: FormGroup;
  public step4Form: FormGroup;

  public couldNotFindRegistration = false;
  public isSubmitting = false;
  public apiError: any;
  public minDate = moment("20210118", "YYYYMMDD");
  public maxDate = moment("20210120", "YYYYMMDD");
  public availableSlots?: moment.Moment[];
  public registrationId?: number;
  public ssn?: number;
  public firstName?: string;
  public lastName?: string;
  public date?: moment.Moment;
  public datetime?: moment.Moment;
  public vaccinationId?: number;

  @ViewChild(MatVerticalStepper) stepper!: MatVerticalStepper;

  constructor(private fb: FormBuilder, private client: HttpClient) {
    this.step1Form = fb.group({
      ssn: [null, [Validators.required]],
      pin: [null, [Validators.required]]
    });

    this.step2Form = fb.group({
      date: [null, [Validators.required]]
    });

    this.step3Form = fb.group({
      datetime: [null, [Validators.required]]
    });

    this.step4Form = fb.group({ });
  }

  ngOnInit(): void {
  }

  next1() {
    if (this.step1Form.valid && !this.isSubmitting) {
      this.isSubmitting = true;
      const url = `${environment.baseApiUrl}/registrations?ssn=${this.step1Form.controls.ssn.value}&pin=${this.step1Form.controls.pin.value}`;
      this.client.get<IGetRegistrationResult>(url).subscribe(result => {
        this.apiError = '';
        this.isSubmitting = false

        if (!result) this.couldNotFindRegistration = true;
        else {
          this.couldNotFindRegistration = false;
          this.ssn = result.ssn;
          this.firstName = result.firstName;
          this.lastName = result.lastName;
          this.registrationId = result.id;
          this.stepper.next();
        }
      },
      err => {
        this.isSubmitting = false
        if (err.status === 404) {
          this.couldNotFindRegistration = true;
          this.apiError = '';
        } else {
          this.apiError = err;
        }
      });
    }
  }

  next2() {
    if (this.step2Form.valid) {
      this.date = this.step2Form.controls.date.value;
      this.stepper.next();
    }
  }

  next3() {
    if (this.step3Form.valid) {
      this.datetime = this.step3Form.controls.datetime.value;
      this.stepper.next();
    }
  }

  stepChanged(event: StepperSelectionEvent) {
    switch (event.selectedIndex)
    {
      case 2:
        this.isSubmitting = true;
        const url = `${environment.baseApiUrl}/registrations/timeSlots?date=${this.date?.format('YYYY-MM-DD')}`;
        this.client.get<string[]>(url).subscribe(result => {
          this.isSubmitting = false;
          if (!result) {
            this.apiError = 'Web API did not return meaningful time slots';
            return;
          }

          this.availableSlots = result.map(s => moment(s));
        },
        err => {
          this.isSubmitting = false;
          this.apiError = err;
        });
        break;
      case 3:
        this.isSubmitting = true;
        this.client.post<IVaccinationResponse>(`${environment.baseApiUrl}/vaccinations`, {
          registrationId: this.registrationId,
          datetime: this.datetime?.format('YYYY-MM-DDThh:mm:ss')
        }).subscribe(result => {
          this.isSubmitting = false;
          if (!result) {
            this.apiError = 'Web API did not return meaningful vaccination response';
            return;
          }

          this.vaccinationId = result.id;
        },
        err => {
          this.isSubmitting = false;
          this.apiError = err;
        });
        break;
    }
  }
}
