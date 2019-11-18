using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SeaBattle
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpPage : PopupPage
    {
        public PopUpPage(string title,string text)
        {
            InitializeComponent();
            titleLabel.Text = title;
            PopupTextLabel.Text = text;
        }
    }
}