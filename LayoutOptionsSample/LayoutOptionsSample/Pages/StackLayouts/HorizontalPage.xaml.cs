using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace LayoutOptionsSample.Pages.StackLayouts
{
    public partial class HorizontalPage : ContentPage
    {
        private static readonly Color[] _colors = new[]
                {
                    Color.Aqua,
                    Color.Red,
                    Color.Yellow,
                    Color.Green,
                    Color.Blue,
                    Color.Fuchsia,
                };

        private static readonly Tuple<LayoutOptions, string>[] _layoutOptions = new[]
            {
                Tuple.Create(LayoutOptions.Start, "Start"),
                Tuple.Create(LayoutOptions.StartAndExpand, "StartAndExpand"),
                Tuple.Create(LayoutOptions.Center, "Center"),
                Tuple.Create(LayoutOptions.CenterAndExpand, "CenterAndExpand"),
                Tuple.Create(LayoutOptions.End, "End"),
                Tuple.Create(LayoutOptions.EndAndExpand, "EndAndExpand"),
                Tuple.Create(LayoutOptions.Fill, "Fill"),
                Tuple.Create(LayoutOptions.FillAndExpand, "FillAndExpand"),
            };

        public HorizontalPage()
        {
            InitializeComponent();

            GenerateButton.Clicked += GenerateButton_Clicked;
            NextSetButton.Clicked += NextSetButton_Clicked;
        }

        private void GenerateButton_Clicked(object sender, EventArgs e)
        {
            NextSetButton.IsVisible = false;

            var minLength = Convert.ToInt32(MinLength.Text);
            var maxLength = Convert.ToInt32(MaxLength.Text);
            var minCombinations = Convert.ToInt32(MinCombination.Text);
            var maxCombinations = minCombinations + Convert.ToInt32(MaxCombination.Text);
            var combinationCount = 0;

            MainPanel.Children.Clear();

            Combination.GenerateCombinations(_layoutOptions, minLength, maxLength, (layouts) =>
                {
                    combinationCount++;

                    if (combinationCount < minCombinations)
                    {
                        return true;
                    }

                    if (combinationCount > maxCombinations)
                    {
                        NextSetButton.IsVisible = true;

                        return false;
                    }

                    var firstLayout = layouts.First().Item2;

                    if (!layouts.All(l => l.Item2.Equals(firstLayout, StringComparison.Ordinal)))
                    {
                        var stackPanel = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                        };

                        for (int i = 0; i < layouts.Length; i++)
                        {
                            var layout = layouts[i];

                            stackPanel.Children.Add(new Label
                            {
                                Text = layout.Item2,
                                HorizontalOptions = layout.Item1,
                                BackgroundColor = _colors[(i + 1) % _colors.Length],
                            });
                        }

                        MainPanel.Children.Add(new Frame
                        {
                            Content = stackPanel,
                        });
                    }

                    return true;
                });
        }

        private void NextSetButton_Clicked(object sender, EventArgs e)
        {
            var minCombinations = Convert.ToInt32(MinCombination.Text);
            var maxCombinations = Convert.ToInt32(MaxCombination.Text);

            MinCombination.Text = Convert.ToString(minCombinations + maxCombinations);

            GenerateButton_Clicked(null, EventArgs.Empty);
        }
    }
}