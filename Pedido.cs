public class Pedido
{
    //numero, obs, cliente, estado
    private int nroPedido;
    private string observaciones, estado;
    private Cliente costumer;

    public int NroPedido { get => nroPedido; set => nroPedido = value; }
    public string Observaciones { get => observaciones; set => observaciones = value; }
    public string Estado { get => estado; set => estado = value; }
    public Cliente Costumer { get => costumer; set => costumer = value; }

    public Pedido()
    {

    }

    public Pedido(int i, int est, Cliente cos)
    {
        this.NroPedido = i;

        Console.WriteLine("¿Alguna observación acerca del pedido?");
        this.Observaciones = Console.ReadLine();

        this.Estado = Convert.ToString((status)est);

        this.Costumer = cos;
    }
}

enum status
{
    En_Preparación = 1,
    En_Camino = 2,
    Entregado = 3,
}