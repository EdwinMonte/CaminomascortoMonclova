namespace CaminomascortoMonclova
{
    public partial class Form1 : Form
    {
        Dictionary<string, Point> cityPositions;
        Dictionary<string, List<Tuple<string, int>>> graph;
        List<string> caminoMasCorto;
        List<string> caminoMasLargo;

        public Form1()
        {
            InitializeComponent();
            InicializarGrafo();
            LlenarComboBox();
            InicializarMapa();

            // Configuramos el evento Paint
            picMap.Paint += DibujarMapa;

            // Asignar eventos a los botones
            btnCalculateShortest.Click += btnCalculateShortest_Click;
            btnCalculateLongest.Click += btnCalculateLongest_Click;
        }

        private void InicializarGrafo()
        {
            graph = new Dictionary<string, List<Tuple<string, int>>>()
    {
        { "Monclova", new List<Tuple<string, int>>() { new Tuple<string, int>("Frontera", 7), new Tuple<string, int>("Castaños", 10), new Tuple<string, int>("Saltillo", 150), new Tuple<string, int>("Torreón", 220), new Tuple<string, int>("Sabinas", 110) } },
        { "Frontera", new List<Tuple<string, int>>() { new Tuple<string, int>("Monclova", 7), new Tuple<string, int>("Castaños", 17), new Tuple<string, int>("Saltillo", 130), new Tuple<string, int>("Piedras Negras", 200) } },
        { "Castaños", new List<Tuple<string, int>>() { new Tuple<string, int>("Monclova", 10), new Tuple<string, int>("Frontera", 17), new Tuple<string, int>("Saltillo", 160) } },
        { "Saltillo", new List<Tuple<string, int>>() { new Tuple<string, int>("Monclova", 150), new Tuple<string, int>("Frontera", 130), new Tuple<string, int>("Castaños", 160), new Tuple<string, int>("Ramos Arizpe", 20), new Tuple<string, int>("Parras de la Fuente", 130) } },
        { "Torreón", new List<Tuple<string, int>>() { new Tuple<string, int>("Monclova", 220), new Tuple<string, int>("Parras de la Fuente", 120), new Tuple<string, int>("San Pedro", 50) } },
        { "Piedras Negras", new List<Tuple<string, int>>() { new Tuple<string, int>("Frontera", 200), new Tuple<string, int>("Sabinas", 100), new Tuple<string, int>("Acuña", 90) } },
        { "Ramos Arizpe", new List<Tuple<string, int>>() { new Tuple<string, int>("Saltillo", 20) } },
        { "Parras de la Fuente", new List<Tuple<string, int>>() { new Tuple<string, int>("Saltillo", 130), new Tuple<string, int>("Torreón", 120) } },
        { "Sabinas", new List<Tuple<string, int>>() { new Tuple<string, int>("Monclova", 110), new Tuple<string, int>("Piedras Negras", 100), new Tuple<string, int>("Múzquiz", 70) } },
        { "Acuña", new List<Tuple<string, int>>() { new Tuple<string, int>("Piedras Negras", 90) } },
        { "San Pedro", new List<Tuple<string, int>>() { new Tuple<string, int>("Torreón", 50) } },
        { "Múzquiz", new List<Tuple<string, int>>() { new Tuple<string, int>("Sabinas", 70), new Tuple<string, int>("Nava", 80) } },
        { "Nava", new List<Tuple<string, int>>() { new Tuple<string, int>("Múzquiz", 80) } }
    };
        }

        private void InicializarMapa()
        {
            cityPositions = new Dictionary<string, Point>
    {
        { "Monclova", new Point(150, 200) },
        { "Frontera", new Point(250, 200) },
        { "Castaños", new Point(150, 300) },
        { "Saltillo", new Point(300, 400) },
        { "Torreón", new Point(400, 250) },
        { "Piedras Negras", new Point(100, 50) },
        { "Ramos Arizpe", new Point(320, 380) },
        { "Parras de la Fuente", new Point(350, 300) },
        { "Sabinas", new Point(180, 100) },
        { "Acuña", new Point(50, 70) },
        { "San Pedro", new Point(380, 280) },
        { "Múzquiz", new Point(160, 120) },
        { "Nava", new Point(140, 140) }
    };
        }

        private void LlenarComboBox()
        {
            cmbCityoforigin.Items.AddRange(new string[]
    {
        "Monclova", "Frontera", "Castaños", "Saltillo", "Torreón", "Piedras Negras",
        "Ramos Arizpe", "Parras de la Fuente", "Sabinas", "Acuña", "San Pedro",
        "Múzquiz", "Nava"
    });
            cmbDestinationcity.Items.AddRange(new string[]
            {
        "Monclova", "Frontera", "Castaños", "Saltillo", "Torreón", "Piedras Negras",
        "Ramos Arizpe", "Parras de la Fuente", "Sabinas", "Acuña", "San Pedro",
        "Múzquiz", "Nava"
            });
        }

        private void DibujarMapa(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen penCorto = new Pen(Color.Green, 3); // Línea para el camino más corto
            Pen penLargo = new Pen(Color.Red, 3);   // Línea para el camino más largo

            // Dibujar las ciudades como puntos
            foreach (var city in cityPositions)
            {
                g.FillEllipse(Brushes.Blue, city.Value.X - 5, city.Value.Y - 5, 10, 10);
                g.DrawString(city.Key, this.Font, Brushes.Black, city.Value.X + 10, city.Value.Y - 10);
            }

            // Dibujar el camino más corto (en verde)
            if (caminoMasCorto != null && caminoMasCorto.Count > 1)
            {
                for (int i = 0; i < caminoMasCorto.Count - 1; i++)
                {
                    Point start = cityPositions[caminoMasCorto[i]];
                    Point end = cityPositions[caminoMasCorto[i + 1]];
                    g.DrawLine(penCorto, start, end);
                }
            }

            // Dibujar el camino más largo (en rojo)
            if (caminoMasLargo != null && caminoMasLargo.Count > 1)
            {
                for (int i = 0; i < caminoMasLargo.Count - 1; i++)
                {
                    Point start = cityPositions[caminoMasLargo[i]];
                    Point end = cityPositions[caminoMasLargo[i + 1]];
                    g.DrawLine(penLargo, start, end);
                }
            }
        }

        private void btnCalculateShortest_Click(object sender, EventArgs e)
        {
            string origen = cmbCityoforigin.SelectedItem?.ToString();
            string destino = cmbDestinationcity.SelectedItem?.ToString();

            if (origen != null && destino != null)
            {
                var previous = new Dictionary<string, string>();

                // Obtener el camino más corto usando Dijkstra
                int distanciaCorta = Dijkstra(graph, origen, destino, previous);
                caminoMasCorto = ObtenerCamino(previous, origen, destino);

                // Limpiar el camino más largo
                caminoMasLargo = null;

                // Actualizamos el resultado en el TextBox
                if (caminoMasCorto != null && caminoMasCorto.Count > 0)
                {
                    txtresult.Text = $"El camino más corto de {origen} a {destino} es de {distanciaCorta} km";
                }
                else
                {
                    txtresult.Text = "No se encontró un camino entre las ciudades seleccionadas.";
                }

                // Forzar la actualización del mapa
                picMap.Invalidate();
            }
        }

        private void btnCalculateLongest_Click(object sender, EventArgs e)
        {
            string origen = cmbCityoforigin.SelectedItem?.ToString();
            string destino = cmbDestinationcity.SelectedItem?.ToString();

            if (origen != null && destino != null)
            {
                caminoMasLargo = new List<string>();
                List<string> caminoActual = new List<string>();
                HashSet<string> visitados = new HashSet<string>();
                int maxDistance = 0;
                int distanciaLarga = 0;

                // Buscar el camino más largo usando DFS
                DFSLongestPath(origen, destino, visitados, caminoActual, ref maxDistance, ref distanciaLarga);

                // Actualizar el camino más largo encontrado
                if (caminoMasLargo.Count > 0)
                {
                    txtresult.Text = $"El camino más largo de {origen} a {destino} es de {distanciaLarga} km";
                }
                else
                {
                    txtresult.Text = "No se encontró un camino entre las ciudades seleccionadas.";
                }

                // Limpiar el camino más corto
                caminoMasCorto = null;

                // Forzar la actualización del mapa
                picMap.Invalidate();
            }
        }

        private void DFSLongestPath(string actual, string destino, HashSet<string> visitados, List<string> caminoActual, ref int maxDistance, ref int distanciaLarga)
        {
            visitados.Add(actual);
            caminoActual.Add(actual);

            if (actual == destino)
            {
                int distancia = CalcularDistancia(caminoActual);
                if (distancia > maxDistance)
                {
                    maxDistance = distancia;
                    caminoMasLargo = new List<string>(caminoActual);
                    distanciaLarga = distancia;
                }
            }
            else
            {
                if (graph.ContainsKey(actual))
                {
                    foreach (var vecino in graph[actual])
                    {
                        if (!visitados.Contains(vecino.Item1))
                        {
                            DFSLongestPath(vecino.Item1, destino, visitados, caminoActual, ref maxDistance, ref distanciaLarga);
                        }
                    }
                }
            }

            visitados.Remove(actual);
            caminoActual.RemoveAt(caminoActual.Count - 1);
        }

        private int CalcularDistancia(List<string> camino)
        {
            int distancia = 0;
            for (int i = 0; i < camino.Count - 1; i++)
            {
                string ciudadActual = camino[i];
                string siguienteCiudad = camino[i + 1];
                var conexion = graph[ciudadActual].FirstOrDefault(c => c.Item1 == siguienteCiudad);
                if (conexion != null)
                {
                    distancia += conexion.Item2;
                }
                else
                {
                    // Si no hay conexión directa, retorna una distancia inválida
                    return 0;
                }
            }
            return distancia;
        }

        private List<string> ObtenerCamino(Dictionary<string, string> previous, string origen, string destino)
        {
            var camino = new List<string>();
            string actual = destino;

            while (actual != null && actual != origen)
            {
                camino.Insert(0, actual);
                previous.TryGetValue(actual, out actual);
            }

            if (actual == origen)
            {
                camino.Insert(0, origen);
                return camino;
            }

            return new List<string>(); // Retorna una lista vacía si no se encontró el camino
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
                nodes.Sort((x, y) => distances[x].CompareTo(distances[y]));
                var smallest = nodes[0];
                nodes.RemoveAt(0);

                if (smallest == end)
                    return distances[smallest];  // Camino más corto encontrado

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

