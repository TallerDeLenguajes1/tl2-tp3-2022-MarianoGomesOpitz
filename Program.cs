// See https://aka.ms/new-console-template for more information
using System;

internal class Program
{
    static void Main(string[] args)
    {
        var rand = new Random();
        string archivoNombres = "Nombres.csv";
        var leer = File.ReadAllLines(archivoNombres);

        int repetido_cad;
        var listaCadetes = new List<Cadete>();
        int cantCadetes = rand.Next(3, 7);
        Console.WriteLine($"\nCantidad de cadetes: {cantCadetes}");
        for (int h = 0; h < cantCadetes; h++)
        {
            repetido_cad = 0;
            int posicion1 = rand.Next(leer.Length);
            var eleccion_cad = (leer[posicion1]).Split(", ");


            var listaPedidos = new List<Pedido>();

            if (listaCadetes.Count != 0)
            {
                foreach (var item in listaCadetes)
                {
                    if (eleccion_cad[0] == item.Nombre)
                    {
                        repetido_cad = 1;
                    }
                }

                if (repetido_cad == 0)
                {
                    CrearCadetes(listaCadetes, eleccion_cad, h + 1, rand, leer, listaPedidos);
                }
                else
                {
                    h -= 1;
                }
            }
            else
            {
                CrearCadetes(listaCadetes, eleccion_cad, h + 1, rand, leer, listaPedidos);
            }
        }

        string archivoCadeterias = "Cadeterias.csv";
        var lectura = File.ReadAllLines(archivoCadeterias);
        int posicion = rand.Next(lectura.Length);
        var eleccion = (lectura[posicion]).Split(", ");
        var cadeteria = new Cadeteria(eleccion[0], eleccion[1], listaCadetes);

        Console.WriteLine($"\nCadetería: {cadeteria.Nombre}");
        Console.WriteLine($"Teléfono: {cadeteria.Telefono}");
        Console.WriteLine($"Cadetes:");
        foreach (var item_1 in cadeteria.Cadetes)
        {
            Console.WriteLine($"\n    ID: {item_1.Id}");
            Console.WriteLine($"    Nombre: {item_1.Nombre}");
            Console.WriteLine($"    Teléfono: {item_1.Telefono}");
            Console.WriteLine($"    Dirección: {item_1.Direccion}");
            Console.WriteLine($"    Pedidos asignados:");
            foreach (var item_2 in item_1.Pedidos)
            {
                Console.WriteLine($"\n        Nro de pedido: {item_2.NroPedido}");
                Console.WriteLine($"        Observaciones: {item_2.Observaciones}");
                Console.WriteLine($"        Estado: {item_2.Estado}");
                Console.WriteLine($"        Nombre del cliente: {item_2.Costumer.Nombre}");
                Console.WriteLine($"        Teléfono: {item_2.Costumer.Telefono}");
                Console.WriteLine($"        Dirección: {item_2.Costumer.Direccion}");
                Console.WriteLine($"        Datos de referencia de la dirección: {item_2.Costumer.DatosReferenciaDireccion}");
            }
        }
    }

    private static void ClientePedido(List<Pedido> listaPedidos, int i, string[] eleccion)
    {
        Console.WriteLine($"\nCliente {i}");
        var cli = new Cliente(i, eleccion);
        var ped = new Pedido(i, 1, cli);
        listaPedidos.Add(ped);
    }

    private static void CrearCadetes(List<Cadete> listaCadetes, string[] eleccion_cad, int h, Random rand, string[] leer, List<Pedido> listaPedidos)
    {
        Console.WriteLine($"\nCadete {h}");
        int repetido_cli;
        int cantPedidos = rand.Next(1, 6);
        Console.WriteLine($"\nCantidad de pedidos asignados al cadete: {cantPedidos}");
        for (int i = 0; i < cantPedidos; i++)
        {
            repetido_cli = 0;

            int posicion2 = rand.Next(leer.Length);
            var eleccion_cli = (leer[posicion2]).Split(", ");

            if (listaPedidos.Count != 0)
            {
                foreach (var item in listaPedidos)
                {
                    if (eleccion_cli[0] == item.Costumer.Nombre)
                    {
                        repetido_cli = 1;
                    }
                }

                if (repetido_cli == 0)
                {
                    ClientePedido(listaPedidos, i + 1, eleccion_cli);
                }
                else
                {
                    i -= 1;
                }
            }
            else
            {
                ClientePedido(listaPedidos, i + 1, eleccion_cli);
            }
        }

        var cade = new Cadete(h + 1, eleccion_cad, listaPedidos);
        listaCadetes.Add(cade);
    }
}