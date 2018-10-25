using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace WordPuzzle
{
    class Show
    {
        public static void DisplayAllWord(List<ArrayList> myWords)
        {
            foreach (var item in myWords)
            {
                foreach (var ch in item)
                {
                    Console.Write(ch);
                    Console.Write(" ");

                }
                Console.WriteLine();
            }
        }
        public static void DispCode(ArrayList codedString)
        {
            foreach (var item in codedString)
            {
                Console.WriteLine(item);

            }
            Console.WriteLine();

        }
    }
}
