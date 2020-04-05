using Microsoft.AspNetCore.Components.Desktop;
using System;

namespace MyDesktopApp
{
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ComponentsDesktop.Run<Startup>("index.html", form =>
            {
                form.Text = "MyDesktopApp";
            }, uri =>
            {
                var x = typeof(MatBlazor.BaseMapper).Assembly.GetManifestResourceNames();
                return null;
            });
        }
    }
}
