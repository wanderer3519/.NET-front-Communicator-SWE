using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasDataModel
{
    public class EllipseShape : IShape
    {
        public ShapeType Type => ShapeType.EllipseShape;
        public List<Point> Points { get; } = new();

        public EllipseShape(List<Point> points)
        {
            Points.AddRange(points);
        }
    }
}
