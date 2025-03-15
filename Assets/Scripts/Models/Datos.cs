using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    [System.Serializable]
    public class Datos
    {
        public string nombre;
        public int puntaje;
    }

    [System.Serializable]
    public class DatosList
    {
        public List<Datos> datos;
    }
}
