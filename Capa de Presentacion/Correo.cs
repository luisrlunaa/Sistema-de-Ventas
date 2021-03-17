using EASendMail;
using System;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
    class Correo
    {
        public void enviarCorreo(string emisor, string password, string mensaje, string asunto, string destinatario, string ruta)
        {
            string message = "No se envio el correo correctamente";
            try
            {
                SmtpMail msg = new SmtpMail("Tryit");
                msg.From = emisor;
                msg.To = destinatario;
                msg.Subject = asunto;
                msg.TextBody = mensaje;
                msg.AddAttachment(ruta);

                SmtpServer server = new SmtpServer("smtp.gmail.com");
                server.User = emisor;
                server.Password = password;
                server.Port = 587;
                server.ConnectType = SmtpConnectType.ConnectSSLAuto;

                SmtpClient cliente = new SmtpClient();
                cliente.SendMail(server, msg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(message, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
