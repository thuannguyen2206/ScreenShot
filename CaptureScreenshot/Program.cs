using System;
using System.Text;
using System.Drawing;
using System.Threading;
using System.IO;

namespace CaptureScreenshot
{
    class Program
    {
        private static void KeyEnterFunc()
        {
            try
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Environment.Exit(0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
            } 
        }
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Console.WriteLine("\nKhởi động và chụp hình màn hình...\n");
            string path = "File\\test.txt";
            string screenshot = "ScreenShot\\";
            string[] lines = new string[100];
            if (File.Exists(path))
            {
                lines = File.ReadAllLines(path);
            }
            // chỉ định size của ảnh
            //Console.WriteLine("Nhập vào kích thước của ảnh (chiều dài x chiều cao): ");
            //int x = int.Parse(Console.ReadLine());
            //int y = int.Parse(Console.ReadLine());
            int x = int.Parse(lines[0].Trim().ToString());
            int y = int.Parse(lines[1].Trim().ToString());
            Bitmap CSImage = new Bitmap(x, y);
            // Chỉ định nơi lưu file
            Console.WriteLine("Size và Nơi lưu được đọc từ file có đường dẫn {0}: ", screenshot);

            while (true)
            {
                string fileName = screenshot + "Screenshot_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
                Console.WriteLine("Ảnh đã lưu tại : " + fileName);
                // Khởi tạo 1 đối tượng System.Drawing.Size từ kích thước chỉ định 
                Size s = new Size(CSImage.Width, CSImage.Height);
                // tạo 1 đối tượng Graphic từ Image được chỉ định
                Graphics memoryGraphics = Graphics.FromImage(CSImage);
                // Thực hiện chuyển khối bit dữ liệu màu với size chỉ định từ màn hình sang đối tượng Graphics.
                memoryGraphics.CopyFromScreen(0, 0, 0, 0, s);
                // Lưu file với đường dẫn chỉ định
                CSImage.Save(fileName);
                //Ngủ sau 3 giây
                Thread.Sleep(3000);
                //Tạo 1 thread khác lắng nghe input, nếu nhập enter thì thoát chương trình
                var thread = new Thread(KeyEnterFunc);
                thread.Start();
            }
        }
    }
}
