using LeilaoOnLine.TestXUnit;
using System.Collections.Generic;

namespace LeilaOnLineCore
{
    public enum EstadoLeilao
    {
        LeilaoAntesDoPregao,
        LeilaoEmAndamento,
        LeilaoFinalizado
    }

    public class Leilao
    {
        private IModalidadeLeilao _avaliador;
        private Interessado _ultimoCliente;
        private IList<Lance> _lances;

        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; private set; }
        public double ValorDestino { get; }

        public Leilao(string peca,IModalidadeLeilao avaliador)
        {
            _lances = new List<Lance>();
            Peca = peca;
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
            _avaliador = avaliador;
        }

        //método que verifica a aceitação do lance. Se o Estado está em andamento e o cliente a fazer a oferta é diferente
        //do último pois não é permitido ofertas seguidas do mesmo cliente.
        private bool AceitarLances(Interessado cliente, double valor)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento) && cliente != _ultimoCliente;
        }

        //método que aceita o lance e valida o cliente que realizou
        public void ReceberLance(Interessado cliente, double valor)
        {
            if (AceitarLances(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
        }

        public void IniciarPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        //método que encerra o pregão. O ganhador do leilão foi o que fez o maior lance por último
        //(verificado por OrderBy e LastOrDefault)) e encerra o Estado do leilão.
        public void TerminarPregao()
        {
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
            {
                throw new System.Exception("Não é possível finalizar um pregão sem ser iniciado");
            }

            Ganhador = _avaliador.Modalidade(this);
           
            Estado = EstadoLeilao.LeilaoFinalizado;
        }
    }
}
