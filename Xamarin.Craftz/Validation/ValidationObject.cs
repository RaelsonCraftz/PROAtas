using Craftz.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Xamarin.Craftz.Validation
{
    public class ValidationObject<T> : INotifyPropertyChanged
    {
		public List<IValidationRule<T>> Validations { get; set; } = new List<IValidationRule<T>>();

		public List<string> Errors { get; set; } = new List<string>();

		public T Value
		{
			get => _value;
			set { _value = value; NotifyPropertyChanged(); }
		}
		private T _value;

		public bool IsValid
		{
			get => _isValid;
			set { _isValid = value; NotifyPropertyChanged(); }
		}
		private bool _isValid = true;

		public bool Validate()
		{
			Errors.Clear();

			IEnumerable<string> errors = Validations.Where(l => !l.Check(Value)).Select(l => l.ValidationMessage);
			Errors = errors.ToList();

			IsValid = !Errors.Any();

			return this.IsValid;
		}

		#region INotifyPropertyChanged Implementation
		protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
	}
}
