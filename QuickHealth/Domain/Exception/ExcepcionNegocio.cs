namespace QuickHealth.Domain.Exception
{
    public class ExcepcionNegocio:System.Exception
    {
        public ExcepcionNegocio(string mensaje) : base(mensaje) { }
    }
}
