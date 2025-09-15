using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Canvas.Tool;

public interface ITool
{
    void OnPointerDown(EventArgs e);
    void OnPointerUp(EventArgs e);
    void OnPointerMove(EventArgs e);
}
