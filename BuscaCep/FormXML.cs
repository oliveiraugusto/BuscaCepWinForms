using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BuscaCep.WSCorreios;

namespace BuscaCep
{
    public partial class FormXML : Form
    {
        public FormXML()
        {
            InitializeComponent();
        }

        private void buttonBuscarCEP_Click(object sender, EventArgs e)
        {
            //retira a formatação do maskedTextBox
            maskedTextBoxCEP.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            if (string.IsNullOrEmpty(maskedTextBoxCEP.Text))
                MessageBox.Show("Este campo é obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                try
                {
                    //link do Web Service oficial dos correios: 
                    // https://apps.correios.com.br/SigepMasterJPA/AtendeClienteService/AtendeCliente?wsdl

                    AtendeClienteClient conexao = new AtendeClienteClient();
                    var resposta = conexao.consultaCEP(maskedTextBoxCEP.Text);

                    textBoxLogradouro.Text = resposta.end;
                    textBoxComplemento.Text = resposta.complemento2;
                    textBoxBairro.Text = resposta.bairro;
                    textBoxLocalidade.Text = resposta.cidade;
                    textBoxUF.Text = resposta.uf.ToUpper();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"Não foi possivel executar a solicitação:\n \nDetalhes:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            //retorna a formatação do maskedTextBox
            maskedTextBoxCEP.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        }
    }
}
