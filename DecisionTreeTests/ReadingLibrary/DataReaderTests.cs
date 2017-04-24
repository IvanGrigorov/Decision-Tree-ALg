using Decision_Tree_ALg.DataEntities;
using System.Collections.Generic;
using Decision_Tree_ALg.PrepareDataLib;
using System;
using Decision_Tree_ALg.Config;
using System.Reflection;
using NUnit.Framework;
using Decision_Tree_ALg.ReadingLibrary;

namespace DecisionTreeTests.ReadingLibrary
{
    [TestFixture]
    public class DataReaderTests
    {
        [TestCase(@"C:\Users\Ivan Grigorov\Desktop\DataExamples.txt", typeof(IDataEntity))]
        [TestCase(@"C: \Users\Ivan Grigorov\Desktop\DataExamples.txt", typeof(ADataEntity))]
        public void DataReader_ReturnAllExamplesFromFile_ShouldReturnArgumentExceptionWhenTypeIsInterfaceOrAbstractClass(string dataFilePath, Type typeOfClassToCreate)
        {
            // Arrange 
            // Act 
            // Arrange && Act && Assert 
            Assert.Throws<ArgumentException>(() => DataReader.ReturnAllExamplesFromFile(dataFilePath, typeOfClassToCreate));
        }

        [TestCase(@"C:\Users\Ivan Grigorov\Desktop\DataExamples.txt", typeof(DataEntity))]
        public void DataReader_ReturnAllExamplesFromFile_ShouldNotThrowWhenTypeIsClass(string dataFilePath, Type typeOfClassToCreate)
        {
            // Arrange 
            // Act 
            // Arrange && Act && Assert 
            Assert.DoesNotThrow(() => DataReader.ReturnAllExamplesFromFile(dataFilePath, typeOfClassToCreate));
        }

        [TestCase(@"C:\Users\Ivan Grigorov\Desktop\InvalidFile.txt", typeof(DataEntity))]
        public void DataReader_ReturnAllExamplesFromFile_ShouldThrowAnExceptionWhenPathIsInvalid(string dataFilePath, Type typeOfClassToCreate)
        {
            // Arrange 
            // Act 
            // Arrange && Act && Assert 
            Assert.Throws<System.IO.FileNotFoundException>(() => DataReader.ReturnAllExamplesFromFile(dataFilePath, typeOfClassToCreate));
        }

    }
}
