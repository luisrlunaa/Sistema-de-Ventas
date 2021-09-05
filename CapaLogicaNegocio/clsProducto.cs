﻿using CapaEnlaceDatos;
using System;
using System.Collections.Generic;
using System.Data;

namespace CapaLogicaNegocio
{
    public class Producto
    {
        public int m_IdP { get; set; }
        public int m_IdCategoria { get; set; }
        public string m_Producto { get; set; }
        public string m_Marca { get; set; }
        public string m_tipoGoma { get; set; }
        public int m_Stock { get; set; }
        public decimal m_PrecioCompra { get; set; }
        public decimal m_PrecioVenta { get; set; }
        public decimal m_Preciomin { get; set; }
        public decimal m_Preciomax { get; set; }
        public decimal m_itbis { get; set; }
        public DateTime m_FechaVencimiento { get; set; }
        public DateTime m_FechaModificacion { get; set; }
    }
    public class clsProducto
    {
        private clsManejador M = new clsManejador();

        private Int32 m_IdP;
        private Int32 m_IdCategoria;
        private String m_Producto;
        private String m_Marca;
        private String m_tipoGoma;
        private Int32 m_Stock;
        private decimal m_PrecioCompra;
        private decimal m_PrecioVenta;
        private decimal m_itbis;
        private DateTime m_FechaVencimiento;

        public Int32 IdP
        {
            get { return m_IdP; }
            set { m_IdP = value; }
        }

        public Int32 IdCategoria
        {
            get { return m_IdCategoria; }
            set { m_IdCategoria = value; }
        }

        public String Producto
        {
            get { return m_Producto; }
            set { m_Producto = value; }
        }

        public String Marca
        {
            get { return m_Marca; }
            set { m_Marca = value; }
        }
        public String tipoGoma
        {
            get { return m_tipoGoma; }
            set { m_tipoGoma = value; }
        }

        public Int32 Stock
        {
            get { return m_Stock; }
            set { m_Stock = value; }
        }

        public decimal PrecioCompra
        {
            get { return m_PrecioCompra; }
            set { m_PrecioCompra = value; }
        }

        public decimal PrecioVenta
        {
            get { return m_PrecioVenta; }
            set { m_PrecioVenta = value; }
        }
        public decimal itbis
        {
            get { return m_itbis; }
            set { m_itbis = value; }
        }

        public DateTime FechaVencimiento
        {
            get { return m_FechaVencimiento; }
            set { m_FechaVencimiento = value; }
        }

        public DateTime FechaModificacion
        {
            get { return m_FechaVencimiento; }
            set { m_FechaVencimiento = value; }
        }

        public DataTable Listar()
        {
            return M.Listado("ListadoProductos", null);
        }

        public DataTable BusquedaProductos(String objDatos)
        {
            DataTable dt = new DataTable();
            List<clsParametro> lst = new List<clsParametro>();
            try
            {
                lst.Add(new clsParametro("@Datos", objDatos));
                dt = M.Listado("FiltrarDatosProducto", lst);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public String RegistrarProductos()
        {
            List<clsParametro> lst = new List<clsParametro>();
            String Mensaje = "";

            try
            {
                lst.Add(new clsParametro("@IdCategoria", m_IdCategoria));
                lst.Add(new clsParametro("@Nombre", m_Producto));
                lst.Add(new clsParametro("@Marca", m_Marca));
                lst.Add(new clsParametro("@Stock", m_Stock));
                lst.Add(new clsParametro("@PrecioCompra", m_PrecioCompra));
                lst.Add(new clsParametro("@PrecioVenta", m_PrecioVenta));
                lst.Add(new clsParametro("@FechaVencimiento", m_FechaVencimiento.ToString("dd/MM/yyyy")));
                M.EjecutarSP("RegistrarProducto", ref lst);
                Mensaje = lst[7].Valor.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Mensaje;
        }

        public String ActualizarProductos()
        {
            List<clsParametro> lst = new List<clsParametro>();
            String Mensaje = "";

            try
            {
                lst.Add(new clsParametro("@IdProducto", m_IdP));
                lst.Add(new clsParametro("@IdCategoria", m_IdCategoria));
                lst.Add(new clsParametro("@Nombre", m_Producto));
                lst.Add(new clsParametro("@Marca", m_Marca));
                lst.Add(new clsParametro("@Stock", m_Stock));
                lst.Add(new clsParametro("@PrecioCompra", m_PrecioCompra));
                lst.Add(new clsParametro("@PrecioVenta", m_PrecioVenta));
                lst.Add(new clsParametro("@FechaVencimiento", m_FechaVencimiento.ToString("dd/MM/yyyy")));
                M.EjecutarSP("ActualizarProducto", ref lst);
                Mensaje = lst[8].Valor.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Mensaje;
        }
    }
}