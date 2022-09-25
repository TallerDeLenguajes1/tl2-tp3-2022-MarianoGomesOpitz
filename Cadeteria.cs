public class Cadeteria
{
    private string nombre;
    private string telefono;
    private List<Cadete> cadetes;

    public string Nombre { get => nombre; set => nombre = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public List<Cadete> Cadetes { get => cadetes; set => cadetes = value; }

    public Cadeteria()
    {

    }

    public Cadeteria(string nombreCadeteria, string tel, List<Cadete> listaCadetes)
    {
        this.Nombre = nombreCadeteria;
        this.Telefono = tel;
        this.cadetes = listaCadetes;
    }
}