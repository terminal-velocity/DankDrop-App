using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace DankDrop
{
    public partial class SubmitMemePage : ContentPage
    {
        public SubmitMemePage(DropPoint drop)
        {
            _id = drop.ID;
            InitializeComponent();
        }

        private readonly string _id;
    }
}
