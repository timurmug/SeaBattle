using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;

namespace SeaBattle
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EnemyPage : PopupPage
    {
        public EnemyPage ()
		{
			InitializeComponent ();

            IGameplayService gameplayService;
            int x, y;
            gameplayService = StubGameplayService.StartGame();

            x = gameplayService.GetXY_ForAdversary().Item1;
            y = gameplayService.GetXY_ForAdversary().Item2;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    BoxView boxView = new BoxView
                    {
                        Color = Color.LightGray,
                        WidthRequest=15,
                        HeightRequest=15
                    };

                    if (gameplayService.IsOrientationVertical_ForAdversary())
                    {
                        if (i == x)
                        {
                            if (j == y || j == y + 1 || j == y + 2 || j == y + 3)
                                boxView.Color = Color.DarkGray;
                            else
                                boxView.Color = Color.LightGray;
                        }
                    }
                    else
                    {
                        if (j == y)
                        {
                            if (i == x || i == x + 1 || i == x + 2 || i == x + 3)
                                boxView.Color = Color.DarkGray;
                            else
                                boxView.Color = Color.LightGray;
                        }
                    }

                    grid.Children.Add(boxView, i, j);
                }
            }
        }
	}
}