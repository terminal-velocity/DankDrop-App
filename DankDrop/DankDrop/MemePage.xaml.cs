using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DankDrop
{
    public partial class MemePage : ContentPage
    {
        private readonly DropPoint _point;

        public MemePage(DropPoint point)
        {
            _point = point;
            InitializeComponent();

            Refresh();

            SubmitMemeButton.Clicked += onClickAddMeme;
        }

        public void Refresh()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                list.ItemsSource = await _point.GetMemesHere();
            });
        }

        private void onClickAddMeme(object sender, EventArgs e)
        {

        }
    }
}
