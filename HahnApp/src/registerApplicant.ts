import { bindable } from 'aurelia-framework';
import { Applicant } from './models/Applicant';
import { BootstrapFormRenderer } from './commons/BootstrapFormRenderer';
import { ValidationControllerFactory, ValidationRules } from 'aurelia-validation';
import { inject } from 'aurelia-dependency-injection';
import { WebApi } from './commons/WebApi';
import { json } from 'aurelia-fetch-client';

@inject(ValidationControllerFactory)
export class RegisterApplicant{
  @bindable formApplicant:Applicant;
  controller = null;
  webapi:WebApi;

  constructor(controllerFactory: ValidationControllerFactory){
    this.controller=controllerFactory.createForCurrentScope();
    this.controller.addRenderer(new BootstrapFormRenderer());
    this.webapi = new WebApi();    
  }
  
  send_onclick() {
    
    this.controller.validate().then(result=>{
      if(result.valid){
        this.postApplicant(this.formApplicant);
      }
      else{
        console.log("invalid");
      }
    });
  }

  reset_onclick(){
    console.log("reset clicked");
    this.formApplicant = null;
  }

  formApplicantChanged(newValue) {
    if (this.formApplicant) {
      ValidationRules
        .ensure('Name').required().minLength(5).withMessage('Name can not be less than 5 characters')
        .ensure('FamilyName').required().minLength(5).withMessage('Family name can not be less than 5 characters')
        .ensure('Address').required().minLength(10).withMessage('Address can not be less than 10 characters')
        .ensure('CountryOfOrigin').required()//need to implement api call
        .ensure('EmailAddress').required().email().withMessage(`\${$value} is not a valid email.`)
        .ensure('Age').displayName('Age').required().range(20,60)
        .on(this.formApplicant);
    }
  }

  postApplicant(applicant:Applicant)
  {
    this.webapi.client.fetch('create',{
      method:"post",
      body:json(applicant)
    })
    .then(response => {
        if(response.status!=201)
        {
          console.log(response.json());
          alert("error");
          return;
        }
        else
        {
          this.formApplicant=null;
        }
    })
    .catch(error =>{
      console.log(error);
      //return error;
    });
  }
}

