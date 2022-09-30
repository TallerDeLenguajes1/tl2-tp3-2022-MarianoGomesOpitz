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
        int cantCadetes = rand.Next(5, 11);      //Cantidad de cadetes que pueden aparecer
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


        var listaPedidos = new List<Pedido>();      //Creo la lista de pedidos

        ///////////////////////////////////////////////////////////////////
        char conf;      //Variable para manejar la interfaz principal
        int pedidosCreados = 1;
        Console.WriteLine($"\nBienvenido a la interfaz de consola de la cadetería {cadeteria.Nombre}");
        do
        {
            Console.WriteLine("\n\n¿Qué acción desea llevar a cabo?");
            Console.WriteLine("Mostrar información: \'1\'");
            Console.WriteLine("Dar de alta un pedido: \'2\'");
            Console.WriteLine("Asignar un pedido a un cadete: \'3\'");
            Console.WriteLine("Cambiar de estado un pedido: \'4\'");
            Console.WriteLine("Cambiar de cadete asignado un pedido: \'5\'");
            Console.WriteLine("Finalizar jornada laboral: \'F\'");
            conf = Console.ReadKey().KeyChar;

            switch (conf)
            {
                case '1':
                    MostrarInfo(cadeteria);
                    MostrarPedidosNoAsignados(listaPedidos);
                    break;

                case '2':
                    pedidosCreados = DarDeAltaPedidos(listaPedidos, rand, leer, pedidosCreados);
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
                    CambiarDeEstado(cadeteria.Cadetes);
                    break;

                case '5':
                    CambiarDeCadete(cadeteria.Cadetes);
                    break;

                case 'f':
                case 'F':
                    FinDeJornada(cadeteria.Cadetes);
                    break;

                default:
                    Console.WriteLine("\nElección inválida");
                    break;
            }
        } while (conf != 'f' && conf != 'F');
    }

    //****************************************************************FUNCIONES

    private static void FinDeJornada(List<Cadete> listaCadetes)
    {
        int montoTotal = 0, montoPorCadete, pedidosTotales = 0, pedidosPorCadete;

        foreach (var item in listaCadetes)
        {
            montoPorCadete = item.JornalACobrar();
            pedidosPorCadete = montoPorCadete / 300;
            Console.WriteLine($"\nCadete: {item.Nombre}");
            Console.WriteLine($"Paquetes entregados: {pedidosPorCadete}");
            Console.WriteLine($"Dinero ganado: {montoPorCadete}");

            pedidosTotales += pedidosPorCadete;
            montoTotal += montoPorCadete;
        }

        double enviosPromedio = Convert.ToDouble(pedidosTotales) / listaCadetes.Count();

        Console.WriteLine($"\nPedidos entregados en total: {pedidosTotales}");
        Console.WriteLine($"Monto total ganado: {montoTotal}");
        Console.WriteLine($"Promedio de pedidos entregados por cadete: {enviosPromedio}");
    }

    private static void CambiarDeCadete(List<Cadete> cadetes)
    {
        char verif;
        int repe = 0;
        do
        {
            if (repe > 0)       //Si ya se ha realizado un proceso de asignación, pregunta lo siguiente
            {
                Console.WriteLine("\n¿Desea realizar otro cambio de cadete?");
                Console.WriteLine("\'1\' para confirmar");
                verif = Console.ReadKey().KeyChar;

                if (verif != '1')
                {
                    break;
                }
            }

            Console.Write("\nSeleccione el ID del cadete que posee el pedido a cambiar: ");
            int idCadeteBase = Convert.ToInt32(Console.ReadLine());
            Cadete cadeteBase = null;
            foreach (var item in cadetes)
            {
                if (idCadeteBase == item.Id)
                {
                    cadeteBase = item;
                }
            }

            if (cadeteBase != null)
            {
                if (cadeteBase.Pedidos.Count() > 0)
                {
                    Console.Write("\nSeleccione el Nro de pedido a cambiar de cadete: ");
                    int nroPedido = Convert.ToInt32(Console.ReadLine());
                    Pedido pedidoSeleccionado = null;
                    foreach (var item in cadeteBase.Pedidos)
                    {
                        if (nroPedido == item.NroPedido)
                        {
                            pedidoSeleccionado = item;
                        }
                    }

                    if (pedidoSeleccionado != null)
                    {
                        Console.Write("\nSeleccione el ID del cadete que recibirá el pedido: ");
                        int idCadeteACambiar = Convert.ToInt32(Console.ReadLine());
                        Cadete cadeteACambiar = null;
                        foreach (var item in cadetes)
                        {
                            if (idCadeteACambiar == item.Id)
                            {
                                cadeteACambiar = item;
                            }
                        }

                        if (cadeteACambiar != null)
                        {
                            if (cadeteBase != cadeteACambiar)
                            {
                                Console.WriteLine($"\n¿Está seguro que desea cambiar el pedido Nro {pedidoSeleccionado.NroPedido} a nombre de {pedidoSeleccionado.Costumer.Nombre} del cadete {cadeteBase.Nombre} a {cadeteACambiar.Nombre}?");
                                Console.WriteLine("\'1\' para confirmar");
                                verif = Console.ReadKey().KeyChar;

                                if (verif == '1')
                                {
                                    cadeteACambiar.Pedidos.Add(pedidoSeleccionado);
                                    cadeteBase.Pedidos.Remove(pedidoSeleccionado);
                                    Console.WriteLine("\nCambio de cadete exitoso");
                                }
                                else
                                {
                                    Console.WriteLine("\nCambio de cadete cancelado");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nNo se puede cambiar el pedido al mismo cadete");
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
                }
                else
                {
                    Console.WriteLine("\nEl cadete seleccionado no posee ningún pedido seleccionado");
                }
            }
            else
            {
                Console.WriteLine("\nNo existe tal cadete");
            }

            repe++;
        } while (true);
    }

    private static void CambiarDeEstado(List<Cadete> cadetes)
    {
        char verif;
        int repe = 0;
        do
        {
            if (repe > 0)       //Si ya se ha realizado un proceso de asignación, pregunta lo siguiente
            {
                Console.WriteLine("\n¿Desea realizar otro cambio de estado?");
                Console.WriteLine("\'1\' para confirmar");
                verif = Console.ReadKey().KeyChar;

                if (verif != '1')
                {
                    break;
                }
            }

            Console.Write("\nSeleccione el ID del cadete que posee el pedido: ");
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
                if (cadeteSeleccionado.Pedidos.Count() > 0)
                {
                    Console.Write("\nSeleccione el Nro de pedido a cambiar de estado: ");
                    int nroPedido = Convert.ToInt32(Console.ReadLine());
                    Pedido pedidoSeleccionado = null;
                    foreach (var item in cadeteSeleccionado.Pedidos)
                    {
                        if (nroPedido == item.NroPedido)
                        {
                            pedidoSeleccionado = item;
                        }
                    }

                    if (pedidoSeleccionado != null)
                    {
                        Console.WriteLine("\nSeleccione el estado al cual cambiar el pedido:");
                        Console.WriteLine("1: En preparación");
                        Console.WriteLine("2: En camino");
                        Console.WriteLine("3: Entregado");
                        int est = Convert.ToInt32(Console.ReadLine());
                        if (est >= 1 && est <= 3)
                        {
                            if (pedidoSeleccionado.Estado != Convert.ToString((status)est))
                            {
                                Console.WriteLine($"\n¿Está seguro que desea cambiar el estado del pedido {pedidoSeleccionado.NroPedido} de \"{pedidoSeleccionado.Estado}\" a \"{(status)est}\"?");
                                Console.WriteLine("\'1\' para confirmar");
                                verif = Console.ReadKey().KeyChar;

                                if (verif == '1')
                                {
                                    pedidoSeleccionado.Estado = Convert.ToString((status)est);
                                    Console.WriteLine("\nCambio de estado exitoso");
                                }
                                else
                                {
                                    Console.WriteLine("\nCambio de estado cancelado");
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nEl pedido ya posee dicho estado");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nNo existe tal estado");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNo existe tal pedido");
                    }
                }
                else
                {
                    Console.WriteLine("\nEste cadete no tiene ningún pedido asignado");
                }
            }
            else
            {
                Console.WriteLine("\nNo existe dicho cadete");
            }

            repe++;
        } while (true);

    }

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

    private static int DarDeAltaPedidos(List<Pedido> pedidosSinAsignar, Random rand, string[] leer, int pedidosCreados)
    {
        char verif;
        int repe = 0;
        do
        {
            if (repe > 0)       //Si ya se ha realizado un proceso de creación, pregunta lo siguiente
            {
                Console.WriteLine("\n¿Desea crear otro pedido?");
                Console.WriteLine("\'1\' para confirmar");
                verif = Console.ReadKey().KeyChar;

                if (verif != '1')
                {
                    break;
                }
            }

            int repetido = 0;
            do
            {
                int posicion = rand.Next(leer.Length);     //Obtengo la información de una parsona del archivo de nombres al azar
                var eleccion = (leer[posicion]).Split(", ");       //Trato la información como arreglo

                if (pedidosSinAsignar.Count != 0)        //Me pregunto si la lista de pedidos está vacía o no
                {
                    foreach (var item in pedidosSinAsignar)      //Si no está vacía, la recorro 
                    {
                        if (eleccion[0] == item.Costumer.Nombre)        //Pregunto si se repitió la información obtenida
                        {
                            repetido = 1;       //La variable en 1 indica que se repitió
                        }
                    }

                    if (repetido == 0)      //Pregunto si se repitió la información
                    {
                        ClientePedido(pedidosSinAsignar, pedidosCreados, eleccion);       //Si no se repitió, creo el pedido a partir de la información recopilada
                        pedidosCreados++;
                    }
                }
                else
                {
                    ClientePedido(pedidosSinAsignar, pedidosCreados, eleccion);       //Si la lista está vacía, inserto la información
                    pedidosCreados++;
                }
            } while (repetido == 1);

            repe++;
        } while (true);

        return (pedidosCreados);
    }

    private static void MostrarPedidosNoAsignados(List<Pedido> pedidos)
    {
        Console.WriteLine("\nPedidos no asignados:");
        if (pedidos.Count > 0)
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
        Console.WriteLine($"\nNombre de la cadetería: {cadeteria.Nombre}");
        Console.WriteLine($"Teléfono: {cadeteria.Telefono}");
        Console.WriteLine($"Cadetes:");
        foreach (var item_1 in cadeteria.Cadetes)
        {
            Console.WriteLine($"\n    ID: {item_1.Id}");
            Console.WriteLine($"    Nombre: {item_1.Nombre}");
            Console.WriteLine($"    Teléfono: {item_1.Telefono}");
            Console.WriteLine($"    Dirección: {item_1.Direccion}");
            Console.WriteLine($"    Pedidos asignados:");
            if (item_1.Pedidos.Count > 0)
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
        Console.WriteLine("\nPedido creado exitosamente");
    }
}