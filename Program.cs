using System.Text;

namespace DrawingInConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Canvas canva = new Canvas(20);
            canva.Line(new Position(0, 0), new Position(19, 20));
            canva.Line(new Position(2, 0), new Position(4, 1));
            canva.Line(new Position(1, 3), new Position(3, 4));
            canva.Line(new Position(7, 9), new Position(9, 1));
            Console.WriteLine(canva);
        }
    }

    internal struct Position
    {
        public float x;
        public float y;
        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    internal class Canvas
    {
        public int Size { get; }
        private char[,] image;

        public Canvas(int size)
        {
            Size = size;
            image = new char[Size, Size];
        }

        public void ClearCanvas()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    image[i, j] = ' ';
                }
            }
        }

        public void FillCanvas(char c)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    image[i, j] = c;
                }
            }
        }

        public void SetCharAt(Position pos, char c)
        {
            image[(int)pos.y, (int)pos.x] = c;
        }

        public void RemoveCharAt(Position pos)
        {
            image[(int)pos.y, (int)pos.x] = ' ';
        }

        private float XLineEquation(Position pos1, Position pos2, int x)
        {
            return (((pos1.y - pos2.y) / (pos1.x - pos2.x)) * (x - pos1.x) + pos1.y);
        }
        private float YLineEquation(Position pos1, Position pos2, int y)
        {
            return (((pos1.x - pos2.x) / (pos1.y - pos2.y)) * (y - pos1.y) + pos1.x);
        }

        public void Line(Position pos1, Position pos2, char main = '#', char secondary = '*')
        {
            Position point;
            if(Math.Abs(pos1.x - pos2.x) >= Math.Abs(pos1.y - pos2.y))
            {
                for(int i = (int)Math.Min(Math.Round(pos1.x), Math.Round(pos2.x)); i < Math.Max(Math.Round(pos1.x), Math.Round(pos2.x)); i++)
                {
                    point.x = i;

                    if (XLineEquation(pos1, pos2, i) - Math.Round(XLineEquation(pos1, pos2, i)) < 0.3)
                    {
                        point.y = (int)Math.Floor(XLineEquation(pos1, pos2, i));

                        SetCharAt(point, secondary);
                    }
                    else if (XLineEquation(pos1, pos2, i) - Math.Round(XLineEquation(pos1, pos2, i)) > 0.3)
                    {
                        point.y = (int)Math.Ceiling(XLineEquation(pos1, pos2, i));

                        SetCharAt(point, secondary);
                    }

                    point.x = i;
                    point.y = (int)Math.Round(XLineEquation(pos1, pos2, i));

                    SetCharAt(point, main);
                }
            }
            else
            {
                for (int i = (int)Math.Min(Math.Round(pos1.y), Math.Round(pos2.y)); i < Math.Max(Math.Round(pos1.y), Math.Round(pos2.y)); i++)
                {
                    point.y = i;

                    if (YLineEquation(pos1, pos2, i) - Math.Round(YLineEquation(pos1, pos2, i)) < 0.3)
                    {
                        point.x = (int)Math.Floor(YLineEquation(pos1, pos2, i));
                        SetCharAt(point, secondary);
                    }
                    else if (YLineEquation(pos1, pos2, i) - Math.Round(YLineEquation(pos1, pos2, i)) > 0.3)
                    {
                        point.x = (int)Math.Ceiling(YLineEquation(pos1, pos2, i));
                        SetCharAt(point, secondary);
                    }

                    point.x = (int)Math.Round(YLineEquation(pos1, pos2, i));
                    point.y = i;

                    SetCharAt(point, main);
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    sb.Append(image[i, j]);
                    sb.Append(' ');
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }

    }

}
