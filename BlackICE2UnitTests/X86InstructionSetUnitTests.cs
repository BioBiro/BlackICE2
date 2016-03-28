using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

namespace BlackICE2UnitTests
{
    [TestClass]
    public class X86InstructionSetUnitTests
    {
        [TestMethod]
        public void Test_184()
        {
            BlackICE2.Computer computer = new BlackICE2.Computer();
            computer.cPU = new BlackICE2.X86CPU(computer);
            computer.memory = new BlackICE2.Memory();

            BlackICE2.Human human = new BlackICE2.Human();

            computer.memory.virtualAddressSpace[0].value = 184;
            computer.memory.virtualAddressSpace[1].value = 10;

            human.loader.Step(computer);

            Assert.AreEqual(10, computer.cPU.GetRegisters().GetRegister((int)(BlackICE2.X86Registers.RegisterPointers.ACCUMULATOR), 0)[0]);
        }
    }
}
