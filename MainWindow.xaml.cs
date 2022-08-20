using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using NHotkey;
using NHotkey.Wpf;

namespace AutoClicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Timer? _timer;
        
        public MainWindow()
        {
            HotkeyManager.Current.AddOrReplace("Increment", Key.F8, ModifierKeys.None, HotkeyAutoClicker);
            InitializeComponent();
        }

        private void HotkeyAutoClicker(object? sender, HotkeyEventArgs e)
        {
            ToggleButtonAutoClick.IsChecked = ToggleButtonAutoClick.IsChecked != true;
        }

        private void ToggleButtonAutoClick_Checked(object sender, RoutedEventArgs e)
        {
            int interval = int.Parse(TextBoxInterval.Text);

            if (interval == 0)
            {
                interval = 1;
            }
            
            _timer = new Timer((object? state) =>
            {
                MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown | MouseOperations.MouseEventFlags.LeftUp);
            }, null, new TimeSpan(0), new TimeSpan(TimeSpan.TicksPerMillisecond * interval));
        }

        private void ToggleButtonAutoClick_UnChecked(object sender, RoutedEventArgs e)
        {
            _timer?.Dispose();
        }

        private void TextBoxInterval_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }
    }
}