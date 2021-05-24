import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public clusters: Cluster[];
  public attributes: Attribute[];
  public precedent: Precedent;

  public GetClusters(http: HttpClient, @Inject('BASE_URL') baseUrl: string, filter: Filter) {
    http.post<Cluster[]>(baseUrl + 'api/main/get-clusters', filter).subscribe(result => {
      this.clusters = result;      
    }, error => console.error(error))
  };

  public GetAttributes(http: HttpClient, @Inject('BASE_URL') baseUrl: string, ids: number[]) {
    http.post<Attribute[]>(baseUrl + 'api/main/get-attributes', ids).subscribe(result => {
      this.attributes = result;      
    }, error => console.error(error))
  }

  public GetPrecedent(http: HttpClient, @Inject('BASE_URL') baseUrl: string, precedentId: number) {
    http.get<Precedent>(baseUrl + 'api/main/get-precedent/{precedentId}').subscribe(result => {
      this.precedent = result;      
    }, error => console.error(error))
  }
}

interface Filter {
  attributeType: number;
  tags: string[];
}

interface Cluster {
  id: number;
  num: number;
  tags: TagVector[];
  attributeTypeId: number;
  distance: number;
  attributeIds: number[];
}

interface TagVector {
  vector: number[];
  tag: string;
}

interface Attribute {
  id: number;
  desc: string;
  precedentId: number;
  clusterId: number;
  attributeTypeId: number;
}

interface Precedent {
  id: number;
  factDesc: Desc[];
  forecastDesc?: Desc[];
}

interface Desc {
  typeId: number;
  type: string;
  desc: string;
}