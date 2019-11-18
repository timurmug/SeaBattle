using System;
using System.Collections.Generic;
using System.Text;

namespace SeaBattle
{
    public interface IGameplayService
    {
        //static IGameplayService StartGame();
        bool IsOrientationVertical_ForAdversary();
        (int, int) GetXY_ForAdversary();

        bool IsOrientationVertical_ForPlayer();
        (int, int) GetXY_ForPlayer();

        int AdversaryShoots();

        void StopGame();
    }
}
