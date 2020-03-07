using CSharpForMarkup;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.Controls;
using PROAtas.ViewModel;
using PROAtas.ViewModel.Elements;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace PROAtas.Views.DataTemplates
{
    public class InformationTemplate
    {
        enum Col { Reorder, Content, Action }

        public static DataTemplate New(MinuteViewModel vm) => new DataTemplate(() =>
        {
            // Outer Grid
            var grid = new Grid
            {
                BackgroundColor = Colors.TextIcons,
                Padding = 5,

                Children =
                {
                    new Frame
                    {
                        Padding = 5, CornerRadius = 6, BackgroundColor = Colors.Primary,

                        Content = new Grid
                        {
                            ColumnDefinitions = Columns.Define(
                                (Col.Reorder, GridLength.Auto),
                                (Col.Content, GridLength.Star),
                                (Col.Action, GridLength.Auto)),

                            RowSpacing = 0, ColumnSpacing = 5,

                            Children =
                            {
                                new CustomButton ("Nenhuma informação...", Colors.TextIcons, Colors.SecondaryText) .Standard()
                                    .Col(Col.Content)
                                    .Bind(CustomButton.MainTextProperty, nameof(InformationElement.Text))
                                    .Bind(CustomButton.CommandParameterProperty)
                                    .Bind(nameof(vm.SelectInformation), source: vm),

                                new Button { ImageSource = Images.Delete } .Danger() .Round(40) .Center()
                                    .Col(Col.Action)
                                    .Bind(Button.CommandParameterProperty)
                                    .Bind(nameof(vm.DeleteInformation), source: vm),
                            }
                        }
                    },
                }
            };

            return grid;
        });
    }
}
