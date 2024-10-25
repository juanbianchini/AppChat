// Establece un servidor que recibe paquetes de un cliente y los envía de vuelta al cliente

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;

namespace ServidorChatUDP
{
    public partial class ServidorChatFormUDP : Form
    {
        public ServidorChatFormUDP()
        {
            InitializeComponent();
        } //fin del constructor

        private UdpClient cliente;
        private IPEndPoint puntoRecepcion;

        //inicializa las variables y el subproceso para recibir paquetes 
        private void ServidorChatFormUDP_Load(object sender, EventArgs e)
        {
            cliente = new UdpClient(50000);
            puntoRecepcion = new IPEndPoint(new IPAddress(0), (0));
            Thread lecturaThread = new Thread(new ThreadStart(EsperarPaquetes));
            lecturaThread.Start();
        }
        //fin del método ServidorChatFormUDP_Load

        //cierra el servidor
        private void ServidorChatFormUDP_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }
        //fin del método ServidorChatFormUDP_FormClosing

        //delegado que permite llamar al método MostrarMensaje en el subproceso que crea y mantiene la GUI
        private delegate void DisplayDelegate(string message);

        //el método MostrarMensaje establece la propiedad text de mostrarTextBox de una manera segura para el proceso
        private void MostrarMensaje(string mensaje)
        {
            //si la modificación de mostrarTextBox no es segura para el subproceso
            if (mostrarTextBox.InvokeRequired)
            {
                //usa un método heredado de Invoke para ejecutar MostarMensaje a través de un delegado
                Invoke(new DisplayDelegate(MostrarMensaje), new object[] { mensaje });
            }
            //fin del if

            //si se puede modificar mostrarTextBox en el subproceso actual
            else
                mostrarTextBox.Text += mensaje;
        }
        //fin del método MostrarMensaje

        //espera que llegue un paquete
        public void EsperarPaquetes()
        {
            while (true)
            {
                //prepara el paquete
                byte[] datos = cliente.Receive(ref puntoRecepcion);
                MostrarMensaje("\r\nSe recibió paquete:" +  "\r\nLongitud: " + datos.Length + "\r\nContenido: " + System.Text.Encoding.ASCII.GetString(datos));

                //devuelve (eco) la información del paquete de vuelta al cliente
                MostrarMensaje("\r\n\r\nEnviando de vuelta al cliente...");
                cliente.Send(datos, datos.Length, puntoRecepcion);
                MostrarMensaje("\r\nPaquete enviado\r\n");
            }
            //fin del while 
        }
        //fin del método EsperarPaquetes
    }
    //fin de la clase ServidorChatFormUDP
}
