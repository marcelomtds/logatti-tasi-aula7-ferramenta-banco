namespace Biblioteca
{
    public class Ferramenta
    {
        public Ferramenta()
        {
        }

        public Ferramenta(string descricao, string tipo, string marca, float preco)
        {
            Descricao = descricao;
            Tipo = tipo;
            Marca = marca;
            Preco = preco;
        }

        public Ferramenta(int id, string descricao, string tipo, string marca, float preco)
        {
            Id = id;
            Descricao = descricao;
            Tipo = tipo;
            Marca = marca;
            Preco = preco;
        }

        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Tipo { get; set; }

        public string Marca { get; set; }

        public float Preco { get; set; }

        public override string ToString()
        {
            return "Id: " + this.Id +
                 "\nDescrição: " + this.Descricao +
                 "\nTipo: " + this.Tipo +
                 "\nMarca: " + this.Marca +
                 "\nPreço: R$ " + this.Preco.ToString(".00");
        }
    }
}
