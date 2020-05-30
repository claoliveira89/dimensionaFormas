using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimensionaFormas
{
    class Carregamento
    {
        private double permanente;
        private double variavel;

        // Construtor 1
        public Carregamento()
        {
            permanente = 0;
            variavel = 0;
        }

        // Construtor 2
        public Carregamento(double perman, double variav)
        {
            permanente = perman;
            variavel = variav;
        }

        // Construtor 3
        public Carregamento(Carregamento carregam)
        {
            this.permanente = carregam.permanente;
            this.variavel = carregam.variavel;
        }


    }
}
