using System;
using System.IO;
using TurtleLibrary;

class MainClass
{
    public static void Main(string[] args)
    {
        try
        {
            MineField mineField = new MineField();
            mineField.LoadSettings("TurtleInstructions.txt");

            Console.WriteLine("Successfully loaded settings!");

            mineField.MovementSequences.ForEach((sequence) =>
            {
                Console.WriteLine("Taversing sequence: " + sequence);
                switch (mineField.TraverseMineField(mineField.InitialTurtleState, sequence))
                {
                    case MineField.eTraversalResult.NoInstructionsLeft:
                        Console.WriteLine("Still In Danger: Turtle ran out of instructions before reaching exit!");
                        break;
                    case MineField.eTraversalResult.OutOfBounds:
                        Console.WriteLine("Failure: Turtle went out of bounds!");
                        break;
                    case MineField.eTraversalResult.MineHit:
                        Console.WriteLine("Failure: Turtle hit mine!");
                        break;
                    case MineField.eTraversalResult.ReachedExit:
                        Console.WriteLine("Succes: Turtle reached exit!");
                        break;
                }
            });
        }
        catch (IOException ex)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(ex.Message);
        }
        catch(InvalidDataException ex)
        {
            Console.WriteLine("Error in file input format:");
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }
    }
}

