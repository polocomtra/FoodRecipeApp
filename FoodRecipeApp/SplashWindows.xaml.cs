using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace FoodRecipeApp
{
    /// <summary>
    /// Interaction logic for SplashWindows.xaml
    /// </summary>
    public partial class SplashWindows : Window
    {
        public Random _rng = new Random();
        public SplashWindows()
        {
            InitializeComponent();
        }

        System.Timers.Timer timer;
        int count = 0;
        int target = 5;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var value = ConfigurationManager.AppSettings["ShowSplashScreen"];
            var showSplash = bool.Parse(value);
            Debug.WriteLine(value);

            if (showSplash == false)
            {
                CloseSplashWindow();
            }
            else
            {
                timer = new System.Timers.Timer();
                timer.Elapsed += Timer_Elapsed;
                timer.Interval = 1000;

                int index = _rng.Next(InfomationList.Length);
                var infomation = InfomationList[index];
                FoodInformation.Text = infomation;

                SplashOKBtn.Content = $"OK ({target})";
                timer.Start();
            }
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            count++;
            Dispatcher.Invoke(() =>
            {
                SplashOKBtn.Content = $"OK ({target - count})";
            });
            if (count == target)
            {
                timer.Stop();

                Dispatcher.Invoke(() =>
                {
                    CloseSplashWindow();
                });
            }
        }
      
        private void DontShowThisAgain(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
            config.Save(ConfigurationSaveMode.Minimal);
        }

        private void ShowThisAgain(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(
                ConfigurationUserLevel.None);
            config.AppSettings.Settings["ShowSplashScreen"].Value = "true";
            config.Save(ConfigurationSaveMode.Minimal);
        }

        string[] InfomationList =
        {
            "Màu sắc của ớt thực sự không nói lên mùi vị hay mức độ của nó. Bạn chỉ nên quan tâm đến kích thước của nó bởi quả ớt bé thường sẽ cay hơn quả ớt to.",
            "Có vẻ hơi điên rồ một tí nhưng hầu hết muỗi đều bị thu hút bởi những người vừa mới ăn chuối.",
            "Quả bơ thực sự là loại quả có “tính cách” không thể đoán trước được.",
            "Đừng bao giờ cho chim ăn quả bơ, thậm chí nếu nó có thích bơ đi nữa thì quả bơ cũng là “chất độc” đối với chim.",
            "Trong mỗi bình nước, số lượng nước luôn luôn khác nhau. Bạn sẽ không bao giờ tìm ra được 2 chai nước nào có số nước chính xác bằng nhau.",
            "Màu sắc trong thức ăn khiến trẻ phấn khích, đặc biệt là màu đỏ hoặc vàng.",
            "Bạn không nên uống trà đã pha quá lâu. Người Nhật có câu nói: “Trà của ngày hôm qua còn độc hơn cả rắn”. Trà lạnh thường tác động đến thành dạ dày, do đó người Trung Quốc cũng coi trà lạnh như là thuốc độc.",
            "Mật ong là thứ bất tử. Chai mật ong lâu đời nhất được nhà khảo cổ học Howard Carter tìm thấy vào năm 1922 trong lăng mộ pharaoh Tutankhamun cổ đại. Chai mật ong này đã được các nhà sinh học kiểm tra và nhận thấy vẫn còn sử dụng được.",
            "Phomai nên ăn càng gần ngày sản xuất càng tốt, nhưng không bao giờ nên ăn sau ngày quá hạn.",
            "Nếu quả trứng nổi lên bề mặt của nước thì không còn ăn được nữa.",
            "Nếu bạn ăn một miếng nhỏ socola đen trước bữa ăn thì bạn sẽ bị tăng hàm lượng đường trong máu. Thậm chí nếu lúc đó bạn quá đói thì socola cũng sẽ khiến bạn không ăn được nhiều, cảm giác bị ngấy.",
            "Thức ăn luôn tạo cảm giác ngon miệng hơn vào ban đêm. Tất cả đều như vậy."
        };

        private void CloseSplashWindow()
        {
            var screen = new MainWindow();
            this.Close();
            screen.Show();
        }

        private void CloseSplashWindowBtn(object sender, RoutedEventArgs e)
        {
            CloseSplashWindow();
        }
    }
}
