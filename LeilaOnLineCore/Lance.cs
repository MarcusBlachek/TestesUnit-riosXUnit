namespace LeilaOnLineCore
{
    public class Lance
    {
        public Interessado Cliente { get; }
        public double Valor { get; }

        public Lance(Interessado cliente, double valor)
        {
            if (valor < 0)
            {
                throw new System.ArgumentException("Não é possível dar lances negativos");
            }
            Cliente = cliente;
            Valor = valor;
        }
    }
}
