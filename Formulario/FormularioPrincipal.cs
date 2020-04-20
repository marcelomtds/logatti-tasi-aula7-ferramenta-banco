using System;
using System.IO;
using System.Windows.Forms;
using Biblioteca;

namespace Formulario
{
    public partial class frmPrincipal : Form
    {
        FerramentaDB ferramentaDB = new FerramentaDB();
        public frmPrincipal()
        {
            InitializeComponent();
            AdicionarBotoesAlterarRemover();
            ListarTodas();
        }

        private void AdicionarBotoesAlterarRemover()
        {
            var btnAlterar = new DataGridViewButtonColumn();
            btnAlterar.Text = "Alterar";
            btnAlterar.HeaderText = "Ação";
            btnAlterar.UseColumnTextForButtonValue = true;

            var btnRemover = new DataGridViewButtonColumn();
            btnRemover.Text = "Remover";
            btnRemover.HeaderText = "Ação";
            btnRemover.UseColumnTextForButtonValue = true;

            dgvFerramentas.Columns.Add(btnAlterar);
            dgvFerramentas.Columns.Add(btnRemover);

            dgvFerramentas.CellContentClick += DgvFerramenta_CellContentClick;
        }

        private void DgvFerramenta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = (Ferramenta)dgvFerramentas.Rows[e.RowIndex].DataBoundItem;
                if (e.ColumnIndex == 0)
                {
                    var ferramenta = ferramentaDB.BuscarPorId(row.Id);
                    txtID.Text = ferramenta.Id.ToString();
                    txtDescricao.Text = ferramenta.Descricao;
                    txtTipo.Text = ferramenta.Tipo;
                    txtMarca.Text = ferramenta.Marca;
                    txtPreco.Text = Convert.ToString(ferramenta.Preco);
                }
                else if (e.ColumnIndex == 1)
                {
                    ferramentaDB.Remover(row.Id);
                    MessageBox.Show("Ferramenta removida com sucesso.");
                    LimparCampos();
                }
                ListarTodas();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (IsFormularioInvalido())
            {
                MessageBox.Show("É necessário preencher todos campos obrigatórios.");
            }
            else
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(txtID.Text))
                    {
                        ferramentaDB.Inserir(new Ferramenta(txtDescricao.Text, txtTipo.Text, txtMarca.Text, float.Parse(txtPreco.Text)));
                        MessageBox.Show("Ferramenta adicionada com sucesso.");
                    }
                    else
                    {
                        ferramentaDB.Alterar(new Ferramenta(int.Parse(txtID.Text), txtDescricao.Text, txtTipo.Text, txtMarca.Text, float.Parse(txtPreco.Text)));
                        MessageBox.Show("Ferramenta alterada com sucesso.");
                    }
                    LimparCampos();
                    ListarTodas();
                }
                catch (FormatException)
                {
                    MessageBox.Show("O preço informado é inválido.");
                }
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void LimparCampos()
        {
            txtID.Clear();
            txtDescricao.Clear();
            txtTipo.Clear();
            txtMarca.Clear();
            txtPreco.Clear();
            txtDescricao.Focus();
        }

        private void btnGerarRelatorio_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter(@"C:\Users\Marcelo\Desktop\ralatorio-ferramenta" + DateTime.Now.ToString("dd-MM-yyyy-HH-mm") + ".txt");
            foreach (var ferramenta in ferramentaDB.ListarTodas())
            {
                writer.WriteLine(ferramenta + "\n-----------------------------");
            }
            writer.Close();
            MessageBox.Show("Relatório gerado com sucesso.");
        }

        private bool IsFormularioInvalido()
        {
            return string.IsNullOrWhiteSpace(txtDescricao.Text)
                || string.IsNullOrWhiteSpace(txtTipo.Text)
                || string.IsNullOrWhiteSpace(txtMarca.Text)
                || string.IsNullOrWhiteSpace(txtPreco.Text);
        }

        private void ListarTodas()
        {
            dgvFerramentas.DataSource = null;
            dgvFerramentas.DataSource = ferramentaDB.ListarTodas();
        }

    }
}
