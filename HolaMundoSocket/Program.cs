using HolaMundoSocket.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HolaMundoSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Iniciando Servidor en puerto {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);
            if (servidor.Iniciar())
            {
                Console.WriteLine("[Info] Servidor iniciado correctamente.");
                while (true)
                {
                    Console.WriteLine("[Info] Esperando Cliente...");
                    Socket socketcliente = servidor.ObtenerCliente();
                    ClienteCom cliente = new ClienteCom(socketcliente);
                    cliente.Escribir("Hola Mundo cliente, dime tu nombre:");
                    string respuesta = cliente.Leer();
                    Console.WriteLine("[Cliente] {0}", respuesta);
                    cliente.Escribir("Chao, loh vimoh " + respuesta);
                    cliente.Desconectar();
                }
            } else
            {
                Console.WriteLine("[Error] El puerto {0} está en uso", puerto);
            }
        }
    }
}
