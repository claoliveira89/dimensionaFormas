using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DimensionaFormas
{
    public class Viga : ElemEstrut
    {
        private double altura;
        private double comprimento;
        private double largura;

        // Construtor 1
        public Viga()
        {
            altura = 0;
            comprimento = 0;
            largura = 0;
        }

        // Construtor 2
        public Viga(double h, double a, double b)
        {
            altura = h;
            comprimento = a;
            largura = b;
        }

        // Construtor 3
        public Viga(Viga vig)
        {
            this.altura = vig.altura;
            this.comprimento = vig.comprimento;
            this.largura = vig.largura;
        }

        // Metodos get
        public double getAltura()
        {
            return altura;
        }

        public double getComprimento()
        {
            return comprimento;
        }

        public double getLargura()
        {
            return largura;
        }

        // Metodos set
        public void setAltura( double alt)
        {
            altura = alt;
        }

        public void setComprimento(double compri)
        {
            comprimento = compri;
        }

        public void setLargura(double larg)
        {
            largura = larg;
        }
    }
}