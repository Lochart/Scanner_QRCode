using Xamarin.UITest;

namespace QRCode.UITests
{
    public static class AppInitializer
    {
        public static IApp StartApp(Platform platform) => 
            platform == Platform.Android ? ConfigureApp.Android.StartApp() : 
            (IApp)ConfigureApp.iOS.StartApp();
    }
}