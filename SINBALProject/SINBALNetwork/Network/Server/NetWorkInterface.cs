using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SINBALNetwork.Network.Interface
{
    public interface TexBoxForm
    {
        void DrawTex(string text);
        void DrawColorTex(string text, int r, int g, int b);
        void ClearTex();
        void ShowMessageBox(string text, string caption);
    }
}
