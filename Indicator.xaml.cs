using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp17
{
    /// <summary>
    /// Логика взаимодействия для Indicator.xaml
    /// </summary>
    public partial class Indicator : UserControl
    {
        public static readonly RoutedEvent ColorChangedEvent;
        public ObservableCollection<Indicator> colorslist;

        public static DependencyProperty ColorProperty;
        public static DependencyProperty RedProperty;
        public static DependencyProperty GreenProperty;
        public static DependencyProperty BlueProperty;
        
        

        public Indicator()
        {
            InitializeComponent();
            colorslist = new ObservableCollection<Indicator>();
            listType.ItemsSource = colorslist;

        }
        public class Colorslist
        {
            public string Name { get; set; }
        }
     

        static Indicator()
        {
            

            ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(Indicator),
                new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorChanged)));
            RedProperty = DependencyProperty.Register("Red", typeof(byte), typeof(Indicator),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            GreenProperty = DependencyProperty.Register("Green", typeof(byte), typeof(Indicator),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            BlueProperty = DependencyProperty.Register("Blue", typeof(byte), typeof(Indicator),
                 new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));

            ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChanged", RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<Color>), typeof(Indicator));
        }

        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        
        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }
        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }
        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }

        public object ColorName { get; private set; }

        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }

        private static void OnColorRGBChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            Indicator colorPicker = (Indicator)sender;
            Color color = colorPicker.Color;
            if (e.Property == RedProperty)
                color.R = (byte)e.NewValue;
            else if (e.Property == GreenProperty)
                color.G = (byte)e.NewValue;
            else if (e.Property == BlueProperty)
                color.B = (byte)e.NewValue;

            colorPicker.Color = color;
        }

        private static void OnColorChanged(DependencyObject sender,
        DependencyPropertyChangedEventArgs e)
        {
            Color newColor = (Color)e.NewValue;
            Indicator colorpicker = (Indicator)sender;
            colorpicker.Red = newColor.R;
            colorpicker.Green = newColor.G;
            colorpicker.Blue = newColor.B;
        }

        //private void colorPicker_ColorChanged(DependencyObject sender,
        //DependencyPropertyChangedEventArgs e)
        //{
        //    Color newColor = (Color)e.NewValue;
        //    Indicator colorpicker = (Indicator)sender;
        //    txb1.Text = e.NewValue.ToString();
        //}
        
        private void txb2_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txb2.Text == "")
                {
                    Color color1 = (Color)ColorConverter.ConvertFromString(txb1.Text);
                    colorPicker.Color = color1;
                }
                else if (txb2.Text != "")
                {
                    Color color = (Color)ColorConverter.ConvertFromString(txb2.Text);
                    colorPicker.Color = color;
                }
            }
            catch
            {

            }

        }

        private void resetColor_Click(object sender, RoutedEventArgs e)
        {
            Color color = Colors.Black;
            colorPicker.Color = color;
        }

        private void rememberColor_Click(object sender, RoutedEventArgs e)
        {
            string colorcode = (txb1.Text).Replace("#","");

            colorslist.Add(new Indicator()
            {
                Name = colorcode,
                ColorName = txb1.Text
            });
        }

        private void removeList_Click(object sender, RoutedEventArgs e)
        {
            colorslist.Clear();
        }

        private void removeListAt_Click(object sender, RoutedEventArgs e)
        {
            colorslist.RemoveAt(colorslist.Count - 1);
        }
    }
}
