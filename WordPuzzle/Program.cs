using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace WordPuzzle
{
    class puzzle
    {

        // it cant be a character Bcuz some of the code have 2 digit
        public static Dictionary<string, string> dic = new Dictionary<string, string>()
        {
            {"ا","1"},{"ب","2"},{"پ","3"},{"ت","4"},{"ث","5"},
            {"ج","6"},{"چ","7"},{"ح","8"},{"خ","9"},{"د","10"},
            {"ذ","11"},{"ر","12"},{"ز","13"},{"ژ","14"},{"س","15"},
            {"ش","16"},{"ص","17"},{"ض","18"},{"ط","19"},{"ظ","20"},
            {"ع","21"},{"غ","22"},{"ف","23"},{"ق","24"},{"ک","25"},
            {"گ","26"},{"ل","27"},{"م","28"},{"ن","29"},{"و","30"},
            {"ه","31"},{"ی","32"}
        };
        // this fucntion use to display string code
        private static void DispCode(ArrayList codedString)
        {
            foreach (var item in codedString)
            {
                Console.WriteLine(item);

            }

        }
        public static void TestWord(string inputWord)
        {
            DispCode(Word2Code(inputWord, puzzle.dic));
        }

        public static ArrayList Word2Code(string inputString, Dictionary<string, string> dic)
        {
            ArrayList myCode = new ArrayList();
            for (int i = 0; i < inputString.Length; i++)
            {
                myCode.Add(dic[inputString[i].ToString()]);
            }

            return myCode;
        }
        static void Main(string[] args)
        {
            string inputWord = ("پرگار");
            ;
            //TestWord("پرگار");
            //DetectWord mydetect = new DetectWord();
            //mydetect.

            DetectWord mydetect = new DetectWord();
            Console.WriteLine("start from here");
            bool result=DetectWord.FindCodeInPuzzle(Word2Code(inputWord, puzzle.dic), mydetect.InitilizeSamplePuzzle());
            Console.WriteLine(result.ToString());
            Console.Read();



        }
    }
}
