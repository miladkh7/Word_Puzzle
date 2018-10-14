using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
namespace WordPuzzle
{
    public struct findedWordInTable
    {
        public ArrayList wordCode;
        public List<int> positions;
    }
    class puzzle
    {
        public static List<findedWordInTable> findedList = new List<findedWordInTable>();
       
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

        public static Dictionary<string, string> faDic = new Dictionary<string, string>()
        {
            {"1","ا"},{"2","ب"},{"3","پ"},{"4","ت"},{"5","ث"},
            {"6","ج"},{"7","چ"},{"8","ح"},{"9","خ"},{"10","د"},
            {"11","ذ"},{"12","ر"},{"13","ز"},{"14","ژ"},{"15","س"},
            {"16","ش"},{"17","ص"},{"18","ض"},{"19","ط"},{"20","ظ"},
            {"21","ع"},{"22","غ"},{"23","ف"},{"24","ق"},{"25","ک"},
            {"26","گ"},{"27","ل"},{"28","م"},{"29","ن"},{"30","و"},
            {"31","ه"},{"32","ی"},

        };

        public static ArrayList FaCode2Word(string inputString, Dictionary<string, string> dic)
        {
            ArrayList myCode = new ArrayList();
            myCode.Clear();
            foreach (string word in inputString.Split(' '))
            {
                myCode.Add(dic[word.ToString()]);
            }


            //for (int i = 0; i < inputString.Length; i++)
            //{
            //    myCode.Add(dic[inputString[i].ToString()]);
            //}

            return myCode;
        }
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
               
                if (puzzle == null)
                {
                    Console.WriteLine("puzzell is null");
                }
                bool result=DetectWord.FindCodeInPuzzle(item, puzzle);
                Console.WriteLine(result.ToString());
            }
        }
        public static int CalCostFunction(List<ArrayList> wordsList, int[] puzzle)
        {
            int mycoset = 100;
            findedList.Clear();
            foreach (ArrayList item in wordsList)
            {
                DetectWord mydetect = new DetectWord();
                DetectWord.ReadyToUse();
                if (puzzle == null)
                {
                    Console.WriteLine("puzzell is null");
                }
                bool result = DetectWord.FindCodeInPuzzle(item, puzzle);
                if (result)
                {
                    mycoset--;
                    findedWordInTable itemToAddInList = new findedWordInTable();
                    itemToAddInList.wordCode = item;
                    itemToAddInList.positions = DetectWord.findedWord;
                    findedList.Add(itemToAddInList);

                }
                
            }
            return mycoset;
        }
        public string CodeToWord(ArrayList inputWordCode)

        {
            string outputWord=null;
            foreach (var item in inputWordCode)
            {
                //outputWord += dic2[item.ToString()].ToString();
            }
            return outputWord;
        }
        public static string LocationToString(List<int> locaions)

        {
            string outPut = null;
            foreach (var item in locaions)
            {
                Console.WriteLine(item.ToString());
                outPut+=" { " +item.ToString() + " } " ;
            }
            return outPut;
        }
        public static void WriteFinalSolotionToFile(string ouputFile)
        {
            using (StreamWriter writetext = new StreamWriter(ouputFile))
            {
                string toBePrint = null;
                foreach (var item in findedList)
                {
                    //FaCode2Word(item.wordCode, faDic)
                    
                    
                    toBePrint = Code2String(FaCode2Word(Code2String(item.wordCode), faDic))+ " " + LocationToString(item.positions);
                    //Console.WriteLine(toBePrint);
                    writetext.WriteLine(toBePrint);
                }
            }

        }
        public static string Code2String(ArrayList codedString)
        {
            string result = null;
            foreach (var item in codedString)
            {
                result += item;
            }
            return result;
        }
        private static void StatToSolve()
        {
            string inputFileName = @"E:\Project\ponisha\arman\testcase\testCare.txt"; //defult file
          
            do
            {
                Console.WriteLine("Please Enter address of input file( text file) ");
                inputFileName =Console.ReadLine();
            } while (!File.Exists(inputFileName));
           

            string outputFileName = @"E:\Project\ponisha\arman\words1_final22.txt";
            string saveFileName = "word1.txt";
            if (System.IO.File.Exists(saveFileName)) File.Delete(saveFileName);
           // StreamWriter writetext = new StreamWriter("write.txt",append:true);
            List<ArrayList> myWords2 = ReadFile(inputFileName);
            myWords2.Clear();
            RealGA myGa = new RealGA(myWords2);
            myGa.DoGA(saveFileName);
        }
        private static void ShowLogo()
        {
           
            Console.WriteLine(".-.  .-. .---. .---. .----.     .-.-. .-. .-..---. .---. .-.   .----. ");
            Console.WriteLine("| {  } |/ {-. \\} }}_}} {-. \\    | } }}| } { |`-`} }`-`} }} |   } |__}");
            Console.WriteLine("{  /\\  }\\ '-} /| } \\ } '-} /    | |-' \\ `-' /{ /.-.{ /.-.} '--.} '__} ");
            Console.WriteLine("`-'  `-' `---' `-'-' `----'     `-'    `---'  `---' `---'`----'`----' ");
            
        }
        private static int ShowMenu()
        {
            int selectedIteminMenu = 0;
            do
            {
                Console.WriteLine("Please enter one of the items");
                Console.WriteLine("1- About");
                Console.WriteLine("2- Change Parameters");
                Console.WriteLine("3- Start Finding Soulotion ");
                Console.WriteLine("5- Exit");
                int.TryParse(Console.ReadLine(),out selectedIteminMenu);
            } while (selectedIteminMenu > 5 && selectedIteminMenu < 0);
            Console.WriteLine(selectedIteminMenu);
            return selectedIteminMenu;
        }
        private static void ShowAbout()
        {
            Console.WriteLine();
            Console.WriteLine("this program use an input text file to create  puzzle table that contain maximum word");
            Console.WriteLine();
            Console.WriteLine("Author : Milad Khaleghi");
            Console.WriteLine("\t 0935 299 7106");
            Console.WriteLine("\t Milad_khaleghi@live.com");
            Console.WriteLine();
        }
        private static void Start()
        {
            
               bool wantToReDo = true;
            do
            {
               int  selectedItem = ShowMenu();
                switch (selectedItem)
                {
                    case 1:
                        ShowAbout();
                        break;

                    case 2:
                        break;
                    case 3:
                        StatToSolve();
                        break;
                    case 4:
                        break;
                       
                    default:
                        Environment.Exit(0);
                        break;
                }

            } while (wantToReDo);
        }
        #endregion
        static void Main(string[] args)
        {
            #region MyRegion
            // TestWord("نگخفرپاباشردگقعک");
            // string inputWord = ("پرگار");
            //string inputFileName = @"E:\Project\ponisha\arman\words1.txt";
            //string outputFileName= @"E:\Project\ponisha\arman\words1_final.txt";
            //StreamWriter writetext = new StreamWriter("write.txt");
            // int[] testPuzzle = { 0,29, 26, 9 ,23, 12, 3, 1, 2, 1, 16, 12, 10, 26 ,24, 21, 25 };
            //List<ArrayList> myWords2=ReadFile(inputFileName);
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
            //string inputFileName = @"E:\Project\ponisha\arman\words1.txt";
            //string inputFileName = @"E:\Project\ponisha\arman\answers\answers5.txt";
            ShowLogo();
            Start();

            //WriteFinalSolotionToFile(outputFileName);
            Console.Read();

        }
    }
}
