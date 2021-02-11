using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
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

using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Win32;
using System.IO;

namespace _8bitcam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window


    {

        SerialPort serialPort1;
        Bitmap resized;
        Bitmap resized__;
        public MainWindow()
        {
            InitializeComponent();
         
            serialPort1 = new SerialPort();
            for (int a = 0; a < 18; a++) combox1.Items.Add("COM" + a);
            for (int a = 0; a < 18; a++) combox2.Items.Add(a * 1200);


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(combox1.SelectedItem);
            var baudrate =(int)combox2.SelectedItem;
            var port = (string)combox1.SelectedItem;
            serialPort1.BaudRate = baudrate;
            serialPort1.PortName = port;
            serialPort1.Open();

            Thread core = new Thread(Thread);
            core.Start();
        }


        public void Thread()
        {
            while (true)
            {
                var input = serialPort1.ReadLine().Split(',');
                if (input.Length > 8)
                {
                    Bitmap flag = new Bitmap(10, 1);


                    for (int f = 0; f < 7; f++)
                    {
                        if (input[f] == "1")
                        {
                            flag.SetPixel(f, 0, System.Drawing.Color.White);

                        }
                        if (input[f] == "0")
                        {
                            flag.SetPixel(f, 0, System.Drawing.Color.Black);
                        }
                    }
                   resized = new Bitmap(flag, new System.Drawing.Size(flag.Width * 1, flag.Height * 1));
                    resized__ = new Bitmap(flag, new System.Drawing.Size(flag.Width * 50, flag.Height * 50));
                    this.Dispatcher.Invoke((Action)(() => { 
                   picturebox1.Source = CreateBitmapSourceFromGdiBitmap(resized);
                        label.Content = "Connected";
                    }));
                    resized.Dispose();
                    flag.Dispose();
                                 }
            }

        }
        private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            return new Bitmap(bitmapImage.StreamSource);
        }
            public static BitmapSource CreateBitmapSourceFromGdiBitmap(Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            var rect = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);

            var bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            try
            {
                var size = (rect.Width * rect.Height) * 4;

                return BitmapSource.Create(
                    bitmap.Width,
                    bitmap.Height,
                    bitmap.HorizontalResolution,
                    bitmap.VerticalResolution,
                    PixelFormats.Bgra32,
                    null,
                    bitmapData.Scan0,
                    size,
                    bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            SaveFileDialog main = new SaveFileDialog();
           
            main.ShowDialog();
            resized__.Save(main.FileName);



        }

        private void github(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/alex5250/8BitCam");
        }
    }
}
