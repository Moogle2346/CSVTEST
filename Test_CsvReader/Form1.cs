using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_CsvReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> data = new List<string>();
            string path = @"C:\Users\PCuser\Desktop\test.csv";
            string tmp;

            try
            {
                //ファイルが存在するか確認
                if (File.Exists(path) == true)
                {
                    using (StreamReader sr = new StreamReader(path, Encoding.GetEncoding("Shift-JIS")))
                    {
                        //EOFまで読み取り
                        while (sr.Peek() > -1)
                        {
                            // Anser = """12","3,4","5,6"""
                            tmp = sr.ReadLine();
                            int DoubleQuote = 0;
                            int StartPosition = 0;
                            bool QuoteExist = false;

                            for (int i = 0; i < tmp.Length; i++)
                            {
                                if (tmp[i] == '"')
                                {
                                    DoubleQuote++;
                                    if(QuoteExist == false)
                                    {
                                        StartPosition = i + 1;
                                        QuoteExist = true;
                                    }
                                }

                                if ((tmp[i] == ',' && DoubleQuote % 2 == 0) || (i == tmp.Length-1))
                                {
                                    if (QuoteExist == true)
                                    {
                                        string tmp2 = tmp.Substring(StartPosition, i - StartPosition - 1);
                                        data.Add(tmp2.Replace("\"\"", "\""));
                                    }
                                    else
                                    {
                                        data.Add(tmp.Substring(StartPosition, i - StartPosition));
                                    }
                                    StartPosition = i;
                                    QuoteExist = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            ;
        }
    }
}
