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
		public Action PostValidation;

		public ValidationObject() { }

		public List<IValidationRule<T>> Validations { get; set; } = new List<IValidationRule<T>>();

		public List<string> Errors { get; set; } = new List<string>();
		public string FirstError
		{
			get => _firstError;
			set { _firstError = value; NotifyPropertyChanged(); }
		}
		private string _firstError;

		public T Value
		{
			get => _value;
			set { _value = value; NotifyPropertyChanged(); Validate(); PostValidation?.Invoke(); }
		}
		private T _value;

		public bool IsNotValid => !IsValid;

		public bool IsValid
		{
			get => _isValid;
			set { _isValid = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(IsNotValid)); }
		}
		private bool _isValid = true;

		public bool Validate()
		{
			Errors.Clear();

			IEnumerable<string> errors = Validations.Where(l => !l.Check(Value)).Select(l => l.ValidationMessage);
			Errors = errors.ToList();

			IsValid = !Errors.Any();

			if (!IsValid)
			{
				FirstError = Errors.FirstOrDefault();
				NotifyPropertyChanged(nameof(FirstError));
			}

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
