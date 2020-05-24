using Microsoft.Toolkit.Forms.UI.Controls;
using Microsoft.Toolkit.Win32.UI.Controls.Interop.WinRT;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.AspNetCore.Components.Desktop
{
    internal partial class RootForm : Form
    {
        public RootForm(IUriToStreamResolver streamResolver)
        {
            InitializeComponent();
            StreamResolver = streamResolver;
        }

        public IUriToStreamResolver StreamResolver { get; }

        public IPC CreateChannel(string hostHtmlPath)
        {
            var webView = CreateWindow(hostHtmlPath);
            var ipc = new IPC(webView);
            return ipc;
        }

#pragma warning disable CS0618 // Type or member is obsolete
        private WebView CreateWindow(string hostHtmlPath)
#pragma warning restore CS0618 // Type or member is obsolete
        {
#pragma warning disable CS0618 // Type or member is obsolete
            var wvc = new WebView();
#pragma warning restore CS0618 // Type or member is obsolete
            ((ISupportInitialize)wvc).BeginInit();
            wvc.Dock = DockStyle.Fill;
            Controls.Add(wvc);
            ((ISupportInitialize)wvc).EndInit();
            wvc.IsScriptNotifyAllowed = true;
            wvc.NavigateToLocalStreamUri(new Uri(hostHtmlPath, UriKind.Relative), this.StreamResolver);
            return wvc;
        }
    }
}
