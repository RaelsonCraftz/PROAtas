using Xamarin.Forms;

namespace PROAtas.Controls
{
    public class CustomButton : Button
    {
        private readonly string Placeholder;
        private readonly Color MainTextColor;
        private readonly Color EmptyTextColor;

        public CustomButton(string placeholder, Color mainTextColor, Color emptyTextColor)
        {
            this.Placeholder = placeholder;
            this.MainTextColor = mainTextColor;
            this.EmptyTextColor = emptyTextColor;
        }

        public string MainText
        {
            get { return (string)GetValue(MainTextProperty); }
            set { SetValue(MainTextProperty, value); }
        }
        public static readonly BindableProperty MainTextProperty = BindableProperty.Create(nameof(MainText), typeof(string), typeof(CustomButton), default(string), defaultValueCreator: PlaceholderMainText, propertyChanged: ChangeMainTextExecute);

        private static object PlaceholderMainText(BindableObject bindable)
        {
            var button = (CustomButton)bindable;

            return button.Placeholder;
        }

        private static void ChangeMainTextExecute(BindableObject bindable, object oldvalue, object newvalue)
        {
            var control = (CustomButton)bindable;
            control.ChangeMainText(control);
        }
        private void ChangeMainText(CustomButton control)
        {
            if (MainText != null)
                if (MainText.Length == 0)
                {
                    TextColor = EmptyTextColor;
                    Text = Placeholder;
                }
                else
                {
                    TextColor = MainTextColor;
                    Text = MainText;
                }
            else
            {
                TextColor = EmptyTextColor;
                Text = string.Empty;
            }
        }
    }
}
