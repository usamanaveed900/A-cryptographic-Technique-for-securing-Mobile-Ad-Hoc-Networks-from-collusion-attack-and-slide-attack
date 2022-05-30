using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace MZ_Technique
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string plainText = null;
        string loadFileName = null;
        string b = Convert.ToString(Math.Pow(10, 2));
        string c = Convert.ToString(Math.Pow(10, 5));

        public static void removeFileContent(string name)
        {
            System.IO.File.WriteAllText(name + @"Encrypted.txt", string.Empty);
        }

        public static void write(string data, string name)
        {
            removeFileContent(name);
            string path = @"";
            string Filename = name + "Encrypted.txt";
            var newpath = path + Filename;
            using (StreamWriter sw = System.IO.File.AppendText(newpath))
            {
                sw.WriteLine(data);
            }

        }

        public static string convertToASCII(string unicodeString)
        {
            string ASCIIcode = null;
            var utf8bytes = Encoding.UTF8.GetBytes(unicodeString);
            var win1252Bytes = Encoding.Convert(Encoding.UTF8, Encoding.ASCII, utf8bytes);
            foreach (var item in win1252Bytes)
            {
                ASCIIcode += item + " ";
            }
            return ASCIIcode;
        }

        public static string ASCIIToBinary(string str)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in str.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        public static int gcd(int a, int b)
        {
            if (a < b)
            {
                return gcd(b, a);
            }
            else if (a % b == 0)
            {
                return b;
            }
            else
            {
                return gcd(b, a % b);
            }
        }

        public static int genKey(int q)
        {
            Random rand1 = new Random();
            string b = Convert.ToString(Math.Pow(10, 2));
            int key = int.Parse(Convert.ToString(rand1.Next(int.Parse(b), q)));

            while (gcd(q, key) != 1)
            {
                b = Convert.ToString(Math.Pow(10, 2));
                key = int.Parse(Convert.ToString(rand1.Next(int.Parse(b), q)));
            }
            return key;
        }

        public static string S_box(string temp)
        {
            string[,] Box = new string[17, 17] 
            { {"" ,"1000", "1001", "1010", "1011","1100","1101","1110","1111","0000","0001","0010","0011","0100","0101","0110","0111" },
              {"0000" ,"A1", "B3", "0A", "C2","D3","DS","E3","J4","CM","O2","PP","LO","IR","HQ","MZ","QH" },
              {"0001" ,"Q1", "B2", "D5", "0B","D2","8B","Q4","QS","K4","HR","P2","AR","DO","JS","RI","HO" },
              {"0010" ,"AB", "RJ", "C3", "E6","OC","E2","PB","F3","TO","L4","CO","Q2","ZM","SJ","UR","QY" },
              {"0011","BC", "ZA", "SK", "D4","F7","0D","F2","NP","G3","MD","M4","KI","TK","VE","EV","RU" },
              {"0100" ,"DE", "XB", "JL", "TL","E5","G8","OE","G2","GM","H3","DP","UL","7B","R2","3T","OY" },
              {"0101" ,"FG", "YC", "KI", "E4","UM","F6","H9","OF","H2","MV","VM","5E","N4","6C","S2","LO" },
              {"0110" ,"HI", "WD", "L2", "F4","9V","VN","G7","IA","0G","WN","I2","I3","7Z","O4","YO","T2" },
              {"0111" ,"JK", "UE", "M3", "7O","G4","8X","WO","H8","XO","0H","N3","J2","J3","5N","P4","CV" },
              {"1000" ,"LM", "SF", "N4", "4L","6B","H4","XP","YP","I9","JB","0I","O3","K2","K3","4W","Q4" },
              {"1001" ,"NO", "QG", "O5", "4A","ST","3D","ZQ","YQ","OT","JA","KC","0J","P3","L2","L3","TV" },
              {"1010" ,"PQ", "OH", "P6", "I4","QE","IR","0U","V3","ZR","OS","KB","LD","0K","Q3","M2","MP" },
              {"1011" ,"RS", "MI", "Q7", "2M","2S","OV","U2","W3","X3","IS","OR","LC","ME","0L","S3","N2" },
              {"1100" ,"TU", "KJ", "R8", "GT","0W","U4","FP","V2","Y3","Z3","2T","OQ","ON","NF","0M","R3" },
              {"1101" ,"VW", "IK", "4U", "0X","T4","WZ","LT","PT","W2","XA","PO","3U","OP","NE","OG","0N" },
              {"1110" ,"XY", "5V", "0Y", "S4","ZB","YX","V4","W4","BS","X2","Y4","T3","4V","0O","FT","PH" },
              {"1111" ,"6W", "0Z", "R4", "9L","Z4","ZW","MS","RP","9C","CS","Y2","U3","Z2","5W","6X","PG" }
            };
            string first = temp.Substring(0, 4);
            string second = temp.Substring(4, 4);
            int firstIndex = 0;
            int secondIndex = 0;
            for (int i = 1; i < 17;i++ )
            {
                if(Box[i,0]==first)
                {
                    firstIndex = i;
                }
            }
            for (int j = 1; j < 17;j++ )
            {
                if(Box[0,j]==second)
                {
                    secondIndex = j;
                }
            }
            return (temp = Box[firstIndex, secondIndex].ToString());
        }

        public static string DNAdigitalCoding(string binary)
        {
            string DNAdigitalSequence=null;
            if (binary == "00")
                DNAdigitalSequence = "A";
            else if (binary == "01")
                DNAdigitalSequence = "C";
            else if (binary == "10")
                DNAdigitalSequence = "G";
            else if (binary == "11")
                DNAdigitalSequence = "T";
            else
                MessageBox.Show("INVALID BINARY!!");
            return DNAdigitalSequence;

        }

        private void chs_btn_Click(object sender, EventArgs e)
        {
            Stream browse = null;
            OpenFileDialog readfile = new OpenFileDialog();
            readfile.InitialDirectory = "c:\\";
            readfile.Filter = "txt file (*.txt)|*.txt";
            if (readfile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileName = readfile.FileName;
                    if ((browse = readfile.OpenFile()) != null)
                    {
                        using (browse)
                        {
                            plainText = System.IO.File.ReadAllText(readfile.FileName);
                        }
                    }
                    textBox1.Text = fileName;
                    loadFileName = readfile.SafeFileName;
                    int index = loadFileName.IndexOf(".");
                    StringBuilder sr = new StringBuilder(loadFileName);
                    sr.Remove(index, 4);
                    loadFileName = sr.ToString();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Encrypt_btn_Click(object sender, EventArgs e)
        {
            Stopwatch s3 = new Stopwatch();
            s3.Start();
            Random rand2 = new Random();
            string ASCII = convertToASCII(plainText);
            string binary = ASCIIToBinary(ASCII);
            string s_BoxValue = null;
            string Elgamals_BoxValue = null;
            string cipherText_1 = null;
            string cipherText_2 = null;
            Stopwatch s1 = new Stopwatch();
            s1.Start();
            for (int i=0;i<binary.Length;i=i+8)
            {
                s_BoxValue += S_box(binary.Substring(i,8));
            }
            s1.Stop();
            string q = Convert.ToString(rand2.Next(int.Parse(b), int.Parse(c)));
            Stopwatch s2 = new Stopwatch();
            s2.Start();
            string ElgamalKey=Convert.ToString(genKey(int.Parse(q)));
            s2.Stop();
            string ElgamalBinary = ASCIIToBinary(ElgamalKey);
            for (int i = 0; i < ElgamalBinary.Length; i = i + 8)
            {
                Elgamals_BoxValue += S_box(ElgamalBinary.Substring(i, 8));
            }
            string ASCII_2 = convertToASCII(s_BoxValue);
            string binary_2 = ASCIIToBinary(ASCII_2);
            for (int i = 0; i < binary_2.Length; i+=2)
            {
                cipherText_1 += DNAdigitalCoding(binary_2.Substring(i, 2));
            }
            string ASCII_3 = convertToASCII(Elgamals_BoxValue);
            string binary_3 = ASCIIToBinary(ASCII_2);
            for (int i = 0; i < binary_3.Length; i += 2)
            {
                cipherText_2 += DNAdigitalCoding(binary_3.Substring(i, 2));
            }
            s3.Stop();
            string cipherText = cipherText_1 + "$$$" + cipherText_2;
            write(cipherText, loadFileName);
            string path = @"";
            string Filename = textBox1.Text;
            var newpath = path + Filename;
            long length = new System.IO.FileInfo(@newpath).Length;
            label1.Text = "GICT : " + s1.Elapsed + "\n" + "KGT : " + s2.Elapsed + "\n" + "GET : " + s3.Elapsed + "\n" + "Size : " + Convert.ToString(length) + "Bytes";
        }

        public static string reverseDNAprocess(string text)
        {
            string binary=null;
            if (text=="A")
                binary="00";
            else if (text=="C")
                binary="01";
            else if (text=="G")
                binary="10";
            else if (text=="T")
                binary="11";
            return binary;
        }

        public static Byte[] GetBytesFromBinaryString(String binary)
        {
            var list = new List<Byte>();

            for (int i = 0; i < binary.Length; i += 8)
            {
                String t = binary.Substring(i, 8);

                list.Add(Convert.ToByte(t, 2));
            }

            return list.ToArray();
        }     

        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();
 
            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }

        public static string S_boxDecrypt(string temp)
        {
            string[,] Box = new string[17, 17] 
            { {"" ,"1000", "1001", "1010", "1011","1100","1101","1110","1111","0000","0001","0010","0011","0100","0101","0110","0111" },
              {"0000" ,"A1", "B3", "0A", "C2","D3","DS","E3","J4","CM","O2","PP","LO","IR","HQ","MZ","QH" },
              {"0001" ,"Q1", "B2", "D5", "0B","D2","8B","Q4","QS","K4","HR","P2","AR","DO","JS","RI","HO" },
              {"0010" ,"AB", "RJ", "C3", "E6","OC","E2","PB","F3","TO","L4","CO","Q2","ZM","SJ","UR","QY" },
              {"0011","BC", "ZA", "SK", "D4","F7","0D","F2","NP","G3","MD","M4","KI","TK","VE","EV","RU" },
              {"0100" ,"DE", "XB", "JL", "TL","E5","G8","OE","G2","GM","H3","DP","UL","7B","R2","3T","OY" },
              {"0101" ,"FG", "YC", "KI", "E4","UM","F6","H9","OF","H2","MV","VM","5E","N4","6C","S2","LO" },
              {"0110" ,"HI", "WD", "L2", "F4","9V","VN","G7","IA","0G","WN","I2","I3","7Z","O4","YO","T2" },
              {"0111" ,"JK", "UE", "M3", "7O","G4","8X","WO","H8","XO","0H","N3","J2","J3","5N","P4","CV" },
              {"1000" ,"LM", "SF", "N4", "4L","6B","H4","XP","YP","I9","JB","0I","O3","K2","K3","4W","Q4" },
              {"1001" ,"NO", "QG", "O5", "4A","ST","3D","ZQ","YQ","OT","JA","KC","0J","P3","L2","L3","TV" },
              {"1010" ,"PQ", "OH", "P6", "I4","QE","IR","0U","V3","ZR","OS","KB","LD","0K","Q3","M2","MP" },
              {"1011" ,"RS", "MI", "Q7", "2M","2S","OV","U2","W3","X3","IS","OR","LC","ME","0L","S3","N2" },
              {"1100" ,"TU", "KJ", "R8", "GT","0W","U4","FP","V2","Y3","Z3","2T","OQ","ON","NF","0M","R3" },
              {"1101" ,"VW", "IK", "4U", "0X","T4","WZ","LT","PT","W2","XA","PO","3U","OP","NE","OG","0N" },
              {"1110" ,"XY", "5V", "0Y", "S4","ZB","YX","V4","W4","BS","X2","Y4","T3","4V","0O","FT","PH" },
              {"1111" ,"6W", "0Z", "R4", "9L","Z4","ZW","MS","RP","9C","CS","Y2","U3","Z2","5W","6X","PG" }
            };
            string firstbinary = null;
            string secondbinary = null;
            for (int i = 1; i < 17; i++)
            {
                for (int j = 1; j < 17; j++)
                {
                    if (Box[i, j] == temp)
                    {
                        firstbinary = Box[i, 0];
                        secondbinary = Box[0, j];
                        break;
                    }
                }
            }
            temp = null;
            temp = (firstbinary + secondbinary);
            return temp;
        }

        public static void writeDecoded(string data, string name)
        {
            string path = @"";
            string Filename = name + "Decoded.txt";
            var newpath = path + Filename;
            using (StreamWriter sw = System.IO.File.AppendText(newpath))
            {
                sw.WriteLine(data);
            }

        }

        public static void removeFileContentDecoded(string name)
        {
            System.IO.File.WriteAllText(name + @"Decoded.txt", string.Empty);
        }

        private void decrypt_btn_Click(object sender, EventArgs e)
        {
            Stopwatch s1 = new Stopwatch();
            s1.Start();
            string DNAReverseValue = null;
            string path = @"";
            string Filename = textBox2.Text;
            var newpath = path + Filename;
            plainText=System.IO.File.ReadAllText(@newpath);
            try
            {
                int index = plainText.IndexOf('$');

                StringBuilder sb = new StringBuilder(plainText);
                int length = sb.Length - index;
                sb.Remove(index, length);
                string cipherText = sb.ToString();
                string temp1=plainText.Remove(index, 2);
                for (int i = 0; i < cipherText.Length;i++ )
                {
                    DNAReverseValue += reverseDNAprocess(cipherText.Substring(i, 1));
                }
                var data = GetBytesFromBinaryString(DNAReverseValue);
                var text = Encoding.ASCII.GetString(data);
                string value = null;
                for (int i = 0; i < text.Length;i+=3)
                {
                    string temp = text.Substring(i, 3);
                    temp.Remove(2, 1);
                    char character = (char)int.Parse(temp);
                    value= value + character.ToString();
                }
                string abc = null;
                for (int i = 0; i < value.Length; i += 2)
                {
                    abc += S_boxDecrypt(value.Substring(i, 2));
                }
                var data1 = GetBytesFromBinaryString(abc);
                var text1 = Encoding.ASCII.GetString(data1);
                string removedSpace= text1.Remove((text1.Length - 1), 1);
                string value1 = null;
                string[] ASCIIList = removedSpace.Split(' ');
                foreach(string xyz in ASCIIList)
                {
                    char character = (char)int.Parse(xyz);
                    value1 = value1 + character.ToString();
                }
                s1.Stop();
                removeFileContentDecoded(loadFileName);
                writeDecoded(value1, loadFileName);
                long length1 = new System.IO.FileInfo(loadFileName+"Encrypted.txt").Length;
                label2.Text = "GDT : " + s1.Elapsed + "\n" + "Size : " + Convert.ToString(length) + "Bytes";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
