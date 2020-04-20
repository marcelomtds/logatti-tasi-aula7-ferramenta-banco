using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Biblioteca
{
    public class FerramentaDB
    {
        private DB conexao;

        private StringBuilder sb;

        public void Inserir(Ferramenta ferramenta)
        {
            sb = new StringBuilder();
            sb.Append("INSERT INTO FERRAMENTA (descricao, tipo, marca, preco) VALUES ");
            sb.Append(string.Format("('{0}', '{1}', '{2}', '{3}')", ferramenta.Descricao, ferramenta.Tipo, ferramenta.Marca, ferramenta.Preco));
            using (conexao = new DB())
            {
                conexao.ExecutaComando(sb.ToString());
            }
        }

        public void Alterar(Ferramenta ferramenta)
        {
            sb = new StringBuilder();
            sb.Append($"UPDATE FERRAMENTA SET descricao = '{ferramenta.Descricao}', tipo = '{ferramenta.Tipo}', marca = '{ferramenta.Marca}', preco = {ferramenta.Preco} WHERE id = {ferramenta.Id}");
            using (conexao = new DB())
            {
                conexao.ExecutaComando(sb.ToString());
            }
        }
        public List<Ferramenta> ListarTodas()
        {
            using (conexao = new DB())
            {
                var sql = "SELECT id, descricao, tipo, marca, preco FROM FERRAMENTA";
                var retorno = conexao.ExecutaComandoRetorno(sql);
                return TransformaSQLReaderEmList(retorno);
            }
        }

        public Ferramenta BuscarPorId(int id)
        {
            using (conexao = new DB())
            {
                var sql = $"SELECT id, descricao, tipo, marca, preco FROM FERRAMENTA WHERE id = {id}";
                var retorno = conexao.ExecutaComandoRetorno(sql);
                return TransformaSQLReaderEmList(retorno)[0];
            }
        }

        public void Remover(int id)
        {
            using (conexao = new DB())
            {
                var sql = $"DELETE FROM FERRAMENTA WHERE id = {id}";
                conexao.ExecutaComando(sql);
            }
        }

        private List<Ferramenta> TransformaSQLReaderEmList(SqlDataReader retorno)
        {
            List<Ferramenta> ferramentas = new List<Ferramenta>();
            while (retorno.Read())
            {
                ferramentas.Add(new Ferramenta(Convert.ToInt32(retorno["id"]), retorno["descricao"].ToString(), retorno["tipo"].ToString(), retorno["marca"].ToString(), float.Parse(retorno["preco"].ToString())));
            }
            return ferramentas;
        }
    }
}
