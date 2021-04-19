using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
