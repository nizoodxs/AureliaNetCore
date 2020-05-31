import { WebApi } from './commons/WebApi';
import { Applicant } from "models/Applicant";


export class ViewApplicant{
  
  applicants: Applicant[] = []; 
  webapi:WebApi;

  constructor(){
    this.webapi = new WebApi();
    this.loadApplicants();
  }

  remove(applicant:Applicant){
    let index = this.applicants.indexOf(applicant);
    if (index !== -1) {
      this.deleteApplicant(applicant.ID);
      this.applicants.splice(index, 1);      
    }
  }


  deleteApplicant(id:number){
    this.webapi.client.fetch(`delete/${id}`,{
      method:"delete"
    })
    .then(response => response.json())
    .then(applicant => {
      console.log("deleted");
    })
    .catch(error => {
      console.log("error");
    });
  }

  loadApplicants() {
    this.webapi.client.fetch('',{
      method:"get"
    })
    .then(response => response.json())
    .then(applicantList => {
      applicantList.forEach(element => {

        var applicant = new Applicant();
        applicant.ID = element.id;
        applicant.Name = element.name;
        applicant.Address = element.address;
        applicant.Age=element.age;
        applicant.CountryOfOrigin=element.countryoforigin;
        applicant.EmailAddress=element.emailaddress;
        applicant.FamilyName=element.familyname;
        applicant.Hired=element.hired;

        this.applicants.push(applicant);
      });
    })
    .catch(error =>{
      console.log(error);
    });
  }

}
