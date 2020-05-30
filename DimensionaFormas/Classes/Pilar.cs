using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DimensionaFormas
{
    public class Pilar : ElemEstrut
    {
        private double altura;
        private double maiorDimensao;
        private double menorDimensao;

        // Construtor 1
        public Pilar()
        {
            altura = 0;
            maiorDimensao = 0;
            menorDimensao = 0;
        }

        // Construtor 2
        public Pilar(double h, double maior, double menor)
        {
            altura = h;
            maiorDimensao = maior;
            menorDimensao = menor;
        }

        // Construtor 3
        public Pilar(Pilar p)
        {
            this.altura = p.altura;
            this.maiorDimensao = p.maiorDimensao;
            this.menorDimensao = p.menorDimensao;
        }

        // Metodos get
        public double getAltura()
        {
            return altura;
        }

        public double getMaiorDimensao()
        {
            return maiorDimensao;
        }

        public double getMenorDimensao()
        {
            return menorDimensao;
        }

        // Metodos set
        public void setAltura(double alt)
        {
            altura = alt;
        }

        public void setMaiorDimensao(double maior)
        {
            maiorDimensao = maior;
        }

        public void setMenorDimensao( double menor)
        {
            menorDimensao = menor;
        }
    }
}