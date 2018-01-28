using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ActivityButton
{
    public class ActivityButtonControl : RelativeLayout
    {
        /// <summary>
        /// The command property
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(propertyName:
            nameof(Command),
            returnType: typeof(ICommand),
            declaringType: typeof(ActivityButtonControl));

        /// <summary>
        /// The is busy property
        /// </summary>
        public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(nameof(IsBusy),
            typeof(bool),
            typeof(ActivityButtonControl),
            false);

        /// <summary>
        /// The text property
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(propertyName: nameof(Text),
            returnType: typeof(string),
            declaringType: typeof(ActivityButtonControl),
            defaultValue: string.Empty,
            propertyChanged: OnTextChanged);

        /// <summary>
        /// Gets or sets the activity indicator.
        /// </summary>
        public ActivityIndicator ActivityIndicator { get; set; }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is busy; otherwise, <c>false</c>.
        /// </value>
        public bool IsBusy
        {
            get => (bool)GetValue(IsBusyProperty);
            set => SetValue(IsBusyProperty, value);
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
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
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    HeightRequest = 30,
                    WidthRequest = 30
                };
            Children.Add(new CustomButton
            {
                Text = this.Text,
                Opacity = this.Opacity
            },
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent(p => p.Width),
                Constraint.RelativeToParent(p => p.Height));

            Children.Add(ActivityIndicator,
                Constraint.RelativeToParent(p => p.Width / 4 - 15),
                Constraint.RelativeToParent(p => p.Height / 2 - 15));

            Children.Add(new Image
            {
                HeightRequest = 30,
                WidthRequest = 30,
                HorizontalOptions = LayoutOptions.Start,
                IsVisible = false
            },
                Constraint.RelativeToParent(p => p.Width / 4 - 15),
                Constraint.RelativeToParent(p => p.Height / 2 - 15));

            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = TransitionCommand
            });
        }

        private static ActivityIndicator GetActivityIndicator(ActivityButtonControl control) => control.Children[1] as ActivityIndicator;

        private static Label GetLabel(ActivityButtonControl control) => control.Children[0] as CustomButton;

        //private static void OnIsBusyChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    if (!(bindable is ActivityButtonControl control))
        //    {
        //        return;
        //    }

        //    SetTextBasedOnBusy(control, (bool)newValue, control.Text);
        //}

        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is ActivityButtonControl control))
            {
                return;
            }

            SetTextBasedOnBusy(control, control.IsBusy, newValue as string);
        }

        private static void SetTextBasedOnBusy(ActivityButtonControl control, bool isBusy, string text)
        {
            var activityIndicator = GetActivityIndicator(control);
            var button = GetLabel(control);

            if (activityIndicator == null || button == null)
            {
                return;
            }
            button.Text = isBusy ? text : control.Text;
        }
    }
}