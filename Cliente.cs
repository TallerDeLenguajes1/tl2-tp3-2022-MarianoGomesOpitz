public class Cliente : Persona
{
    private string datosReferenciaDireccion;

    public string DatosReferenciaDireccion { get => datosReferenciaDireccion; set => datosReferenciaDireccion = value; }

    public Cliente() : base()
    { }

    public Cliente(int i, string[] eleccion)
    {
        this.Id = i;

        this.Nombre = eleccion[0];
        this.Telefono = eleccion[1];
        this.Direccion = eleccion[2];

        Console.Write("Ingrese unos datos de referencia para la dirección: ");
        this.DatosReferenciaDireccion = Console.ReadLine();
    }
}