// See https://aka.ms/new-console-template for more information
using System;

internal class Program
{
    static void Main(string[] args)
    {
        var rand = new Random();        //Creo la variable de aleatoriedad a usar en todo el programa
        string archivoNombres = "Nombres.csv";      //Archivo que contiene nombres, números de teléfonos, y direcciones
        var leer = File.ReadAllLines(archivoNombres);       //Leo el archivo de nombres

        ///////////////////////////////////////////////////////////////////Creación de cadetes
        int repetido_cad;       //Variable para asegurarme de que no se repitan nombres en los cadetes
        var listaCadetes = new List<Cadete>();      //Lista de cadetes
        int cantCadetes = rand.Next(3, 7);      //Cantidad de cadetes que pueden aparecer
        Console.WriteLine($"\nCantidad de cadetes: {cantCadetes}");
        for (int h = 0; h < cantCadetes; h++)
        {
            repetido_cad = 0;       //Inicializo la variable en 0
            int posicion1 = rand.Next(leer.Length);     //Obtengo una información aleatoria del archivo
            var eleccion_cad = (leer[posicion1]).Split(", ");       //Divido la información para tratarla como un arreglo


            if (listaCadetes.Count != 0)        //Pregunto si la lista de cadetes no está vacía
            {
                foreach (var item in listaCadetes)      //Si no está vacía, la recorro
                {
                    if (eleccion_cad[0] == item.Nombre)     //Pregunto si en algun momento el nombre de la información aleatoria se repitió
                    {
                        repetido_cad = 1;       //La variable en 1 indica que se repitió
                    }
                }

                if (repetido_cad == 0)      //Pregunto si se repitió la información
                {
                    CrearCadete(listaCadetes, h, eleccion_cad);     //Si no se repite, se crea el cadete
                }
                else
                {
                    h -= 1;     //Si se repitió, se repite todo lo anterior
                }
            }
            else
            {
                CrearCadete(listaCadetes, h, eleccion_cad);     //Si está vacía, le inserto el primer elemento
            }
        }

        ///////////////////////////////////////////////////////////////////Creación de cadetería
        string archivoCadeterias = "Cadeterias.csv";        //Archivo que contiene el nombre, y teléfono de cadeterías
        var lectura = File.ReadAllLines(archivoCadeterias);     //Leo el archivo
        int posicion = rand.Next(lectura.Length);       //Obtengo una cadetería aleatoria
        var eleccion = (lectura[posicion]).Split(", ");     //La trato como un arreglo
        var cadeteria = new Cadeteria(eleccion[0], eleccion[1], listaCadetes);      //Creo la cadetería con la información recopilada anteriormente

        ///////////////////////////////////////////////////////////////////Creación de clientes y pedidos sin asignar
        int repetido_cli;       //Variable para asegurarme que no se repitan nombres de clientes
        var listaPedidos = new List<Pedido>();      //Creo la lista de pedidos
        int cantPedidos = rand.Next(1, 21);     //Cantidad de pedidos que pueden aparecer  
        Console.WriteLine($"\nCantidad de pedidos en total: {cantPedidos}");
        for (int i = 0; i < cantPedidos; i++)
        {
            repetido_cli = 0;       //Inicializo la variable en 0

            int posicion2 = rand.Next(leer.Length);     //Obtengo la información de una parsona del archivo de nombres al azar
            var eleccion_cli = (leer[posicion2]).Split(", ");       //Trato la información como arreglo

            if (listaPedidos.Count != 0)        //Me pregunto si la lista de pedidos está vacía o no
            {
                foreach (var item in listaPedidos)      //Si no está vacía, la recorro 
                {
                    if (eleccion_cli[0] == item.Costumer.Nombre)        //Pregunto si se repitió la información obtenida
                    {
                        repetido_cli = 1;       //La variable en 1 indica que se repitió
                    }
                }

                if (repetido_cli == 0)      //Pregunto si se repitió la información
                {
                    ClientePedido(listaPedidos, i + 1, eleccion_cli);       //Si no se repitió, creo el pedido a partir de la información recopilada
                }
                else
                {
                    i -= 1;     //Si se repitió, se repite todo lo anterior
                }
            }
            else
            {
                ClientePedido(listaPedidos, i + 1, eleccion_cli);       //Si la lista está vacía, inserto la información
            }
        }

        ///////////////////////////////////////////////////////////////////
        char conf;      //Variable para manejar la interfaz principal
        do
        {
            Console.WriteLine("\n\nQue acción desea llevar a cabo");
            Console.WriteLine("Mostrar información: \'1\'");
            Console.WriteLine("Dar de alta un pedido: \'2\'");
            Console.WriteLine("Asignar un pedido a un cadete: \'3\'");
            Console.WriteLine("Cambiar de estado un pedido: \'4\'");
            Console.WriteLine("Cambiar de cadete asignado un pedido: \'5\'");
            Console.WriteLine("Parar programa: \'Q\'");
            conf = Console.ReadKey().KeyChar;

            switch (conf)
            {
                case '1':
                    MostrarInfo(cadeteria);
                    MostrarPedidosNoAsignados(listaPedidos);
                    break;

                case '2':
                    break;

                case '3':
                    if (listaPedidos.Count != 0)
                    {
                        AsignarPedido(listaPedidos, cadeteria.Cadetes);
                    }
                    else
                    {
                        Console.WriteLine("\nNo hay pedidos sin asignar");
                    }
                    break;

                case '4':
                    break;

                case '5':
                    break;

                case 'q':
                case 'Q':
                    break;

                default:
                    Console.WriteLine("\nElección inválida");
                    break;
            }
        } while (conf != 'q' && conf != 'Q');
    }

    //****************************************************************FUNCIONES
    private static void CambiarDeCadete() { }

    private static void CambiarDeEstado() { }

    private static void AsignarPedido(List<Pedido> pedidosNoAsignados, List<Cadete> cadetes)
    {
        char verif;
        int repe = 0;
        do
        {
            if (repe > 0)       //Si ya se ha realizado un proceso de asignación, pregunta lo siguiente
            {
                Console.WriteLine("\n¿Desea realizar otra asignación?");
                Console.WriteLine("\'1\' para confirmar");
                verif = Console.ReadKey().KeyChar;

                if (verif != '1')
                {
                    break;
                }
            }

            Console.Write("\nSelecciones el número de pedido a asignar: ");
            int nroPedido = Convert.ToInt32(Console.ReadLine());        //Selección del pedido a asignar
            Pedido pedidoSeleccionado = null;
            foreach (var item in pedidosNoAsignados)
            {
                if (nroPedido == item.NroPedido)
                {
                    pedidoSeleccionado = item;
                }
            }

            if (pedidoSeleccionado != null)
            {
                Console.Write("\nSeleccione el ID del cadete a asignar el pedido: ");
                int idCadete = Convert.ToInt32(Console.ReadLine());
                Cadete cadeteSeleccionado = null;
                foreach (var item in cadetes)
                {
                    if (idCadete == item.Id)
                    {
                        cadeteSeleccionado = item;
                    }
                }

                if (cadeteSeleccionado != null)
                {
                    Console.WriteLine($"\n¿Está seguro de querer asignar el pedido Nro {pedidoSeleccionado.NroPedido} a nombre de {pedidoSeleccionado.Costumer.Nombre} al cadete {cadeteSeleccionado.Nombre}?");
                    Console.WriteLine("\'1\' para confirmar");
                    verif = Console.ReadKey().KeyChar;

                    if (verif == '1')
                    {
                        cadeteSeleccionado.Pedidos.Add(pedidoSeleccionado);
                        pedidosNoAsignados.Remove(pedidoSeleccionado);
                        Console.WriteLine("\nAsignación exitosa");
                    }
                    else
                    {
                        Console.WriteLine("\nAsignación cancelada");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo existe tal cadete");
                }
            }
            else
            {
                Console.WriteLine("\nNo existe tal pedido");
            }

            repe++;
        } while (true);
    }

    private static void DarDeAlta() { }

    private static void MostrarPedidosNoAsignados(List<Pedido> pedidos)
    {
        Console.WriteLine("\nPedidos no asignados:");
        if (pedidos.Count != 0)
        {
            foreach (var item in pedidos)
            {
                Console.WriteLine($"\n    Nro de pedido: {item.NroPedido}");
                Console.WriteLine($"    Observaciones: {item.Observaciones}");
                Console.WriteLine($"    Estado: {item.Estado}");
                Console.WriteLine($"    Nombre del cliente: {item.Costumer.Nombre}");
                Console.WriteLine($"    Teléfono: {item.Costumer.Telefono}");
                Console.WriteLine($"    Dirección: {item.Costumer.Direccion}");
                Console.WriteLine($"    Datos de referencia de la dirección: {item.Costumer.DatosReferenciaDireccion}");
            }
        }
        else
        {
            Console.WriteLine("    No hay pedidos sin asignar");
        }
    }
    private static void MostrarInfo(Cadeteria cadeteria)
    {
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
            if (item_1.Pedidos.Count != 0)
            {
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
            else
            {
                Console.WriteLine("        Este cadete no tiene ningún pedido asignado");
            }
        }
    }

    private static void CrearCadete(List<Cadete> listaCadetes, int h, string[] eleccion_cad)
    {
        var cad = new Cadete(h + 1, eleccion_cad);
        listaCadetes.Add(cad);
    }

    private static void ClientePedido(List<Pedido> listaPedidos, int i, string[] eleccion)
    {
        Console.WriteLine($"\nCliente {i}");
        var cli = new Cliente(i, eleccion);
        var ped = new Pedido(i, 1, cli);
        listaPedidos.Add(ped);
    }
}