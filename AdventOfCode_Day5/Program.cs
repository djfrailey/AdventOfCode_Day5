using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode_Day5
{

    class Decoder
    {
        public Tuple<int,int> Decode(string input)
        {
            int row = _Decode(input.Substring(0, 7), 128);
            int column = _Decode(input.Substring(7), 8);
            return new Tuple<int, int>(row, column);
        }

        int _Decode(string input, int start_size)
        {
            int[] l = Enumerable.Range(0, start_size).ToArray();

            foreach (char c in input)
            {
                int half = l.Length / 2;

                switch (c)
                {
                    case 'F':
                    case 'L':
                        l = l.Take(half).ToArray();
                        break;
                    case 'B':
                    case 'R':
                        l = l.TakeLast(half).ToArray();
                        break;
                }
            }

            return l[0];
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Decoder decoder = new Decoder();

            List<int> seat_ids = new List<int>();

            using (StreamReader sr = File.OpenText("input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    Tuple<int, int> decoded = decoder.Decode(sr.ReadLine());
                    seat_ids.Add(decoded.Item1 * 8 + decoded.Item2);
                }
            }

            int my_seat_id = Enumerable.Range(seat_ids.Min(), (seat_ids.Max() - seat_ids.Min() + 1)).Except(seat_ids).ToList().First<int>();

            Console.WriteLine("Maximum Seat ID: {0}", seat_ids.Max());
            Console.WriteLine("My Seat: {0}", my_seat_id);
        }
    }
}
