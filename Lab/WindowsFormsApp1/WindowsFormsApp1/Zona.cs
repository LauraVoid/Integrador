using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Zona
    {
        private ArrayList municipios;
        
        public Zona()
        {
            
            municipios = new ArrayList();
            zonas = new ArrayList();

        }
        

        public void readFiles()
        {
            StreamReader st = new StreamReader("...\\..\\archivos\\ZONAS_WIFI_GRATIS_PARA_LA_GENTE.csv");

            string line = st.ReadLine();
            line = st.ReadLine();


            while (line != null)
            {

                string[] info = line.Split(',');
                string name = info[8];
                string longitud = info[14];
                string latitud = info[15];


                writeFile(name, longitud, latitud);
               
                
                line = st.ReadLine();

            }

            st.Close();







        }

        public void addMunicipios()
        {
            StreamReader st = new StreamReader("...\\..\\archivos\\ZONAS_WIFI_GRATIS_PARA_LA_GENTE.csv");

            string line = st.ReadLine();
            line = st.ReadLine();


            while (line != null)
            {

                string[] info = line.Split(',');
                string name = info[8];
               


                
                municipios.Add(name);

                line = st.ReadLine();

            }

            st.Close();







        }
        public void writeFile(string name, string longitud, string latitud)
        {
            Console.WriteLine(name);
            StreamWriter sw = new StreamWriter("...\\..\\archivos\\" + name + ".txt", true);
            
            
            sw.WriteLine(longitud + "," + latitud);
            sw.Close();
        }

        public ArrayList leerZonas(String name)
        {
             ArrayList zonas = new ArrayList();
        StreamReader st = new StreamReader("...\\..\\archivos\\" + name + ".txt");

            string line = st.ReadLine();
            line = st.ReadLine();


            while (line != null)
            {


                zonas.Add(line);




                line = st.ReadLine();

            }

            st.Close();
            return zonas;
        }

        public ArrayList darMunicipios()
        {
            return municipios;
        }

        
    }
}
