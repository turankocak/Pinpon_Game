using System;
using System.Collections.Generic;
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

namespace Pinpon_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Game : Window
    {
        private const int BallSize = 10;
        private const int BallSpeed = 2;
        private Ellipse Ball;
        private Point BallPosition; // Topun Baslangıç Pozisyonu 
        private Point BallVelocity; //Topun Güncel Hızı
        Rectangle p1 = new Rectangle();
        Rectangle p2 = new Rectangle();
        Rect pinpon;
        double x, y; //Şu anki pozisyon 
        int formW = 250;  // Form 
        int formH = 350;

        // 1.Oyuncu p1 oyuncusu
        int p1W = 60;
        int p1H = 8;

        //2.Oyuncu  p2 oyuncusu
        int p2W = 60;
        int p2H = 8;

        bool z=false; 
        public Game()
        {
            InitializeComponent();

            Canvas1.Children.Add(p1);
            setP1();
            Canvas1.Children.Add(p2);
            setP2();
            Ball = new Ellipse{Width = BallSize, Height = BallSize, Fill = Brushes.Black};
            Canvas1.Children.Add(Ball);

            //Topun  başlangıç ​​konumunu ve hızı
            BallPosition = new Point(Canvas1.ActualWidth / 10 - BallSize / 70,
                                       Canvas1.ActualHeight / 10- BallSize / 70);
            BallVelocity = new Point(2, 2);

            // Enter Tusuna Basınca Oyunu Baslat
            Canvas1.KeyDown += Window_KeyDown;

            // Oyun döngüsünü başlat
            CompositionTarget.Rendering += GameLoop;
            
        }
        private void setP1() 
        {
            p1.Tag = "pinpon";
            p1.Width = p1W;
            p1.Height = p1H;
            p1.Stroke = Brushes.Black;
            p1.Fill = Brushes.Silver;
            Canvas.SetLeft(p1, formW / 2);
            Canvas.SetTop(p1, 334);
            
        }
        private void setP2()
        {
            p2.Tag = "pinpon";
            p2.Width = p2W;
            p2.Height = p2H;
            p2.Stroke = Brushes.Black;
            p2.Fill = Brushes.Silver;
            Canvas.SetLeft(p2, formW / 2);
            Canvas.SetTop(p2, 10);
           // MessageBox.Show("Oyuna Baslamak için Enter Basınız");
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    x = Canvas.GetLeft(p1) - 10;
                    if (x < 0) x = 0;
                    Canvas.SetLeft(p1, x);
                    break;
                case Key.Right:
                    x = Canvas.GetLeft(p1) + 10;
                    if (x > formW - p1W) x = formW - p1W;
                    Canvas.SetLeft(p1, x);
                    break;

                case Key.Enter:
                    BallVelocity.X = -BallSpeed;
                    BallVelocity.Y = BallSpeed;
                    break;
                    case Key.A:
                    x = Canvas.GetLeft(p2) - 10;
                    if (x < 0) x = 0;
                    Canvas.SetLeft(p2, x);
                    break;
                case Key.D:
                    x = Canvas.GetLeft(p2) + 10;
                    if (x > formW - p2W) x = formW - p2W;
                    Canvas.SetLeft(p2, x);
                    break;
            }
        }
        private void GameLoop(object sender, EventArgs e)
        {
            // Top pozisyonunu hıza göre güncelleyin
            BallPosition.X += BallVelocity.X;
            BallPosition.Y += BallVelocity.Y;

            // Tuvalin kenarlarıyla çarpışma olup olmadığını kontrolü
            if (BallPosition.X < 0 || BallPosition.X + BallSize > Canvas1.ActualWidth)
            {
                BallVelocity.X = -BallVelocity.X; 
                
            }
            // Tuvalin assagısına carpinca oyunu bitir 
            if (BallPosition.Y < 0 || BallPosition.Y + BallSize > Canvas1.ActualHeight)
            {
                //BallVelocity.Y = -BallVelocity.Y;
                  if(z)
                  {
                      Oyun_Bitti();
                  }   
            }
            z=true;
            // Topun Gidiş Yönleri
                        Canvas.SetLeft(Ball, BallPosition.X);
                        Canvas.SetTop(Ball, BallPosition.Y);

            //Pinpon Carpma Kontrolü
            pinpon = new Rect(Canvas.GetLeft(Ball), Canvas.GetTop(Ball), Ball.Width, Ball.Height);
            foreach (var x in Canvas1.Children.OfType<Rectangle>())
            {
                Rect hit = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                if ((string)x.Tag == "pinpon")
                {
                    if (pinpon.IntersectsWith(hit)) //pinponla kesiştigi zaman carp 
                    {
                        BallVelocity.Y = -BallVelocity.Y;  
                    }
                }
             }
         }
         private void Oyun_Bitti()
         {
             MessageBox.Show("---GAME OVER---");
             this.Close();
         } 
    }
}
