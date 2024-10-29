// Establece un cliente que enviará y leerá información de un servidor

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
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace ClienteChatTCP
{
    public partial class ClienteChatForm : Form
    {
        private ChatHistoryManager chatHistoryManager; // Definir la instancia aquí
        public ClienteChatForm()
        {
            InitializeComponent();

            // Configuración de MongoDB en el constructor
            var connectionString = "mongodb://localhost:27017"; // Ajusta según tu configuración
            var databaseName = "ChatDatabase";
            var collectionName = "ChatHistory";
            chatHistoryManager = new ChatHistoryManager(connectionString, databaseName, collectionName);

        } //fin del constructor

        private Thread lecturaThread; //Thread para procesar los mensajes entrantes 
        private NetworkStream salida; //flujo de datos de red
        private BinaryWriter escritor; //facilita la escritura del flujo
        private BinaryReader lector; // facilita la lectura del flujo 
        private string mensaje = "";

        //inicializa el subproceso para lectura
        private void ClienteChatForm_Load(object sender, EventArgs e)
        {
            lecturaThread = new Thread(new ThreadStart(EjecutarCliente));
            lecturaThread.Start();
        }
        //fin del método ClienteChatForm_Load

        //cierra todos los subprocesos asociados con esta aplicación
        private void ClienteChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }
        //fin del método ClienteChatForm_FormClosing

        //delegado que permite que se haga una llamada al método MostrarMensaje en el subproceso que crea y mantiene la GUI
        private delegate void DisplayDelegate(string mensaje);

        //el método DisplayDelegate establece la propiedad Text de mostrarTextBox en forma segura para los subprocesos
        private void MostrarMensaje(string mensaje)
        {
            //si la modificación de mostrarTextBox no es segura para el subproceso actual
            if (mostrarTextBox.InvokeRequired)
            {
                //usa el método Invoke heredado para ejecutar MostrarMensaje a través de un delegado
                Invoke(new DisplayDelegate(MostrarMensaje),
                    new object[] { mensaje });
            }
            //se puede modificar mostrarTextBox en el subproceso actual
            else
                mostrarTextBox.Text += mensaje;
        }
        //fin del método MostrarMensaje

        //delegado que permite llamar al método DeshabilitarEntrada en el subproceso que crea y mantiene la GUI
        private delegate void DisableInputDelegate(bool value);
        //el método DeshabilitarEntrada establece la propiedad ReadOnly de entradaTextBox de una manera segura para los subprocesos
        private void DeshabilitarEntrada(bool valor)
        {
            //si la modificación de entrarTextBox no es segura para el subproceso actual
            if (entradaTextBox.InvokeRequired)

            {
                //usa el método heredado Invoke para ejecutar DeshabilitarEntrada a través de un delegado
                Invoke(new DisableInputDelegate(DeshabilitarEntrada),
                    new object[] { valor });
            }
            //se puede modificar entradaTextBox en el subproceso actual
            else
                entradaTextBox.ReadOnly = valor;
        }
        //fin del método DeshabilitarEntrada

        private void nombreTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               
                if (!string.IsNullOrWhiteSpace(nombreTextBox.Text))
                {
                    entradaTextBox.ReadOnly = false; // Habilita el cuadro de entrada de texto
                    entradaTextBox.Focus(); // Enfoca el cuadro de entrada
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese un nombre válido.");
                }
            }
        }


        //envía al servidor el texto escrito en el cliente
        private async void entradaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //envía el texto al servidor
            try
            {
                if (e.KeyCode == Keys.Enter && entradaTextBox.ReadOnly == false)
                {
                    // Obtener el nombre del cliente
                    string nombreCliente = nombreTextBox.Text;
                    // Enviar el mensaje al servidor
                    escritor.Write(nombreCliente + ">>> " + entradaTextBox.Text);
                    mostrarTextBox.Text += $"\r\n{nombreCliente}>>> " + entradaTextBox.Text;
                    // Guardar el mensaje en MongoDB
                    await chatHistoryManager.SaveMessageAsync(nombreCliente, entradaTextBox.Text, DateTime.Now);
                    entradaTextBox.Clear();

                    //borra la entrada del usuario
                }
                //fin del if
            }
            //fin del try
            catch (SocketException)
            {
                mostrarTextBox.Text += "\nError al escribir objeto";
            }
            //fin del catch
        }
        //fin del método entradaTextBox_KeyDown

        //se conecta al servidor y muestra el texto generado por el servidor
        public void EjecutarCliente()
        {
            //crea una instancia de TcpClient para enviar datos al servidor
            TcpClient cliente;

            try
            {
                MostrarMensaje("Tratando de conectar\r\n");

                //Paso 1: crear TcpClient y conectar al servidor
                cliente = new TcpClient();
                cliente.Connect("192.168.0.14", 50000);

                //Paso 2: obtener NetWorkStream asociado con TcpClient
                salida = cliente.GetStream();

                //crea objetos para escribir y leer a través del flujo
                escritor = new BinaryWriter(salida);
                lector = new BinaryReader(salida);

                MostrarMensaje("\r\nSe recibieron flujos de E/S\r\n");
                DeshabilitarEntrada(false);
                //habilita entradaTextBox

                //itera hasta que el servidor indica terminación
                do
                {
                    //Paso 3: fase de procesamiento
                    try
                    {
                        //lee mensaje del servidor
                        mensaje = lector.ReadString();
                        MostrarMensaje("\r\n" + mensaje);
                    }
                    //fin del try
                    catch (Exception)
                    {
                        //maneja excepción si hay error al leer datos del servidor 
                        System.Environment.Exit(System.Environment.ExitCode);
                    }
                    //fin del catch
                } while (mensaje != "SERVIDOR>>> TERMINAR");

                //Paso 4: cierra la conexión
                escritor.Close();
                lector.Close();
                salida.Close();
                cliente.Close();
                Application.Exit();
            }
            //fin del try
            catch(Exception error)
            {
                //maneja excepción si hay error al establecer la conexión
                MessageBox.Show(error.ToString(), "Error en la conexión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(System.Environment.ExitCode);   
            }
            //fin del catch
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //fin del método EjecutarCliente
    }
    //fin de la clase ClienteChatForm
}
