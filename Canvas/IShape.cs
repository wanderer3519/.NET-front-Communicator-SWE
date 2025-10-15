
using System.Drawing;

namespace CanvasDataModel
{
    public interface IShape
    {
        ShapeType Type { get; }
        public List<Point> Points { get; }

        //void RenderShape(Controls.Canvas canvas);
    }
}