﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;

namespace SAC.metodos
{
    public class metodosOdontograma
    {
        consulta.consulta consultar = new consulta.consulta();
        conexion.conexion con = new conexion.conexion();

        public void agregarOdontograma(String col, String die, String secc)
        {
            consultar.ejecutar_consulta("INSERT INTO `bd_sac`.`tbl_odontograma` (`colorOdontograma`, `dienteOdontograma`, `seccionOdontograma`) VALUES ('" + col + "', '" + die + "', '" + secc + "');", con.abrir_conexion()).ExecuteNonQuery();
            con.cerrar_Conexion();
        }

        public DataTable Paciente()
        {
            string consulta = "SELECT cedulaPaciente, nombre1Paciente , apellido1Paciente, apellido2Paciente FROM tbl_paciente";
            MySqlCommand comando = new MySqlCommand(consulta, con.abrir_conexion());
            MySqlDataAdapter da = new MySqlDataAdapter(comando);
            using (DataTable dt = new DataTable())
            {
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable TratamientosRealizados(string numexpediente)
        {
            string consulta = "select fechaExpedienteTratamiento,tratamientoExpedienteTratamiento,piezaExpedienteTratamiento,descripcionExpedienteTratamiento from bd_sac.tbl_expedientetramiento where codigoExpediente='" + numexpediente + "';";
            MySqlCommand comando = new MySqlCommand(consulta, con.abrir_conexion());
            MySqlDataAdapter da = new MySqlDataAdapter(comando);
            using (DataTable dt = new DataTable())
            {
                da.Fill(dt);
                return dt;
            }
        }



        public string NumTipoTratamiento(string nombreTratamiento)
        {
            string codigo = "";
            MySqlDataReader existencia = consultar.ejecutar_consulta("select codigoTipoTratamiento from bd_sac.tbl_tipotratamiento where  nombreTipoTratamiento='" + nombreTratamiento + "';", con.abrir_conexion()).ExecuteReader();
            if (existencia.Read())
            {
                codigo = existencia.GetString(0);
            }
            else
            {

            }
            return codigo;
        }
        public DataTable TiposdeTratamientos()
        {
            DataTable dt = new DataTable();
            string consulta = "select nombreTipoTratamiento from bd_sac.tbl_tipotratamiento;";
            MySqlCommand comando = new MySqlCommand(consulta, con.abrir_conexion());
            MySqlDataAdapter da = new MySqlDataAdapter(comando);
            da.Fill(dt);
            return dt;
        }
        public int obtenerPrecio(string tratamiento)
        {
            int numero = 0;
            string precio = "";

            MySqlDataReader existencia = consultar.ejecutar_consulta("select precioTratamiento from bd_sac.tbl_tratamiento where nombreTratamiento='" + tratamiento + "';", con.abrir_conexion()).ExecuteReader();
            if (existencia.Read())
            {
                precio = existencia.GetString(0);
            }
            else
            {

            }
            numero = Int32.Parse(precio);

            return numero;
        }
        public int buscarExpediente(int codigoE)
        {
            int estado = 0; string resultado = "";
            MySqlDataReader existencia = consultar.ejecutar_consulta("select codigoExpediente from tbl_expedienteodontograma where codigoExpediente = '" + codigoE + "'; ", con.abrir_conexion()).ExecuteReader();
            while (existencia.Read())
            {
                resultado = existencia.GetString(0);
            }
            if (resultado != "")
            {
                estado = 1;
            }

            return estado;
        }
        public void agregarOdontograma2(String marc)
        {

            consultar.ejecutar_consulta("INSERT INTO `bd_sac`.`tbl_odontograma` (`marcaOdontograma`) VALUES ('" + marc + "');", con.abrir_conexion()).ExecuteNonQuery();
            con.cerrar_Conexion();
        }
        public String[] buscarOdontograma(String odo)
        {
            string[] stringArray1 = new string[4];
            MySqlDataReader actualizar = consultar.ejecutar_consulta("SELECT colorOdontograma, dienteOdontograma,seccionOdontograma,marcaOdontograma FROM bd_sac.tbl_odontograma where codigoOdontograma = '" + odo + "'; ", con.abrir_conexion()).ExecuteReader();
            while (actualizar.Read())
            {
                //stringArray1[0] = actualizar.GetString(0);
                //stringArray1[1] = actualizar.GetString(1);
                //stringArray1[2] = actualizar.GetString(2);
                stringArray1[3] = actualizar.GetString(3);
            }
            con.cerrar_Conexion();
            return stringArray1;
        }
        public string codigoTratamiento(string nombreT)
        {
            string codi = "";

            MySqlDataReader existencia = consultar.ejecutar_consulta("select codigoTratamiento from bd_sac.tbl_tratamiento where nombreTratamiento='" + nombreT + "';", con.abrir_conexion()).ExecuteReader();
            if (existencia.Read())
            {
                codi = existencia.GetString(0);
            }

            return codi;

        }
        public void agregarPacienteTratamiento(int codigoEx, string tratamiento, string fecha, string tratamientorealizado, string pieza, string descripcion)
        {
            consultar.ejecutar_consulta("INSERT INTO `bd_sac`.`tbl_expedientetramiento` (`codigoExpediente`, `codigoTratamiento`, `fechaExpedienteTratamiento`, `tratamientoExpedienteTratamiento`, `piezaExpedienteTratamiento`, `descripcionExpedienteTratamiento`) VALUES('" + codigoEx + "', '" + tratamiento + "', '" + fecha + "', '" + tratamientorealizado + "','" + pieza + "','" + descripcion + "');", con.abrir_conexion()).ExecuteNonQuery();
            con.cerrar_Conexion();

        }
    }
}