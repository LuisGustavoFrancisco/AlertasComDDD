namespace Alertas.Domain
{

    public class Alerta
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Categoria { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataHora { get; set; }
        public string Hostname { get; set; }
        public string Criticidade { get; set; }
    }

}
