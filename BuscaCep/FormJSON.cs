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
    public partial class FormJSON : Form
    {
        public FormJSON()
        {
            InitializeComponent();
        }

        private async void buttonBuscarCEP_Click(object sender, EventArgs e)
        {
            //retira a formatação do maskedTextBox
            maskedTextBoxCEP.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;

            if (string.IsNullOrEmpty(maskedTextBoxCEP.Text))
                MessageBox.Show("Este campo é obrigatório", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                try
                {
                    //bibliotecas necessarias:
                    // System.Net.HTTP: vem com a plataforma, é só acrescenta-la no using
                    // Newtonsoft.Json: ela é uma biblioteca de terceiros, precisa ser baixada no NuGet

                    var cliente = new HttpClient();
                    var resposta = await cliente.GetStringAsync($"https://viacep.com.br/ws/{maskedTextBoxCEP.Text}/json/");                
                    var dados = JsonConvert.DeserializeObject<Classes.JsonCEP>(resposta);

                    if (string.IsNullOrEmpty(dados.cep))
                    {
                        MessageBox.Show("CEP não encontrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        textBoxLogradouro.Text = dados.logradouro;
                        textBoxComplemento.Text = dados.complemento;
                        textBoxBairro.Text = dados.bairro;
                        textBoxLocalidade.Text = dados.localidade;
                        textBoxUF.Text = dados.uf;
                    }
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
