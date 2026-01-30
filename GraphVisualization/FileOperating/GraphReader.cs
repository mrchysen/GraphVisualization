using GraphVisualization.Models;

namespace GraphVisualization.FileOperating;

public static class GraphReader
{
    /// <summary>
    /// IsOriented - ориентированный - если true то добавляется путь в обе стороны
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Graph ReadGraph(
        string path, 
        bool IsOriented = false, 
        bool IsWeighted = false)
    {
        List<Node> nodes = new();

        if (File.Exists(path))
        {
            try
            {
                using StreamReader sr = new(path);

                var data = sr.ReadLine().Split().Select(int.Parse).ToArray();

                int n = data[0];
                int m = data[1];


                for (int i = 0; i < n; i++)
                {
                    nodes.Add(new Node(i));
                }

                for (int i = 0; i < m; i++)
                {
                    data = sr.ReadLine().Split().Select(int.Parse).ToArray();

                    int weight = 1;

                    if (IsWeighted)
                    {
                        weight = data[2];
                    }

                    nodes[data[0] - 1].Add(new Edge(nodes[data[1] - 1], weight));

                    if (!IsOriented)
                    {
                        nodes[data[1] - 1].Add(new Edge(nodes[data[0] - 1], weight));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error_reading: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("info: No such file");
        }

        Graph graph = new(nodes, IsWeighted, IsOriented);

        return graph;
    }
}
