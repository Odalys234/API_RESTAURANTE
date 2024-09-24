using System;

namespace RestauranteApi.Models;

 public class Platillo
    {
        public int Id { get; set; }
        public string NombrePlatillo { get; set; }
        public string Descripcion { get; set; } 
        public string Imagen { get; set; }
        public decimal Precio { get; set; }
        public int CategoriaId { get; set; }
    }