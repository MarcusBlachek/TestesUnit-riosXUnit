using LeilaOnLineCore;
using Xunit;

namespace LeilaoOnLine.TestXUnit
{
    public class LancesConstrutor
    {
        [Fact]
        public void LancaArgumentExceptionDadoLanceNegativo()
        {
            //Arrange
            var valorNegativo = -100;

            //Assert
            var resultadoObtido = Assert.Throws<System.ArgumentException>(

                //Act
                () => new Lance(null, valorNegativo)
                );

            var resultadoEsperado = "Não é possível dar lances negativos";

            Assert.Equal(resultadoEsperado, resultadoObtido.Message);
        }


    }
}
