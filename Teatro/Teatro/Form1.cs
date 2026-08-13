using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teatro
{
    enum EstadoPoltrona { Vaga, Inteira, Meia }
    public partial class Form1 : Form
    {
        private EstadoPoltrona[,] mapaTeatro = new EstadoPoltrona[15, 40];
        private Label lblResultado;
        public Form1()
        {
            InitializeComponent();
        }
        public void Poltrona_Click(object sender, EventArgs e)
        {
            Button botaoClicado = (Button)sender;

            Point posicao = (Point)botaoClicado.Tag;
            int fileira = posicao.X;
            int poltrona = posicao.Y;

            EstadoPoltrona estadoAtual = mapaTeatro[fileira, poltrona];

            if (estadoAtual == EstadoPoltrona.Vaga)
            {
                DialogResult resposta = MessageBox.Show(
                        $"Fileira {fileira + 1}, Poltrona {poltrona + 1}\n\nDeseja reservar como MEIA ENTRADA?\n(Clique em 'Sim' para Meia, 'Não' para Inteira)",
                        "Reservar Poltrona",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question
                    );

                if (resposta == DialogResult.Yes)
                {
                    mapaTeatro[fileira, poltrona] = EstadoPoltrona.Meia;
                    botaoClicado.BackColor = Color.Yellow;
                }
                else if (resposta == DialogResult.No)
                {
                    mapaTeatro[fileira, poltrona] = EstadoPoltrona.Inteira;
                    botaoClicado.BackColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show(
            $"A Poltrona {poltrona + 1} da Fileira {fileira + 1} já está ocupada! ({estadoAtual})",
            "Aviso",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
                    );
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            int larguraBotao = 25;
            int alturaBotao = 25;
            int espaceamento = 4;
            for (int lin = 0; lin < 15; lin++)
            {
                for (int col = 0; col < 40; col++)
                {
                    Button btn = new Button();
                    btn.Width = larguraBotao;
                    btn.Height = alturaBotao;
                    btn.Tag = new Point(lin, col);

                    btn.Left = 10 + col * (larguraBotao + espaceamento);

                    btn.Top = 10 + lin * (alturaBotao + espaceamento);

                    btn.BackColor = Color.Green;

                    btn.Click += Poltrona_Click;

                    this.Controls.Add(btn);

                    Button btnFaturamento = new Button();
                    btnFaturamento.Text = "Faturamento";
                    Label estat = new Label();
                }
            }
        }
    }
}
