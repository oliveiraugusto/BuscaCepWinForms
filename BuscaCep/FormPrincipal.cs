using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;

namespace BuscaCep
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void buttonJson_Click(object sender, EventArgs e)
        {
            FormJSON fj = new FormJSON();
            fj.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormXML fx = new FormXML();
            fx.Show();
        }
    }
}
