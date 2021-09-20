using LeilaOnLineCore;
using System.Linq;
using Xunit;

namespace LeilaoOnLine.TestXUnit
{
    public class LeilaoRecebeLances
    {
        [Fact]
        public void NãoAceitarProximoLanceConsecutivoDadoLanceFeitoPeloMesmoCliente()
        {
            //Arrange = Entrada dos dados a serem testados
            var modalidade = new ModalidadeMaiorValor();
            var leilao = new Leilao("Obras de arte",modalidade);
            var interessado = new Interessado("Fulano", leilao);

            leilao.IniciarPregao();

            leilao.ReceberLance(interessado, 1000);

            //Act = A ação, o método a executar o teste.
            leilao.ReceberLance(interessado, 2000);

            leilao.TerminarPregao();

            //Assert = O Resultado obtido

            var resultadoObtido = leilao.Lances.Count();
            var resultadoEsperado = 1;

            Assert.Equal(resultadoObtido, resultadoEsperado);
        }


        [Theory]
        [InlineData(4, new double[] { 100, 1200, 1400, 1300 })]
        [InlineData(2, new double[] { 800, 900 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qndeEsperada, double[] ofertas)
        {
            //Arrange = Entrada dos dados a serem testados
            var modalidade = new ModalidadeMaiorValor();

            var leilao = new Leilao("Obras de arte",modalidade);

            var interessado = new Interessado("Fulano", leilao);
            var interessado2 = new Interessado("Ciclano", leilao);


            leilao.IniciarPregao();

            //método que não permite lances consecutivos do mesmo cliente.
            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                {
                    leilao.ReceberLance(interessado, valor);
                }
                else
                {
                    leilao.ReceberLance(interessado2, valor);
                }
            }

            //leilão finalizado. 
            leilao.TerminarPregao();

            //Act = A ação, o método a executar o teste.
            leilao.ReceberLance(interessado, 1000);

            //Assert = O Resultado obtido
            var resultadoObtido = leilao.Lances.Count();
            Assert.Equal(resultadoObtido, qndeEsperada);

        }
    }
}
