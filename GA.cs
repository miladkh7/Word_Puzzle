using System;
namespace WordPuzzle
{
    public class Ga
    {
        public static int[] CreateRandomString()
        {
            int[] myRandomString = new int[17];
            Random rnd = new Random();
            myRandomString[0] = 0;
            for (int i = 1; i < 17; i++)
            {
                myRandomString[i] = rnd.Next(1, 33);
            }
            return myRandomString;
        }
        public Ga()
        {
        }
    }
}