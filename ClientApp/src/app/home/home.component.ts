import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  words: string[] = [];
  selectedType: number;
  clusters: Cluster[] = [];
  types = [
    {id: 1, name: 'Symptom'},
    {id: 2, name: 'Consequence'},
    {id: 3, name: 'Vulnerability'},
    {id: 4, name: 'Countermeasure'},
    {id: 5, name: 'Losses'}
  ];

  constructor(private http: HttpClient){

  }

  onClick() {
    const filter: Filter = {attributeType: this.selectedType, tags: this.words};
    this.GetClustersJson(filter);
  }

  public GetClusters(filter: Filter) {
    this.http.post<Cluster[]>('api/main/get-clusters', filter).subscribe(result => {
      this.clusters = result;
    }, error => console.error(error))
  };

  public GetClustersJson(filter: Filter) {
    this.http.get<Cluster[]>('./assets/HierarchyClusters.json').subscribe(result => {
      this.clusters = result;
    }, error => console.error(error))
  };

  // public GetAttributes(http: HttpClient, @Inject('BASE_URL') baseUrl: string, ids: number[]) {
  //   http.post<Attribute[]>(baseUrl + 'api/main/get-attributes', ids).subscribe(result => {
  //     this.attributes = result;      
  //   }, error => console.error(error))
  // }

  // public GetPrecedent(http: HttpClient, @Inject('BASE_URL') baseUrl: string, precedentId: number) {
  //   http.get<Precedent>(baseUrl + 'api/main/get-precedent/{precedentId}').subscribe(result => {
  //     this.precedent = result;      
  //   }, error => console.error(error))
  // }
}