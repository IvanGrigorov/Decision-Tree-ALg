﻿using NUnit.Framework;
using Decision_Tree_ALg.TreeStructures;
using System;
using System.Collections.Generic;

namespace DecisionTreeTests.TreeStructures
{
    [TestFixture]
    class TreeNodeTests
    {
        //  { "RainTypeClassificated", new string[] { "Low", "High", "Normal" } },
        // { "TempClassificated", new string[] { "Hot", "Mild", "Cool" } },
        //  { "HumidClassificated", new string[] { "Low", "High", "Normal" } },
        //  { "WindClassificated", new string[] { "Strong", "Weak" } },
        //  { "ClassifiedResult", new string[] { "Yes", "No" } },


        // Start Testing ReturnAllExamplesAfterValue Method

        [TestCase("RainTypeClassificated", "InvalidOutcome")]
        public void TreeNode_ReturnAllExamplesAfterValue_ShouldReturnEmptyArrayAfterNoMatch(string feature, string outcome)
        {
            // Arrange
            var treeNode = new TreeNode("TreeNodeExample", new string[] { "Low", "High", "Normal" }, new List<ItreeNode>(), new Dictionary<string, string>());

            // Act
            var result = treeNode.ReturnAllExamplesAfterValueCondition(feature, outcome);

            //Assert
            Assert.IsEmpty(result);
        }

        [TestCase("InvalidFeatureName", "InvalidOutcome")]
        public void TreeNode_ReturnAllExamplesAfterValue_ShouldThrowArgumentExceptionWithInvalidFeatureName(string feature, string outcome)
        {
            // Arrange
            var treeNode = new TreeNode("TreeNodeExample", new string[] { "Low", "High", "Normal" }, new List<ItreeNode>(), new Dictionary<string, string>());


            // Act && Assert
            Assert.Throws<NullReferenceException>(() => treeNode.ReturnAllExamplesAfterValueCondition(feature, outcome));
        }

        [TestCase("RainTypeClassificated", "Low")]
        public void TreeNode_ReturnAllExamplesAfterValue_ShouldReturnCorrectIntOnMatch(string feature, string outcome)
        {
            // Arrange
            var treeNode = new TreeNode("TreeNodeExample", new string[] { "Low", "High", "Normal" }, new List<ItreeNode>(), new Dictionary<string, string>());

            // Act
            var result = treeNode.ReturnAllExamplesAfterValueCondition(feature, outcome);


            // Assert
            Assert.AreEqual(5, result.Length);
        }

        // Start Testing DeclareLeafInf Method
        /// <summary>
        /// In order to test this method remember what you have set in the Config file.
        /// The Input Params strongly depend on the  number of classification outcomes set in the configuration :) 
        /// </summary>

        [TestCase(new int[] { 1, 1 })]
        [TestCase(new int[] { 5, 16 })]
        [TestCase(new int[] { 16, 5 })]
        public void TreeNode_DeclareLeafInf_ShouldReturnNodeWhenBothPositiveAndNegaitiveExamplesAmountAreDifferentFromZero(int[] positiveAndNegativeExamplesAmount)
        {
            // Arrange
            var treeNode = new TreeNode("TreeNodeExample", new string[] { "Low", "High", "Normal" }, new List<ItreeNode>(), new Dictionary<string, string>());

            // Act
            var result = treeNode.DeclareLeafInf(positiveAndNegativeExamplesAmount);

            // Assert
            Assert.AreEqual("Node", result);
        }

        [TestCase(new int[] { 0, 1 })]
        [TestCase(new int[] { 0, 5 })]
        [TestCase(new int[] { 0, 30 })]
        public void TreeNode_DeclareLeafInf_ShouldReturnYesWhenFirstElementIsZero(int[] positiveAndNegativeExamplesAmount)
        {
            // Arrange
            var treeNode = new TreeNode("TreeNodeExample", new string[] { "Low", "High", "Normal" }, new List<ItreeNode>(), new Dictionary<string, string>());

            // Act
            var result = treeNode.DeclareLeafInf(positiveAndNegativeExamplesAmount);


            // Assert
            Assert.AreEqual("No", result);
        }

        [TestCase(new int[] { 0, 0 })]
        public void TreeNode_DeclareLeafInf_ShouldThrowArgumentExceptionWhenBothPositiveAndNegativeExamplesAmountsAreZero(int[] positiveAndNegativeExamplesAmount)
        {
            // Arrange
            var treeNode = new TreeNode("TreeNodeExample", new string[] { "Low", "High", "Normal" }, new List<ItreeNode>(), new Dictionary<string, string>());

            // Act && Assert
            Assert.Throws<ArgumentException>(() => treeNode.DeclareLeafInf(positiveAndNegativeExamplesAmount));
        }


        [TestCase(new int[] { -1, -1 })]
        public void TreeNode_DeclareLeafInf_ShouldThrowArgumentExceptionWhenBothPositiveAndNegativeExamplesAmountsAreNegative(int[] positiveAndNegativeExamplesAmount)
        {
            // Arrange
            var treeNode = new TreeNode("TreeNodeExample", new string[] { "Low", "High", "Normal" }, new List<ItreeNode>(), new Dictionary<string, string>());

            // Act && Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => treeNode.DeclareLeafInf(positiveAndNegativeExamplesAmount));
        }

        [TestCase(new int[] { 1, 0 })]
        [TestCase(new int[] { 5, 0 })]
        [TestCase(new int[] { 30, 0 })]
        public void TreeNode_DeclareLeafInf_ShouldReturnNoWhenSecondElementIsZero(int[] positiveAndNegativeExamplesAmount)
        {
            // Arrange
            var treeNode = new TreeNode("TreeNodeExample", new string[] { "Low", "High", "Normal" }, new List<ItreeNode>(), new Dictionary<string, string>());

            // Act
            var result = treeNode.DeclareLeafInf(positiveAndNegativeExamplesAmount);


            // Assert
            Assert.AreEqual("Yes", result);
        }

        // Testing CalculateAmountOfExmplesWithSpecificProperties method 

        [TestCase("InvalidFeature", "Low")]
        [TestCase("InvalidFeature", "InvalidOutcome")]
        public void TreeNode_CalculateAmountOfExmplesWithSpecificProperties_ShouldThrowNullArgumentExceptionWhenFeatureNameIsInvalid(string feature, string outcome)
        {
            // Arrange
            var treeNode = new TreeNode("TreeNodeExample", new string[] { "Low", "High", "Normal" }, new List<ItreeNode>(), new Dictionary<string, string>());

            // Act && Assert
            Assert.Throws<NullReferenceException>(() => treeNode.CalculateAmountOfExmplesWithSpecificProperties(feature, outcome));
        }

        [TestCase("RainTypeClassificated", "Loww")]
        [TestCase("RainTypeClassificated", "InvalidOutcome")]
        public void TreeNode_CalculateAmountOfExmplesWithSpecificProperties_ShouldThrowArgumentExceptionAfterInvalidOutcome(string feature, string outcome)
        {
            // Arrange
            var treeNode = new TreeNode("TreeNodeExample", new string[] { "Low", "High", "Normal" }, new List<ItreeNode>(), new Dictionary<string, string>());

            // Act && Assert
            Assert.Throws<ArgumentException>(() => treeNode.CalculateAmountOfExmplesWithSpecificProperties(feature, outcome));
        }

        // Testing CalculateAmountOfExmplesWithSpecificProperties method 

        [TestCase("RainTypeClassificated", "Low")]
        public void TreeNode_CalculateAmountOfExmplesWithSpecificProperties_ShouldReturnCorrectAmountOnMatch(string feature, string outcome)
        {
            // Arrange
            var treeNode = new TreeNode("TreeNodeExample", new string[] { "Low", "High", "Normal" }, new List<ItreeNode>(), new Dictionary<string, string>());

            // Act 
            int amount = treeNode.CalculateAmountOfExmplesWithSpecificProperties(feature, outcome);

            // Assert 
            Assert.AreEqual(5, amount);
        }

        [TestCase("HumidClassificated", "Low")]
        public void TreeNode_CalculateAmountOfExmplesWithSpecificProperties_ShouldReturnZeroOnMissMatsch(string feature, string outcome)
        {
            // Arrange
            var treeNode = new TreeNode("TreeNodeExample", new string[] { "Low", "High", "Normal" }, new List<ItreeNode>(), new Dictionary<string, string>());

            // Act 
            int amount = treeNode.CalculateAmountOfExmplesWithSpecificProperties(feature, outcome);

            // Assert 
            Assert.AreEqual(0, amount);
        }

        /// <remarks>
        /// Tests for UpdateInfo and UpdateInfGain should be included
        /// </remarks>
        /// 
        /// <remarks> 
        /// Include Mock Testing 
        /// <remarks> 

    }
}