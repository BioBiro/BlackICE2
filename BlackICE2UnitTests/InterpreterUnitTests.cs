using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

namespace BlackICE2UnitTests
{
    [TestClass]
    public class InterpreterUnitTests
    {
        [TestMethod]
        public void TestInterpreter_Program1()
        {
            BlackICE2.Computer computer = new BlackICE2.Computer();
            computer.cPU = new BlackICE2.X86CPU(computer);
            computer.memory = new BlackICE2.Memory();

            BlackICE2.Human human = new BlackICE2.Human();

            List<string> listing = new List<string>();
            listing.Add("mov eax, 10");
            listing.Add("call @@myfunction");
            listing.Add("inc eax");
            listing.Add("push eax");
            listing.Add("inc eax");
            listing.Add("pop eax");
            listing.Add("@@myfunction");
            listing.Add("mov ebx, eax");
            listing.Add("push 77");
            listing.Add("inc eax");
            listing.Add("pop eax");
            listing.Add("ret");

            BlackICE2.Program program = human.CreateProgram(computer, listing);

            Assert.AreEqual(program.codeSegment.Item1[0], 184);
            Assert.AreEqual(program.codeSegment.Item2[0], 0);

            Assert.AreEqual(program.codeSegment.Item1[1], 10);
            Assert.AreEqual(program.codeSegment.Item2[1], 0);

            Assert.AreEqual(program.codeSegment.Item1[2], 232);
            Assert.AreEqual(program.codeSegment.Item2[2], 1);

            Assert.AreEqual(program.codeSegment.Item1[3], 8);
            Assert.AreEqual(program.codeSegment.Item2[3], 1);

            Assert.AreEqual(program.codeSegment.Item1[4], 40);
            Assert.AreEqual(program.codeSegment.Item2[4], 2);

            Assert.AreEqual(program.codeSegment.Item1[5], 50);
            Assert.AreEqual(program.codeSegment.Item2[5], 3);

            Assert.AreEqual(program.codeSegment.Item1[6], 40);
            Assert.AreEqual(program.codeSegment.Item2[6], 4);

            Assert.AreEqual(program.codeSegment.Item1[7], 58);
            Assert.AreEqual(program.codeSegment.Item2[7], 5);

            Assert.AreEqual(program.codeSegment.Item1[8], 89);
            Assert.AreEqual(program.codeSegment.Item2[8], 7);

            Assert.AreEqual(program.codeSegment.Item1[9], 195);
            Assert.AreEqual(program.codeSegment.Item2[9], 7);

            Assert.AreEqual(program.codeSegment.Item1[10], 106);
            Assert.AreEqual(program.codeSegment.Item2[10], 8);

            Assert.AreEqual(program.codeSegment.Item1[11], 77);
            Assert.AreEqual(program.codeSegment.Item2[11], 8);

            Assert.AreEqual(program.codeSegment.Item1[12], 40);
            Assert.AreEqual(program.codeSegment.Item2[12], 9);

            Assert.AreEqual(program.codeSegment.Item1[13], 58);
            Assert.AreEqual(program.codeSegment.Item2[13], 10);

            Assert.AreEqual(program.codeSegment.Item1[14], 195);
            Assert.AreEqual(program.codeSegment.Item2[14], 11);
        }
    }
}
