using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab4_23
{
    /// <summary>
    /// Представляет граф энергосистемы в виде списка смежности.
    /// Граф неориентированный, рёбра имеют вес (мощность линии в МВт).
    /// </summary>
    public class Graph
    {
        // Список смежности: вершина -> список (сосед, вес)
        private readonly Dictionary<string, List<(string neighbor, int weight)>> _adjacency;

        public IReadOnlyList<string> Vertices { get; private set; } = new List<string>();

        public Graph()
        {
            _adjacency = new Dictionary<string, List<(string, int)>>();
        }

        //  Загрузка из файла

        /// <summary>
        /// Загружает граф из текстового файла.
        /// Секция VERTICES — список вершин, секция EDGES — рёбра в формате A;B;вес.
        /// Строки, начинающиеся с '#', игнорируются.
        /// </summary>
        public void LoadFromFile(string path)
        {
            _adjacency.Clear();

            var lines = File.ReadAllLines(path, Encoding.UTF8);
            string section = "";

            foreach (var raw in lines)
            {
                string line = raw.Trim();
                if (string.IsNullOrEmpty(line) || line.StartsWith("#")) continue;

                if (line == "VERTICES") { section = "V"; continue; }
                if (line == "EDGES") { section = "E"; continue; }

                if (section == "V")
                {
                    AddVertex(line);
                }
                else if (section == "E")
                {
                    var parts = line.Split(';');
                    if (parts.Length < 3) continue;
                    string a = parts[0].Trim();
                    string b = parts[1].Trim();
                    int w = int.TryParse(parts[2].Trim(), out int parsed) ? parsed : 1;
                    AddEdge(a, b, w);
                }
            }

            Vertices = _adjacency.Keys.ToList();
        }

        private void AddVertex(string name)
        {
            if (!_adjacency.ContainsKey(name))
                _adjacency[name] = new List<(string, int)>();
        }

        private void AddEdge(string a, string b, int weight)
        {
            AddVertex(a);
            AddVertex(b);
            _adjacency[a].Add((b, weight));
            _adjacency[b].Add((a, weight)); // неориентированный граф
        }

        public IReadOnlyList<(string neighbor, int weight)> GetNeighbors(string vertex)
        {
            return _adjacency.TryGetValue(vertex, out var list) ? list : new List<(string, int)>();
        }

        public bool HasVertex(string v) => _adjacency.ContainsKey(v);

        //  BFS — обход в ширину

        /// <summary>
        /// Обход в ширину от стартовой вершины.
        /// Возвращает порядок посещения вершин.
        /// </summary>
        public List<string> BFS(string start)
        {
            if (!HasVertex(start)) return new List<string>();

            var visited = new HashSet<string>();
            var queue = new Queue<string>();
            var order = new List<string>();

            visited.Add(start);
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();
                order.Add(current);

                foreach (var (neighbor, _) in _adjacency[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return order;
        }

        //  DFS — обход в глубину (итеративный, через стек)

        /// <summary>
        /// Обход в глубину от стартовой вершины.
        /// Возвращает порядок посещения вершин.
        /// </summary>
        public List<string> DFS(string start)
        {
            if (!HasVertex(start)) return new List<string>();

            var visited = new HashSet<string>();
            var stack = new Stack<string>();
            var order = new List<string>();

            stack.Push(start);

            while (stack.Count > 0)
            {
                string current = stack.Pop();
                if (visited.Contains(current)) continue;

                visited.Add(current);
                order.Add(current);

                // Добавляем соседей в обратном порядке, чтобы порядок обхода
                // совпадал с рекурсивным DFS
                var neighbors = _adjacency[current];
                for (int i = neighbors.Count - 1; i >= 0; i--)
                {
                    if (!visited.Contains(neighbors[i].neighbor))
                        stack.Push(neighbors[i].neighbor);
                }
            }

            return order;
        }

        //  Достижимость: достижима ли B из A (BFS)

        /// <summary>
        /// Проверяет, достижима ли вершина <paramref name="target"/> из <paramref name="source"/>
        /// с помощью BFS. Возвращает true/false и путь (если найден).
        /// </summary>
        public (bool reachable, List<string> path) IsReachable(string source, string target)
        {
            if (!HasVertex(source) || !HasVertex(target))
                return (false, new List<string>());

            if (source == target)
                return (true, new List<string> { source });

            var visited = new HashSet<string>();
            var queue = new Queue<string>();
            var parent = new Dictionary<string, string?>();

            visited.Add(source);
            queue.Enqueue(source);
            parent[source] = null;

            while (queue.Count > 0)
            {
                string current = queue.Dequeue();

                foreach (var (neighbor, _) in _adjacency[current])
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        parent[neighbor] = current;
                        queue.Enqueue(neighbor);

                        if (neighbor == target)
                        {
                            // Восстанавливаем путь
                            var path = new List<string>();
                            string? node = target;
                            while (node != null)
                            {
                                path.Add(node);
                                parent.TryGetValue(node, out node);
                            }
                            path.Reverse();
                            return (true, path);
                        }
                    }
                }
            }

            return (false, new List<string>());
        }

        //  Компоненты связности

        /// <summary>
        /// Находит все компоненты связности графа.
        /// Возвращает список компонент, каждая — список вершин.
        /// </summary>
        public List<List<string>> GetConnectedComponents()
        {
            var visited = new HashSet<string>();
            var components = new List<List<string>>();

            foreach (string vertex in _adjacency.Keys)
            {
                if (!visited.Contains(vertex))
                {
                    var component = BFS(vertex);
                    foreach (string v in component)
                        visited.Add(v);
                    components.Add(component);
                }
            }

            return components;
        }
    }
}
