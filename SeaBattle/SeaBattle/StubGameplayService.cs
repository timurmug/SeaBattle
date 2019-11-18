using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle
{
    class StubGameplayService:IGameplayService
    {
        static IGameplayService instance;
        Random random;
        int orientationInt_ForAdversary, rnd1_ForAdversary, rnd2_ForAdversary;
        int orientationInt_ForPlayer, rnd1_ForPlayer, rnd2_ForPlayer;

        private StubGameplayService()
        {
            random = new Random();

            orientationInt_ForAdversary = random.Next(2);
            rnd1_ForAdversary = random.Next(7);
            rnd2_ForAdversary = random.Next(9);

            orientationInt_ForPlayer = random.Next(2);
            rnd1_ForPlayer = random.Next(7);
            rnd2_ForPlayer = random.Next(9);
        }

        public static IGameplayService StartGame()
        {
            if (instance == null)
                instance = new StubGameplayService();
            return instance;
        }

        public void StopGame()
        {
            instance = null;
        }

        public bool IsOrientationVertical_ForAdversary()
        {
            if (orientationInt_ForAdversary == 0)
                return true;
            else
                return false;
        }

        public (int,int) GetXY_ForAdversary()
        {
            if (orientationInt_ForAdversary == 0)
                //9,7
                return (rnd2_ForAdversary, rnd1_ForAdversary);
            else
                //7,9
                return (rnd1_ForAdversary, rnd2_ForAdversary);
           
        }

        public bool IsOrientationVertical_ForPlayer()
        {
            if (orientationInt_ForPlayer == 0)
                return true;
            else
                return false;
        }

        public (int,int) GetXY_ForPlayer()
        {
            if (orientationInt_ForPlayer == 0)
                //9,7
                return (rnd2_ForPlayer, rnd1_ForPlayer);
            else  
                //7,9
                return (rnd1_ForPlayer, rnd2_ForPlayer);
        }

        public int AdversaryShoots()
        {
            return random.Next(99);
        }
    }
}
