using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace dieuxe.CustomControl
{
    public class TitleWithIcon : Grid
    {
        readonly Image _Icon;
        readonly Label _Title;

        public static BindableProperty IconProperty = BindableProperty.Create(
            propertyName: "Icon",
            returnType: typeof(string),
            declaringType: typeof(TitleWithIcon),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: ImageSourcePropertyChanged);
        public string Icon
        {
            get { return this.GetType().GetProperty(IconProperty.PropertyName).GetValue(this, null).ToString(); }
            set { this.GetType().GetProperty(IconProperty.PropertyName).SetValue(this, value, null); }
        }

        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as TitleWithIcon;
            control._Icon.Source = ImageSource.FromFile(newValue.ToString());
        }

        public static BindableProperty TitleProperty = BindableProperty.Create(
            propertyName: "Title",
            returnType: typeof(string),
            declaringType: typeof(TitleWithIcon),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: TitlePropertyChanged);

        public string Title
        {
            get { return base.GetValue(TitleProperty).ToString(); }
            set { base.SetValue(TitleProperty, value); }
        }
        private static void TitlePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (TitleWithIcon)bindable;
            control._Title.Text = newValue.ToString();
        }

        private static BindableProperty FontSizeProperty = BindableProperty.Create(
            propertyName: "FontSize",
            returnType: typeof(double),
            declaringType: typeof(TitleWithIcon),
            defaultValue: 18.0,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: FontSizePropertyChanged);

        public double FontSize
        {
            get { return (double)base.GetValue(FontSizeProperty); }
            set { base.SetValue(FontSizeProperty, value); }
        }
        private static void FontSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (TitleWithIcon)bindable;
            control._Title.FontSize = (double)newValue;
        }

        private static BindableProperty IconHeightRequestProperty = BindableProperty.Create(
            propertyName: "IconHeightRequest",
            returnType: typeof(string),
            declaringType: typeof(TitleWithIcon),
            //defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: IconHeightRequestPropertyChanged);

        public string IconHeightRequest
        {
            get { return (string)base.GetValue(IconHeightRequestProperty); }
            set { base.SetValue(IconHeightRequestProperty, value); }
        }
        private static void IconHeightRequestPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (TitleWithIcon)bindable;
            control._Icon.HeightRequest = Convert.ToDouble( newValue);
        }

        private static BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: "TextColor",
            returnType: typeof(Color),
            declaringType: typeof(TitleWithIcon),
            defaultValue: Color.Black,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: TextColorPropertyChanged);

        public Color TextColor
        {
            get { return (Color)base.GetValue(TextColorProperty); }
            set { base.SetValue(TextColorProperty, value); }
        }
        private static void TextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (TitleWithIcon)bindable;
            control._Title.TextColor = (Color)newValue;
        }

        public TitleWithIcon()
        {
            _Icon = new Image();
            _Title = new Label();

            RowDefinitions.Add(new RowDefinition());
            RowDefinitions.Add(new RowDefinition());
            ColumnDefinitions.Add(new ColumnDefinition());
            RowDefinitions[0].Height = GridLength.Auto;
            RowDefinitions[1].Height = GridLength.Auto;
            ColumnDefinitions[0].Width = GridLength.Auto;

            RowSpacing = 0;
            ColumnSpacing = 0;

            Children.Add(_Icon);
            Children.Add(_Title);
            Grid.SetRow(_Title, 1);

            _Title.FontSize = FontSize;
            _Title.HorizontalOptions = LayoutOptions.Center;
            _Title.VerticalOptions = LayoutOptions.Center;
            _Icon.HorizontalOptions = LayoutOptions.Center;
            _Icon.VerticalOptions = LayoutOptions.Center;
        }
    }
}