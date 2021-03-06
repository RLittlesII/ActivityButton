﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ActivityButton
{
	public class ActivityButtonControl : AbsoluteLayout
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
		/// Gets or sets the activity indicator.
		/// </summary>
		public ActivityIndicator ActivityIndicator { get; set; }

		/// <summary>
		/// Gets or sets the button.
		/// </summary>
		public CustomButton Button { get; set; }

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

		private ICommand TransitionCommand => new Command(() =>
		{
			if (Command != null && Command.CanExecute(null))
			{
				Command?.Execute(null);
			}
		});

		/// <summary>
		/// Initializes a new instance of the <see cref="ActivityButtonControl"/> class.
		/// </summary>
		public ActivityButtonControl()
		{
			ActivityIndicator =
				new ActivityIndicator
				{
					Color = Color.White,
					HeightRequest = 30,
					WidthRequest = 30,
					IsVisible = true,
					IsRunning = true
				};

			Button = new CustomButton
			{
				Text = "Login",
				Opacity = this.Opacity,
				HeightRequest = 48,
				WidthRequest = 350,
				InputTransparent = true
			};

			Children.Add(ActivityIndicator, new Rectangle(.5, .5, 1, 1), AbsoluteLayoutFlags.PositionProportional);

			Children.Add(Button, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);

			GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = TransitionCommand
			});
		}
	}
}