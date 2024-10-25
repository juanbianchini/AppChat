// Establece un cliente que envía paquetes a un servidor y recibe paquetes de ese servidor

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace ClienteChatUDP
{
    public partial class ClienteChatFormUDP : Form
    {
        public ClienteChatFormUDP()
        {
            InitializeComponent();
        } //fin del constructor 

        private UdpClient cliente;
        private IPEndPoint puntoRecepcion;

        //inicializa las variables y el subproceso para recibir paquetes
        private void ClienteChatFormUDP_Load(object sender, EventArgs e)
        {
            cliente = new UdpClient(50001);
            puntoRecepcion = new IPEndPoint(new IPAddress(0), (0));
            Thread subproceso = new Thread(new ThreadStart(EsperarPaquetes));
            subproceso.Start();
        }
        //fin del método ClienteChatFormUDP_Load

        //cierra el cliente
        private void ClienteChatFormUDP_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }
        //fin del método ClienteChatFormUDP_FormClosing

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

        //envía un paquete 
        private void entradaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //crea un paquete (datagrama) como objeto string 
                string paquete = entradaTextBox.Text;
                mostrarTextBox.Text += "\r\nEnviando paquete que contiene: " + paquete;

                //convierte el paquete en arreglo de bytes
                byte[] datos = System.Text.Encoding.ASCII.GetBytes(paquete);

                //envía el paquete al servidor en el puerto 50000
                cliente.Send(datos, datos.Length, "127.0.0.1", 50000);
                mostrarTextBox.Text += "\r\nPaquete enviado\r\n";
                entradaTextBox.Clear();
            }
            //fin del if
        }
        //fin del método entradaTextBox_KeyDown

        //espera a lleguen los paquetes 
        public void EsperarPaquetes()
        {
            while (true)
            {
                //recibe arreglo de bytes del servidor
                byte[] datos = cliente.Receive(ref puntoRecepcion);

                //envía el paquete de datos al control TextBox
                MostrarMensaje("\r\nPaquete recibido:" + System.Text.Encoding.ASCII.GetString(datos) + "\r\n");
            }
            //fin del while
        }
        //fin del método EsperarPaquetes
    }
    //fin de la clase ClienteChatFormUDP
}
