using FFMpegCore.Helpers;
using FFMpegCore;

namespace MauiApp_FFMpegCore_test;

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

        pickTest();
        
        try
        {
            FFMpegHelper.VerifyFFMpegExists(new FFOptions());
        }
        catch (Exception ex)
        {
            CounterBtn.Text = $"Exception {ex}";
        }

        SemanticScreenReader.Announce(CounterBtn.Text);
   }

   public async Task<FileResult> PickAndShow(PickOptions options)
   {
      try
      {
         var result = await FilePicker.PickAsync(options);
         if (result != null)
            CounterBtn.Text = $"PickAndShow {result.FileName}";
        
         return result;
      }
      catch (Exception ex)
      {
         CounterBtn.Text = $"PickAndShow {ex}";
      }
    
      return null;
   }

   public async Task pickTest()
   {
      try
      {
         // get images from zip file
         // https://android.googlesource.com/platform/external/mime-support/+/fa3f892f28db393b1411f046877ee48179f6a4cf/mime.types
         // https://developer.apple.com/documentation/uniformtypeidentifiers/uttypearchive?changes=_9
         var zipFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                                                  {
                                                     { DevicePlatform.iOS, new[] { "public.archive" } },
                                                     { DevicePlatform.Android, new[] { "application/zip" } },
                                                     { DevicePlatform.WinUI, new[] { "zip" } },
                                                     { DevicePlatform.MacCatalyst, new[] { "zip" } },
                                                        });
         var optionsZip = new PickOptions
            {
               PickerTitle = "Please select a previously backed up zip file",
               FileTypes = zipFileType
            };

         string extractPath;
         var zipFile = await PickAndShow(optionsZip);
         if (zipFile != null)
         {
            CounterBtn.Text = $"Pick success: {zipFile.FullPath}";
         }
      }
      catch (OperationCanceledException ex)
      {
         CounterBtn.Text = $"pickTest {ex}";
      }
      catch (Exception ex)
      {
         CounterBtn.Text = $"pickTest {ex}";
      }
   }
}
