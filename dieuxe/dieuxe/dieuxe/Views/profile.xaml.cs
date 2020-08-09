
using dieuxe.Helpers;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace dieuxe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class profile : ContentPage
    {
        public profile()
        {
            InitializeComponent();
            sdt.Text = Settings.SdtLienLac;
            chucvu.Text = Settings.chucvu;
            ten.Text = Settings.TenLienHe;
            email.Text = Settings.email;
        }
        private async void Sendsms_Clicked(object sender, System.EventArgs e)
        {

            //SmsManager sms = SmsManager.Default;
            //sms.SendTextMessage(txtNumber.Text, null, txtMessage.Text, null, null);


        }

        //public async Task SendSms(string messageText, string recipient)
        //{
        //    try
        //    {
        //        var message = new Xamarin.Essentials.SmsMessage(messageText, recipient);
        //        await Sms.ComposeAsync(message);
        //    }
        //    catch (FeatureNotSupportedException ex)
        //    {
        //        await DisplayAlert("Failed", "Sms is not supported on this device.", "OK");
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("Failed", ex.Message, "OK");
        //    }
        //}
    }
}