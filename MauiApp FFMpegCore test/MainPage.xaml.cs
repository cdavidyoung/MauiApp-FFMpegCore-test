using FFMpegCore.Helpers;
using FFMpegCore;

namespace MauiApp_FFMpegCore_test;

public partial class MainPage : ContentPage
{
   int count = 0;

   public MainPage()
   {
      InitializeComponent();
      try
      {
         FFMpegHelper.VerifyFFMpegExists(new FFOptions());
      }
      catch (Exception ex)
      {
         CounterBtn.Text = $"Exception {ex}";
      }
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
}

