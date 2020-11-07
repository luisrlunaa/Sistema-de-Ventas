using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Capa_de_Presentacion
{
	class validar
	{
		public static void sololetras(KeyPressEventArgs v)
		{
			if (char.IsLetter(v.KeyChar))
			{
				v.Handled = false;
			}
			else if (char.IsSeparator(v.KeyChar))
			{
				v.Handled = false;
			}
			else if (char.IsControl(v.KeyChar))
			{
				v.Handled = false;
			}
			else
			{
				v.Handled = true;
			}
		}
		public static void solonumeros(KeyPressEventArgs v)
		{
			if (char.IsNumber(v.KeyChar))
			{
				v.Handled = false;
			}
			else if (char.IsSeparator(v.KeyChar))
			{
				v.Handled = false;
			}
			else if (char.IsControl(v.KeyChar))
			{
				v.Handled = false;
			}
			else if (char.IsPunctuation(v.KeyChar))
			{
				v.Handled = false;
			}
			else
			{
				v.Handled = true;
			}
		}
	}
}
