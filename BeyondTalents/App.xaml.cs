using System.Configuration;
using System.Data;
using System.Windows;
using BeyondTalents.Views;

namespace BeyondTalents
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            //Load the login view
            //var loginView = new LoginView();
            //loginView.Show();

            //loginView.IsVisibleChanged += (s, ev) =>
            //{
            //    if (!loginView.IsVisible && loginView.IsLoaded)
            //    {
                    var mainView = new MainWindow();
                    mainView.Show();

            //    }
            //};
        }
    }

}
