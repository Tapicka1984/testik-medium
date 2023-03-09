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

namespace TEST_TEXT_HARD_SUPER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
        }

        double CZK(double penize)
        {
            penize = (penize * 22.3);
            return penize;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string name = string.Empty;
                OpenFileDialog otevri = new OpenFileDialog();
                otevri.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                if (otevri.ShowDialog() == DialogResult.OK)
                {
                    name = otevri.FileName;
                }
                StreamReader ctenar = new StreamReader(name);
                int pocet_zen = 0;
                int pocet_muzu = 0;
                int pocet_jinych_veci = 0;
                int vek = 0;
                int minimalni_mzda = 17300; // datum z 09.03.2023 - v případě potřeby možno změnit
                string best_one = "";

                double best = 0;
                while (!ctenar.EndOfStream)
                {
                    string line = ctenar.ReadLine();
                    string line2 = "";
                    foreach (char X in line)
                    {
                        if (X != 34)
                        {
                            line2 += X;
                        }
                    }
                    string[] radek = line2.Split(',');
                    double penize = double.Parse(radek[4]);
                    penize = CZK(penize);
                    line2 = radek[0] + ", " + radek[1] + ", " + radek[2] + ", " + radek[3] + ", " + penize.ToString();
                    vek += Int32.Parse(radek[3]);
                    if (radek[2] == "Male")
                    {
                        pocet_muzu++;
                    }
                    else if (radek[2] == "Female")
                    {
                        pocet_zen++;
                    }
                    else
                    {
                        pocet_jinych_veci++;
                    }
                    listBox1.Items.Add(line2);
                    if (penize < minimalni_mzda)
                    {
                        listBox2.Items.Add(line2);
                    }

                    if (penize > best)
                    {
                        best_one = line2;
                        best = penize;
                    }
                }

                //zona po cyklu
                StreamWriter zapis = new StreamWriter("best.txt");
                zapis.WriteLine("Prumerny vek je: " + (vek / 100));
                MessageBox.Show("Pocet Zen v Nasem Seznamu Je:  " + pocet_zen + "\nPocet Muzu v Nasem Seznamu Je:  " + pocet_muzu + "\nPocet Jinych Veci V nasem Seznamu Je:    " + pocet_jinych_veci);

                string[] nejlepsi_ze_vsech = best_one.Split(',');
                zapis.WriteLine("nejlepsi je: " + nejlepsi_ze_vsech[0] + " " + nejlepsi_ze_vsech[1] + " je to:" + nejlepsi_ze_vsech[2] + " má:" + nejlepsi_ze_vsech[3] + " let, a vydela si:" + nejlepsi_ze_vsech[4]);
                zapis.Close();
            }
            catch
            {
                MessageBox.Show("Chyba: \n1) Pokuste se restartovat program \n2)Zkontrolujte ktery soubor vybirate\n3)Restartujte PC");
            }
        }
    }
}
