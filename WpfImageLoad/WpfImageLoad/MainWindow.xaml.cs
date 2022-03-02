using Microsoft.Win32;
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

namespace WpfImageLoad
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// 1.폴더 버튼을 눌러 이미지를 선택합니다.
    /// 2. 불러온 이미지 파일을 scroll viewer에 출력합니다.
    /// 3. 이미지 리스트 내에서 파일을 선택하면 stack panel에 해당 이미지가 표시됩니다.
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> imgList = new List<string>();
        List<Image> imgCtrolList = new List<Image>();
        public MainWindow()
        {
            InitializeComponent();
        }

        //Folder Button 클릭 이벤트
        private void Button_Folder_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                string fullPath = openFileDialog.FileName;
                string fileName = openFileDialog.SafeFileName;
                string path = fullPath.Replace(fileName, "");

                uiTxt_Folder.Text = path;

                string[] files = Directory.GetFiles(path);
                //jpg, JPG, png, PNG 이미지 리스트 불러오기
                imgList = files.Where(x => x.IndexOf(".jpg", StringComparison.OrdinalIgnoreCase) >= 0 || x.IndexOf(".png", StringComparison.OrdinalIgnoreCase) >= 0).Select(x => x).ToList();

            }
            CreateImage(imgList);
        }

        //이미지 클릭 이벤트
        private void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            
            Image image = sender as Image;

            try
            {
                //Image img = null;
                CreateBitmap(image, image.Source.ToString());
                

                if(((image.Parent) as WrapPanel) != null)
                {
                    ((image.Parent) as WrapPanel).Children.Clear();
                    image.Stretch = Stretch.UniformToFill;
                    uiCanvas_Image.Children.Clear();
                    uiCanvas_Image.Children.Add(image);

                    CreateImage(imgList);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("잘못 눌렀습니다.");
            }


        }
        
        //이미지 생성
        private void CreateImage(List<string> imgList)
        {
            for (int i = 0; i < imgList.Count; i++)
            {
                Image image = new Image();
                CreateBitmap(image, imgList[i]);
                imgCtrolList.Add(image);
                image.MouseDown += ImageButton_Click;
                uiWrap_Image.Children.Add(image);
            }
        }

        //비트맵 이미지 생성
        private void CreateBitmap(Image image, string imageList)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.CacheOption = BitmapCacheOption.OnDemand;
            img.CreateOptions = BitmapCreateOptions.DelayCreation;
            img.DecodePixelWidth = 300;
            img.UriSource = new Uri(imageList.ToString());
            img.EndInit();
            image.Source = img;
        }
    }
}
