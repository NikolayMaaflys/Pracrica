using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;

namespace game
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int Time;
        Ellipse ball;

        double R = 10;
        double x, y;
        double V = 3;
        double vx, vy;

        Rectangle PlateRed;
        Rectangle PlateBlue;
        double H = 100;
        double PRx;
        double PBx;
        double Pv = 25;

        DispatcherTimer Timer;



        public MainWindow()
        {
            InitializeComponent();
            Restart();
            
            ball = new Ellipse();
            ball.Fill = Brushes.White;
            ball.Width = 2 * R;
            ball.Height = 2 * R;
            ball.Margin = new Thickness(x, y, 0, 0);
            GamePlace.Children.Add(ball);


            PlateRed = new Rectangle();
            PlateRed.Fill = Brushes.Red;
            PlateRed.Width = H;
            PlateRed.Height = 5;
            PRx = GamePlace.Width / 2 - H / 2;
            PlateRed.Margin = new Thickness(PRx, 5, 0, 0);
            GamePlace.Children.Add(PlateRed);

            PlateBlue = new Rectangle();
            PlateBlue.Fill = Brushes.Blue;
            PlateBlue.Width = H;
            PlateBlue.Height = 5;
            PBx = GamePlace.Width / 2;
            PlateBlue.Margin = new Thickness(PBx, GamePlace.Height - 10, 0, 0);
            GamePlace.Children.Add(PlateBlue);

            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(onTick);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            Timer.Start();
        }

        void Restart()
        {
            x = GamePlace.Width / 2 - R;
            y = GamePlace.Height / 2 - R;

            Random rnd = new Random();

            double alpha = rnd.NextDouble() * Math.PI / 2 + Math.PI / 4;

            vx = V * Math.Cos(alpha);
            vy = V * Math.Sin(alpha);

            PRx = GamePlace.Width / 2 - H / 2;
            PBx= GamePlace.Width / 2 - H/2;

            Time = 0;
        }

        void onTick(object sender, EventArgs e)
        {
            Time++;

            if ((x < 0) || (x > GamePlace.Width - 2 * R))
            {
                vx = -vx;
            }

            

            if (y > 368)
            {
                double c = x + R;

                
                if ((c >= PBx) && (c <= PBx + H))
                {
                    vx *= 1.02;
                    vy *= 1.02;
                    vy = -vy;
                }
                else
                {
                    MessageBox.Show("Победа красных");
                    Restart();
                    PlateBlue.Margin = new Thickness(PBx, GamePlace.Height - 10, 0, 0);
                }
            }


            
            if (y < 16)
            {
                double cr = x + R;

                
                if ((cr >= PRx) && (cr <= PRx + H))
                {
                    vx *= 1.02;
                    vy *= 1.02;
                    vy = -vy;

                }
                else
                {
                    MessageBox.Show("Победа синих"); 
                    Restart(); 
                    PlateRed.Margin = new Thickness(PRx, 5, 0, 0);
                }
            }

            x += vx;
            y += vy;

            ball.Margin = new Thickness(x, y, 0, 0);

            TimePlay.Text = (Time / 100).ToString();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                PRx -= Pv;
            }

            if (e.Key == Key.Right)
            {
                PRx += Pv;
            }

            if (PRx < 0)
            {
                PRx = 0; 
            }

            if (PRx > GamePlace.Width - H)
            {
                PRx = GamePlace.Width - H;
            }

            PlateRed.Margin = new Thickness(PRx, 5, 0, 0);
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                PBx -= Pv;
            }

            if (e.Key == Key.D)
            {
                PBx += Pv;
            }

            if (PBx < 0)
            {
                PBx = 0; 
            }

            if (PBx > GamePlace.Width - H)
            {
                PBx = GamePlace.Width - H; 
            }

            PlateBlue.Margin = new Thickness(PBx, GamePlace.Height - 10, 0, 0);
        }
    }
}
