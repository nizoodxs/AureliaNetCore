import { PLATFORM } from 'aurelia-pal';
import {RouterConfiguration, Router} from 'aurelia-router';


export class App {
  heading = "HahnApp";

  
  router: Router;
  
  configureRouter(config: RouterConfiguration, router: Router): void {
    this.router = router;
    config.title = 'HahnApp';
    config.map([
      { route: ['','applicant'], name: 'view', moduleId:PLATFORM.moduleName('viewApplicant'), title:'HahnApp' },
      { route: 'create', name: 'create', moduleId:PLATFORM.moduleName('registerApplicant'), title:'HahnApp' }
    ]);
  }

}
