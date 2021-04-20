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
using System.Xml.Linq;

namespace Weather
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReadXmlAndWriteText();
        }

        private void ReadXmlAndWriteText()
        {
            List<Feature> weatherList = new List<Feature>();
            XDocument xDocument = XDocument.Load("https://www.mgm.gov.tr/FTPDATA/analiz/sonSOA.xml");
            weatherList = xDocument.Descendants("sehirler").Where(x => (string) x.Element("Merkez") == "İSTANBUL")
                .Select(o => new Feature
                {
                    Region = (string) o.Element("Bolge"),
                    City = (string) o.Element("ili"),
                    Center = (string) o.Element("Merkez"),
                    Status = (string) o.Element("Durum"),
                    MaxDegree = (string) o.Element("Mak"),
                }).ToList();
            TextWriter(weatherList);
        }

        private void TextWriter(List<Feature> weatherList)
        {
            string save = weatherList[0].Region + " | " +
                          weatherList[0].City + " | " +
                          weatherList[0].Center + " | " +
                          weatherList[0].MaxDegree + "         | " +
                          weatherList[0].Status;
            string save2 = "Bolge      | Şehir         | Merkez      | Derece  | Durum\n";
            label1.Text = save;
            TextWriter textWriter = new StreamWriter("Weather.text");
            textWriter.Write(save2 + save);
            textWriter.Close();
        }

        private void tmrTime_Tick(object sender, EventArgs e)
        {
            ReadXmlAndWriteText();
            label1.Text +="güncellendi";

        }
        
    }

    public class Feature
    {
        public string Region { get; set; }
        public string City { get; set; }
        public string Center { get; set; }
        public string Status { get; set; }
        public string MaxDegree { get; set; }
    }
}
