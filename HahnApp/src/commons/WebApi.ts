import { Applicant } from './../models/Applicant';
import {HttpClient, json} from 'aurelia-fetch-client';

export class WebApi
{
  static inject = [HttpClient];
  baseUrl: string;
  client: HttpClient;
  constructor()
  {
      this.baseUrl = 'https://localhost:5001/api/';
      this.client = new HttpClient()
          .configure(x => {
          x.withBaseUrl(this.baseUrl)
          .withDefaults({
            headers: {
              'Accept': 'application/json',
              'X-Requested-With': 'Fetch'
            }
          })
          .withInterceptor({
            request(request) {
              console.log(`Requesting ${request.method} ${request.url}`);
              return request;
            },
            response(response) {
              console.log(`Received ${response.status} ${response.url}`);
              return response;
            }
          });
      });
  }

  postApplicant(applicant:Applicant)
  {
    applicant.ID = null;
    this.client.fetch('create',{
      method:"post",
      body:json(applicant)
    })
    .then(response => response.json())
    .then(savedApplicant=> {
      return savedApplicant;
    })
    .catch(error =>{
      return error;
    });
  }

  async getApplicants() {
    await this.client.fetch('',{
      method:"get"
    })
    .then(response => response.json())
    .then(applicantList => {
      console.log(applicantList);
      return applicantList;
    })
    .catch(error =>{
      return error;
    });
  }

  getApplicantWithId(id:number){
    this.client.fetch(`${id}`,{
      method:"get"
    })
    .then(response => response.json())
    .then(applicant => {
      return applicant;
    })
    .catch(error => {
      return error;
    });
  }

  deleteApplicant(id:number){
    this.client.fetch(`delete/${id}`,{
      method:"delete"
    })
    .then(response => response.json())
    .then(applicant => {
      return applicant;
    })
    .catch(error => {
      return error;
    });
  }

  updateApplicant(id:number,applicant:Applicant){
    this.client.fetch(`update/${id}`,{
      method:"delete",
      body:json(applicant)
    })
    .then(response => response.json())
    .then(applicant => {
      return applicant;
    })
    .catch(error => {
      return error;
    });
  }

}

