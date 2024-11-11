using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace BeyondTalents.Views
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            SendMessage(helper.Handle, 161,2,0);
        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }



        //private void StartAnimation(object sender, RoutedEventArgs e)
        //{
        //    double targetValue = 75; // Example target value, could come from your match percentage

        //    DoubleAnimation animation = new DoubleAnimation
        //    {
        //        From = MatchProgressBar.Value,    // Start from current value
        //        To = targetValue,                 // Animate to target value
        //        Duration = TimeSpan.FromSeconds(1), // Duration of the animation
        //        EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseIn } // Easing function
        //    };

        //    MatchProgressBar.BeginAnimation(ProgressBar.ValueProperty, animation);
        //}
    }
}