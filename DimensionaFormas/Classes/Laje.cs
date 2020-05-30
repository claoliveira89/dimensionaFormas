using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DimensionaFormas
{
    public class Laje : ElemEstrut
    {
        private double altura;
        private double comprimento;
        private double largura;

        // Construtor 1
        public Laje()
        {
            altura = 0;
            comprimento = 0;
            largura = 0;
        }

        // Construtor 2
        public Laje(double h, double a, double b)
        {
            altura = h;
            comprimento = a;
            largura = b;
        }

        // Construtor 3
        public Laje(Laje l)
        {
            this.altura = l.altura;
            this.comprimento = l.comprimento;
            this.largura = l.largura;
        }

        // Metodos get
        public double getAltura()
        {
            return altura;
        }

        public double getLargura()
        {
            return largura;
        }

        public double getComprimento()
        {
            return comprimento;
        }

        // Metodos set
        public void setAltura(double alt)
        {
            altura = alt;
        }

        public void setLargura(double larg)
        {
            largura = larg;
        }

        public void setComprimento(double compri)
        {
            comprimento = compri;
        }
    }
}