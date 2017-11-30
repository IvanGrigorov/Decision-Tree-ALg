using Decision_Tree_Alg.Testing;
using Decision_Tree_ALg;
using Decision_Tree_ALg.Config;
using Decision_Tree_ALg.ReadingLibrary;
using Decision_Tree_ALg.TreeStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace ConsoleInteraction
{
    public class InteractionConfig
    {
        ITutorialLoader TutorialLoader { get; } = new ConsoleTutorialLoader();
        private static Lazy<InteractionConfig> singelton = new Lazy<InteractionConfig>();
        public static InteractionConfig LazyInteractionConfig { get { return singelton.Value; } }
        IIteractionDialog dialog = new ConsoleDialog();
        private Boolean AreExamplesLoaded { get; set; } = false;
        private Boolean AreTestExamplesLoaded { get; set; } = false;
        private Boolean IsTreeGenerated { get; set; } = false;
        private Testing testingConfig = new Testing();
        private ItreeNode tree { get; set; }
        private Boolean IsConfigLoaded { get; set; } = false;

        public ICollection<Tuple<String, String>> commandCollection = new List<Tuple<String, String>>()
        {
            new Tuple<string, string>("exit", "This exits the application"),
            new Tuple<string, string>("help", "This shows all the possible commands and their description"),
            new Tuple<string, string>("load examples <pathfile>", "load examples <pathfile> - loads the examples for training"),
            // new Tuple<string, string>("test metrics with <pathfile>", "test metrics with <pathfile> - give the test results for trained tree"),
            new Tuple<string, string>("generate to <target path file>", "generates the tree"),
            new Tuple<string, string>("minify", "sets the tree to minified version"),
            new Tuple<string, string>("maxify", "sets the tree to maxified version"),
            new Tuple<string, string>("load test examples <pathfile>","load examples for testing"),
            new Tuple<string, string>("test", "tests the algorithm and returns the accuracy percentage "),
            new Tuple<string, string>("generate config <pathfile>", " Generates new configuration for the node structure")

        };

        /// <summary>
        /// Here start the methods that correspond to the commands
        /// </summary>
        /// <remarks>
        /// Should remove all Console.Writeline() messages and make them from interface 
        /// </remarks>
        public void Help()
        {
            foreach (Tuple<String, String> command in this.commandCollection)
            {
                Console.WriteLine("{0} ----- {1}", command.Item1, command.Item2);
            }
            this.MoveToNextCommand();
        }

        public void Exit()
        {
            Environment.Exit(0);
        }

        public void Minify()
        {
            InitialConfig.GetInstance().IsMemoryOptimized = true;
            //Console.WriteLine("The tree has been minified successfully.");
            dialog.PrintSuccessfullMessage("The tree has been minified successfully.");
            dialog.PrintAllertMessage("To maxify it again use 'maxify'.");
            this.MoveToNextCommand();
        }

        public void Maxify()
        {
            InitialConfig.GetInstance().IsMemoryOptimized = false;
            dialog.PrintSuccessfullMessage("The tree has been maxified successfully.");
            dialog.PrintAllertMessage("To minify it again use 'minify'.");
            this.MoveToNextCommand();
        }

        public void Generate(string pathFile)
        {
            if (!this.IsConfigLoaded)
            {
                dialog.PrintErrorMessage("Please load the config first");
                this.MoveToNextCommand();
            }
            else if (!this.AreExamplesLoaded)
            {

                dialog.PrintErrorMessage("Please load the examples first");
                this.MoveToNextCommand();

            }
            else if (!Directory.Exists(pathFile))
            {
                dialog.PrintErrorMessage("Such directory does not exist. Please give new one.");
                dialog.PrintAllertMessage("It will be more reliable if you use the whole path.");
                this.MoveToNextCommand();
            }
            else
            {
                Console.WriteLine("Generating...");
                InitialConfig.GetInstance().ResultDirectory = pathFile;
                Task startedTreeConstruction = Task.Factory.StartNew(() => StartUp.Start());
                startedTreeConstruction.ContinueWith((antecendent) =>
                {
                    this.IsTreeGenerated = true;
                    dialog.PrintSuccessfullMessage("The tree has been generated successfully.");
                    this.MoveToNextCommand();
                }).Wait();

            }

        }

        public void LoadExamples(string ressourcePathFile)
        {
            if (!File.Exists(ressourcePathFile))
            {
                dialog.PrintErrorMessage("Such file does not exist. Please give new one.");
                dialog.PrintAllertMessage("It will be more reliable if you use the whole path.");
                this.MoveToNextCommand();
            }
            else if (!this.IsConfigLoaded)
            {
                dialog.PrintErrorMessage("Load tree config first ");
                this.MoveToNextCommand();
            }
            else
            {
                try
                {
                    // TODO: Generate new class and give the type 
                    // Type generatedType = generatedClass.GetType()
                    InitialConfig.InitialExamples = DataReader.ReturnAllExamplesFromFile(ressourcePathFile);
                    this.AreExamplesLoaded = true;
                    dialog.PrintSuccessfullMessage("The data has been loaded successfully.");
                    this.MoveToNextCommand();


                }
                catch (InvalidDataException exception)
                {
                    dialog.PrintErrorMessage(exception.Message);
                    this.MoveToNextCommand();
                }
                //this.MoveToNextCommand();
            }
        }
        public void Test()
        {
            // Test for generated Tree
            // Test for file
            // Test for static tree 
            if (!this.AreTestExamplesLoaded)
            {
                dialog.PrintErrorMessage("Please load test examples");
                this.MoveToNextCommand();
            }
            else if (!this.IsTreeGenerated)
            {
                dialog.PrintErrorMessage("Please generate a tree first");
                this.MoveToNextCommand();
            }
            else
            {
                dialog.PrintSuccessfullMessage(testingConfig.ReturnSuccessfullPercentageRateAfterTesting().ToString() + " % Accuracy");
                this.MoveToNextCommand();
            }

        }

        public void LoadTestExamples(string pathFile)
        {

            if (!File.Exists(pathFile))
            {
                dialog.PrintErrorMessage("There is no such file.");
                dialog.PrintAllertMessage("It will be more reliable if you use the whole path.");
                this.MoveToNextCommand();
            }
            else
            {
                testingConfig.LoadDataTestExamples(pathFile);
                dialog.PrintSuccessfullMessage("Test examples are loaded successfully.");
                this.AreTestExamplesLoaded = true;
                this.MoveToNextCommand();

            }

        }

        public void GenerateConfig(string pathFile)
        {
            if (!File.Exists(pathFile))
            {
                dialog.PrintErrorMessage("There is no such file.");
                dialog.PrintAllertMessage("It will be more reliable if you use the whole path.");
                this.MoveToNextCommand();
            }
            else
            {
                try
                {
                    DataReader.ReadTreeNodeConfigFromFile(pathFile);
                    dialog.PrintSuccessfullMessage("The config is loaded successfully.");
                    this.IsConfigLoaded = true;
                    this.MoveToNextCommand();

                }
                catch (InvalidDataException exception)
                {
                    dialog.PrintErrorMessage(exception.Message);
                    dialog.PrintAllertMessage("Remember, no symbols such as !@#$%^&*()_- and numbers are allowed in config files");
                    dialog.PrintAllertMessage("Remember, also that for a tree config there must be at least one possible value for node name");
                    dialog.PrintAllertMessage("For more details you can see the examles for tree config and data files in the Config folder of the library ");
                    this.MoveToNextCommand();

                }

            }
        }

        public void CommandMapping(string command, Queue<String> parameters = null)
        {
            MethodInfo commandMethod = this.GetType().GetMethod(command);
            if (parameters == null)
            {
                commandMethod.Invoke(this, null);
            }
            else
            {
                commandMethod.Invoke(this, parameters.ToArray());
            }
        }
        public void MoveToNextCommand()
        {
            //Queue<string> commandDetails;
            try
            {
                Queue<string> commandDetails = dialog.GetCommand();
                this.CommandMapping(commandDetails.Dequeue(), commandDetails);
            }
            catch (ArgumentException exception)
            {
                dialog.PrintErrorMessage(exception.Message);
                this.MoveToNextCommand();
            }

        }
    }
}
