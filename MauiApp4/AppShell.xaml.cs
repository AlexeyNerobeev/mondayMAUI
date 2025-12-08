namespace MauiApp4
{
    public partial class AppShell : Shell
    {
        AppTheme _currentTheme = Application.Current.UserAppTheme;
        public AppShell()
        {
            InitializeComponent();
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (_currentTheme == AppTheme.Dark)
            {
                Application.Current.UserAppTheme = AppTheme.Light;
                _currentTheme = AppTheme.Light;
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
                _currentTheme = AppTheme.Dark;
            }
        }
    }
}
