using Craftz.Views;
using CSharpForMarkup;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace PROAtas.Views.Dialogs
{
    public class OptionDialog : BaseDialog
    {
        public string LastWarning { get; set; }

        public OptionDialog(string title, string firstTitle = null, string secondTitle = null, string thirdTitle = null, string lastTitle = null,
            ImageSource firstImage = null, ImageSource secondmage = null, ImageSource thirdImage = null, ImageSource lastImage = null,
            Color? firstTextColor = null, Color? secondTextColor = null, Color? thirdTextColor = null, Color? lastTextColor = null, EDockTo? dockSide = null) : base(dockSide)
            => Build(title, firstTitle, secondTitle, thirdTitle, lastTitle, firstImage, secondmage, thirdImage, lastImage, firstTextColor, secondTextColor, thirdTextColor, lastTextColor);

        private void Build(string title, string firstTitle = null, string secondTitle = null, string thirdTitle = null, string lastTitle = null,
            ImageSource firstImage = null, ImageSource secondImage = null, ImageSource thirdImage = null, ImageSource lastImage = null,
            Color? firstTextColor = null, Color? secondTextColor = null, Color? thirdTextColor = null, Color? lastTextColor = null)
        {
            var optionStack = new StackLayout
            {
                Children =
                {
                    // Header
                    new Label { Text = title } .HeaderText() .Center(),
                }
            }.Invoke(c =>
            {
                // If there's a First command, add to stack
                if (!string.IsNullOrEmpty(firstTitle))
                {
                    var firstButton = new Button { Text = firstTitle, ImageSource = firstImage }.Standard().CenterV().FillExpandH();
                    firstButton.Clicked += FirstClick;

                    if (firstTextColor != null) firstButton.TextColor = firstTextColor ?? firstButton.TextColor;

                    c.Children.Add(firstButton);
                }

                // If there's a Second command, add to stack
                if (!string.IsNullOrEmpty(secondTitle))
                {
                    var secondButton = new Button { Text = secondTitle, ImageSource = secondImage }.Standard().CenterV().FillExpandH();
                    secondButton.Clicked += SecondClick;

                    if (secondTextColor != null) secondButton.TextColor = secondTextColor ?? secondButton.TextColor;

                    c.Children.Add(secondButton);
                }

                // If there's a Third command, add to stack
                if (!string.IsNullOrEmpty(thirdTitle))
                {
                    var thirdButton = new Button { Text = thirdTitle, ImageSource = thirdImage }.Standard().CenterV().FillExpandH();
                    thirdButton.Clicked += ThirdClick;

                    if (thirdTextColor != null) thirdButton.TextColor = thirdTextColor ?? thirdButton.TextColor;

                    c.Children.Add(thirdButton);
                }

                // If there's a Last command, add to stack
                if (!string.IsNullOrEmpty(lastTitle))
                {
                    var lastButton = new Button { Text = lastTitle, ImageSource = lastImage, Margin = new Thickness(0, 20, 0, 0) }.Danger().CenterV().FillExpandH();
                    lastButton.Clicked += LastClick;

                    if (lastTextColor != null) lastButton.TextColor = lastTextColor ?? lastButton.TextColor;

                    c.Children.Add(lastButton);
                }
            });
            
            // Options Dialog
            Content = new Frame
            {
                // Option List
                Content = optionStack,
            } .Standard();
        }

        public ICommand FirstCommand
        {
            get { return (ICommand)GetValue(FirstCommandProperty); }
            set { SetValue(FirstCommandProperty, value); }
        }
        public static readonly BindableProperty FirstCommandProperty = BindableProperty.Create(nameof(FirstCommand), typeof(ICommand), typeof(OptionDialog), default(ICommand));

        public ICommand SecondCommand
        {
            get { return (ICommand)GetValue(SecondCommandProperty); }
            set { SetValue(SecondCommandProperty, value); }
        }
        public static readonly BindableProperty SecondCommandProperty = BindableProperty.Create(nameof(SecondCommand), typeof(ICommand), typeof(OptionDialog), default(ICommand));

        public ICommand ThirdCommand
        {
            get { return (ICommand)GetValue(ThirdCommandProperty); }
            set { SetValue(ThirdCommandProperty, value); }
        }
        public static readonly BindableProperty ThirdCommandProperty = BindableProperty.Create(nameof(ThirdCommand), typeof(ICommand), typeof(OptionDialog), default(ICommand));

        public ICommand LastCommand
        {
            get { return (ICommand)GetValue(LastCommandProperty); }
            set { SetValue(LastCommandProperty, value); }
        }
        public static readonly BindableProperty LastCommandProperty = BindableProperty.Create(nameof(LastCommand), typeof(ICommand), typeof(OptionDialog), default(ICommand));

        private void FirstClick(object sender, EventArgs e)
        {
            FirstCommand?.Execute(null);
        }

        private void SecondClick(object sender, EventArgs e)
        {
            SecondCommand?.Execute(null);
        }

        private void ThirdClick(object sender, EventArgs e)
        {
            ThirdCommand?.Execute(null);
        }

        private void LastClick(object sender, EventArgs e)
        {
            LastCommand?.Execute(null);
        }
    }
}
