using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using System.Threading;

namespace SeaBattle
{
    public partial class MainPage : ContentPage
    {
        CancellationTokenSource source;
        int countPlayerHit, countAdversaryHit = 0;
        IGameplayService gameplayService;

        public MainPage()
        {
            InitializeComponent();
        }

        async void OnTapGestureRecognizerLabelTapped(object sender, EventArgs args)
        {
            await PopupNavigation.Instance.PushAsync(new EnemyPage());
        }

        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            var tappedBoxView = sender as BoxView;
            if (tappedBoxView.Color == Color.LightGray)
            {
                int column;
                int row;
                int x = gameplayService.GetXY_ForAdversary().Item1;
                int y = gameplayService.GetXY_ForAdversary().Item2;

                for (int childIndex = 0; childIndex < grid2.Children.Count; ++childIndex)
                {
                    if (grid2.Children[childIndex].GetHashCode() == tappedBoxView.GetHashCode())
                    {
                        column = Grid.GetColumn(grid2.Children[childIndex]);
                        row = Grid.GetRow(grid2.Children[childIndex]);

                        if (gameplayService.IsOrientationVertical_ForAdversary())
                        {
                            if (column == x)
                            {
                                if (row == y || row == y + 1 || row == y + 2 || row == y + 3)
                                {
                                    countPlayerHit++;
                                    tappedBoxView.Color = Color.IndianRed;
                                }
                            }
                        }
                        else
                        {
                            if (row == y)
                            {
                                if (column == x || column == x + 1 || column == x + 2 || column == x + 3)
                                {
                                    countPlayerHit++;
                                    tappedBoxView.Color = Color.IndianRed;
                                }
                            }
                        }
                    }
                }

                if (tappedBoxView.Color == Color.LightGray)
                    tappedBoxView.Color = Color.PowderBlue;

                if (countPlayerHit == 4)
                {
                    StopGame("Congratulations!", "Adversary's ship is sunk, you won");
                }
                else
                {
                    grid2.IsEnabled = false;
                    //AdversaryShoots();
                    Task task = AdversaryShoots();
                }
            }
        }

        private void StartButton_Clicked(object sender, EventArgs e)
        {
            if (startButton.Text == "Stop")
            {
                StopGame(null,null);
            }
            else
            {
                startButton.Text = "Stop";
                startButton.BackgroundColor = Color.IndianRed;
                source = new CancellationTokenSource();

                gameplayService = StubGameplayService.StartGame();
                int x = gameplayService.GetXY_ForPlayer().Item1;
                int y = gameplayService.GetXY_ForPlayer().Item2;

                grid.Children.Clear();
                grid2.Children.Clear();

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        BoxView boxView2 = new BoxView
                        {
                            Color = Color.LightGray,
                            WidthRequest = 12.5,
                            HeightRequest = 12.5
                        };

                        if (gameplayService.IsOrientationVertical_ForPlayer())
                        {
                            if (i == x)
                            {
                                if (j == y || j == y + 1 || j == y + 2 || j == y + 3)
                                    boxView2.Color = Color.DarkGray;
                                else
                                    boxView2.Color = Color.LightGray;
                            }
                        }
                        else
                        {
                            if (j == y)
                            {
                                if (i == x || i == x + 1 || i == x + 2 || i == x + 3)
                                    boxView2.Color = Color.DarkGray;
                                else
                                    boxView2.Color = Color.LightGray;
                            }
                        }

                        BoxView boxView = new BoxView
                        {
                            Color = Color.LightGray,
                            WidthRequest = 25,
                            HeightRequest = 25
                        };

                        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += OnTapGestureRecognizerTapped;
                        boxView.GestureRecognizers.Add(tapGestureRecognizer);
                        grid.Children.Add(boxView2, i, j);
                        grid2.Children.Add(boxView, i, j);
                    }
                }
                grid2.IsEnabled = true;
                gridsSL.IsVisible = true;
                spyLabel.IsVisible = true;
            }
        }

        async void StopGame(string title,string body)
        {
            source.Cancel();
            if (title != null && body != null)
            {
                await PopupNavigation.Instance.PushAsync(new PopUpPage(title, body));
                Thread.Sleep(1000);
            }
            startButton.Text = "Start";
            startButton.BackgroundColor = Color.CornflowerBlue;
            grid2.IsEnabled = false;
            gridsSL.IsVisible = false;
            spyLabel.IsVisible = false;
            gameplayService.StopGame();

            countPlayerHit = 0;
            countAdversaryHit = 0;
        }

        async Task AdversaryShoots()
        {
            await Task.Delay(1000, source.Token);
            BoxView sender;
            do
            {
                sender = grid.Children[gameplayService.AdversaryShoots()] as BoxView;
            }
            while (sender.Color != Color.LightGray 
            &&sender.Color != Color.DarkGray);

            if (sender.Color == Color.DarkGray)
            {
                countAdversaryHit++;
                sender.Color = Color.IndianRed;
            }
            else 
                sender.Color = Color.PowderBlue;

            if (countAdversaryHit == 4)
            {
                StopGame("UPS...", "Your ship is sunk, adversary won");
            }
            else
                grid2.IsEnabled = true;
        }
    }
}
