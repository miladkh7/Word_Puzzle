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
        public static string  inputFileName;
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
            {"ه","31"},{"ی","32"},{"ي","32"},
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
            {"31","ه"},{"32","ی"}

        };
        public static Dictionary<string, string> FinglishDic = new Dictionary<string, string>()
        {
            {"1","a"},{"2","b"},{"3","p"},{"4","t"},{"5","s"},
            {"6","j"},{"7","ch"},{"8","h"},{"9","kh"},{"10","d"},
            {"11","z"},{"12","r"},{"13","z"},{"14","zh"},{"15","s"},
            {"16","sh"},{"17","s"},{"18","z"},{"19","t"},{"20","z"},
            {"21","e"},{"22","gh"},{"23","f"},{"24","gh"},{"25","k"},
            {"26","g"},{"27","l"},{"28","m"},{"29","n"},{"30","v"},
            {"31","h"},{"32","ye"}
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
        //private static void DispCode(ArrayList codedString)
        //{
        //    foreach (var item in codedString)
        //    {
        //        Console.WriteLine(item);

        //    }
        //    Console.WriteLine();

        //}
        //private static void DisplayAllWord(List<ArrayList> myWords)
        //{
        //    foreach (var item in myWords)
        //    {
        //        foreach (var ch in item)
        //        {
        //            Console.Write(ch);
        //            Console.Write(" ");

        //        }
        //        Console.WriteLine();
        //    } 
        //}
        public static void TestWord(string inputWord)
        {
           Show.DispCode(Word2Code(inputWord, puzzle.dic));
        }

        public static ArrayList Word2Code(string inputString, Dictionary<string, string> dic)
        {
            ArrayList myCode = new ArrayList();
            for (int i = 0; i < inputString.Length; i++)
            {
                //Console.WriteLine(i.ToString());
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
        //public static int CalCostFunction(List<ArrayList> wordsList, int[] puzzle)
        //{
        //    int mycoset = 100;
        //    findedList.Clear();
        //    foreach (ArrayList item in wordsList)
        //    {
        //        DetectWord mydetect = new DetectWord();
        //        DetectWord.ReadyToUse();
        //        if (puzzle == null)
        //        {
        //            Console.WriteLine("puzzell is null");
        //        }
        //        bool result = DetectWord.FindCodeInPuzzle(item, puzzle);
        //        if (result)
        //        {
        //            mycoset--;
        //            findedWordInTable itemToAddInList = new findedWordInTable();
        //            itemToAddInList.wordCode = item;
        //            itemToAddInList.positions = DetectWord.findedWord;
        //            findedList.Add(itemToAddInList);

        //        }
                
        //    }
        //    return mycoset;
        //}
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
        private static void WriteFindedWordsInTable()

        {
            //SavePuzzleInDetail(List < ArrayList > wordsList, int[] puzzle, string fileSaveAddress);
        }

        private static void StatToSolve()
        {
            
            //string inputFileName = @"E:\Project\ponisha\arman\testcase\testCare.txt"; //defult file
            string inputFileName = string.Empty;
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
            List<int> Myleters = DetectWord.ColectAllUniqLeters(myWords2);
            //myWords2.Clear();
            RealGA myGa = new RealGA(myWords2, Myleters);
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
                Console.WriteLine("3- Show Current Seetings ");
                Console.WriteLine("4- Start Finding Soulotion ");
                Console.WriteLine("5- Save puzzle  Base On current input text file ");
                //Console.WriteLine("6- Save puzzle Basae On cusstum input text file ");
                Console.WriteLine("6- Exit");
                int.TryParse(Console.ReadLine(),out selectedIteminMenu);
            } while (selectedIteminMenu > 6 && selectedIteminMenu < 0);
            //Console.WriteLine(selectedIteminMenu);
            return selectedIteminMenu;
        }
        private static void ShowCurrentSettings()
        {
            Console.WriteLine(string.Format(" Number Of Iterations = {0}",Properties.Settings.Default.maxIt));
            Console.WriteLine(string.Format(" Number Of Pupulation = {0}", Properties.Settings.Default.nPop));
            Console.WriteLine(string.Format(" precent Of cross Over = {0}", Properties.Settings.Default.pc));
            Console.WriteLine(string.Format(" precent Of Mutation = {0}", Properties.Settings.Default.pm));
            Console.WriteLine(string.Format(" intensity Of Mutation = {0}", Properties.Settings.Default.mu));
            Console.WriteLine();

        }
        private static void ShowAbout()
        {
            Console.WriteLine();
            Console.WriteLine("this program use an input text file to create  puzzle table that contain maximum word :)");
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
                        ChangeSetiitng();
                        break;

                    case 3:
                        ShowCurrentSettings();
                        break;

                    case 4:
                        StatToSolve();
                        break;

                    case 5:
                        string inputFileName = string.Empty;
                        do
                        {
                            Console.WriteLine("Please Enter address of input file( text file) ");
                            inputFileName = Console.ReadLine();
                        } while (!File.Exists(inputFileName));

                        List<ArrayList> myWords3 = ReadFile(inputFileName);
                        
                        Console.WriteLine("Please enter your puzzle");
                        int[] myPuzzl = { 1, 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};
                        string rowpuzzle = Console.ReadLine();
                        int count = 0; 

                        foreach (string word in rowpuzzle.Split(' '))
                        {
                            count++;
                            int.TryParse(word, out myPuzzl[count]);
                            //myPuzzl[count]=
                                
                            //myCode.Add(dic[word.ToString()]);

                        }
                        string fileSaveAddress = "TableDesinge.txt";
                        DetectWord.SaveCode2Word(myPuzzl, fileSaveAddress);
                        DetectWord.SavePuzzleInDetail(myWords3, myPuzzl,fileSaveAddress);

                        break;

                    //case 6:
                    //    //Console.WriteLine("No things");
                    //    break;

                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                       
                        break;
                }

            } while (wantToReDo);
        }
        private static void DefultSettings()
        {
            SaveSettings(500,400,.8,20,.01);
        }
        private static void SaveSettings(int maxIt,int nPop,double pc,double pm ,double mu)
        {
            Properties.Settings.Default.maxIt = maxIt;
            Properties.Settings.Default.nPop = nPop;
            Properties.Settings.Default.pc = pc;
            Properties.Settings.Default.pm = pm;
            Properties.Settings.Default.mu = .01;
            Properties.Settings.Default.Save();
    }
        private static void ChangeSetiitng()
        {
            int settingSate=0;

            do
            {
                Console.WriteLine("Select Of the Items");
                Console.WriteLine("\t 1- Default Setting");
                Console.WriteLine("\t 2- My Seetings");
                int.TryParse(Console.ReadLine(), out settingSate);

            } while (!(settingSate==1 || settingSate==2));
            if (settingSate==1) DefultSettings();
            if (settingSate == 2)
            {
                int myMaxIt,myNPop;
                double myPc, myPm, myMu;
               
                //for max it
                do
                {
                    Console.WriteLine("Pleease Enter Number Of Iterations (x>0 & x<2000 recommand= 500)");
                    int.TryParse(Console.ReadLine(), out myMaxIt);
                } while (myMaxIt<1 || myMaxIt>1500);


                //for max nPop
                do
                {
                    Console.WriteLine("Pleease Enter Number Of Pupulation (x>0 & x<800 recommand= 400)");
                    int.TryParse(Console.ReadLine(), out myNPop);
                } while (myNPop < 1 || myNPop > 800);

                //for   pc
                do
                {
                    Console.WriteLine("Pleease Enter precent Of cross Over (x>0 & x<1 recommand= .8)");
                    double.TryParse(Console.ReadLine(), out myPc);
                } while (myPc < 0 || myPc >1 );

                //for  pm
                do
                {
                    Console.WriteLine("Pleease Enter precent Of Mutation (x>0 & x<20 recommand= .1)");
                    double.TryParse(Console.ReadLine(), out myPm);
                } while (myPm < 0 || myPm > 20);


                //for  mu	
                
                do
                {
                    Console.WriteLine("Pleease Enter intensity Of Mutation (x>0 & x<1 recommand= .01)");
                   double.TryParse(Console.ReadLine(), out myMu);
                } while (myMu < 0 || myMu > 1);

                Console.WriteLine("Are You Sure?");
                Console.WriteLine("\t 1-Yes");
                Console.WriteLine("\t 2-No");
                int saveResult = 0;
                int.TryParse(Console.ReadLine(), out saveResult);

                if (saveResult == 1) SaveSettings(myMaxIt, myNPop, myPc, myPm, myMu);

                Console.WriteLine("save SuccessFuly");
                Console.WriteLine();


            }
              
        }
        #endregion
        static void Main(string[] args)
        {
            #region testword
            //string inputWord = ("نحریر");
            ////string inputWord = ("تلطیف");
            //int[] testPuzzle = { 0, 19, 27, 21, 27, 23, 15, 4, 32, 23, 12, 18, 29, 32, 12, 8, 3 };
            //DetectWord mydetected = new DetectWord();

            //bool result = DetectWord.FindCodeInPuzzle(Word2Code(inputWord, puzzle.dic), testPuzzle);
            #endregion
            #region MyRegion
            // TestWord("نگخفرپاباشردگقعک");
            //string inputWord = ("تلطیف");
            //string inputFileName = @"E:\Project\ponisha\arman\words1.txt";
            //string outputFileName= @"E:\Project\ponisha\arman\words1_final.txt";
            //StreamWriter writetext = new StreamWriter("write.txt");

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
