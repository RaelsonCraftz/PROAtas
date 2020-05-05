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
    public class ImageCollectionDialog : BaseDialog
    {
        public ImageCollectionDialog(object parentViewModel, EDockTo? dockSide = null) : base(dockSide) => Build(parentViewModel);

        public CollectionView imageCollection;
        private void Build(object parentViewModel)
        {
            Content = new Frame
            {
                // Image Collection
                Content = new CollectionView
                {
                    // HEADER - Image
                    Header = new ContentView
                    {
                        Content = new Image { Source = Images.Storage }.Center(),
                    },

                    // BODY - Minute images list
                    ItemTemplate = MinuteImageTemplate.New(),
                    SelectionMode = SelectionMode.Single,
                } .GridStyle(ItemsLayoutOrientation.Vertical, 3, 5)
                    .Assign(out imageCollection)
                    .Bind(CollectionView.ItemsSourceProperty, nameof(SettingsViewModel.ImageCollection))
                    .Bind(CollectionView.SelectionChangedCommandProperty, nameof(SettingsViewModel.SelectCollection), source: parentViewModel)
                    .Bind(CollectionView.SelectionChangedCommandParameterProperty, nameof(CollectionView.SelectedItem), source: imageCollection),
            } .Standard();
        }
    }
}
