# Decision-Tree-ALg
Creating a decision tree platform for different types of data. Currently only Boolean(Yes/No) problems are solvable. No pruning is available yet.  

[![Shippable branch](https://img.shields.io/shippable/5444c5ecb904a4b21567b0ff/master.svg?style=plastic)](https://github.com/IvanGrigorov/Decision-Tree-ALg.git)

[![Build status](https://ci.appveyor.com/api/projects/status/0wggl5q6degruk4h/branch/master?svg=true)](https://ci.appveyor.com/project/IvanGrigorov/decision-tree-alg/branch/master)

## Latest Build Statuses (Branches):
### UnitTesting

![alt tag](https://decision-tree-platform.visualstudio.com/_apis/public/build/definitions/8adcea37-f6d7-42ab-9dcc-d4fe29ed90b2/1/badge)

[![Jenkins tests](https://img.shields.io/badge/tests-passing-brightgreen.svg)](https://github.com/IvanGrigorov/Decision-Tree-ALg/tree/UnitTesting)

### ConsoleInteraction (Experimental)

![alt tag](https://decision-tree-platform.visualstudio.com/_apis/public/build/definitions/8adcea37-f6d7-42ab-9dcc-d4fe29ed90b2/1/badge)


This project is created to help others with their work. If you like the idea and the project I will be glad for some recommendations like stars, in order more people to see it. You can also fork it and customize your own version of the product. 

# DECISION TREE ALG PLATFORM 


## Summary
--------------

The goal of the project is to create autonomously decision tree platform, in order to be used in different scenarios. The idea behind the algorithm is based on the machine learning approach for classifying different examples. 

More info about the apprach you can find here: https://en.wikipedia.org/wiki/Decision_tree_learning 

(more specificly ID3). 

## How to use: 
----------------- 

### UnitTesting 

There is one config file, which is important for adjusting the algorithm with the situtaion. It is important to know that the names of the Features(Attributes) must be well chosen. Then there is a thing to be done - to add a class similar to DataEntity, which has to implement IDataEntity and its properties must match the names of the features in the configuration. The length of feature sets must also be set, alongside the name of the new class. The result of the program will be saved as a txt file in the Results folder with name, corresponding current DateTime. 

See the [Wiki](https://github.com/IvanGrigorov/Decision-Tree-ALg/wiki) for more information about the configuration. 

### ConsoleInteraction (Experimental)

This is new version of product where there is no need to make any changes into the code, but the autonomous work remains. There is new "dll" library to use the console interaction, but can be easily be adjusted to use commands from other "API" - s. 

This requires no additional configuration. All of the work is done with the help of the console interface, which has some available commands: 

| Command | Description | 
| ------- | ----------- |
| exit | This exits the application |
| help | This shows all the possible commands and their description |
| load examples \<pathfile\> | loads the examples for training |
| generate to \<target path file\> | generates the tree |
| minify | sets the tree to minified version |
| maxify | sets the tree to maxified version |
| load test examples \<pathfile\> | load examples for testing | 
| test | tests the algorithm and returns the accuracy percentage |
| generate config \<pathfile\> | Generates new configuration for the node structure |

The exmples file must have the following syntax: 
----------------------------------------------------- 

RainTypeClassificated,TempClassificated,HumidClassificated,WindClassificated,ClassifiedResult
Low,Hot,High,Weak,No\
Low,Hot,High,Strong,No\
Normal,Hot,High,Weak,Yes\
High,Mild,High,Weak,Yes\
High,Cool,Normal,Weak,Yes\
High,Cool,Normal,Strong,No\
Normal,Cool,Normal,Strong,Yes\
Low,Mild,High,Weak,No\
Low,Cool,Normal,Weak,Yes\
High,Mild,Normal,Weak,Yes\
Low,Mild,Normal,Strong,Yes\
Normal,Mild,High,Strong,Yes\
Normal,Hot,Normal,Weak,Yes\
High,Mild,High,Strong,No

---------------------------------------------------------------

Here we see that we must specify the name of features on the first row and seperated with commas.

The config file must look like this: 
---------------------------------------------------------------

RainTypeClassificated,Low,Normal,High\
TempClassificated,Hot,Mild,Cool\
HumidClassificated,High,Normal,Low\
WindClassificated,Weak,Strong\
ClassifiedResult,No,Yes

---------------------------------------------------------------

Again we see the same structure. **Remember to say the classificaion types and possible values in the end** 

Here is a small demo of the console intreface: 

![](https://github.com/IvanGrigorov/Decision-Tree-ALg/blob/master/Sep%2010%202017%206-20%20PM.gif)


## Now Developing 
----------------- 

Most of the work is concentrated on the interface for now. However you can allways make in issue about any part of the project, but of course you must specify the description of it.   

## More Information: 
----------------------- 

For more information, questions and ideas, you can contact with me: ivangrigorov9@gmail.com
