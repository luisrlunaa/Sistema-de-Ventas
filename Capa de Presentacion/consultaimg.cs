using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
namespace Capa_de_Presentacion
{
    public class consultaimg
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=SalesSystem;Integrated Security=True");
        public void verimagen(PictureBox pb, string id)
        {
            con.Open();
            SqlCommand comand = new SqlCommand("select imagen from Producto where IdProducto ='" + id + "'", con);
            SqlDataAdapter dtadapt = new SqlDataAdapter(comand);
            DataSet dts = new DataSet();
            dtadapt.Fill(dts, "img");

            byte[] datos = new byte[0];
            DataRow dr = dts.Tables["img"].Rows[0];
            datos = (byte[])dr["imagen"];
            MemoryStream ms = new MemoryStream(datos);
            pb.Image = System.Drawing.Bitmap.FromStream(ms);
            con.Close();
        }
    }
}
