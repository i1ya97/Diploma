from gensim import corpora, models, similarities;
import os;
import sys;
import gensim;
import re;
from gensim import corpora;
from gensim import models;
from gensim.models.coherencemodel import CoherenceModel;
import json;

document3 = open('cluster.json')
data = json.load(document3);
stoplist = set('from but it between often other not which other such as no will this that some this could an or may if then be on can so by are is for a of the and to in \n\t\t\t\t\t\t'.split());
texts3 = [[word for word in document.lower().split() if word not in stoplist] for document in data];
all_tokens3 = sum(texts3, []);

tokens_once3 = set(word for word in set(all_tokens3) if all_tokens3.count(word) ==1);

texts3 = [[word for word in text if word not in tokens_once3]for text in texts3];

dictionary3 = corpora.Dictionary(texts3);

corpus3 = [dictionary3.doc2bow(text) for text in texts3];

lda_model3 = gensim.models.ldamodel.LdaModel(corpus=corpus3,
                                           id2word=dictionary3,
                                           num_topics=1, 
                                           random_state=100,
                                           update_every=1,
                                           chunksize=100,
                                           passes=10,
                                           alpha='auto',
                                           per_word_topics=True);

topics3 = lda_model3.print_topics(1,20);

topic3 = "";
for a,b in topics3:
    topic3 = b;
    
topic3 = topic3.split();

top3 = [];
for x in topic3:
    if x != '+':
        x = re.sub(r'[^a-zA-Z]', '', x)
        top3.append(x);
        
jsonstring = json.dumps(top3)
print(jsonstring)
        
