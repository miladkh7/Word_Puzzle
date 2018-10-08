using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
namespace WordPuzzle
{
    class puzzle
    {


        //public static List<ArrayList> MyWords = new List<ArrayList>();
        // it cant be a character Bcuz some of the code have 2 digit
        public static Dictionary<string, string> dic = new Dictionary<string, string>()
        {
            {"ا","1"},{"ب","2"},{"پ","3"},{"ت","4"},{"ث","5"},
            {"ج","6"},{"چ","7"},{"ح","8"},{"خ","9"},{"د","10"},
            {"ذ","11"},{"ر","12"},{"ز","13"},{"ژ","14"},{"س","15"},
            {"ش","16"},{"ص","17"},{"ض","18"},{"ط","19"},{"ظ","20"},
            {"ع","21"},{"غ","22"},{"ف","23"},{"ق","24"},{"ک","25"},
            {"گ","26"},{"ل","27"},{"م","28"},{"ن","29"},{"و","30"},
            {"ه","31"},{"ی","32"},
            {"آ","1"}
        };
        // this fucntion use to display string code
        private static void DispCode(ArrayList codedString)
        {
            foreach (var item in codedString)
            {
                Console.WriteLine(item);

            }
            Console.WriteLine();

        }
        private static void DisplayAllWord(List<ArrayList> myWords)
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

        #region Read and write in file
        public static List<ArrayList> ReadFile(string inputFileName)
        {
            List<ArrayList> MyWords2 = new List<ArrayList>();
            StreamReader reader = new StreamReader(inputFileName);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                MyWords2.Add( Word2Code(line, dic));

                // Do stuff with your line here, it will be called for each 
                // line of text in your file.
            }
            return MyWords2;
        }
        public static void FindListInPuzzle(List<ArrayList> wordsList, int[] puzzle)
        {

            foreach (ArrayList item in wordsList)
            {   DetectWord mydetect = new DetectWord();
                DetectWord.ReadyToUse();
                bool result=DetectWord.FindCodeInPuzzle(item, puzzle);
                Console.WriteLine(result.ToString());
            }
        }
        public static int CalCostFunction(List<ArrayList> wordsList, int[] puzzle)
        {
            int mycoset = 100;
            foreach (ArrayList item in wordsList)
            {
                DetectWord mydetect = new DetectWord();
                DetectWord.ReadyToUse();
                bool result = DetectWord.FindCodeInPuzzle(item, puzzle);
                if (result) mycoset--;
                
            }
            return mycoset;
        }

        #endregion
        static void Main(string[] args)
        {
            #region MyRegion
            // TestWord("نگخفرپاباشردگقعک");
            // string inputWord = ("پرگار");
             string inputFileName = @"E:\Project\ponisha\arman\words1.txt";
            // int[] testPuzzle = { 0,29, 26, 9 ,23, 12, 3, 1, 2, 1, 16, 12, 10, 26 ,24, 21, 25 };
             List<ArrayList> myWords2=ReadFile(inputFileName);
            //  DisplayAllWord(myWords2);
            // Console.WriteLine("results");
            // FindListInPuzzle(myWords2, testPuzzle);
            // Console.WriteLine("emteiz");
            // Console.WriteLine(CalCostFunction(myWords2, testPuzzle).ToString()); 
            //// DisplayAllWord(myWords2);
            // Console.Read();

            //DetectWord mydetect = new DetectWord();
            //mydetect.

            //DetectWord mydetect = new DetectWord();
            //Console.WriteLine("start from here");
            //bool result=DetectWord.FindCodeInPuzzle(Word2Code(inputWord, puzzle.dic), mydetect.InitilizeSamplePuzzle());
            //Console.WriteLine(result.ToString());
            //foreach (var item in DetectWord.findedWord)
            //{
            //    Console.Write(item.ToString());
            //    Console.Write(" ");
            //}
            //Console.WriteLine();
            //Console.Read();
            #endregion

            #region Ga Test
            //RealGA.CreateRandomString().ToString();
            //Console.Read();
            #endregion
            RealGA myGa = new RealGA(myWords2);
            myGa.DoGA();
            Console.Read();


        }
    }
}
