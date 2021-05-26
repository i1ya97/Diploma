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