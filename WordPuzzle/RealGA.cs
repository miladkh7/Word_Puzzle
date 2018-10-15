using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
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
        public static int maxIt=1000;
        public static int nPop = 80;
        public static double pc = .8;
        public static double pm = 1;
        public static double mu=0.01;   
        public static double MulticastDelegate=.8;
        public static double beta = 8;
        public static double nm = Math.Round(pm * nPop);
        public static int nc = 2* (int)Math.Round(pc * nPop / 2);
        public People bestSol;


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
               // Console.Write(myRandomString[i].ToString());
             //   Console.Write(" ");
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
            int randomPlace = rnd.Next(16);
            People[] childs = new People[2];
            int[] childPosition1=new int[17];
            int[] childPosition2= new int[17];
            for (int i = 1; i <= 16; i++)
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



            }
            childs[0].postion = childPosition1;
            childs[1].postion = childPosition2;
           
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
            int[] childPosition1 = new int[17];
            int[] childPosition2 = new int[17];
            double myRandom;
            for (int i = 1; i <=16; i++)
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
            int randomPlace = rnd.Next(17);
            People[] childs = new People[2];
            int[] childPosition1 = new int[17];
            int[] childPosition2 = new int[17];
            for (int i = 1; i <= 16; i++)
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
            }
            childs[0].postion = childPosition1;
            childs[1].postion = childPosition2;
            return childs;
        }
        private People Mutate(double mu, People selectedPeople)
        {
            Random rnd = new Random();
            People mutatePeople = selectedPeople;
            int[] result = new int[17];
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
        private int RouletWheel(double[] p)
        {
            Random rnd = new Random();
            double r= rnd.NextDouble();
            double sum = 0;
            for (int i = 0; i < nPop; i++)
            {

                if (r <= sum) return i;
                sum += p[i];
            }

            int result = 0;



            return result;
        }
        public void DoGA(string saveFileName)
        {
            Console.WriteLine(Properties.Settings.Default.nPop.ToString());
            People myPeople = new People();
            //first Generation
            for (int i = 1; i <= nPop; i++)
            {
                
                int[] postion=CreateRandomString();
                if (postion.Length == 16)
                {
                    Console.WriteLine("error on length");
                }

                myPeople.postion = postion;
                myPeople.cost =puzzle.CalCostFunction(_tableWords, postion);
                pop.Add(myPeople);
            }
            pop = pop.OrderBy(o => o.cost).ToList();
            worstCost = pop[pop.Count - 1].cost;

            //main loop
            for (int it = 0; it < maxIt; it++)
            {
                //        % Calculate Selection Probabilities
                double totalCost = this.pop.Sum(item => item.cost);
                double sum = 0;

                double[] p = new double[nPop];
                for (int i = 0; i < nPop; i++)
                {
                    p[i] = Math.Pow(2.718283, (-1 * beta * pop[i].cost / worstCost));
                    sum += p[i];
                }
                for (int i = 0; i < nPop; i++) p[i] /= sum;
                popc.Clear();
                for (int k = 1; k <= nc/2; k++)
                {
                    //select paretn indices
                    int i1 = RouletWheel(p);
                    int i2 = RouletWheel(p);
                    People parrrent1 = pop[2 * k - 1];
                    if (parrrent1.postion.Length==16)
                    {
                        Console.WriteLine("error on lenght2");
                    }
                    People[] crossOverChild = CrossOver(pop[2 * k - 2], pop[2 * k-1]);
                    //popc[2 * k - 1] = crossOverChild[0];
                    popc.Add( crossOverChild[0]);
                    popc.Add( crossOverChild[1]);

                }

                //mutation
                popm.Clear();
                for (int i = 0; i <= nm; i++)
                {
                    Random nmRand = new Random();
                    int myRandom = nmRand.Next(nPop);
                    popm.Add(Mutate(mu, pop[myRandom]));
                }
                var newpop = pop.Concat(popc)
                                    .Concat(popm)
                                    .ToList();

                // re order pop
                newpop = newpop.OrderBy(o => o.cost).ToList();
                pop = newpop;
                worstCost = GetMax(worstCost, pop[pop.Count - 1].cost);
                pop.RemoveRange(nPop, (int)(nm + nc));
                bestSol = pop[0];
//#ToDo: aval baresi konam agar fili bod pakesh konam

                using (StreamWriter writetext = new StreamWriter(saveFileName, append: true))
                {
                    Console.WriteLine(string.Format("in iteration {0} / {1} : number of word found in table is  {2}", it+1, maxIt, 100 - bestSol.cost));
                    writetext.WriteLine(string.Format("in iteration {0} / {1} : number of word found in table is  {2}", it+1, maxIt, 100 - bestSol.cost));
                    for (int i = 1; i <= 16; i++)
                    {
                        Console.Write(bestSol.postion[i]);
                        Console.Write(" ");
                        writetext.Write(bestSol.postion[i]);
                        writetext.Write(" ");
                    }

                    Console.WriteLine();
                    writetext.WriteLine();
                    Console.WriteLine("***************");
                    writetext.WriteLine("*************");

                }

                    //for (int i = 1; i <=16; i++)
                    //{
                    //    Console.Write(bestSol.postion[i]);
                    //    Console.Write(" ");
                    //}
                    //foreach (var ch in bestSol.postion)
                    //{
                    //    Console.Write(ch);
                    //    Console.Write(" ");
                    //}
                    Console.WriteLine();
                

            }

            Console.WriteLine("finish");
        }
        public static int GetMax(int first, int second)
        {
            return first > second ? first : second;
        }
        public RealGA(List<ArrayList> words)
        {
            //load Seetins
            _tableWords = words;
            maxIt = Properties.Settings.Default.maxIt;
            nPop = Properties.Settings.Default.nPop;
            pc = Properties.Settings.Default.pc;
            pm = Properties.Settings.Default.pm;
            mu = Properties.Settings.Default.mu;
            MulticastDelegate = .8;
            beta = 8;
            nm = Math.Round(pm * nPop);
            nc = 2 * (int)Math.Round(pc * nPop / 2);
    }
    }
}
