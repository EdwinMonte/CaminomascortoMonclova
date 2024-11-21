using System.Drawing;
using System.IO;

namespace CaminomascortoMonclova
{
    public partial class Form1 : Form
    {
        private Dictionary<string, Point> cityPositions = new Dictionary<string, Point>();
        private Dictionary<string, List<Tuple<string, int>>> graph = new Dictionary<string, List<Tuple<string, int>>>(); // Inicializamos graph
        private List<string> caminoMasCorto;
        private List<string> caminoMasLargo;

        public Form1()
        {
            InitializeComponent();
            LlenarComboBox();
            InicializarMapa();

            // Configuramos el evento Paint
            panelMap.Paint += DibujarMapa;

            // Asignar eventos a los botones
            btnCalculateShortest.Click += btnCalculateShortest_Click;
            btnCalculateLongest.Click += btnCalculateLongest_Click;
        }



        public void CargarConexiones(string archivo)
        {
            var lineas = File.ReadAllLines(archivo);

            foreach (var linea in lineas)
            {
                var datos = linea.Split(',');
                var ciudad1 = datos[0];
                var ciudad2 = datos[1];
                var distancia = int.Parse(datos[2]);

                Console.WriteLine($"Agregando conexión entre {ciudad1} y {ciudad2} con distancia {distancia}");
                AgregarConexion(ciudad1, ciudad2, distancia);
            }

        }
        private void AgregarConexion(string ciudad1, string ciudad2, int distancia)
        {
            if (!graph.ContainsKey(ciudad1))
                graph[ciudad1] = new List<Tuple<string, int>>();
            if (!graph.ContainsKey(ciudad2))
                graph[ciudad2] = new List<Tuple<string, int>>();

            // Evitar duplicados de conexiones entre ciudades
            if (!graph[ciudad1].Any(c => c.Item1 == ciudad2))
                graph[ciudad1].Add(new Tuple<string, int>(ciudad2, distancia));
            if (!graph[ciudad2].Any(c => c.Item1 == ciudad1))
                graph[ciudad2].Add(new Tuple<string, int>(ciudad1, distancia));
        }

        private void InicializarMapa()
        {
            cityPositions.Add("Acuña", new Point(560, 37));
            cityPositions.Add("Jimenez", new Point(695, 50));
            cityPositions.Add("Piedras Negras", new Point(750, 70));
            cityPositions.Add("Zaragoza", new Point(580, 80));
            cityPositions.Add("Nava", new Point(780, 110));
            cityPositions.Add("Morelos", new Point(628, 125));
            cityPositions.Add("Allende", new Point(720, 145));
            cityPositions.Add("Villa Union", new Point(710, 175));
            cityPositions.Add("Guerrero", new Point(800, 160));
            cityPositions.Add("Hidalgo", new Point(835, 185));
            cityPositions.Add("Juarez", new Point(760, 210));
            cityPositions.Add("Progreso", new Point(755, 245));
            cityPositions.Add("Ocampo", new Point(390, 170));
            cityPositions.Add("Sierra Mojada", new Point(315, 240));
            cityPositions.Add("San Buenaventura", new Point(500, 190));
            cityPositions.Add("Lamadrid", new Point(550, 280));
            cityPositions.Add("Sacramento", new Point(570, 320));
            cityPositions.Add("Cuatro Cienegas", new Point(480, 370));
            cityPositions.Add("Nadadores", new Point(565, 270));
            cityPositions.Add("San Pedro", new Point(415, 390));
            cityPositions.Add("Monclova", new Point(688, 310));
            cityPositions.Add("Frontera", new Point(612, 305));
            cityPositions.Add("Escobedo", new Point(680, 260));
            cityPositions.Add("Abasolo", new Point(668, 280));
            cityPositions.Add("Candela", new Point(770, 310));
            cityPositions.Add("Sabinas", new Point(625, 180));
            cityPositions.Add("Muzquiz", new Point(540, 120));
            cityPositions.Add("San Juan de Sabinas", new Point(550, 160));
            cityPositions.Add("Torreon", new Point(365, 520));
            cityPositions.Add("Matamoros", new Point(400, 475));
            cityPositions.Add("Francisco I. Madero", new Point(335, 320));
            cityPositions.Add("Viesca", new Point(445, 540));
            cityPositions.Add("Parras de la Fuente", new Point(520, 530));
            cityPositions.Add("General Cepeda", new Point(650, 500));
            cityPositions.Add("Saltillo", new Point(680, 580));
            cityPositions.Add("Ramos Arizpe", new Point(690, 430));
            cityPositions.Add("Arteaga", new Point(810, 560));
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
        "Abasolo", "Acuña", "Allende", "Arteaga", "Candela", "Castaños", "Cuatro Cienegas","Escobedo","Francisco I Madero",
        "Frontera", "General Cepeda", "Guerrero", "Hidalgo", "Jimenez", "Juarez", "Lamadrid", "Matamoros", "Monclova", "Morelos",
        "Muzquiz", "Nadadores", "Nava","Ocampo", "Parras de la Fuente", "Piedras Negras", "Progreso", "Ramos Arizpe",
        "Saltillo", "San Buenaventura", "San Juan De Sabinas", "San Pedro", "Sabinas", "Sacramento", "Sierra Mojada",
        "Torreon", "Viesca", "Villa Union", "Zaragoza"
    });
            cmbDestinationcity.Items.AddRange(new string[]
            {
        "Abasolo", "Acuña", "Allende", "Arteaga", "Candela", "Castaños", "Cuatro Cienegas","Escobedo","Francisco I Madero",
        "Frontera", "General Cepeda", "Guerrero", "Hidalgo", "Jimenez", "Juarez", "Lamadrid", "Matamoros", "Monclova", "Morelos",
        "Muzquiz", "Nadadores", "Nava","Ocampo", "Parras de la Fuente", "Piedras Negras", "Progreso", "Ramos Arizpe",
        "Saltillo", "San Buenaventura", "San Juan De Sabinas", "San Pedro", "Sabinas", "Sacramento", "Sierra Mojada",
        "Torreon", "Viesca", "Villa Union", "Zaragoza"
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

            // Dibujar el camino más corto (en verde) si está definido
            if (caminoMasCorto != null && caminoMasCorto.Count > 1)
            {
                for (int i = 0; i < caminoMasCorto.Count - 1; i++)
                {
                    Point start = cityPositions[caminoMasCorto[i]];
                    Point end = cityPositions[caminoMasCorto[i + 1]];
                    g.DrawLine(penCorto, start, end);
                }
            }

            // Dibujar el camino más largo (en rojo) si está definido
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
                // Verificar que las ciudades estén en el grafo
                if (!graph.ContainsKey(origen) || !graph.ContainsKey(destino))
                {
                    txtresult.Text = "Alguna de las ciudades seleccionadas no existe en el grafo.";
                    return;
                }

                var previous = new Dictionary<string, string>();
                int distanciaCorta = Dijkstra(graph, origen, destino, previous);

                // Obtener el camino más corto
                caminoMasCorto = ObtenerCamino(previous, origen, destino);
                caminoMasLargo = null;

                txtresult.Text = caminoMasCorto != null
                                 ? $"El camino más corto de {origen} a {destino} es de {distanciaCorta} km"
                                 : "No se encontró un camino entre las ciudades seleccionadas.";

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
                    // Esto indica un error en el camino, no debería ocurrir si el grafo está bien construido
                    Console.WriteLine($"Error: No se encontró una conexión directa entre {ciudadActual} y {siguienteCiudad}");
                    return -1;
                }
            }
            return distancia;
        }

        private List<string> ObtenerCamino(Dictionary<string, string> previous, string origen, string destino)
        {
            var path = new List<string>();
            string step = destino;

            // Reconstrucción del camino desde el destino al origen
            while (step != null && step != origen)
            {
                path.Add(step);
                if (!previous.ContainsKey(step)) // Verificación de que haya un camino válido
                    return null;
                step = previous[step];
            }

            if (step == null) // Si no encontramos el origen, no hay camino
                return null;

            path.Add(origen); // Agregar el origen al camino
            path.Reverse(); // Invertir el camino para que vaya del origen al destino
            return path;
        }

        private int Dijkstra(Dictionary<string, List<Tuple<string, int>>> graph, string origen, string destino, Dictionary<string, string> previous)
        {
            var distancias = new Dictionary<string, int>();
            var visited = new HashSet<string>();
            var queue = new SortedSet<(int distance, string city)>();

            foreach (var ciudad in graph.Keys)
            {
                distancias[ciudad] = int.MaxValue;
            }
            distancias[origen] = 0;
            queue.Add((0, origen));

            previous.Clear();

            while (queue.Count > 0)
            {
                var (distanciaActual, ciudadActual) = queue.Min;
                queue.Remove(queue.Min);

                if (visited.Contains(ciudadActual))
                    continue;

                visited.Add(ciudadActual);

                if (ciudadActual == destino)
                    return distancias[destino];

                foreach (var (vecino, peso) in graph[ciudadActual])
                {
                    int nuevaDistancia = distanciaActual + peso;
                    if (nuevaDistancia < distancias[vecino])
                    {
                        // Eliminar cualquier distancia anterior para evitar duplicados
                        queue.Remove((distancias[vecino], vecino));

                        distancias[vecino] = nuevaDistancia;
                        previous[vecino] = ciudadActual;

                        // Añadir la nueva distancia actualizada
                        queue.Add((nuevaDistancia, vecino));
                    }
                }
            }

            return distancias[destino] == int.MaxValue ? -1 : distancias[destino];
        }


        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string archivo = openFileDialog.FileName;
                CargarConexiones(archivo);
                MessageBox.Show("Archivo cargado correctamente.");
            }
        }
    }
}

