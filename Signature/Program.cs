
using System.Data.Common;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace boot_sector
{
    class BS
    {
        static byte[] Readsector(string path, byte pos)
        {
            byte[] data = new byte[512];
            BinaryReader f;
            try
            {
                f = new BinaryReader(new FileStream(path, FileMode.Open));
                f.BaseStream.Position = pos * 512;
            }
            catch
            {
                Console.Error.WriteLine("lỗi đọc");
                return data;
            }
            try
            {
                for (int i = 0; i < 512; i++)
                {
                    data[i] = f.ReadByte();
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
            }
            f.Close();
            return data;
        }
        static void print(byte [] data)
        {
            
            Console.WriteLine("Thông tin trên boot sector của đĩa mềm");
            string b = "";
            for (byte i = 3; i < 11; i++)
            {
                b += ((char)data[i]).ToString();
            }
            Console.WriteLine($"Tên nhà sản xuát: {b}");
            Console.WriteLine($"Số byte trên 1 sector: {data[11] + (data[12] << 8)}");
            Console.WriteLine($"Số sector trên 1 cluster: {data[13]}");
            Console.WriteLine($"Số sector dự trữ: {data[14] + (data[15] << 8)}");
            Console.WriteLine($"Số Bản FAT: {data[16]}");
            Console.WriteLine($"Số đề mục đi vào root directory: {data[17] + (data[18] << 8)}");
            Console.WriteLine($"Tổng số sector trên filesystem: {data[19] + (data[20] << 8)}");
            string a = "";
            if (data[21] == 0xF8)
            {
                a = "Ổ cứng";
            }
            else { a = "Đĩa mềm"; }
            Console.WriteLine($"Mã nhận điện đĩa: {a}");
            Console.WriteLine($"Số sector trên FAT: {data[22] + (data[23] << 8)}");
            Console.WriteLine($"Số sector trên track: {data[24] + (data[25] << 8)}");
            Console.WriteLine($"Số đầu từ : {data[26] + (data[27] << 8)}");
            Console.WriteLine($"Số sector ẩn: {data[28] + (data[29] << 8)}");
            string m = "";
            if (data[36] == 0x80)
            {
                m = "Ổ cứng";
            }
            else { m = "Đĩa mềm"; }
            Console.WriteLine($"Số hiệu đĩa: {m}");
            Console.WriteLine($"Dự trữ: {data[37]}");
            Console.WriteLine($"Chữ ký boot sector: {data[38].ToString("x") + "h"}");
            Console.WriteLine($"Số serial: {data[42].ToString("X") + data[41].ToString("X") + "-" + data[40].ToString("X") + data[39].ToString("X")}");
            string c = "";
            for (byte i = 43; i < 53; i++)
            {
                c += ((char)data[i]).ToString();
            }
            Console.WriteLine($"Nhãn đia: {c}");
            string d = "";
            for (byte i = 54; i < 62; i++)
            {
                d += ((char)data[i]).ToString();
            }
            Console.WriteLine($"Hệ thống file sửa dụng trên đĩa :{d}");
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            string path1 = @"D:\C#\hệ điều hành\Disk Images\floppy.img"; // mềm 0 cứng 63
            byte pos1 = 0;
            string path2 = @"D:\C#\hệ điều hành\Disk Images\FAT16.vhd";
            byte pos2 = 63;
            while (true)
            {
                Console.Write("chọn 0 để thoát 1 là ổ cứng, 2 là đĩa mềm: ");
                byte[] data = null;
                try
                {
                    int n = int.Parse(Console.ReadLine());
                    Console.Clear();
                    switch (n)
                    {
                        case 1:
                            if (data == null)
                            {
                                print(Readsector(path1, pos1));
                                Console.Clear();
                            }
                            break;
                        case 2:
                            if (data == null)
                            {
                                print(Readsector(path2, pos2));
                                Console.Clear();
                            }
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Chỉ có nhận 0, 1 và 2");
                            Console.ReadKey();
                            break;
                    }
                    if (n == 0) { Environment.Exit(0); }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "Chỉ nhận số không nhận ký tự");
                }
            }
            
        }
    }
}