using LeilaOnLineCore;
using System.Linq;

namespace LeilaoOnLine.TestXUnit
{
    public class ModalidadeMaiorValor : IModalidadeLeilao
    {
        public Lance Modalidade(Leilao leilao)
        {
            return leilao.Lances
               .DefaultIfEmpty(new Lance(null, 0))
               .OrderBy(x => x.Valor)
               .LastOrDefault();
        }
    }
}