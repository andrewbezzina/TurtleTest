using System;
using System.IO;

namespace TurtleLibrary
{
    public class TurtleState : IEquatable<TurtleState>
    {
        public enum eDirection
        {
            N = 0,
            E,
            S,
            W,

            Noof
        }

        readonly int m_xCoord;
        readonly int m_yCoord;
        readonly eDirection m_dir;

        public int XCoord => m_xCoord;
        public int YCoord => m_yCoord;
        public eDirection Direction => m_dir;

        public TurtleState(int x, int y, string d)
        {
            m_xCoord = x;
            m_yCoord = y;
            if (!Enum.TryParse<eDirection>(d, out m_dir))
            {
                throw new InvalidDataException("Invalid Turle State Direction: " + d);
            }
        }

        public TurtleState(int x, int y, eDirection d)
        {
            m_xCoord = x;
            m_yCoord = y;
            m_dir = d;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TurtleState);
        }

        public bool Equals(TurtleState p)
        {
            if (Object.ReferenceEquals(p, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, p))
            {
                return true;
            }

            if (this.GetType() != p.GetType())
            {
                return false;
            }

            return (XCoord == p.XCoord) && (YCoord == p.YCoord) && (Direction == p.Direction);
        }

        // will only give unique code for coords up to 32,000 but should be fine.
        public override int GetHashCode()
        {
            return XCoord * 0x00020000 + YCoord * 0x0000004 + (int)Direction;
        }

        public TurtleState GetNewTurleStateAfterMovementInstruction(char instruction)
        {
            switch (instruction)
            {
                case 'R':  // rotate right
                    {
                        eDirection newDir;
                        if (m_dir == eDirection.W)
                        {
                            newDir = eDirection.N;
                        }
                        else
                        {
                            newDir = m_dir + 1;
                        }
                        return new TurtleState(m_xCoord, m_yCoord, newDir);
                    }
                case 'L':  // rotate left
                    {
                        eDirection newDir;
                        if (m_dir == eDirection.N)
                        {
                            newDir = eDirection.W;
                        }
                        else
                        {
                            newDir = m_dir - 1;
                        }
                        return new TurtleState(m_xCoord, m_yCoord, newDir);
                    }
                case 'M':
                    {
                        switch (m_dir)
                        {
                            case eDirection.N:
                                return new TurtleState(m_xCoord, m_yCoord - 1, m_dir);
                            case eDirection.E:
                                return new TurtleState(m_xCoord + 1, m_yCoord, m_dir);
                            case eDirection.S:
                                return new TurtleState(m_xCoord, m_yCoord + 1, m_dir);
                            case eDirection.W:
                                return new TurtleState(m_xCoord - 1, m_yCoord, m_dir);
                            default:
                                throw new Exception ("Invalid Turtle Direction");
                        }
                    }
                default:
                    throw new InvalidDataException("Invalid Movement Insturction: " + instruction);


            }
        }
    }
}
