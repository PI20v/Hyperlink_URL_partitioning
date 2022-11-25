using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hyperlink_and_URL_partitioning
{
    public partial class Partitioning : Form
    {
        Autorization aut;
        string name;

        public Partitioning(Autorization aut, string name)
        {
            InitializeComponent();
            this.aut = aut;
            this.name = name;

            using (StreamWriter writer = new StreamWriter("Log_" + name + ".log", File.Exists("Log_" + name + ".log")))
            {
                writer.WriteLine("User " + name + " logged in, at " + DateTime.Now.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = textBox1.Text;
            using (StreamWriter writer = new StreamWriter("Log_" + name + ".log", File.Exists("Log_" + name + ".log")))
            {
                writer.WriteLine($"The user has entered a link \"{a}\", at " + DateTime.Now.ToString());
            }
            try
            {
                System.Uri uri = new System.Uri(a);

                // Получаем имя протокола
                string protocol = uri.Scheme;

                // Получаем имя хоста
                string host = uri.Host;

                // Получаем порт
                int port = uri.Port;

                // Получаем чистый URL 
                string path = uri.LocalPath;

                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();

                if (uri.Port == -1 && !uri.IsFile)
                {
                    textBox2.Text = $"Неизвестный протокол ({uri.Scheme})";
                }
                else
                    textBox2.Text = protocol;
                
                if (uri.IsFile)
                {
                    textBox3.Text = "Это файл. У файла нет хоста.";
                    textBox4.Text = "Это файл. У файла нет порта.";
                }
                else if (port == -1)
                {
                    textBox3.Text = "Хост неизвестен. (Неверно указан адрес?)";
                    textBox4.Text = "Порт неизвестен.";
                }
                else
                {
                    textBox3.Text = host;
                    textBox4.Text = port.ToString();
                }
                textBox5.Text = path;
            }
            catch (UriFormatException)
            {
                MessageBox.Show("Адрес неверен", "Ошибка");
                using (StreamWriter writer = new StreamWriter("Log_" + name + ".log", File.Exists("Log_" + name + ".log")))
                {
                    writer.WriteLine("User received an error - Wrong address, at " + DateTime.Now.ToString());
                }
            }
        }

        
        private void Partitioning_FormClosed(object sender, FormClosedEventArgs e)
        {
            aut.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
                if (File.Exists("Справка.info")) Process.Start("Справка.info");
                else
                {
                    MessageBox.Show("Файл со справкой отсутствует", "Ошибка");
                    return;
                }
        }
    }
}
