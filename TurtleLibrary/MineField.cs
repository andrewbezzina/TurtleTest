using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace TurtleLibrary
{
    public class MineField
    {
        public enum eTraversalResult
        {
            NoInstructionsLeft,
            OutOfBounds,
            MineHit,
            ReachedExit
        }


        Tuple<int, int> m_tableDims;
        Tuple<int, int> m_exitCoords;
        char[,] m_field; // [x,y] 0 = empty, 'm' = mine, 'x' = exit
        TurtleState m_initialTurtleState;
        List<string> m_movementSequences;

        public char[,] Table => m_field;
        public TurtleState InitialTurtleState => m_initialTurtleState;
        public List<string> MovementSequences => m_movementSequences;

        public void LoadSettings(string settingsFile)
        {
            using (var sr = new StreamReader(settingsFile))
            {
                // Loading instructions from file

                // Read Table Dimensions
                string sTableLine = sr.ReadLine();
                m_tableDims = ParseStringPairToNumbers(sTableLine, ' ');
                if (m_tableDims != null)
                {
                    m_field = new char[m_tableDims.Item1, m_tableDims.Item2]; // first number is N wich is the size of the x dimension.
                }
                else
                {
                    throw new InvalidDataException("Invalid dimensions(" + sTableLine + ") for table!");
                }

                // Read mine location and insert in table
                string[] mineLocations = sr.ReadLine().Split(' ');
                foreach (string sMineLocationString in mineLocations)
                {
                    Tuple<int, int> mineCoords = ParseStringPairToNumbers(sMineLocationString, ',');
                    if (mineCoords != null && CoordsInBounds(mineCoords))
                    {
                        m_field[mineCoords.Item1, mineCoords.Item2] = 'm';
                    }
                    else
                    {
                        throw new InvalidDataException("Invalid Mine coords or  out of bound: " + sMineLocationString);
                    }
                }

                // Read exit
                string sExitLine = sr.ReadLine();
                m_exitCoords = ParseStringPairToNumbers(sExitLine, ' ');
                if (m_exitCoords != null && CoordsInBounds(m_exitCoords))
                {
                    m_field[m_exitCoords.Item1, m_exitCoords.Item2] = 'e';
                }
                else
                {
                    throw new InvalidDataException("Invalid Exit Coords(" + sExitLine + ") for table!");
                }

                // Read turtle position
                string sTurtleStateLine = sr.ReadLine();
                string[] turtleStateStringArray = sTurtleStateLine.Split(' ');
                Tuple<int, int> turtleCoords = ParseStringPairToNumbers(turtleStateStringArray);

                if (turtleCoords != null && CoordsInBounds(turtleCoords))
                {
                    m_initialTurtleState = new TurtleState(turtleCoords.Item1, turtleCoords.Item2, turtleStateStringArray[2]);
                }
                else
                {
                    throw new InvalidDataException("Invalid turle start coord: " + sTurtleStateLine);
                }

                // Read Movement Sequences
                m_movementSequences = new List<string>();
                string sequenceLine = sr.ReadLine();
                while (!string.IsNullOrEmpty(sequenceLine))
                {
                    m_movementSequences.Add(sequenceLine);
                    sequenceLine = sr.ReadLine();
                }
            }
        }

        public eTraversalResult TraverseMineField(TurtleState currentState, string instructions)
        {
            if (!CoordsInBounds(currentState.XCoord, currentState.YCoord))
            {
                return eTraversalResult.OutOfBounds;
            }
            if (m_field[currentState.XCoord, currentState.YCoord] == 'm')
            { 
                return eTraversalResult.MineHit;
            }
            if (m_field[currentState.XCoord, currentState.YCoord] == 'e')
            {
                return eTraversalResult.ReachedExit;
            }
            if (instructions.Length == 0)
            {
                return eTraversalResult.NoInstructionsLeft;
            }

            // find new turtle state
            TurtleState newState = currentState.GetNewTurleStateAfterMovementInstruction(instructions[0]);
            // calling traverse again minus instruction and space, except for last as there won't be a space.
            return TraverseMineField(newState, instructions.Length > 1 ? instructions.Substring(2) : instructions.Substring(1));
        }

        // helper functions
        static Tuple<int, int> ParseStringPairToNumbers(string pairString, char i_seperator)
        {
            if (pairString != null)
            {
                return ParseStringPairToNumbers(pairString.Split(i_seperator));
            }
            return null;
        }


        // Convert first 2 strings in an array into a number pair;
        static Tuple<int, int> ParseStringPairToNumbers(string[] pair)
        {
            if (!int.TryParse(pair[0], out int N))
            {
                Debug.WriteLine("INVALID INPUT: First Value not a valid int");
                return null;
            }

            if (!int.TryParse(pair[1], out int M))
            {
                Debug.WriteLine("INVALID INPUT: Second value not a valid int");
                return null;
            }

            return new Tuple<int, int>(N, M);
        }


        bool CoordsInBounds(int xCoord, int yCoord)
        {
            return CoordsInBounds(new Tuple<int, int>(xCoord, yCoord));
        }

        bool CoordsInBounds(Tuple<int, int> coords)
        {
            return coords != null
                && coords.Item1 >= 0 && coords.Item2 >= 0
                && coords.Item1 <= m_tableDims.Item1 && coords.Item2 <= m_tableDims.Item2;
        }
    }
}
 