using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using WordPuzzle;
using System.IO;
namespace WordPuzzle
{
    public class Find
    {
        static cell[] cells = new cell[17];
        //public List<int> CurrentUsedPlaces = new List<int>();
        public static List<int> findedWord = new List<int>(); // jahat negah dari kalame nahahii
        public static int currentPlace;
        bool IsFind = false;
        public void InitilizeNeighbors()
        {

            cells[0].neghbors = new int[] { 0, 0, 0 };
            cells[1].neghbors = new int[] { 2, 5, 6 };
            cells[2].neghbors = new int[] { 1, 3, 5, 6, 7 };
            cells[3].neghbors = new int[] { 2, 4, 6, 7, 8 };
            cells[4].neghbors = new int[] { 3, 7, 8 };
            cells[5].neghbors = new int[] { 1, 2, 6, 9, 10 };
            cells[6].neghbors = new int[] { 1, 2, 3, 5, 7, 9, 10, 11 };
            cells[7].neghbors = new int[] { 2, 3, 4, 6, 8, 10, 11, 12 };
            cells[8].neghbors = new int[] { 3, 4, 7, 11, 12 };
            cells[9].neghbors = new int[] { 5, 6, 10, 13, 14 };
            cells[10].neghbors = new int[] { 5, 6, 7, 9, 11, 13, 14, 15 };
            cells[11].neghbors = new int[] { 6, 7, 8, 10, 12, 14, 15, 16 };
            cells[12].neghbors = new int[] { 7, 8, 11, 15, 16 };
            cells[13].neghbors = new int[] { 9, 10, 14 };
            cells[14].neghbors = new int[] { 9, 10, 11, 13, 15 };
            cells[15].neghbors = new int[] { 10, 11, 12, 14, 16 };
            cells[16].neghbors = new int[] { 11, 12, 15 };

        }
        public Find()
        {
            InitilizeNeighbors();
        }
        public bool FindCodeInPuzzle(ArrayList wordCode, int[] puzzle, List<int> myDomain, List<int> usedPlaces)
        {
           
            if (IsFind)
            {
                //Console.WriteLine("we find word");
                return true;
            }
            // agar tole reshte ke eshghal shode bozorgrat va ya mosafie kamale yaft shode beshe yani yaftimesh kho
            if (usedPlaces.Count >= wordCode.Count)
            {

                findedWord = usedPlaces;
                IsFind = true;
                //Console.WriteLine("we find word");
                return true;
            }
            foreach (var selectedPlace in myDomain)
            {
                if (puzzle[selectedPlace].ToString() == wordCode[usedPlaces.Count].ToString())
                {
                    List<int> CurrentUsedPlaces = usedPlaces;
                    CurrentUsedPlaces.Add(selectedPlace);
                    List<int> currentNegibor = cells[selectedPlace].neghbors.OfType<int>().ToList();
                    List<int> currentDomain = new List<int>();
                    currentDomain = currentNegibor.FindAll(i => !CurrentUsedPlaces.Contains(i));
                    currentPlace = selectedPlace;
                    Find newFind = new Find();
                    IsFind=newFind.FindCodeInPuzzle(wordCode, puzzle, currentDomain, CurrentUsedPlaces);
                    if (IsFind) return true;
                    if (!IsFind) CurrentUsedPlaces.Remove(selectedPlace);
                    //wordFindIndex++;
                    //FindCodeInPuzzle(wordCode, puzzle, domain);

                }

            }




            return IsFind;

            //    bool IsFind = false;
            //    if (level == 1)
            //    {

            //        ReadyToUse();
            //    }
            //    else
            //    {
            //        // neighbors of current cell
            //        List<int> currentNegibor = cells[currentPlace].neghbors.OfType<int>().ToList();
            //        usedPlaces.Add(currentPlace);
            //        List<int> currentDomain = new List<int>();
            //        currentDomain = currentNegibor.FindAll(i => !usedPlaces.Contains(i));
            //        //domain = 

            //    }
            //    level++;
            //    foreach (var selectedPlace in domain)
            //    {
            //        if (IsFind)
            //        {
            //            //Console.WriteLine("we find word");
            //            return true;
            //        }
            //        if (wordFindIndex >= wordCode.Count)
            //        {

            //            findedWord = usedPlaces;
            //            IsFind = true;
            //            //Console.WriteLine("we find word");
            //            return true;
            //        }
            //        if (puzzle[selectedPlace].ToString() == wordCode[wordFindIndex].ToString())
            //        {
            //            currentPlace = selectedPlace;
            //            wordFindIndex++;
            //            FindCodeInPuzzle(wordCode, puzzle, domain);

            //        }


            //    }

            //    return IsFind;

            //}

        }
    }
   
    public  struct cell
    {
        public int[] neghbors;
     
    }
    public class DetectWord
    {
        static cell[] cells = new cell[17];
        public static List<cell>  cells2=new List<cell>();
        ArrayList findCode = new ArrayList();
        public static UInt16 level;
        public static UInt16 wordFindIndex = 0;
        public static List<int> usedPlaces = new List<int>();
        public static List<int> findedWord = new List<int>();
        //public static ArrayList domain = new ArrayList();
        public static List<int> domain = new List<int>();
        public static List<findedWordInTable> findedList = new List<findedWordInTable>();
        public static int currentPlace;
        public static void ReadyToUse()
        {
            currentPlace = 0;
            wordFindIndex =0;
            usedPlaces.Clear();
            findedWord.Clear();
           
            level = 1;
            domain.Clear();

            domain.Add(1); domain.Add(2); domain.Add(3); domain.Add(4);
            domain.Add(5); domain.Add(6); domain.Add(7); domain.Add(8);
            domain.Add(9); domain.Add(10); domain.Add(11); domain.Add(12);
            domain.Add(13); domain.Add(14); domain.Add(15); domain.Add(16);

        }
        public static List<int> ColectAllUniqLeters(List<ArrayList> wordsCode)
        {
            List<int> myAllLeters = new List<int>();
            foreach (var word in wordsCode)
            {
                foreach (string item in word)
                {
                    if (!myAllLeters.Contains(Convert.ToInt32(item))) myAllLeters.Add( Convert.ToInt32(item));
                }
            }
            return myAllLeters; 
        }
        public static void SaveCode2Word(int[] puzzle, string fileSaveAddress)
        {
            using (StreamWriter tableWirte = new StreamWriter(fileSaveAddress))
            {
                int k = 1;
                for (int i = 0; i <4; i++)
                {
                    for (int j = 0; j <4; j++)
                    {
                        tableWirte.Write(WordPuzzle.puzzle.faDic[ puzzle[k].ToString()].ToString());
                        tableWirte.Write("   ");
                        k++;
                    }
                    tableWirte.WriteLine();

                }

            }
        }
        public static void SavePuzzleInDetail(List<ArrayList> wordsList, int[] puzzle, string fileSaveAddress)
        {

            using (StreamWriter resultPuzzle = new StreamWriter(fileSaveAddress))
            {

                int k = 1;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        resultPuzzle.Write(WordPuzzle.puzzle.faDic[puzzle[k].ToString()].ToString());
                        resultPuzzle.Write("   ");
                        k++;
                    }
                    resultPuzzle.WriteLine();

                }

                resultPuzzle.WriteLine();
                foreach (var wordCode in wordsList)
                {
                    //findedWord = null;
                    DetectWord mydetect = new DetectWord();
                    DetectWord.ReadyToUse();
                    bool isNowFind=FindCodeInPuzzle(wordCode, puzzle);
                    if (!isNowFind) continue;
                    //resultPuzzle.WriteLine(WordPuzzle.puzzle.Code2String(WordPuzzle.puzzle.FaCode2Word(WordPuzzle.puzzle.Code2String(wordCode), WordPuzzle.puzzle.faDic)));
                    string wordCodeToPrint = string.Empty;
                    string persionWord = string.Empty;
                    foreach (var item in Find.findedWord)
                    {

                        wordCodeToPrint += item;
                        wordCodeToPrint += " ";
                        persionWord += WordPuzzle.puzzle.faDic[puzzle[item].ToString()];
                        //resultPuzzle.Write(item);
                        //resultPuzzle.Write(" ");
                    }
                    resultPuzzle.WriteLine(persionWord);
                    resultPuzzle.WriteLine(wordCodeToPrint);
                    resultPuzzle.WriteLine();

                }

                //FindCodeInPuzzle
            }
        }
        public static bool FindCodeInPuzzle(ArrayList wordCode, int[] puzzle)
        {
                //return FindCodeInPuzzle(wordCode, puzzle, domain);
                Find startFind = new Find();
                ReadyToUse();
                return startFind.FindCodeInPuzzle(wordCode, puzzle, domain, usedPlaces);
        }
        public static bool FindCodeInPuzzle(ArrayList wordCode,int[] puzzle, List<int> mydomain)
        {
            bool IsFind = false;
            if ( level== 1)
            {
                
                ReadyToUse();
            }
            else
            {
                // neighbors of current cell
                List<int> currentNegibor = cells[currentPlace].neghbors.OfType<int>().ToList();
                usedPlaces.Add(currentPlace);
                List<int> currentDomain = new List<int>();
                currentDomain= currentNegibor.FindAll(i => !usedPlaces.Contains(i));
                //domain = 
                 
            }
            level++;
            foreach (var selectedPlace in domain)
            {
                if (IsFind)
                {
                    //Console.WriteLine("we find word");
                    return true;  
                }
                if (wordFindIndex >= wordCode.Count)
                {
                    
                    findedWord = usedPlaces;
                    IsFind = true;
                    //Console.WriteLine("we find word");
                    return true;
                }
                if (puzzle[selectedPlace].ToString()==wordCode[wordFindIndex].ToString())
                {
                    currentPlace = selectedPlace;
                    wordFindIndex++;
                    FindCodeInPuzzle(wordCode,puzzle,domain);

                }
               

            }

            return IsFind;

        }
        public void InitilizeNeighbors()
        {
            
            cells[0].neghbors= new int[] {0,0,0};
            cells[1].neghbors = new int[] { 2, 5, 6 };
            cells[2].neghbors = new int[] { 1, 3, 5, 6, 7 };
            cells[3].neghbors = new int[] { 2, 4, 6, 7, 8 };
            cells[4].neghbors = new int[] { 3, 7, 8 };
            cells[5].neghbors = new int[] { 1, 2, 6, 9, 10 };
            cells[6].neghbors = new int[] { 1, 2, 3, 5, 7, 9, 10, 11 };
            cells[7].neghbors = new int[] { 2, 3, 4, 6, 8, 10, 11, 12 };
            cells[8].neghbors = new int[] { 3, 4, 7, 11, 12 };
            cells[9].neghbors = new int[] { 5, 6, 10, 13, 14 };
            cells[10].neghbors = new int[] { 5, 6, 7, 9, 11, 13, 14, 15 };
            cells[11].neghbors = new int[] { 6, 7, 8, 10, 12, 14, 15, 16 };
            cells[12].neghbors = new int[] { 7, 8, 11, 15, 16 };
            cells[13].neghbors = new int[] { 9, 10, 14 };
            cells[14].neghbors = new int[] { 9, 10, 11, 13, 15 };
            cells[15].neghbors = new int[] { 10, 11, 12, 14, 16 };
            cells[16].neghbors = new int[] { 11, 12, 15 };

        }
        public int[] InitilizeSamplePuzzle()
        {
            int[] samplePuzzle = new int[16];
            samplePuzzle[10] = 3;
            samplePuzzle[6] = 12;
            samplePuzzle[7] = 26;
            samplePuzzle[4] = 1;
            samplePuzzle[8] = 12;
            return samplePuzzle;

        }
        private void DisplaySampleWord()
        {
            for (int i = 0; i < 16; i++)
            {
                var selectedItem = cells[i];
                foreach (var item in cells[i].neghbors)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine();
               
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
        public  DetectWord()
        {
            InitilizeNeighbors();
            ReadyToUse();
            InitilizeSamplePuzzle();
            //DisplaySampleWord();
    }

    }
}
