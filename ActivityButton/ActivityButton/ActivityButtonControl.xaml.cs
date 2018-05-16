using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace ActivityButton
{
	public partial class ActivityButtonControl : ContentView
	{
		/// <summary>
		/// The command property
		/// </summary>
		public static readonly BindableProperty CommandProperty = BindableProperty.Create(
			propertyName: nameof(Command),
			returnType: typeof(ICommand),
			declaringType: typeof(ActivityButtonControl));

		/// <summary>
		/// The is busy property
		/// </summary>
		public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(
			propertyName: nameof(IsBusy),
			returnType: typeof(bool),
			declaringType: typeof(ActivityButtonControl),
			defaultValue: false);

		/// <summary>
		/// The text property
		/// </summary>
		public static readonly BindableProperty TextProperty = BindableProperty.Create(
			propertyName: nameof(Text),
			returnType: typeof(string),
			declaringType: typeof(ActivityButtonControl),
			defaultValue: string.Empty);

		/// <summary>
		/// Gets or sets the command.
		/// </summary>
		public ICommand Command
		{
			get => (ICommand)GetValue(CommandProperty);
			set => SetValue(CommandProperty, value);
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is busy.
		/// </summary>
		public bool IsBusy
		{
			get => (bool)GetValue(IsBusyProperty);
			set => SetValue(IsBusyProperty, value);
		}

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		public string Text
		{
			get => (string)GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}
		public ActivityButtonControl()
		{
			InitializeComponent();
		}
	}
}
