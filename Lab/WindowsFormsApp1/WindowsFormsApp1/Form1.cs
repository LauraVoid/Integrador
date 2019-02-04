using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;




namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Zona zona;
        GMarkerGoogle marker;
        GMapOverlay markerOverLay;
        DataTable dt;
        int filaSeleccionada = 0; 
        double latInicial = 6.631919;
        double lngInicial =-76.064467;
        public Form1()
        {
            InitializeComponent();
            zona = new Zona();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            zona.addMunicipios();
            foreach (string n in zona.darMunicipios())
            {
                cMunicipios.Items.Add(n);

            }

            dt = new DataTable();
            dt.Columns.Add(new DataColumn("Descripción", typeof(string)));
            dt.Columns.Add(new DataColumn("Lat", typeof(double)));
            dt.Columns.Add(new DataColumn("Long", typeof(double)));
            //insertar datos al data grid
            dt.Rows.Add("Ubicación 1", latInicial, lngInicial);
            dataGridView1.DataSource = dt;

            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;

            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.CanDragMap = true;
            gMapControl1.MapProvider = GMapProviders.GoogleMap;
            gMapControl1.Position = new PointLatLng(latInicial, lngInicial);
            gMapControl1.MinZoom = 0;
            gMapControl1.MaxZoom = 24;
            gMapControl1.Zoom = 9;
            gMapControl1.AutoScroll = true;

            //Marcador
            markerOverLay = new GMapOverlay("Marcador");
            marker = new GMarkerGoogle(new PointLatLng(latInicial, lngInicial), GMarkerGoogleType.green);
            markerOverLay.Markers.Add(marker);
            
            marker.ToolTipMode = MarkerTooltipMode.Always;
            marker.ToolTipText = string.Format("Ubicacion: \n Latitud:{0} \n Longitud:{1}", latInicial, lngInicial);

            gMapControl1.Overlays.Add(markerOverLay);

            GMarkerGoogle marker2 = new GMarkerGoogle(new PointLatLng(6.376899, -75.1413), GMarkerGoogleType.green);
            
            
            markerOverLay.Markers.Add(marker2);
            marker2.ToolTipMode = MarkerTooltipMode.Always;
            marker2.ToolTipText = string.Format("Ubicacion: \n Latitud:{0} \n Longitud:{1}", 6.376899, -75.1413);
        }

        private void Seleccionar(object sender, DataGridViewCellMouseEventArgs e)
        {
            filaSeleccionada = e.RowIndex;
            //recuperar datos del grid y ponerlos en el mapa
            txtDescripcion.Text = dataGridView1.Rows[filaSeleccionada].Cells[0].Value.ToString();
            txtLatitud.Text= dataGridView1.Rows[filaSeleccionada].Cells[1].Value.ToString();
            txtLongitud.Text= dataGridView1.Rows[filaSeleccionada].Cells[2].Value.ToString();

            marker.Position = new PointLatLng(Convert.ToDouble(txtLatitud.Text), Convert.ToDouble(txtLongitud.Text)) ;
            gMapControl1.Position = marker.Position;
        }

        private void gMapControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            double lat = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lat;
            double lng = gMapControl1.FromLocalToLatLng(e.X, e.Y).Lng;
           
            txtDescripcion.Text = "Ubicacion ";
            txtLatitud.Text = lat.ToString();
            txtLongitud.Text = lng.ToString();



            marker.Position = new PointLatLng(lat, lng);
            marker.ToolTipMode = MarkerTooltipMode.Always;
            marker.ToolTipText = string.Format("Ubicacion: \n Latitud:{0} \n Longitud:{1}", lat, lng);
            

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            dt.Rows.Add(txtDescripcion.Text, txtLatitud.Text, txtLongitud.Text);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(filaSeleccionada);
        }

        private void cMunicipios_MouseDown(object sender, MouseEventArgs e)
        {
            txtDescripcion.Text = "lol";
            txtDescripcion.Text = cMunicipios.SelectedItem.ToString();
            

        }
        

        private void cMunicipios_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDescripcion.Text = cMunicipios.SelectedItem.ToString();
        }

        private void bVer_Click(object sender, EventArgs e)
        {
            txtDescripcion.Text = cMunicipios.SelectedItem.ToString();
            //for(int i=0;i< zona.leerZonas(cMunicipios.SelectedItem.ToString()).Count; i++)
            //{
              
            //    string[] info = [i].Split(',');
            //    string name = cMunicipios.SelectedItem.ToString();
            //    string longitud = info[14];
            //    string latitud = info[15];
            //}
            
        }
    }
}
