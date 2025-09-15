using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas.Shape;

public class Rectangle : IShape
{
    public int Id => throw new NotImplementedException();

    public int Color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool IsSelected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public int StrokeThickness => throw new NotImplementedException();

    public bool ContainsPoint()
    {
        throw new NotImplementedException();
    }

    public void Draw()
    {
        throw new NotImplementedException();
    }

    public void Fill()
    {
        throw new NotImplementedException();
    }

    public void Rotate()
    {
        throw new NotImplementedException();
    }
}
