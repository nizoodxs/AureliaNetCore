import { bindable } from 'aurelia-framework';
import { BootstrapFormRenderer } from './commons/BootstrapFormRenderer';
import { ValidationControllerFactory, ValidationRules, validateTrigger } from 'aurelia-validation';
import { inject } from 'aurelia-dependency-injection';
import { Applicant } from './models/Applicant';

@inject(ValidationControllerFactory)
export class App {
  heading = "HahnApp";
  controller = null;

  applicants: Applicant[] = [];
  @bindable formApplicant:Applicant;

  constructor(controllerFactory: ValidationControllerFactory){
    this.controller=controllerFactory.createForCurrentScope();
    this.controller.addRenderer(new BootstrapFormRenderer());
    
  }

  send_onclick() {
    //console.log(this.formApplicant);

    //check if all fields are entered
    this.controller.validate().then(result=>{
      if(result.valid){
        console.log("valid");
      }
      else{
        console.log("invalid");
      }
    });

    //push to list 
    //this.applicants.push(this.formApplicant);

    //reset all fields
  }

  reset_onclick(){
    console.log("reset clicked");
  }

  removeApplicant(applicant) {
    let index = this.applicants.indexOf(applicant);
    if (index !== -1) {
      this.applicants.splice(index, 1);
    }
  }

  formApplicantChanged(newValue) {
    if (this.formApplicant) {
      ValidationRules
        .ensure('Name').required().minLength(5).withMessage('Name can not be less than 5 characters')
        .ensure('FamilyName').required().minLength(5).withMessage('Family name can not be less than 5 characters')
        .ensure('Address').required().minLength(10).withMessage('Address can not be less than 10 characters')
        .ensure('CountryOfOrigin').required()//need to implement api call
        .ensure('EmailAddress').required().email().withMessage(`\${$value} is not a valid email.`)
        .ensure('Age').displayName('Age').required().satisfiesRule('integerRange',20,60)
        .on(this.formApplicant);
    }
  }
}

ValidationRules.customRule(
  'integerRange',
  (value, obj, min, max) => !(value === null || value === undefined
    || Number.isInteger(value) && value > min && value < max),
  `\${$displayName} must be an integer between \${$config.min} and \${$config.max}.`,
  (min, max) => ({ min, max }));  
