using System;

namespace RestauranteApi.Models
{
    public class Pedido
    {
        public int Id { get; set; }  
        public string Nombre { get; set; } 
        public string Apellido { get; set; }  
        public string Gmail { get; set; }  
        public string Telefono { get; set; }  
        public DateTime FechaPedido { get; set; }  

       
    }
}
