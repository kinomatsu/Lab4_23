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
        /// Алгоритм Дейкстры — кратчайшие расстояния от source до всех вершин.
        /// Работает только с неотрицательными весами рёбер.
        /// Возвращает:
        ///   dist    — словарь кратчайших расстояний от source до каждой вершины;
        ///   previous — словарь предков для восстановления пути.
        /// </summary>
        public (Dictionary<string, int> dist, Dictionary<string, string?> previous)
            Dijkstra(string source)
        {
            //инициализация расстояний — все бесконечность, кроме старта
            var dist = new Dictionary<string, int>();
            var previous = new Dictionary<string, string?>();
            var unvisited = new HashSet<string>();

            foreach (string v in _adjacency.Keys)
            {
                dist[v] = int.MaxValue; //"бесконечность"
                previous[v] = null;
                unvisited.Add(v);
            }
            dist[source] = 0; // расстояние до самого себя = 0

            while (unvisited.Count > 0)
            {
                // выбираем непосещённую вершину с минимальным расстоянием
                string? current = null;
                int minDist = int.MaxValue;
                foreach (string v in unvisited)
                {
                    if (dist[v] < minDist)
                    {
                        minDist = dist[v];
                        current = v;
                    }
                }

                // если минимальное расстояние — бесконечность,
                // оставшиеся вершины недостижимы — выходим
                if (current == null || dist[current] == int.MaxValue)
                    break;

                unvisited.Remove(current);

                // релаксация рёбер — обновляем расстояния до соседей
                foreach (var (neighbor, weight) in _adjacency[current])
                {
                    if (!unvisited.Contains(neighbor)) continue;

                    // формула релаксации: d[v] = min(d[v], d[u] + w(u,v))
                    int newDist = dist[current] + weight;
                    if (newDist < dist[neighbor])
                    {
                        dist[neighbor] = newDist;
                        previous[neighbor] = current; // запоминаем предка
                    }
                }
            }

            return (dist, previous);
        }

        /// <summary>
        /// Восстанавливает путь от source до target
        /// по словарю предков, полученному из Dijkstra().
        /// Возвращает список вершин маршрута или пустой список, если путь не найден.
        /// </summary>
        public List<string> GetDijkstraPath(
            Dictionary<string, string?> previous,
            string source,
            string target)
        {
            //обратный проход по словарю предков
            var path = new List<string>();
            string? current = target;

            while (current != null)
            {
                path.Add(current);
                if (current == source) break;
                previous.TryGetValue(current, out current);
            }

            //если путь не дошёл до source — маршрут не существует
            if (path.Count == 0 || path[^1] != source)
                return new List<string>();

            path.Reverse(); //переворачиваем — был от цели к старту
            return path;
        }
        //  Точки сочленения (алгоритм Тарьяна)

        /// <summary>
        /// Находит все точки сочленения графа алгоритмом Тарьяна.
        /// Точка сочленения — вершина, удаление которой увеличивает число компонент связности.
        /// Алгоритм основан на DFS и вычисляет:
        ///   disc[v] — время первого посещения вершины v;
        ///   low[v]  — минимальное disc, достижимое из поддерева v.
        /// Условие точки сочленения (не корень): low[child] >= disc[v].
        /// Условие для корня DFS: корень является точкой сочленения, если у него >= 2 детей.
        /// </summary>
        public List<string> FindArticulationPoints()
        {
            var result = new List<string>();
            var visited = new HashSet<string>();
            var disc = new Dictionary<string, int>();   // ЛР6: время обнаружения
            var low = new Dictionary<string, int>();   // ЛР6: минимальное достижимое время
            var parent = new Dictionary<string, string?>(); // ЛР6: родитель в дереве DFS

            int timer = 0;

            // ЛР6: рекурсивный DFS для вычисления disc и low
            void Dfs(string v)
            {
                visited.Add(v);
                disc[v] = low[v] = timer++;
                int childCount = 0; // ЛР6: число детей в дереве DFS

                foreach (var (neighbor, _) in _adjacency[v])
                {
                    if (!visited.Contains(neighbor))
                    {
                        childCount++;
                        parent[neighbor] = v;
                        Dfs(neighbor);

                        // ЛР6: обновляем low[v] через потомка
                        low[v] = Math.Min(low[v], low[neighbor]);

                        // ЛР6: условие точки сочленения для не-корня
                        bool isRoot = !parent.ContainsKey(v) || parent[v] == null;
                        if (!isRoot && low[neighbor] >= disc[v] && !result.Contains(v))
                            result.Add(v);

                        // ЛР6: условие точки сочленения для корня DFS
                        if (isRoot && childCount == 2 && !result.Contains(v))
                            result.Add(v);
                    }
                    else if (neighbor != parent.GetValueOrDefault(v))
                    {
                        // ЛР6: обратное ребро — обновляем low через уже посещённого соседа
                        low[v] = Math.Min(low[v], disc[neighbor]);
                    }
                }
            }

            // ЛР6: запускаем DFS от каждой непосещённой вершины
            foreach (string v in _adjacency.Keys)
            {
                if (!visited.Contains(v))
                {
                    parent[v] = null;
                    Dfs(v);
                }
            }

            return result;
        }


        //  Минимальное остовное дерево (алгоритм Прима)

        /// <summary>
        /// Строит минимальное остовное дерево алгоритмом Прима.
        /// МОД — подграф, соединяющий все вершины с минимальной суммой весов рёбер.
        /// Алгоритм жадно добавляет ребро минимального веса, соединяющее
        /// вершину внутри МОД с вершиной вне его.
        /// Возвращает список рёбер МОД в формате (from, to, weight).
        /// </summary>
        public List<(string from, string to, int weight)> BuildMST_Prim()
        {
            if (_adjacency.Count == 0) return new List<(string, string, int)>();

            var mstEdges = new List<(string from, string to, int weight)>();
            var inMST = new HashSet<string>();
            // ЛР6: минимальный вес ребра, соединяющего вершину с МОД
            var minWeight = new Dictionary<string, int>();
            // ЛР6: через какую вершину МОД достигается данная вершина
            var mstParent = new Dictionary<string, string?>();

            //  инициализация — все веса бесконечность
            foreach (string v in _adjacency.Keys)
            {
                minWeight[v] = int.MaxValue;
                mstParent[v] = null;
            }

            //стартуем с первой вершины
            string start = _adjacency.Keys.First();
            minWeight[start] = 0;

            for (int i = 0; i < _adjacency.Count; i++)
            {
                // ЛР6: выбираем вершину вне МОД с минимальным весом ребра к МОД
                string? u = null;
                int best = int.MaxValue;
                foreach (string v in _adjacency.Keys)
                {
                    if (!inMST.Contains(v) && minWeight[v] < best)
                    {
                        best = minWeight[v];
                        u = v;
                    }
                }

                if (u == null) break; // ЛР6: граф несвязный — остальные недостижимы

                inMST.Add(u);

                // ЛР6: добавляем ребро в МОД (кроме стартовой вершины)
                if (mstParent[u] != null)
                    mstEdges.Add((mstParent[u]!, u, minWeight[u]));

                // ЛР6: обновляем минимальные веса для соседей u
                foreach (var (neighbor, weight) in _adjacency[u])
                {
                    if (!inMST.Contains(neighbor) && weight < minWeight[neighbor])
                    {
                        minWeight[neighbor] = weight;
                        mstParent[neighbor] = u; // ЛР6: запоминаем, через кого добавляем
                    }
                }
            }

            return mstEdges;
        }
    }
}
