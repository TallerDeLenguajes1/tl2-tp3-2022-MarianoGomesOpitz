public class Cadete : Persona
{
    private List<Pedido> pedidos;

    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }

    public Cadete() : base()
    {

    }

    public Cadete(int i, string[] eleccion, List<Pedido> peds)
    {
        this.Id = i;
        this.Nombre = eleccion[0];
        this.Telefono = eleccion[1];
        this.Direccion = eleccion[2];
        this.Pedidos = peds;
    }
}