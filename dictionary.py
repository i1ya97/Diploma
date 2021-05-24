import gensim
from gensim.test.utils import common_texts
from gensim.models import Word2Vec
from gensim.models import KeyedVectors
from gensim.test.utils import get_tmpfile
import gensim.downloader as api
import json

document = open('dictionary.json')

jsonData = json.load(document)

data = [d for d in jsonData]

def create_tagged_document(list_of_list_of_words):
    for i, list_of_words in enumerate(list_of_list_of_words):
        yield gensim.models.doc2vec.TaggedDocument(list_of_words, [i])

train_data = list(create_tagged_document(data))

model = gensim.models.doc2vec.Doc2Vec(vector_size=3, min_count=2, epochs=40)

model.build_vocab(train_data)

model.train(train_data, total_examples=model.corpus_count, epochs=model.epochs)

fname = get_tmpfile("train_model")

model.save(fname)