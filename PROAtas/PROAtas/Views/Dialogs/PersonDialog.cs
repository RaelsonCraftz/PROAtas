using Craftz.Views;
using CSharpForMarkup;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.ViewModel;
using PROAtas.Views.DataTemplates;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace PROAtas.Views.Dialogs
{
    public class PersonDialog : BaseDialog
    {
        public PersonDialog(object viewModel, EDockTo? dockSide = null) : base(dockSide) => Build(viewModel);

        private void Build(object viewModel)
        {
            Content = new Frame
            {
                Content = new StackLayout
                {
                    // Dialog Content
                    Children =
                    {
                        new Image { Source = Images.People } .Center(),

                        new CollectionView
                        {
                            ItemTemplate = PersonTemplate.New(viewModel), SelectionMode = SelectionMode.None,

                            // FOOTER - Action
                            Footer = new ContentView
                            {
                                Content = new Button { ImageSource = Images.Add } .Standard() .Round(40) .Center()
                                    .Bind(nameof(MinuteViewModel.CreatePerson)),
                            },
                        } .VerticalListStyle() .FillExpand()
                            .Assign(out CollectionView peopleCollection)
                            .Bind(CollectionView.ItemsSourceProperty, nameof(MinuteViewModel.People)),
                    }
                }
            } .Standard();
        }
    }
}
