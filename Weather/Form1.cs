using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            
            label1.Text = weatherList[0].MaxDegree;
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
