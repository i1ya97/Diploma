import gensim
from gensim.test.utils import common_texts
from gensim.models import Word2Vec
from gensim.models import KeyedVectors
from gensim.test.utils import get_tmpfile
import gensim.downloader as api
import json
import numpy as np
from gensim.test.utils import common_texts
from gensim.models.doc2vec import Doc2Vec, TaggedDocument

document = open('tags.json')

data = json.load(document);

model = Doc2Vec.load('train_model')

vector = []

for x in data:
    vector.append(model.infer_vector([x]).tolist())
    #print(model.infer_vector([x]))


#vectorList = arr.tolist()

jsonVector = json.dumps(vector)
print(jsonVector)
#print(vectorList)