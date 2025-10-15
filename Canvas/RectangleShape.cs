using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDataModel
{
    public class RectangleShape : IShape
    {
        public ShapeType Type => ShapeType.Rectangle;
        public List<Point> Points { get; } = new();

        public RectangleShape(List<Point> points)
        {
            Points.AddRange(points);
        }

    }
}
