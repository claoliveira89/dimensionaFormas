using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DimensionaFormas
{
    public class Coeficientes
    {
        private double kmod1;
        private double kmod2;
        private double kmod3;

        // Construtor 1
        public Coeficientes()
        {
            kmod1 = 0;
            kmod2 = 0;
            kmod3 = 0;
        }

        // Construtor 2
        public Coeficientes(double km1, double km2, double km3)
        {
            kmod1 = km1;
            kmod2 = km2;
            kmod3 = km3;
        }

        // Construtor 3
        public Coeficientes(Coeficientes c)
        {
            this.kmod1 = c.kmod1;
            this.kmod2 = c.kmod2;
            this.kmod3 = c.kmod3;
        }

        // Metodos
        public double calculaKmod()
        {
            return kmod1 * kmod2 * kmod3;
        }

        // Metodos set
        public void setKmod1(double coeficiente1)
        {
            kmod1 = coeficiente1;
        }

        public void setKmod2(double coeficiente2)
        {
            kmod2 = coeficiente2;
        }

        public void setKmod3(double coeficiente3)
        {
            kmod3 = coeficiente3;
        }

        // Metodos get
        public double getKmod1()
        {
            return kmod1;
        }

        public double getKmod2()
        {
            return kmod2;
        }

        public double getKmod3()
        {
            return kmod3;
        }
    }
}