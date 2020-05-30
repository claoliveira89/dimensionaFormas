using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DimensionaFormas
{
    public class Concreto
    {
        private double pesoEspecifico;

        // Construtor 1
        public Concreto()
        {
            pesoEspecifico = 0;
        }

        // Construtor 2
        public Concreto(double ro)
        {
            pesoEspecifico = ro;
        }

        // Construtor 3
        public Concreto(Concreto c)
        {
            this.pesoEspecifico = c.pesoEspecifico;
        }

        // Metodos get
        public double getDensidade()
        {
            return pesoEspecifico;
        }

        // Metodos set
        public void setDensidade(double rho)
        {
            pesoEspecifico = rho;
        }
    }
}