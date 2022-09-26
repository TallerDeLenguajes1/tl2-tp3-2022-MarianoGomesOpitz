public class Cadete : Persona
{
    private List<Pedido> pedidos;

    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

    public Cadete() : base()
    {

    }

    public Cadete(int i, string[] eleccion)
    {
        this.Id = i;
        this.Nombre = eleccion[0];
        this.Telefono = eleccion[1];
        this.Direccion = eleccion[2];
        this.Pedidos = new List<Pedido>();
    }

    public int JornalACobrar()
    {
        int montoBase = 300, montoGanado = 0, pedidosEntregados = 0;

        foreach (var item in this.Pedidos)
        {
            if (item.Estado == Convert.ToString((status)3))
            {
                (pedidosEntregados)++;
            }
        }

        montoGanado = pedidosEntregados * 300;
        return (montoGanado);
    }
}