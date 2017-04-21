# Decision-Tree-ALg
Creating a decision tree platform for different types of data. Currently only Boolean(Yes/No) problems are solvable. No pruning is available yet.  

[![Shippable branch](https://img.shields.io/shippable/5444c5ecb904a4b21567b0ff/master.svg?style=plastic)](https://github.com/IvanGrigorov/Decision-Tree-ALg.git)

[![Build status](https://ci.appveyor.com/api/projects/status/0wggl5q6degruk4h/branch/master?svg=true)](https://ci.appveyor.com/project/IvanGrigorov/decision-tree-alg/branch/master)

https://decision-tree-platform.visualstudio.com/_apis/public/build/definitions/8adcea37-f6d7-42ab-9dcc-d4fe29ed90b2/1/badge

# DECISION TREE ALG PLATFORM 


## Summary
--------------

The goal of the project is to create autonomously decision tree platform, in order to be used in different scenarios. The idea behind the algorithm is based on the machine learning approach for classifying different examples. 

More info about the apprach you can find here: https://en.wikipedia.org/wiki/Decision_tree_learning 

(more specificly ID3). 

## How to use: 
----------------- 

There is one config file, which is important for adjusting the algorithm with the situtaion. It is important to know that the names of the Features(Attributes) must be well chosen. Then there is a thing to be done - to add a class similar to DataEntity, which has to implement IDataEntity and its properties must match the names of the features in the configuration. The length of feature sets must also be set, alongside the name of the new class. The result of the program will be saved as a txt file in the Results folder with name, corresponding current DateTime. 

## More Information: 
----------------------- 

For more information, questions and ideas, you can contact with me: ivangrigorov9@gmail.com
