using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using WordPuzzle;
namespace WordPuzzle
{
   
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
        public static bool FindCodeInPuzzle(ArrayList wordCode,int[] puzzle)
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

                domain = currentNegibor.FindAll(i => !usedPlaces.Contains(i));
                 
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
                    FindCodeInPuzzle(wordCode,puzzle);

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

        public  DetectWord()
        {
            InitilizeNeighbors();
            ReadyToUse();
            InitilizeSamplePuzzle();
            //DisplaySampleWord();
    }

    }
}
