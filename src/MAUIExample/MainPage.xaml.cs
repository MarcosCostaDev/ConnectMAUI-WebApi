using System.Net.Http.Json;

namespace MAUIExample
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void ShowWelcomeButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                using var httpClient = new HttpClient()
                {
#if ANDROID
                    BaseAddress = new Uri("http://10.0.2.2:5017")
#else
                    BaseAddress = new Uri("http://localhost:5017")
#endif
                };

                var message = await httpClient.GetStringAsync("/");

                ShowWelcomeButton.Text = $"Message From Server: {message}.";
            }
            catch(Exception ex)
            {
               await DisplayAlert("error", ex.Message, "Ok");
            }
            
        }
    }
}