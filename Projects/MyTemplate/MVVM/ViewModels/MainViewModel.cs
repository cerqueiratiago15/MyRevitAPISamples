using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTemplate
{
    public class MainViewModel : Notifier
    {
        private string _spareText;

        public string SpareText
        {
            get { return _spareText; }
            set
            {
                if (value != _spareText)
                {
                    _spareText = value;
                }
                this.OnPropertyChanged(nameof(SpareText));
            }

        }
        public MainViewModel(string message)
        {
            this.SpareText = message;
        }
    }
}
