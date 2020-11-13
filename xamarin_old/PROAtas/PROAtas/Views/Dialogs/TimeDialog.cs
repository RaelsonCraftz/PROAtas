using Craftz.Views;
using CSharpForMarkup;
using PROAtas.Assets.Components;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.ViewModel;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;

namespace PROAtas.Views.Dialogs
{
    public class TimeDialog : BaseDialog
    {
        public TimeDialog(EDockTo? dockSide = null) : base(dockSide) => Build();

        private void Build()
        {
            Content = new Frame
            {
                Content = new StackLayout
                {
                    // Dialog Content
                    Children =
                    {
                        new Image { Source = Images.Time } .Center(),

                        new ScrollView
                        {
                            Content = new StackLayout
                            {
                                Spacing = 5,

                                Children =
                                {
                                    new Frame { } .FramedDatePicker(Images.Date, "Data", nameof(MinuteViewModel.SaveDate), nameof(MinuteViewModel.Date)),

                                    new Frame { } .FramedTimePicker(Images.Time, "Início", nameof(MinuteViewModel.SaveStart), nameof(MinuteViewModel.Start)),

                                    new Frame { } .FramedTimePicker(Images.Time, "Fim", nameof(MinuteViewModel.SaveEnd), nameof(MinuteViewModel.End)),
                                }
                            }
                        } .FillExpand(),
                    },
                }
            } .Standard();
        }
    }
}
