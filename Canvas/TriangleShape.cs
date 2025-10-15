using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDataModel
{
    public class TriangleShape : IShape
    {
        public ShapeType Type => ShapeType.Triangle;
        public List<Point> Points { get; } = new();

        public TriangleShape(List<Point> points)
        {
            Points.AddRange(points);
        }
    }
}
