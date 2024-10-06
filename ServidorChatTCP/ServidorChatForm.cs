// Establece un servidor que recibe una conexión de un cliente, le envía una cadena, chatea con él y cierra la conexión

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

namespace ServidorChatTCP
{
    public partial class ServidorChatForm : Form
    {
        public ServidorChatForm()
        {
            InitializeComponent();
        } //fin del constructor

        private Socket conexion; //Socket para aceptara una conexión
        private Thread lecturaThread; //Thread para procesar los mensajes entrantes 
        private NetworkStream socketStream; //flujo de datos de red
        private BinaryWriter escritor; //facilita la escritura del flujo
        private BinaryReader lector; // facilita la lectura del flujo 

        //inicializa el subproceso para la lectura
        private void ServidorChatForm_Load(object sender, EventArgs e)
        {
            lecturaThread = new Thread(new ThreadStart(EjecutarServidor));
            lecturaThread.Start();
        }
        //fin del método ServidorChatForm

        //cierra todos los subprocesos asociados con esta aplicación
        private void ServidorChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }
        //fin del método CharServerForm_FormClosing

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

        //envía al cliente el texto escrito en el servidor
        private void entradaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            //envía el texto al cliente
            try
            {
                if (e.KeyCode == Keys.Enter && entradaTextBox.ReadOnly == false)
                {
                    escritor.Write("SERVIDOR>>> " + entradaTextBox.Text);
                    mostrarTextBox.Text += "\r\nSERVIDOR>>> " + entradaTextBox.Text;

                    //si el usuario en el servidor indico la terminación de la conexión con el cliente 
                    if (entradaTextBox.Text == "TERMINAR")
                        conexion.Close();

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

        //permite que un cliente se conecte; muestra el texto que envía el cliente
        public void EjecutarServidor()
        {
            TcpListener oyente;
            int contador = 1;

            //espera la conexión con un cliente y muestra el texto que envía el cliente 
            try
            {
                //Paso 1: crea TcpListener
                IPAddress local = IPAddress.Parse("127.0.0.1");
                oyente = new TcpListener(local, 50000);

                //Paso 2: TcpListener espera la solicitud de conexión
                oyente.Start();

                //Paso 3: establece la conexión con base en la solicitud del cliente 
                while (true)
                {
                    MostrarMensaje("Esperando una conexión\r\n");

                    //acepta una conexión entrante 
                    conexion = oyente.AcceptSocket();

                    //crea objeto NetworkStream asociado con el socket
                    socketStream = new NetworkStream(conexion);

                    //crea objetos para transferir datos a través de un flujo
                    escritor = new BinaryWriter(socketStream);
                    lector = new BinaryReader(socketStream);

                    MostrarMensaje("Conexión " + contador + " recibida.\r\n");

                    //informa que la conexión fue exitosa 
                    escritor.Write("SERVIDOR>>> Conexión exitosa");

                    DeshabilitarEntrada(false);
                    //habilita entradaTextBox

                    string laRespuesta = "";

                    //Paso 4: lee los datos de cadena que envía el cliente
                    do
                    {
                        try
                        {
                            //lee la cadena que se envía al cliente
                            laRespuesta = lector.ReadString();

                            //muestra el mensaje
                            MostrarMensaje("\r\n" + laRespuesta);
                        }
                        //fin del try
                        catch (Exception)
                        {
                            //maneja la excepción si hay error al leer los datos
                            break;
                        }
                        //fin del catch
                    } while (laRespuesta != "CLIENTE>>> TERMINAR" && conexion.Connected);

                    MostrarMensaje("\r\nEl usuario terminó la conexión\r\n");

                    //Paso 5: cierra la conexión
                    escritor.Close();
                    lector.Close();
                    socketStream.Close();
                    conexion.Close();

                    //deshabilita entradaTextBox
                    DeshabilitarEntrada(true);
                    contador++;
                }
                //fin del while
            }
            //fin del try
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
            //fin del catch
        }
        //fin del método EjecutarServidor
    }
    //fin de la clase ServidorChatForm
}
