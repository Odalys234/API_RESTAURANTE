using System;

namespace RestauranteApi.Models
{
    public class Voto
    {
        public int Id { get; set; }
        public int PlatilloId { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaVotacion { get; set; }
        public int Puntuacion { get; set; }  
        public string Comentario { get; set; } 
    }
}
