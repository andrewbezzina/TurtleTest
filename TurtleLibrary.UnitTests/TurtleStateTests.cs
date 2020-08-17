using NUnit.Framework;
using System;

namespace TurtleLibrary.UnitTests
{
    [TestFixture()]
    public class TurtleStateTests
    {
        [TestCase("N", TurtleState.eDirection.E)]
        [TestCase("E", TurtleState.eDirection.S)]
        [TestCase("S", TurtleState.eDirection.W)]
        [TestCase("W", TurtleState.eDirection.N)]
        public void GetNewTurleStateAfterMovementInstruction_TurnRight(string initialDir, TurtleState.eDirection expectedDir)
        {
            var initialState = new TurtleState(0, 0, initialDir);

            var result = initialState.GetNewTurleStateAfterMovementInstruction('R');

            Assert.That(result.Direction == expectedDir);
        }

        [TestCase("N", TurtleState.eDirection.W)]
        [TestCase("E", TurtleState.eDirection.N)]
        [TestCase("S", TurtleState.eDirection.E)]
        [TestCase("W", TurtleState.eDirection.S)]
        public void GetNewTurleStateAfterMovementInstruction_TurnLeft(string initialDir, TurtleState.eDirection expectedDir)
        {
            var initialState = new TurtleState(0, 0, initialDir);

            var result = initialState.GetNewTurleStateAfterMovementInstruction('L');

            Assert.That(result.Direction == expectedDir);
        }

        [TestCase(0, 0, "S", 0, 1, "S")]
        [TestCase(1, 1, "N", 1, 0, "N")]
        [TestCase(0, 0, "E", 1, 0, "E")]
        [TestCase(1, 1, "W", 0, 1, "W")]
        public void GetNewTurleStateAfterMovementInstruction_Move(int initX, int initY, string initDir, int expX, int expY, string expDir)
        {
            var initialState = new TurtleState(initX, initY, initDir);
            var expectedState = new TurtleState(expX, expY, expDir);

            var actualstate = initialState.GetNewTurleStateAfterMovementInstruction('M');

            Assert.That(actualstate.Equals(expectedState));
        }
    }
}
