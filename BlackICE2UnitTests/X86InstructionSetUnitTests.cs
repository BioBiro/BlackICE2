using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;

namespace BlackICE2UnitTests
{
    [TestClass]
    public class X86InstructionSetUnitTests
    {
        [TestMethod]
        public void TestInstructionSet_184()
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

        [TestMethod]
        public void TestInstructionSet_89()
        {
            BlackICE2.Computer computer = new BlackICE2.Computer();
            computer.cPU = new BlackICE2.X86CPU(computer);
            computer.memory = new BlackICE2.Memory();

            BlackICE2.Human human = new BlackICE2.Human();

            computer.cPU.GetRegisters().SetRegister((int)(BlackICE2.X86Registers.RegisterPointers.ACCUMULATOR), 0, new byte[] { 88 }); // Store value to MOV in EAX.

            computer.memory.virtualAddressSpace[0].value = 89;
            computer.memory.virtualAddressSpace[1].value = 195;

            human.loader.Step(computer);

            Assert.AreEqual(88, computer.cPU.GetRegisters().GetRegister((int)(BlackICE2.X86Registers.RegisterPointers.BASE), 0)[0]);
        }

        // XX

        // ZZ

        [TestMethod]
        public void TestInstructionSet_40()
        {
            BlackICE2.Computer computer = new BlackICE2.Computer();
            computer.cPU = new BlackICE2.X86CPU(computer);
            computer.memory = new BlackICE2.Memory();

            BlackICE2.Human human = new BlackICE2.Human();

            computer.cPU.GetRegisters().SetRegister((int)(BlackICE2.X86Registers.RegisterPointers.ACCUMULATOR), 0, new byte[] { 33 }); // Store value to MOV in EAX.

            computer.memory.virtualAddressSpace[0].value = 40;

            human.loader.Step(computer);

            Assert.AreEqual(34, computer.cPU.GetRegisters().GetRegister((int)(BlackICE2.X86Registers.RegisterPointers.ACCUMULATOR), 0)[0]);
        }

        [TestMethod]
        public void TestInstructionSet_233()
        {
            BlackICE2.Computer computer = new BlackICE2.Computer();
            computer.cPU = new BlackICE2.X86CPU(computer);
            computer.memory = new BlackICE2.Memory();

            BlackICE2.Human human = new BlackICE2.Human();

            // todo - This is not the best test we could really do. It checks for '2' because that's the ToSkip() parameter count for the JMP instruction, being set in the IP.
            // To make it better, maybe you could JMP to actual instructions further down in memory, but this would require you to use actual instructions, making this unit test dependent on other unit tests.
            // Aa NOP instruction/test, for example, would suffice.

            computer.memory.virtualAddressSpace[0].value = 233;
            computer.memory.virtualAddressSpace[1].value = 1;

            human.loader.Step(computer);

            Assert.AreEqual(2, computer.cPU.GetRegisters().GetRegister((int)(BlackICE2.X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0)[0]);
        }

        [TestMethod]
        public void TestInstructionSet_106()
        {
            BlackICE2.Computer computer = new BlackICE2.Computer();
            computer.cPU = new BlackICE2.X86CPU(computer);
            computer.memory = new BlackICE2.Memory();

            BlackICE2.Human human = new BlackICE2.Human();

            computer.memory.virtualAddressSpace[0].value = 106;
            computer.memory.virtualAddressSpace[1].value = 91;

            human.loader.Step(computer);

            Assert.AreEqual(91, computer.memory.virtualAddressSpace[31].value);
        }

        [TestMethod]
        public void TestInstructionSet_50()
        {
            BlackICE2.Computer computer = new BlackICE2.Computer();
            computer.cPU = new BlackICE2.X86CPU(computer);
            computer.memory = new BlackICE2.Memory();

            BlackICE2.Human human = new BlackICE2.Human();

            computer.cPU.GetRegisters().SetRegister((int)(BlackICE2.X86Registers.RegisterPointers.ACCUMULATOR), 0, new byte[] { 37 }); // Store value to MOV in EAX.

            computer.memory.virtualAddressSpace[0].value = 50;

            human.loader.Step(computer);

            Assert.AreEqual(37, computer.memory.virtualAddressSpace[31].value);
        }

        [TestMethod]
        public void TestInstructionSet_58()
        {
            BlackICE2.Computer computer = new BlackICE2.Computer();
            computer.cPU = new BlackICE2.X86CPU(computer);
            computer.memory = new BlackICE2.Memory();

            BlackICE2.Human human = new BlackICE2.Human();

            computer.memory.virtualAddressSpace[31].value = 74; // Stack value to POP.

            computer.cPU.GetRegisters().DecrementStackPointer(); // Push stack pointer up.

            computer.memory.virtualAddressSpace[0].value = 58;

            human.loader.Step(computer);

            Assert.AreEqual(74, computer.cPU.GetRegisters().GetRegister((int)(BlackICE2.X86Registers.RegisterPointers.ACCUMULATOR), 0)[0]);
        }

        [TestMethod]
        public void TestInstructionSet_232()
        {
            BlackICE2.Computer computer = new BlackICE2.Computer();
            computer.cPU = new BlackICE2.X86CPU(computer);
            computer.memory = new BlackICE2.Memory();

            BlackICE2.Human human = new BlackICE2.Human();

            computer.memory.virtualAddressSpace[0].value = 232;
            computer.memory.virtualAddressSpace[1].value = 1;

            human.loader.Step(computer);

            Assert.AreEqual(0, computer.memory.virtualAddressSpace[31].value); // Test the stack (saved IP value). This is 2, because that's the ToSkip() value of CALL.
            Assert.AreEqual(2, computer.cPU.GetRegisters().GetRegister((int)(BlackICE2.X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0)[0]); // Test the JMP (IP value).            
        }

        [TestMethod]
        public void TestInstructionSet_195()
        {
            BlackICE2.Computer computer = new BlackICE2.Computer();
            computer.cPU = new BlackICE2.X86CPU(computer);
            computer.memory = new BlackICE2.Memory();

            BlackICE2.Human human = new BlackICE2.Human();

            computer.memory.virtualAddressSpace[31].value = 0; // Saved IP value.

            computer.cPU.GetRegisters().DecrementStackPointer(); // Push stack pointer up.

            computer.memory.virtualAddressSpace[0].value = 195;

            human.loader.Step(computer);

            Assert.AreEqual(1, computer.cPU.GetRegisters().GetRegister((int)(BlackICE2.X86Registers.RegisterPointers.INSTRUCTION_POINTER), 0)[0]); // Test the IP has been set back to the saved value. This is 1, because that's the ToSkip() value of RET.
            Assert.AreEqual(0, computer.memory.virtualAddressSpace[31].value); // Test the saved IP value has been popped.
        }
    }
}
