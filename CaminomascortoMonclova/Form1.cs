using System.Drawing;

namespace CaminomascortoMonclova
{
    public partial class Form1 : Form
    {
        private Dictionary<string, Point> cityPositions = new Dictionary<string, Point>();
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
            panelMap.Paint += DibujarMapa;

            // Asignar eventos a los botones
            btnCalculateShortest.Click += btnCalculateShortest_Click;
            btnCalculateLongest.Click += btnCalculateLongest_Click;
        }

        private void InicializarGrafo()
        {
            graph = new Dictionary<string, List<Tuple<string, int>>>()
    {
        { "Monclova", new List<Tuple<string, int>>() { new Tuple<string, int>("Frontera", 7), new Tuple<string, int>("Castaños", 10), new Tuple<string, int>("Saltillo", 150), new Tuple<string, int>("Torreón", 220), new Tuple<string, int>("Sabinas", 110),new Tuple<string, int>("San Pedro" , 380) } },
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
        { "Nava", new List<Tuple<string, int>>() { new Tuple<string, int>("Múzquiz", 80) } },
        { "Allende", new List<Tuple<string, int>>() { new Tuple<string, int>("Nueva Rosita", 50), new Tuple<string, int>("Morelos", 40) } },
        { "Morelos", new List<Tuple<string, int>>() { new Tuple<string, int>("Nueva Rosita", 60), new Tuple<string, int>("Allende", 40) } },
        { "Villa Unión", new List<Tuple<string, int>>() { new Tuple<string, int>("General Cepeda", 30), new Tuple<string, int>("Arteaga", 40) } },
        { "General Cepeda", new List<Tuple<string, int>>() { new Tuple<string, int>("Villa Unión", 30), new Tuple<string, int>("Arteaga", 20) } },
        { "Arteaga", new List<Tuple<string, int>>() { new Tuple<string, int>("Villa Unión", 40), new Tuple<string, int>("General Cepeda", 20) } },
        { "Matamoros", new List<Tuple<string, int>>() { new Tuple<string, int>("San Buenaventura", 50), new Tuple<string, int>("Viesca", 60) } },
        { "San Buenaventura", new List<Tuple<string, int>>() { new Tuple<string, int>("Matamoros", 50), new Tuple<string, int>("Viesca", 40) } },
        { "Viesca", new List<Tuple<string, int>>() { new Tuple<string, int>("Matamoros", 60), new Tuple<string, int>("San Buenaventura", 40) } },
        { "Cuatro Ciénegas", new List<Tuple<string, int>>() { new Tuple<string, int>("Ocampo", 30), new Tuple<string, int>("Zaragoza", 40) } },
        { "Ocampo", new List<Tuple<string, int>>() { new Tuple<string, int>("Cuatro Ciénegas", 30), new Tuple<string, int>("Zaragoza", 20) } },
        { "Zaragoza", new List<Tuple<string, int>>() { new Tuple<string, int>("Cuatro Ciénegas", 40), new Tuple<string, int>("Ocampo", 20) } },
        { "Hidalgo", new List<Tuple<string, int>>() { new Tuple<string, int>("Jiménez", 50), new Tuple<string, int>("Abasolo", 60) } },
        { "Jiménez", new List<Tuple<string, int>>() { new Tuple<string, int>("Hidalgo", 50), new Tuple<string, int>("Abasolo", 40) } },
        { "Abasolo", new List<Tuple<string, int>>() { new Tuple<string, int>("Hidalgo", 60), new Tuple<string, int>("Jiménez", 40) } },
        { "Candela", new List<Tuple<string, int>>() { new Tuple<string, int>("Escobedo", 30), new Tuple<string, int>("Guerrero", 40) } },
        { "Escobedo", new List<Tuple<string, int>>() { new Tuple<string, int>("Candela", 30), new Tuple<string, int>("Guerrero", 20) } },
        { "Guerrero", new List<Tuple<string, int>>() { new Tuple<string, int>("Candela", 40), new Tuple<string, int>("Escobedo", 20) } },
        { "Juárez", new List<Tuple<string, int>>() { new Tuple<string, int>("Lamadrid", 30), new Tuple<string, int>("Sacramento", 40) } },
        { "Lamadrid", new List<Tuple<string, int>>() { new Tuple<string, int>("Juárez", 30), new Tuple<string, int>("Sacramento", 20) } },
        { "Sacramento", new List<Tuple<string, int>>() { new Tuple<string, int>("Juárez", 40), new Tuple<string, int>("Lamadrid", 20) } },
        { "Sierra Mojada", new List<Tuple<string, int>>() { new Tuple<string, int>("Progreso", 30), new Tuple<string, int>("Francisco I Madero", 40) } },
        { "Progreso", new List<Tuple<string, int>>() { new Tuple<string, int>("Sierra Mojada", 30), new Tuple<string, int>("Francisco I Madero", 20) } },
        { "Francisco I Madero", new List<Tuple<string, int>>() { new Tuple<string, int>("Sierra Mojada", 40), new Tuple<string, int>("Progreso", 20) } },
        { "San Juan De Sabinas", new List<Tuple<string, int>>() { new Tuple<string, int>("Nadadores", 30) } },
        { "Nadadores", new List<Tuple<string, int>>() { new Tuple<string, int>("San Juan De Sabinas", 30) } }
    };
        }

        private void InicializarMapa()
        {
            cityPositions.Add("Acuña", new Point(560, 37));
            cityPositions.Add("Jiménez", new Point(695, 50));
            cityPositions.Add("Piedras Negras", new Point(750, 70));
            cityPositions.Add("Zaragoza", new Point(580, 80));
            cityPositions.Add("Nava", new Point(780, 110));
            cityPositions.Add("Morelos", new Point(628, 125));
            cityPositions.Add("Allende", new Point(720, 145));
            cityPositions.Add("Villa Unión", new Point(710, 175));
            cityPositions.Add("Guerrero", new Point(800, 160));
            cityPositions.Add("Hidalgo", new Point(835, 185));
            cityPositions.Add("Juárez", new Point(760, 210));
            cityPositions.Add("Progreso", new Point(755, 245));
            cityPositions.Add("Ocampo", new Point(370, 170));
            cityPositions.Add("Sierra Mojada", new Point(350, 290));
            cityPositions.Add("San Buenaventura", new Point(495, 340));
            cityPositions.Add("Lamadrid", new Point(565, 230));
            cityPositions.Add("Sacramento", new Point(385, 390));
            cityPositions.Add("Cuatro Ciénegas", new Point(500, 250));
            cityPositions.Add("Nadadores", new Point(515, 330));
            cityPositions.Add("San Pedro", new Point(415, 430));
            cityPositions.Add("Monclova", new Point(688, 310));
            cityPositions.Add("Frontera", new Point(620, 305));
            cityPositions.Add("Escobedo", new Point(680, 260));
            cityPositions.Add("Abasolo", new Point(668, 280));
            cityPositions.Add("Candela", new Point(770, 310));
            cityPositions.Add("Sabinas", new Point(625, 180));
            cityPositions.Add("Múzquiz", new Point(540, 120));
            cityPositions.Add("San Juan de Sabinas", new Point(550, 160));
            cityPositions.Add("Torreón", new Point(380, 520));
            cityPositions.Add("Matamoros", new Point(420, 530));
            cityPositions.Add("Francisco I. Madero", new Point(435, 490));
            cityPositions.Add("Viesca", new Point(410, 600));
            cityPositions.Add("Parras de la Fuente", new Point(520, 570));
            cityPositions.Add("General Cepeda", new Point(600, 560));
            cityPositions.Add("Saltillo", new Point(680, 630));
            cityPositions.Add("Ramos Arizpe", new Point(650, 580));
            cityPositions.Add("Arteaga", new Point(720, 650));
            cityPositions.Add("Castaños", new Point(685, 340));
        }

        private void DibujarMunicipio(Graphics g, string nombre, float x, float y)
        {
            g.FillEllipse(Brushes.Blue, x, y, 10, 10); // Dibuja un punto azul
            g.DrawString(nombre, new Font("Arial", 8), Brushes.Black, x + 10, y); // Escribe el nombre
        }

        private void LlenarComboBox()
        {
            cmbCityoforigin.Items.AddRange(new string[]
    {
        "Abasolo", "Acuña", "Allende", "Arteaga", "Candela", "Castaños", "Cuatro Ciénegas","Escobedo","Francisco I Madero," +
        "Frontera", "General Cepeda", "Guerrero", "Hidalgo", "Jiménez", "Juárez", "Lamadrid", "Matamoros", "Monclova", "Morelos",
        "Múzquiz", "Nadadores", "Nava","Ocampo", "Parras de la Fuente", "Piedras Negras", "Progreso", "Ramos Arizpe",
        "Saltillo", "San Buenaventura", "San Juan De Sabinas", "San Pedro", "Sabinas", "Sacramento", "Sierra Mojada",
        "Torreón", "Viesca", "Villa Unión", "Zaragoza"
    });
            cmbDestinationcity.Items.AddRange(new string[]
            {
        "Abasolo", "Acuña", "Allende", "Arteaga", "Candela", "Castaños", "Cuatro Ciénegas","Escobedo","Francisco I Madero," +
        "Frontera", "General Cepeda", "Guerrero", "Hidalgo", "Jiménez", "Juárez", "Lamadrid", "Matamoros", "Monclova", "Morelos",
        "Múzquiz", "Nadadores", "Nava","Ocampo", "Parras de la Fuente", "Piedras Negras", "Progreso", "Ramos Arizpe",
        "Saltillo", "San Buenaventura", "San Juan De Sabinas", "San Pedro", "Sabinas", "Sacramento", "Sierra Mojada",
        "Torreón", "Viesca", "Villa Unión", "Zaragoza"
            });
        }

        private void DibujarMapa(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen penCorto = new Pen(Color.Green, 3); // Línea para el camino más corto
            Pen penLargo = new Pen(Color.Red, 3);   // Línea para el camino más largo

            try
            {
                // Dibujar las ciudades como puntos
                foreach (var city in cityPositions)
                {
                    if (city.Value == null)
                    {
                        Console.WriteLine($"La ciudad {city.Key} tiene una posición nula.");
                        continue; // Omite esta ciudad si la posición es nula
                    }

                    g.FillEllipse(Brushes.Blue, city.Value.X - 5, city.Value.Y - 5, 10, 10);
                    g.DrawString(city.Key, this.Font ?? new Font("Arial", 8), Brushes.Black, city.Value.X + 10, city.Value.Y - 10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al dibujar las ciudades: {ex.Message}");
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
                panelMap.Invalidate();
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
                panelMap.Invalidate();
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

