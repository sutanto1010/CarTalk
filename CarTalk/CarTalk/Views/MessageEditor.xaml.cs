using System;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms.Xaml;

namespace CarTalk.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MessageEditor
	{
	    private static MessageEditor _instance;
	    public Action<string, string> OnSaveButton;

	    private MessageEditor ()
		{
			InitializeComponent ();
		}

	    public static MessageEditor Current => _instance ?? (_instance = new MessageEditor());

	    public void Show()
	    {
	        PopupNavigation.PushAsync(this);
	    }

        public void Hide()
        {
            PopupNavigation.PopAsync(true);
        }

	    private void OnCancelButtonTapped(object sender, EventArgs e)
	    {
	        Hide();
	    }

	    private void OnSaveButtonTapped(object sender, EventArgs e)
	    {
	        OnSaveButton?.Invoke(editorTitle.Text, editorMessage.Text);
            Hide();
	    }
	}
}