using LeilaOnLineCore;
using System.Linq;

namespace LeilaoOnLine.TestXUnit
{
    public class ModalidadeValorSuperiorMaisProximo : IModalidadeLeilao
    {
        public double ValorDestino { get; }
        public ModalidadeValorSuperiorMaisProximo(double valorDestino)
        {
            ValorDestino = valorDestino;
        }
        public Lance Modalidade(Leilao leilao)
        {
           return leilao.Lances
                   .DefaultIfEmpty(new Lance(null, 0))
                   .Where(l => l.Valor > ValorDestino)
                   .OrderBy(l => l.Valor)
                   .FirstOrDefault();
        }
    }
}