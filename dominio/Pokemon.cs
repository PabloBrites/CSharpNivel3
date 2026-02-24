using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pokemon //nombre de la clase, clase que me define el objeto que voy a utilizar
    {
        //aca le damos "forma" a nuestra clase de modelo
        //propiedades
        public int Id { get; set; }
        [DisplayName("Número")]
        public int Numero { get ; set; } //Getter: devuelve el valor del campo/ Setter: establece el valor del campo privado
        public string Nombre { get ; set; } //estos son los datos que me voy a traer de la base de datos, usamos las propiedades para Traer y poder obtener esos valores que albergan las variables ya sea nombre, descripcion, etc.
        [DisplayName("Descripción")]
        public string Descripcion { get ; set; }
        public string UrlImagen { get ; set; } //aca le estas agregando la url de la base de datos, estas usando esta propiedad para poder acceder a ese valor.
        public Elemento Tipo { get ; set; }
        public Elemento Debilidad { get ; set; }
        
        //get: Permite obtener el valor de la propiedad.
        //set: Permite asignar un valor a la propiedad.
    }
}
