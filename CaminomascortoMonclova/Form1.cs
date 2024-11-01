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
        { "Monclova", new List<Tuple<string, int>>() { new Tuple<string, int>("Frontera", 7), new Tuple<string, int>("Casta�os", 10), new Tuple<string, int>("Saltillo", 150), new Tuple<string, int>("Torre�n", 220), new Tuple<string, int>("Sabinas", 110),new Tuple<string, int>("San Pedro" , 380) } },
        { "Frontera", new List<Tuple<string, int>>() { new Tuple<string, int>("Monclova", 7), new Tuple<string, int>("Casta�os", 17), new Tuple<string, int>("Saltillo", 130), new Tuple<string, int>("Piedras Negras", 200) } },
        { "Casta�os", new List<Tuple<string, int>>() { new Tuple<string, int>("Monclova", 10), new Tuple<string, int>("Frontera", 17), new Tuple<string, int>("Saltillo", 160) } },
        { "Saltillo", new List<Tuple<string, int>>() { new Tuple<string, int>("Monclova", 150), new Tuple<string, int>("Frontera", 130), new Tuple<string, int>("Casta�os", 160), new Tuple<string, int>("Ramos Arizpe", 20), new Tuple<string, int>("Parras de la Fuente", 130) } },
        { "Torre�n", new List<Tuple<string, int>>() { new Tuple<string, int>("Monclova", 220), new Tuple<string, int>("Parras de la Fuente", 120), new Tuple<string, int>("San Pedro", 50) } },
        { "Piedras Negras", new List<Tuple<string, int>>() { new Tuple<string, int>("Frontera", 200), new Tuple<string, int>("Sabinas", 100), new Tuple<string, int>("Acu�a", 90) } },
        { "Ramos Arizpe", new List<Tuple<string, int>>() { new Tuple<string, int>("Saltillo", 20) } },
        { "Parras de la Fuente", new List<Tuple<string, int>>() { new Tuple<string, int>("Saltillo", 130), new Tuple<string, int>("Torre�n", 120) } },
        { "Sabinas", new List<Tuple<string, int>>() { new Tuple<string, int>("Monclova", 110), new Tuple<string, int>("Piedras Negras", 100), new Tuple<string, int>("M�zquiz", 70) } },
        { "Acu�a", new List<Tuple<string, int>>() { new Tuple<string, int>("Piedras Negras", 90) } },
        { "San Pedro", new List<Tuple<string, int>>() { new Tuple<string, int>("Torre�n", 50) } },
        { "M�zquiz", new List<Tuple<string, int>>() { new Tuple<string, int>("Sabinas", 70), new Tuple<string, int>("Nava", 80) } },
        { "Nava", new List<Tuple<string, int>>() { new Tuple<string, int>("M�zquiz", 80) } },
        { "Allende", new List<Tuple<string, int>>() { new Tuple<string, int>("Nueva Rosita", 50), new Tuple<string, int>("Morelos", 40) } },
        { "Morelos", new List<Tuple<string, int>>() { new Tuple<string, int>("Nueva Rosita", 60), new Tuple<string, int>("Allende", 40) } },
        { "Villa Uni�n", new List<Tuple<string, int>>() { new Tuple<string, int>("General Cepeda", 30), new Tuple<string, int>("Arteaga", 40) } },
        { "General Cepeda", new List<Tuple<string, int>>() { new Tuple<string, int>("Villa Uni�n", 30), new Tuple<string, int>("Arteaga", 20) } },
        { "Arteaga", new List<Tuple<string, int>>() { new Tuple<string, int>("Villa Uni�n", 40), new Tuple<string, int>("General Cepeda", 20) } },
        { "Matamoros", new List<Tuple<string, int>>() { new Tuple<string, int>("San Buenaventura", 50), new Tuple<string, int>("Viesca", 60) } },
        { "San Buenaventura", new List<Tuple<string, int>>() { new Tuple<string, int>("Matamoros", 50), new Tuple<string, int>("Viesca", 40) } },
        { "Viesca", new List<Tuple<string, int>>() { new Tuple<string, int>("Matamoros", 60), new Tuple<string, int>("San Buenaventura", 40) } },
        { "Cuatro Ci�negas", new List<Tuple<string, int>>() { new Tuple<string, int>("Ocampo", 30), new Tuple<string, int>("Zaragoza", 40) } },
        { "Ocampo", new List<Tuple<string, int>>() { new Tuple<string, int>("Cuatro Ci�negas", 30), new Tuple<string, int>("Zaragoza", 20) } },
        { "Zaragoza", new List<Tuple<string, int>>() { new Tuple<string, int>("Cuatro Ci�negas", 40), new Tuple<string, int>("Ocampo", 20) } },
        { "Hidalgo", new List<Tuple<string, int>>() { new Tuple<string, int>("Jim�nez", 50), new Tuple<string, int>("Abasolo", 60) } },
        { "Jim�nez", new List<Tuple<string, int>>() { new Tuple<string, int>("Hidalgo", 50), new Tuple<string, int>("Abasolo", 40) } },
        { "Abasolo", new List<Tuple<string, int>>() { new Tuple<string, int>("Hidalgo", 60), new Tuple<string, int>("Jim�nez", 40) } },
        { "Candela", new List<Tuple<string, int>>() { new Tuple<string, int>("Escobedo", 30), new Tuple<string, int>("Guerrero", 40) } },
        { "Escobedo", new List<Tuple<string, int>>() { new Tuple<string, int>("Candela", 30), new Tuple<string, int>("Guerrero", 20) } },
        { "Guerrero", new List<Tuple<string, int>>() { new Tuple<string, int>("Candela", 40), new Tuple<string, int>("Escobedo", 20) } },
        { "Ju�rez", new List<Tuple<string, int>>() { new Tuple<string, int>("Lamadrid", 30), new Tuple<string, int>("Sacramento", 40) } },
        { "Lamadrid", new List<Tuple<string, int>>() { new Tuple<string, int>("Ju�rez", 30), new Tuple<string, int>("Sacramento", 20) } },
        { "Sacramento", new List<Tuple<string, int>>() { new Tuple<string, int>("Ju�rez", 40), new Tuple<string, int>("Lamadrid", 20) } },
        { "Sierra Mojada", new List<Tuple<string, int>>() { new Tuple<string, int>("Progreso", 30), new Tuple<string, int>("Francisco I Madero", 40) } },
        { "Progreso", new List<Tuple<string, int>>() { new Tuple<string, int>("Sierra Mojada", 30), new Tuple<string, int>("Francisco I Madero", 20) } },
        { "Francisco I Madero", new List<Tuple<string, int>>() { new Tuple<string, int>("Sierra Mojada", 40), new Tuple<string, int>("Progreso", 20) } },
        { "San Juan De Sabinas", new List<Tuple<string, int>>() { new Tuple<string, int>("Nadadores", 30) } },
        { "Nadadores", new List<Tuple<string, int>>() { new Tuple<string, int>("San Juan De Sabinas", 30) } }
    };
        }

        private void InicializarMapa()
        {
            cityPositions.Add("Acu�a", new Point(560, 37));
            cityPositions.Add("Jim�nez", new Point(695, 50));
            cityPositions.Add("Piedras Negras", new Point(750, 70));
            cityPositions.Add("Zaragoza", new Point(580, 80));
            cityPositions.Add("Nava", new Point(780, 110));
            cityPositions.Add("Morelos", new Point(628, 125));
            cityPositions.Add("Allende", new Point(720, 145));
            cityPositions.Add("Villa Uni�n", new Point(710, 175));
            cityPositions.Add("Guerrero", new Point(800, 160));
            cityPositions.Add("Hidalgo", new Point(835, 185));
            cityPositions.Add("Ju�rez", new Point(760, 210));
            cityPositions.Add("Progreso", new Point(755, 245));
            cityPositions.Add("Ocampo", new Point(370, 170));
            cityPositions.Add("Sierra Mojada", new Point(350, 290));
            cityPositions.Add("San Buenaventura", new Point(495, 340));
            cityPositions.Add("Lamadrid", new Point(565, 230));
            cityPositions.Add("Sacramento", new Point(385, 390));
            cityPositions.Add("Cuatro Ci�negas", new Point(500, 250));
            cityPositions.Add("Nadadores", new Point(515, 330));
            cityPositions.Add("San Pedro", new Point(415, 430));
            cityPositions.Add("Monclova", new Point(688, 310));
            cityPositions.Add("Frontera", new Point(620, 305));
            cityPositions.Add("Escobedo", new Point(680, 260));
            cityPositions.Add("Abasolo", new Point(668, 280));
            cityPositions.Add("Candela", new Point(770, 310));
            cityPositions.Add("Sabinas", new Point(625, 180));
            cityPositions.Add("M�zquiz", new Point(540, 120));
            cityPositions.Add("San Juan de Sabinas", new Point(550, 160));
            cityPositions.Add("Torre�n", new Point(380, 520));
            cityPositions.Add("Matamoros", new Point(420, 530));
            cityPositions.Add("Francisco I. Madero", new Point(435, 490));
            cityPositions.Add("Viesca", new Point(410, 600));
            cityPositions.Add("Parras de la Fuente", new Point(520, 570));
            cityPositions.Add("General Cepeda", new Point(600, 560));
            cityPositions.Add("Saltillo", new Point(680, 630));
            cityPositions.Add("Ramos Arizpe", new Point(650, 580));
            cityPositions.Add("Arteaga", new Point(720, 650));
            cityPositions.Add("Casta�os", new Point(685, 340));
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
        "Abasolo", "Acu�a", "Allende", "Arteaga", "Candela", "Casta�os", "Cuatro Ci�negas","Escobedo","Francisco I Madero," +
        "Frontera", "General Cepeda", "Guerrero", "Hidalgo", "Jim�nez", "Ju�rez", "Lamadrid", "Matamoros", "Monclova", "Morelos",
        "M�zquiz", "Nadadores", "Nava","Ocampo", "Parras de la Fuente", "Piedras Negras", "Progreso", "Ramos Arizpe",
        "Saltillo", "San Buenaventura", "San Juan De Sabinas", "San Pedro", "Sabinas", "Sacramento", "Sierra Mojada",
        "Torre�n", "Viesca", "Villa Uni�n", "Zaragoza"
    });
            cmbDestinationcity.Items.AddRange(new string[]
            {
        "Abasolo", "Acu�a", "Allende", "Arteaga", "Candela", "Casta�os", "Cuatro Ci�negas","Escobedo","Francisco I Madero," +
        "Frontera", "General Cepeda", "Guerrero", "Hidalgo", "Jim�nez", "Ju�rez", "Lamadrid", "Matamoros", "Monclova", "Morelos",
        "M�zquiz", "Nadadores", "Nava","Ocampo", "Parras de la Fuente", "Piedras Negras", "Progreso", "Ramos Arizpe",
        "Saltillo", "San Buenaventura", "San Juan De Sabinas", "San Pedro", "Sabinas", "Sacramento", "Sierra Mojada",
        "Torre�n", "Viesca", "Villa Uni�n", "Zaragoza"
            });
        }

        private void DibujarMapa(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen penCorto = new Pen(Color.Green, 3); // L�nea para el camino m�s corto
            Pen penLargo = new Pen(Color.Red, 3);   // L�nea para el camino m�s largo

            try
            {
                // Dibujar las ciudades como puntos
                foreach (var city in cityPositions)
                {
                    if (city.Value == null)
                    {
                        Console.WriteLine($"La ciudad {city.Key} tiene una posici�n nula.");
                        continue; // Omite esta ciudad si la posici�n es nula
                    }

                    g.FillEllipse(Brushes.Blue, city.Value.X - 5, city.Value.Y - 5, 10, 10);
                    g.DrawString(city.Key, this.Font ?? new Font("Arial", 8), Brushes.Black, city.Value.X + 10, city.Value.Y - 10);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al dibujar las ciudades: {ex.Message}");
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

            // Dibujar el camino m�s largo (en rojo)
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

                // Obtener el camino m�s corto usando Dijkstra
                int distanciaCorta = Dijkstra(graph, origen, destino, previous);
                caminoMasCorto = ObtenerCamino(previous, origen, destino);

                // Limpiar el camino m�s largo
                caminoMasLargo = null;

                // Actualizamos el resultado en el TextBox
                if (caminoMasCorto != null && caminoMasCorto.Count > 0)
                {
                    txtresult.Text = $"El camino m�s corto de {origen} a {destino} es de {distanciaCorta} km";
                }
                else
                {
                    txtresult.Text = "No se encontr� un camino entre las ciudades seleccionadas.";
                }

                // Forzar la actualizaci�n del mapa
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

                // Buscar el camino m�s largo usando DFS
                DFSLongestPath(origen, destino, visitados, caminoActual, ref maxDistance, ref distanciaLarga);

                // Actualizar el camino m�s largo encontrado
                if (caminoMasLargo.Count > 0)
                {
                    txtresult.Text = $"El camino m�s largo de {origen} a {destino} es de {distanciaLarga} km";
                }
                else
                {
                    txtresult.Text = "No se encontr� un camino entre las ciudades seleccionadas.";
                }

                // Limpiar el camino m�s corto
                caminoMasCorto = null;

                // Forzar la actualizaci�n del mapa
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
                    // Si no hay conexi�n directa, retorna una distancia inv�lida
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

            return new List<string>(); // Retorna una lista vac�a si no se encontr� el camino
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

