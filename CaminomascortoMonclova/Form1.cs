namespace CaminomascortoMonclova
{
    public partial class Form1 : Form
    {
        Dictionary<string, Point> cityPositions;
        Dictionary<string, List<Tuple<string, int>>> graph;
        List<string> caminoMasCorto;

        public Form1()
        {
            InitializeComponent();
            InicializarGrafo();
            LlenarComboBox();
            InicializarMapa();

            // Configuramos el evento Paint
            picMap.Paint += DibujarMapa;
        }

        private void InicializarGrafo()
        {
            graph = new Dictionary<string, List<Tuple<string, int>>>()
        {
            { "Monclova", new List<Tuple<string, int>>() { new Tuple<string, int>("Frontera", 7), new Tuple<string, int>("Casta�os", 10), new Tuple<string, int>("Saltillo", 150) } },
            { "Frontera", new List<Tuple<string, int>>() { new Tuple<string, int>("Monclova", 7), new Tuple<string, int>("Casta�os", 17), new Tuple<string, int>("Saltillo", 130) } },
            { "Casta�os", new List<Tuple<string, int>>() { new Tuple<string, int>("Monclova", 10), new Tuple<string, int>("Frontera", 17) } },
            { "Saltillo", new List<Tuple<string, int>>() { new Tuple<string, int>("Monclova", 150), new Tuple<string, int>("Casta�os", 130) } }
        };
        }

        private void InicializarMapa()
        {
            cityPositions = new Dictionary<string, Point>
        {
            { "Monclova", new Point(150, 200) },
            { "Frontera", new Point(250, 200) },
            { "Casta�os", new Point(150, 300) },
            { "Saltillo", new Point(300, 400) }
        };
        }

        private void LlenarComboBox()
        {
            cmbCityoforigin.Items.AddRange(new string[] { "Monclova", "Frontera", "Casta�os", "Saltillo" });
            cmbDestinationcity.Items.AddRange(new string[] { "Monclova", "Frontera", "Casta�os", "Saltillo" });
        }

        private void DibujarMapa(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen penCorto = new Pen(Color.Green, 3); // Usamos una l�nea m�s gruesa para que se note mejor

            // Dibujar las ciudades como puntos
            foreach (var city in cityPositions)
            {
                g.FillEllipse(Brushes.Blue, city.Value.X - 5, city.Value.Y - 5, 10, 10);
                g.DrawString(city.Key, this.Font, Brushes.Black, city.Value.X + 10, city.Value.Y - 10);
            }

            // Dibujar el camino m�s corto (en verde)
            if (caminoMasCorto != null && caminoMasCorto.Count > 1)
            {
                for (int i = 0; i < caminoMasCorto.Count - 1; i++)
                {
                    Point start = cityPositions[caminoMasCorto[i]];
                    Point end = cityPositions[caminoMasCorto[i + 1]];
                    g.DrawLine(penCorto, start, end);
                }
            }
        }

        private void btncalculate_Click(object sender, EventArgs e)
        {
            string origen = cmbCityoforigin.SelectedItem?.ToString();
            string destino = cmbDestinationcity.SelectedItem?.ToString();

            if (origen != null && destino != null)
            {
                // Diccionario para almacenar el camino m�s corto
                var previous = new Dictionary<string, string>();

                // Obtener el camino m�s corto usando Dijkstra
                int distanciaCorta = Dijkstra(graph, origen, destino, previous);
                caminoMasCorto = ObtenerCaminoMasCorto(origen, destino, previous);

                // Actualizamos el resultado en el TextBox
                if (caminoMasCorto.Count > 0)
                {
                    txtresult.Text = $"El camino m�s corto de {origen} a {destino} es de {distanciaCorta} km";
                }
                else
                {
                    txtresult.Text = "No se encontr� un camino entre las ciudades seleccionadas.";
                }

                // Forzar la actualizaci�n del mapa para que se dibuje el camino
                picMap.Invalidate();
            }
        }

        private List<string> ObtenerCaminoMasCorto(string origen, string destino, Dictionary<string, string> previous)
        {
            var camino = new List<string>();
            string actual = destino;

            while (actual != null)
            {
                camino.Insert(0, actual);
                previous.TryGetValue(actual, out actual);
            }

            return camino;
        }

        private int Dijkstra(Dictionary<string, List<Tuple<string, int>>> graph, string start, string end, Dictionary<string, string> previous)
        {
            var distances = new Dictionary<string, int>();
            var nodes = new List<string>();

            foreach (var vertex in graph)
            {
                distances[vertex.Key] = vertex.Key == start ? 0 : int.MaxValue;
                nodes.Add(vertex.Key);
            }

            while (nodes.Count != 0)
            {
                nodes.Sort((x, y) => distances[x] - distances[y]);
                var smallest = nodes[0];
                nodes.Remove(smallest);

                if (smallest == end)
                    return distances[smallest];  // Camino m�s corto encontrado

                if (distances[smallest] == int.MaxValue)
                    break;

                foreach (var neighbor in graph[smallest])
                {
                    int alt = distances[smallest] + neighbor.Item2;
                    if (alt < distances[neighbor.Item1])
                    {
                        distances[neighbor.Item1] = alt;
                        previous[neighbor.Item1] = smallest; // Guardamos el nodo anterior
                    }
                }
            }

            return distances[end];
        }
    }



}

