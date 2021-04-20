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
            var weatherList = ReadXmlWriteText();
            FillLabel(weatherList);
        }

        private void FillLabel(List<Feature> weatherList)
        {
            lblCity.Text = weatherList[0].City;
            lblDegree.Text = " ===> " + weatherList[0].MaxDegree + " Derece";
            lblStatus.Text = weatherList[0].Status;
            lblNotification.Text = DateTime.Now.ToLongTimeString() + "  Saatinde Bilgiler Güncellendi";
        }

        private static List<Feature> ReadXmlWriteText()
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

            string save = weatherList[0].Region + " | " +
                          weatherList[0].City + " | " +
                          weatherList[0].Center + " | " +
                          weatherList[0].MaxDegree + "         | " +
                          weatherList[0].Status;
            string save2 = "Bolge      | Şehir         | Merkez      | Derece  | Durum\n";
            TextWriter textWriter = new StreamWriter("Weather.text");
            textWriter.Write(save2 + save);
            textWriter.Close();
            return weatherList;
        }

        private void tmrTime_Tick(object sender, EventArgs e)
        {
            var weatherList = ReadXmlWriteText();
            FillLabel(weatherList);
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
