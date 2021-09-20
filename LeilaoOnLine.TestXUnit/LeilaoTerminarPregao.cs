using LeilaOnLineCore;
using Xunit;

namespace LeilaoOnLine.TestXUnit
{
    public class LeilaoTerminarPregao
    {
        [Theory]
        //lances desordenados
        [InlineData(new double[] { 200, 300, 350, 340 }, 350)]
        //lances ordenados
        [InlineData(new double[] { 200, 300, 350, 355, 360, 365, 370 }, 370)]
        //apenas 1 lance
        [InlineData(new double[] { 200 }, 200)]
        public void RetornaMaiorValorDadoLeilaoComPeloMenosUmLance(double[] ofertas, double valorEsperado)
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
            //Act = A ação, o método a executar o teste.
            leilao.TerminarPregao();

            //Assert = O Resultado obtido
            var resultadoObtido = leilao.Ganhador.Valor;

            Assert.Equal(resultadoObtido, valorEsperado);

        }


        [Fact]
        public void RetornaZeroDadoLeilaoSemLance()
        {
            //Arrange = Entrada dos dados a serem testados
            var modalidade = new ModalidadeMaiorValor();

            var leilao = new Leilao("Obras de arte",modalidade);
            var interessado = new Interessado("Fulano", leilao);
            leilao.IniciarPregao();

            //Act = A ação, o método a executar o teste.
            leilao.TerminarPregao();

            //Assert = O Resultado obtido
            var resultadoObtido = leilao.Ganhador.Valor;
            var resultadoEsperado = 0;
            Assert.Equal(resultadoObtido, resultadoEsperado);
        }

        [Fact]
        public void RetornaInvalidOperationExceptionQuandoPregaoFinalizaSemIniciar()
        {
            var modalidade = new ModalidadeMaiorValor();


            //Arrange = Entrada dos dados a serem testados
            var leilao = new Leilao("Obras de arte", modalidade);
            var interessado = new Interessado("Fulano", leilao);

            //Assert = O Resultado obtido
            var resultadoObtido = Assert.Throws<System.InvalidOperationException>(

                 //Act = A ação, o método a executar o teste.
                 () => leilao.TerminarPregao());

            var resultadoEsperado = "Não é possível finalizar um pregão sem ser iniciado";

            //Assert = O Resultado obtido
            Assert.Equal(resultadoEsperado, resultadoObtido.Message);
        }

        [Theory]
        [InlineData(1200,1250,new double[] { 800,1000,1100,1200,1250,1300})]
        public void RetornaValorSuperiorMaisProximoQuandoValorDeDestinoDadaModalidade(double valorDestino,double valorEsperado,double[]ofertas)
        {

            //Arrange = Entrada dos dados a serem testados
            var modalidade = new ModalidadeValorSuperiorMaisProximo(valorDestino);

            var leilao = new Leilao("Obras de arte", modalidade);
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
            //Act = A ação, o método a executar o teste.
            leilao.TerminarPregao();

            //Assert = O Resultado obtido
            var resultadoObtido = leilao.Ganhador.Valor;
            Assert.Equal(resultadoObtido, valorEsperado);
        }
    }
}
