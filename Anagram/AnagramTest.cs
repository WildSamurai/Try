using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnagramApp;

namespace AnagramTest
{
    [TestClass]
    public class AnagramTests
    {
        private readonly Anagram _anagram = new Anagram();

        [TestMethod]
        public void Reverse()
        {

            string input = "Test";


            string result = _anagram.ReverseWord(input);


            Assert.AreEqual("tseT", result);
        }

        [TestMethod]
        public void ReverseA()
        {

            string input = "a1bcd";


           string result = _anagram.ReverseWord(input);

            
            Assert.AreEqual("d1cba", result);
        }

        [TestMethod]
        public void ReverseB()
        {

            string input = "a1b2c3d";


            string result = _anagram.ReverseWord(input);
            

            Assert.AreEqual("d1c2b3a", result);
        }

        [TestMethod]
        public void ReverseC()
        {

            string input = "";


            string result = _anagram.ReverseWord(input);
            

            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void ReverseD()
        {

            string input = "a";


            string result = _anagram.ReverseWord(input);
           

            Assert.AreEqual("a", result);
        }

        [TestMethod]
        public void ReverseE()
        {

            string input = "abcd efgh";


            string result = _anagram.Reverse(input);
            

            Assert.AreEqual("dcba hgfe", result);
        }

        [TestMethod]
        public void ReverseF()
        {

            string input = " a1bcd efg!h";


            string result = _anagram.Reverse(input);

            Assert.AreEqual(" d1cba hgf!e", result);
        }

        [TestMethod]
        public void ReverseG()
        {

            string input = "  ab  cd  ";


            string result = _anagram.Reverse(input);
  
            Assert.AreEqual("  ba  dc  ", result);
        }

        [TestMethod]
        public void ReverseH()
        {

            string input = "";


            string result = _anagram.Reverse(input);
 
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void ReverseO()
        {

            string input = "123!@#";


            string result = _anagram.Reverse(input);
 
            Assert.AreEqual("123!@#", result);
        }

        [TestMethod]
        public void ReverseMix()
        {

            string input = "a1b2c3 d4e5f6!g";


            string result = _anagram.Reverse(input);
        
            Assert.AreEqual("c1b2a3 g6f5e4!d", result);
        }
        
    }
}