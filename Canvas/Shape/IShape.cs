using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas.Shape;

public interface IShape
{
    int Id { get; }

    int Color { get; set; }

    bool IsSelected { get; set; }

    int StrokeThickness { get; }

    void Draw();

    bool ContainsPoint();

    void Rotate();

    void Fill();
}
