using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MedManagementLibrary
{
    public class Treatment
    {
        private bool _isSelected;
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if(_isSelected != value)
                {
                    _isSelected = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Treatment()
        {
            Name = string.Empty;
            Price = 0m;
            IsSelected = false;
        }
    }
}

