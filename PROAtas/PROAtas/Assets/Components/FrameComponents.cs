using CSharpForMarkup;
using PROAtas.Assets.Styles;
using PROAtas.Assets.Theme;
using PROAtas.Behaviors;
using PROAtas.Controls;
using PROAtas.Converters;
using System.Collections;
using Xamarin.Forms;
using static CSharpForMarkup.EnumsForGridRowsAndColumns;
using static PROAtas.Behaviors.MovingBehavior;

namespace PROAtas.Assets.Components
{
    public static class FrameComponents
    {
        public static TFrame FramedHorizontalLabelInput<TFrame>(this TFrame frame, ImageSource imageHeader, string textHeader, string inputPath, string sendPath) where TFrame : Frame
        {
            frame.Standard();
            frame.Content = new Grid
            {
                ColumnDefinitions = Columns.Define(
                    (0, 32),
                    (1, Star),
                    (2, Star)),

                RowSpacing = 0, ColumnSpacing = 5,

                Children =
                {
                    // Header image
                    new Image { Source = imageHeader }
                        .Col(0),

                    new Label { FontSize = 20, TextColor = Colors.TextIcons, Text = textHeader } .Center()
                        .Col(1),

                    new Grid
                    {
                        ColumnDefinitions = Columns.Define(
                            (0, 32),
                            (1, Star),
                            (2, 32)),

                        RowSpacing = 0, ColumnSpacing = 5,

                        Children =
                        {
                            // Decrease input button by 1
                            new Button { ImageSource = Images.Remove, CommandParameter = -1 } .Danger() .Round(32) .Center()
                                .Col(0)
                                .Bind(sendPath),

                            // Input Label
                            new Frame 
                            { 
                                Padding = 5, CornerRadius = 6, BackgroundColor = Colors.TextIcons,
                    
                                Content = new Label { FontSize = 14, TextColor = Colors.PrimaryText } .Center()
                                            .Bind(inputPath),
                            } .Col(1) .Height(32),

                            // Increase input button by 1
                            new Button { ImageSource = Images.Add, CommandParameter = 1 } .Success() .Round(32) .Center()
                                .Col(2)
                                .Bind(sendPath),
                        }
                    } .Col(2),
                },
            };

            return frame;
        }

        public static TFrame FramedVerticalEntryInput<TFrame>(this TFrame frame, ImageSource imageHeader, string placeholder, string inputPath, string sendPath, int saveDelay = 1500) where TFrame : Frame
        {
            frame.Standard();
            frame.Content = new Grid
            {
                RowDefinitions = Rows.Define(
                    (0, 32),
                    (1, Star),
                    (2, 32)),

                RowSpacing = 5, ColumnSpacing = 0,

                Children =
                {
                    new Frame
                    {
                        Padding = 5, CornerRadius = 6, BackgroundColor = Colors.TextIcons,

                        Content = new Grid
                        {
                            ColumnDefinitions = Columns.Define(
                                (0, Star),
                                (1, 32)),

                            Children =
                            {
                                new CustomEntry { Keyboard = Keyboard.Numeric, SaveDelay = saveDelay, Placeholder = placeholder, PlaceholderColor = Colors.SecondaryText, HorizontalTextAlignment = TextAlignment.Center, Visual = VisualMarker.Default } .Standard()
                                    .Assign(out CustomEntry customEntry)
                                    .Col(0)
                                    .Bind(CustomEntry.TextProperty, inputPath, mode: BindingMode.OneWay)
                                    .Bind(CustomEntry.SaveCommandProperty, sendPath),

                                new Label { FontSize = 12, TextColor = Colors.PrimaryText, Text = "cm" } .Center()
                                    .Col(1),
                            }
                        },
                    } .Row(1),

                    // Header image
                    new Image 
                    { 
                        Source = imageHeader,
                        Behaviors =
                        {
                            new MovingBehavior { MoveTo = EMoveTo.Top }
                                .BindBehavior(MovingBehavior.IsActiveProperty, nameof(CustomEntry.IsSaving), source: customEntry, converter: new BoolToInvert()),
                            new FadingBehavior { }
                                .BindBehavior(FadingBehavior.IsActiveProperty, nameof(CustomEntry.IsSaving), source: customEntry, converter: new BoolToInvert()),
                        }
                    } .Row(0) .Size(32),

                    // Busy indicator
                    new ActivityIndicator { Color = Colors.TextIcons } .Size(32) .Center()
                        .Row(0)
                        .Bind(nameof(CustomEntry.IsSaving), source: customEntry),

                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,

                        Spacing = 5,

                        Children =
                        {
                            // Decrease input button by 1
                            new Button { ImageSource = Images.Remove, CommandParameter = "-1" } .Danger() .Round(32)
                                .Bind(sendPath),

                            // Increase input button by 1
                            new Button { ImageSource = Images.Add, CommandParameter = "+1" } .Success() .Round(32)
                                .Bind(sendPath),
                        }
                    } .Row(2) .Center(),
                },
            };

            return frame;
        }

        public static TFrame FramedPicker<TFrame>(this TFrame frame, ImageSource imageHeader, string textHeader, string itemPath, string sendPath, IList itemsSource, string displayPath = null) where TFrame : Frame
        {
            frame.Standard();
            frame.Content = new Grid
            {
                ColumnDefinitions = Columns.Define(
                    (0, 32),
                    (1, GridLength.Star),
                    (2, GridLength.Star)),

                RowSpacing = 0,
                ColumnSpacing = 5,

                Children =
                {
                    // Header image
                    new Image { Source = imageHeader }
                        .Col(0),

                    new Label { FontSize = 20, TextColor = Colors.TextIcons, Text = textHeader } .Center()
                        .Col(1),

                    // Picker input
                    new Frame
                    {
                        Padding = 5, CornerRadius = 6, BackgroundColor = Colors.TextIcons,

                        Content = new CustomPicker { ItemsSource = itemsSource, BackgroundColor = Colors.TextIcons }
                            .Assign(out CustomPicker customPicker)
                            .Bind(CustomPicker.CommandProperty, sendPath)
                            .Bind(CustomPicker.SelectedItemProperty, itemPath),
                    } .Col(2)
                },
            };

            if (!string.IsNullOrEmpty(displayPath))
                customPicker.ItemDisplayBinding = new Binding(displayPath);

            return frame;
        }

        public static TFrame FramedCustomEntry<TFrame>(this TFrame frame, out CustomEntry customEntry, ImageSource headerImage) where TFrame : Frame
        {
            frame.CornerRadius = 6;
            frame.Padding = 5;
            frame.BackgroundColor = Colors.TextIcons;
            frame.Content = new Grid
            {
                ColumnDefinitions = Columns.Define(
                    (0, 32),
                    (1, GridLength.Star)),

                RowSpacing = 0, ColumnSpacing = 5,

                Children =
                {
                    // Required bindings
                    new CustomEntry { SaveDelay = 1500, PlaceholderColor = Colors.SecondaryText, Visual = VisualMarker.Default } .Standard()
                        .Assign(out customEntry)
                        .Col(1),

                    // Header image
                    new Image
                    {
                        Source = headerImage,
                        Behaviors =
                        {
                            new MovingBehavior { MoveTo = EMoveTo.End }
                                .BindBehavior(MovingBehavior.IsActiveProperty, nameof(CustomEntry.IsSaving), source: customEntry, converter: new BoolToInvert()),
                            new FadingBehavior { }
                                .BindBehavior(FadingBehavior.IsActiveProperty, nameof(CustomEntry.IsSaving), source: customEntry, converter: new BoolToInvert())
                        }
                    } .Col(0) .Size(32),

                    // Busy indicator
                    new ActivityIndicator { Color = Colors.Primary } .Size(32)
                        .Col(0)
                        .Bind(nameof(CustomEntry.IsSaving), source: customEntry),
                }
            };

            return frame;
        }

        public static TFrame FramedCustomEditor<TFrame>(this TFrame frame, string savePath, string isSavingPath, string textPath, object saveSource = null, bool hasSaveParameter = false, int saveDelay = 1500) where TFrame : Frame
        {
            var customEditor = new CustomEditor { SaveDelay = saveDelay, Placeholder = "Toque para escrever...", PlaceholderColor = Colors.SecondaryText, Visual = VisualMarker.Default };

            frame.CornerRadius = 6;
            frame.Padding = 5;
            frame.BackgroundColor = Colors.TextIcons;
            frame.Content = new Grid
            {
                RowDefinitions = Rows.Define(
                    (0, 32),
                    (1, GridLength.Star)),

                GestureRecognizers =
                {
                    new TapGestureRecognizer { } .Invoke(l => l.Tapped += (s, e) => customEditor.Focus())
                },

                Children =
                {
                    // Busy indicator
                    new ActivityIndicator { Color = Colors.Primary } .Center() .Size(32)
                        .Row(0)
                        .Bind(nameof(CustomEditor.IsSaving), source: customEditor),

                    // Required bindings
                    customEditor .Standard()
                        .Row(1)
                        .Bind(CustomEditor.TextProperty, textPath, mode: BindingMode.TwoWay)
                        .Bind(CustomEditor.SaveCommandProperty, savePath, source: saveSource)
                        .Bind(CustomEditor.IsSavingProperty, isSavingPath, mode: BindingMode.OneWayToSource),
                }
            } .Standard();

            // Optional bindings
            if (hasSaveParameter)
                customEditor.Bind(CustomEditor.SaveCommandParameterProperty);

            return frame;
        }

        public static TFrame FramedDatePicker<TFrame>(this TFrame frame, ImageSource headerImage, string headerText, string saveDatePath, string datePath) where TFrame : Frame
        {
            frame.CornerRadius = 6;
            frame.Padding = 5;
            frame.BackgroundColor = Colors.Divider;
            frame.Content = new Grid
            {
                ColumnDefinitions = Columns.Define(
                    (0, 18),
                    (1, GridLength.Star)),

                RowDefinitions = Rows.Define(
                    (0, 32),
                    (1, 48)),

                RowSpacing = 0, ColumnSpacing = 5,

                Children =
                {
                    // Header image
                    new Image
                    {
                        Source = headerImage,
                    } .Col(0) .RowSpan(2) .Size(32) .Center(),

                    // Header text
                    new Label { Text = headerText, FontSize = 14, TextColor = Colors.TextIcons }
                        .Col(1) .Row(0),

                    // Date picker
                    new Frame
                    {
                        Padding = new Thickness(5, 0, 5, 0), CornerRadius = 6, BackgroundColor = Colors.Accent,

                        Content = new CustomDatePicker { FontSize = 14, TextColor = Colors.TextIcons, BackgroundColor = Colors.Accent, Visual = VisualMarker.Default }
                            .Assign(out CustomDatePicker datePicker)
                            .Bind(CustomDatePicker.CommandProperty, saveDatePath)
                            .Bind(CustomDatePicker.DateProperty, datePath),

                    } .Col(1) .Row(1),
                },

                GestureRecognizers =
                {
                    new TapGestureRecognizer {} .Invoke(l => l.Tapped += (s, e) => datePicker.Focus())
                },
            };

            return frame;
        }

        public static TFrame FramedTimePicker<TFrame>(this TFrame frame, ImageSource headerImage, string headerText, string saveTimePath, string timePath) where TFrame : Frame
        {
            frame.CornerRadius = 6;
            frame.Padding = 5;
            frame.BackgroundColor = Colors.Divider;
            frame.Content = new Grid
            {
                ColumnDefinitions = Columns.Define(
                    (0, 18),
                    (1, GridLength.Star)),

                RowDefinitions = Rows.Define(
                    (0, 32),
                    (1, 48)),

                RowSpacing = 0,
                ColumnSpacing = 5,

                Children =
                {
                    // Header image
                    new Image
                    {
                        Source = headerImage,
                    } .Col(0) .RowSpan(2) .Size(32) .Center(),

                    // Header text
                    new Label { Text = headerText, FontSize = 14, TextColor = Colors.TextIcons }
                        .Col(1) .Row(0),

                    // Time picker
                    new Frame
                    {
                        Padding = new Thickness(5, 0, 5, 0), CornerRadius = 6, BackgroundColor = Colors.Accent,

                        Content = new CustomTimePicker { FontSize = 14, TextColor = Colors.TextIcons, BackgroundColor = Colors.Accent, Visual = VisualMarker.Default }
                            .Assign(out CustomTimePicker timePicker)
                            .Bind(CustomTimePicker.CommandProperty, saveTimePath)
                            .Bind(CustomTimePicker.TimeProperty, timePath),

                    } .Col(1) .Row(1),
                },
                
                GestureRecognizers =
                {
                    new TapGestureRecognizer {} .Invoke(l => l.Tapped += (s, e) => timePicker.Focus())
                },
            };

            return frame;
        }

        public static TFrame FramedEntry<TFrame>(this TFrame frame, string header, string entryBindingPath = null) where TFrame : Frame
        {
            frame.CornerRadius = 6;
            frame.Padding = 5;
            frame.BackgroundColor = Colors.TextIcons;
            frame.Content = new StackLayout
            {
                Spacing = 0,

                Children =
                {
                    new Label { Text = header } .SecondaryText(),

                    new Entry { Margin = 0 } .Standard()
                        .Bind(entryBindingPath),
                }
            };

            return frame;
        }

        public static TFrame FramedEditor<TFrame>(this TFrame frame, string entryBindingPath, string header = null) where TFrame : Frame
        {
            var editor = new Editor { Margin = 0, Visual = VisualMarker.Default };

            frame.CornerRadius = 6;
            frame.Padding = 5;
            frame.BackgroundColor = Colors.TextIcons;
            frame.Content = new Grid
            {
                RowDefinitions = Rows.Define(
                    (0, 32),
                    (1, GridLength.Star)),

                Children =
                {
                    editor .Standard()
                        .Row(1)
                        .Bind(entryBindingPath),
                }
            } .Assign(out Grid grid);

            if (!string.IsNullOrEmpty(header))
                grid.Children.Add(new Label { Text = header } .SecondaryText());

            return frame;
        }
    }
}
