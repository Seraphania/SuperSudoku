using Android.App;
using Android.Runtime;
using Microsoft.Maui.Platform;

namespace SuperSudoku
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() 
        {
            // Remove Entry control underline
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
                h.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(
                        Colors.Transparent.ToPlatform());

                h.PlatformView.ShowSoftInputOnFocus = false;
            });

            return MauiProgram.CreateMauiApp();
        }
    }
}
