using System;

namespace RestauranteApi.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int ClienteId { get; set; } 
        public DateTime FechaReserva { get; set; }
        public TimeSpan HoraReserva { get; set; }
        public int NumeroPersonas { get; set; }
        public string MesaAsignada { get; set; }
        
    }
}
