// export class App {
//   public message: string = 'Hello World!';
// }

interface Applicant {
  
  ID: number;
  Name: string;
  FamilyName: string;
  Address: string;
  CountryOfOrigin: string;
  EmailAddress: string;
  Age: number;
  Hired: boolean;
}

export class App {
  heading = "HahnApp";
  applicants: Applicant[] = [];
  formApplicant:Applicant;

  send_onclick() {
    console.log(this.formApplicant);
    //check if all fields are entered

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
}
