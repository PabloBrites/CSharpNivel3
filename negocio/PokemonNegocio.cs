using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; // 1) proporciona las clases y métodos necesarios para interactuar con bases de datos SQL Server desde una aplicación C#.
using dominio;

namespace negocio
{
    public class PokemonNegocio //Logica de negocio/ Conexion a base de datos // esta es la clase en la cual yo voy a crear los metodos de acceso a datos para los pokemons, hacemos los metodos que se conectan a la base de datos.
    {
        public List<Pokemon> listar() //esto es un método que devuelve una lista de objetos 'pokemon'/ el metodo tiene que devolver algo, este metodo lee registros de la base de datos, y devuelve varios registros, varios pokemons que yo los puedo agrupar en una lista, una lista de pokemons va a devolver. 
        {
            List<Pokemon> lista = new List<Pokemon>(); //Se crea la lista para poder almacenar y agrupar todos los pokemons que se leen de la base de datos en un solo objeto List<Pokemon>
            SqlConnection conexion = new SqlConnection(); // 2) crea una instancia de conexión a una base de datos SQL Server, pero sin configurarla aún.
            SqlCommand comando = new SqlCommand(); // 3) se utiliza para realizar acciones sobre una base de datos SQL Server, como ejecutar consultas SQL (SELECT, INSERT, UPDATE, DELETE) o comandos almacenados.
            SqlDataReader lector; // 4) es una clase que se utiliza para leer los datos que se obtienen como resultado de ejecutar una consulta SQL

            try
            {
                conexion.ConnectionString = "server =DESKTOP-2NIT0OI\\SQLEXPRESS; database =POKEDEX_DB; integrated security=true";                                                                                                                                                  //configurar la cadena de conexion y aclararle a donde me voy a conectar (esta seria la direccion de tu motor de base de datos local DESKTOP-2NIT0OI\\SQLEXPRESS") osea tu pc.
                comando.CommandType = System.Data.CommandType.Text;                                                                                                                                                                                                                 //primero le decimos a que servidor te vas a conectar osea la pc de tu casa y segundo le vamos a decir a que base de datos osea (database =POKEDEX_DB" <-- el nombre de tu base de datos)
                comando.CommandText = "select Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id from POKEMONS P, ELEMENTOS E, ELEMENTOS D where E.Id = P.IdTipo and D.Id = P.IdDebilidad And P.Activo = 1";      // y lo proximo es "como" me voy a conectar, integrated security=true, ignifica que la conexión a la base de datos usará las credenciales del usuario que está actualmente conectado al
                comando.Connection = conexion;                                                                                                                                                                                                                                      // sistema operativo (Windows) para autenticar la conexión. No necesitas proporcionar un nombre de usuario y una contraseña adicionales.
                                                                                                                                                                                                                                                                                    // comando.CommandText = aca le ponemos un texto, y el texto va a ser la consulta sql "Select Numero, Nombre, Descripcion From POKEMONS"; (la misma que hacemos en el sql)          
                conexion.Open(); //abro la conexion                                                                                                                                                                                                                                 //Cuando pones comando.Connection = conexion;, le estás diciendo al comando que use la conexión que ya configuraste para hablar con la base de datos. Sin esta línea, el comando no 
                lector = comando.ExecuteReader();                                                                                                                                                                                                                                   //sabría a qué base de datos conectarse para hacer la consulta que le dijiste (en comando.CommandText).

                while (lector.Read()) //si hay un registro a continuacion, devuelve true y entra al while
                {
                    Pokemon aux = new Pokemon(); //me creo un pokemon aux, auxiliar. y lo voy a empezar a cargar con los datos del lector de ese registro, los datos son numero, nombre, descripcion, los de la linea de abajo. 
                    aux.Id = (int)lector["Id"];
                    aux.Numero = lector.GetInt32(0);//el int32 es el int comun, y el cero es el indice. 
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    if(!(lector.IsDBNull(lector.GetOrdinal("UrlImagen"))))
                        aux.UrlImagen = (string)lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)lector["idTipo"];
                    aux.Tipo.Descripcion = (string)lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)lector["idDebilidad"]; 
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];
                    lista.Add(aux); // finalmente agrego ese pokemon a la lista

                }
                
                conexion.Close();//cierro la conexion 
                return lista; //retorna la lista
            }
            catch (Exception ex)
            {

                throw ex;
            }


            
        }

        public void agregar(Pokemon nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into POKEMONS (Numero, Nombre, Descripcion, Activo, IdTipo, IdDebilidad, UrlImagen) values(" + nuevo.Numero + ", '" + nuevo.Nombre + "', '" + nuevo.Descripcion + "', 1, @idTipo, @idDebilidad, @urlImagen)");
                datos.setearParametro("@idTipo", nuevo.Tipo.Id);
                datos.setearParametro("@idDebilidad", nuevo.Debilidad.Id);
                datos.setearParametro("urlImagen", nuevo.UrlImagen);
                datos.ejecutarAccion();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar (Pokemon poke)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update POKEMONS set Numero = @numero, Nombre = @nombre, Descripcion = @desc, UrlImagen = @img, IdTipo = @idTipo, IdDebilidad = @idDebilidad Where Id = @id ");
                datos.setearParametro("@numero", poke.Numero);
                datos.setearParametro("@nombre", poke.Nombre);
                datos.setearParametro("@desc", poke.Descripcion);
                datos.setearParametro("@img", poke.UrlImagen);
                datos.setearParametro("@idTipo", poke.Tipo.Id);
                datos.setearParametro("@idDebilidad", poke.Debilidad.Id);
                datos.setearParametro("@id", poke.Id);

                datos.ejecutarAccion();
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from POKEMONS where id = @id");
                datos.setearParametro("id", id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void eliminarLogico(int id)
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("update POKEMONS set Activo = 0 where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

//Resumen de las Funciones

//List<Pokemon>: Almacena y agrupa los resultados.
//SqlConnection: Establece la conexión con la base de datos.
//SqlCommand: Ejecuta comandos SQL.
//SqlDataReader: Lee los datos resultantes de la consulta SQL.
//conexion.Open(): abre la conexion                                                                                                 
//lector = comando.ExecuteReader(): El SqlDataReader (lector) te permite
//recorrer los resultados de la consulta fila por fila, y acceder a los valores de las columnas de esas filas.

//que hace tu clase? 
//La clase PokemonNegocio es responsable de la lógica de negocio.
//Se encarga de operaciones como la obtención de datos de la base
//de datos la manipulación de datos, y la lógica empresarial 