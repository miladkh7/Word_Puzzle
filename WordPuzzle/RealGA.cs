using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace WordPuzzle
{
    
    public struct People
    {
        public int[] postion;
        public int cost;
    }
    
    
    class RealGA
    {
      //  public People[] childs = new People[1];
        
        //Ga Parameters
        public static int maxIt=200;
        public static int nPop = 80;
        public static double pc = .8;
        public static double pm = 1;
        public static double MulticastDelegate=.8;
        public static double beta = 8;
        public static double nm = Math.Round(pm * nPop);
        public static int nc = 2* (int)Math.Round(pc * nPop / 2);
        


        // initilze
        public List<People> pop = new List<People>();
        
        public List<People> popc = new List<People>();
        public List<People> popm = new List<People>();
        private List<ArrayList> _tableWords;
        private int worstCost;
        public static int[] CreateRandomString()
        {
            int[] myRandomString = new int[17];
            Random rnd = new Random();
            myRandomString[0] = 0;
            for (int i = 1; i < 17; i++)
            {
                myRandomString[i] = rnd.Next(1, 33);
                Console.Write(myRandomString[i].ToString());
                Console.Write(" ");
            }
            return myRandomString;
        }
        private People[] CrossOver(People parent1, People parent2)
        {
            
            Random rnd = new Random();
            People[] child=new People[2];
            int myrandom = rnd.Next(10);
            if (myrandom<=2)
            {
                 child = this.SinglePointCrossOver(parent1, parent2);
            }
            if (myrandom > 2 && myrandom <= 4)
            {
                child = this.DoublePointCrossOver(parent1, parent2);
            }
            if (myrandom > 4)
            {
                child = this.UniformPointCrossOver(parent1, parent2);
            }
            // childs= SingleCrossOver(People parent1, People parent2)
            child[0].cost = puzzle.CalCostFunction(_tableWords, child[0].postion);
            child[1].cost = puzzle.CalCostFunction(_tableWords, child[1].postion);
            return child;

        }

        private People[] SinglePointCrossOver(People parent1,People parent2)
        {
            Random rnd = new Random();
            int randomPlace = rnd.Next(1, 16);
            People[] childs = new People[1];
            int[] childPosition1=new int[16];
            int[] childPosition2= new int[16];
            for (int i = 0; i <= 15; i++)
            {
                if (i<= randomPlace)
                {
                    childPosition1[i] = parent1.postion[i];
                    childPosition2[i] = parent2.postion[i];
                }
                else
                {
                    childPosition2[i] = parent1.postion[i];
                    childPosition1[i] = parent2.postion[i];
                }
                childs[0].postion = childPosition1;
                childs[1].postion = childPosition2;
                return childs;


            }
            return childs;
        }

        private People[] DoublePoint(People parent1, People parent2)
        {
            Random rnd = new Random();
            int randomPlace1 = rnd.Next(1, 16);
            int randomPlace2 = rnd.Next(1, 16);
            if (randomPlace1== randomPlace2) randomPlace2 = rnd.Next(1, 16);
            int randomPlaceHodler;
            if(randomPlace1 > randomPlace2)
            {
                randomPlaceHodler = randomPlace1;
                randomPlace1 = randomPlace2;
                randomPlace2 = randomPlaceHodler;
            }
            People[] childs = new People[2];
            int[] childPosition1 = new int[16];
            int[] childPosition2 = new int[16];
            for (int i = 0; i <16; i++)
            {
                if (i <= randomPlace1)
                {
                    childPosition1[i] = parent1.postion[i];
                    childPosition2[i] = parent2.postion[i];
                }
                if (i >= randomPlace1 && i <= randomPlace2)
                {
                    childPosition2[i] = parent1.postion[i];
                    childPosition1[i] = parent2.postion[i];
                }
                if (i >= randomPlace2)
                {
                    childPosition1[i] = parent1.postion[i];
                    childPosition2[i] = parent2.postion[i];
                }
                childs[0].postion = childPosition1;
                childs[1].postion = childPosition2;
                return childs;


            }
            return childs;
        }

        private People[] UniformPointCrossOver(People parent1, People parent2)
        {
            Random rnd = new Random();
           // int[] randomNumbers=new int[16];
            People[] childs = new People[2];
            int[] childPosition1 = new int[16];
            int[] childPosition2 = new int[16];
            double myRandom;
            for (int i = 0; i < 16; i++)
            {
                myRandom = rnd.NextDouble();
                if (myRandom >= 0.5)
                {
                    childPosition1[i] = parent1.postion[i];
                    childPosition2[i] = parent2.postion[i];
                }
                if (myRandom < 0.5)
                {
                    childPosition1[i] = parent2.postion[i];
                    childPosition2[i] = parent1.postion[i];
                }

            }
            childs[0].postion = childPosition1;
            childs[1].postion = childPosition2;
            return childs;


        }
        private People[] DoublePointCrossOver(People parent1, People parent2)
        {
            Random rnd = new Random();
            int randomPlace = rnd.Next(1, 16);
            People[] childs = new People[1];
            int[] childPosition1 = new int[16];
            int[] childPosition2 = new int[16];
            for (int i = 0; i <= 15; i++)
            {
                if (i <= randomPlace)
                {
                    childPosition1[i] = parent1.postion[i];
                    childPosition2[i] = parent2.postion[i];
                }
                else
                {
                    childPosition2[i] = parent1.postion[i];
                    childPosition1[i] = parent2.postion[i];
                }
                childs[0].postion = childPosition1;
                childs[1].postion = childPosition2;
                return childs;


            }
            return childs;
        }
        private People Mutate(double mu, People selectedPeople)
        {
            Random rnd = new Random();
            People mutatePeople = selectedPeople;
            int[] result = new int[16];
            int numberOfMutation = (int) Math.Ceiling(mu * 16);
            for (int i = 0; i <= numberOfMutation; i++)
            {
                int randomPlace = rnd.Next(16);
                int randomValue = rnd.Next(33);
                mutatePeople.postion[randomPlace] = randomValue;
            }

            mutatePeople.postion = result;
            return mutatePeople;
        }
        private void DoGA()
        {
            //first Generation
            for (int i = 1; i <= 20; i++)
            {
                People myPeople = new People();
                int[] postion=CreateRandomString();
                myPeople.postion = postion;
                myPeople.cost =puzzle.CalCostFunction(_tableWords, postion);
            }
            pop = pop.OrderBy(o => o.cost).ToList();
            worstCost = pop[pop.Count - 1].cost;

            //main loop
            for (int it = 0; it < maxIt; it++)
            {
                //        % Calculate Selection Probabilities
                //P = exp(-beta * Costs / WorstCost);
                //P = P / sum(P);
                for (int k = 1; k <= nc/2; k++)
                {
                    //select paretn indices
                    //int i1 = 0;
                    //int i2 = 0;
                }

            }

        }
        public RealGA(List<ArrayList> words)
        {
            _tableWords = words;
            maxIt =200;
            nPop = 80;
            pc = .8;
            pm = 1;
            MulticastDelegate = .8;
            beta = 8;
            nm = Math.Round(pm * nPop);
            nc = 2 * (int)Math.Round(pc * nPop / 2);
    }
    }
}
