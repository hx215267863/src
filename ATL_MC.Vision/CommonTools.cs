using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HalconDotNet;
using System.Runtime.InteropServices;

public partial class Tools
{
    public int SavePicBMP(Byte[] bmp, int width, int height, string filename)
    {
        HObject Image = null;
        int size = bmp.Length;
        IntPtr p_bmp = Marshal.AllocHGlobal(size);
        Marshal.Copy(bmp, 0, p_bmp, size);
        HOperatorSet.GenImage1Extern(out Image, "byte", width, height, p_bmp, 0);
        HOperatorSet.WriteImage(Image, "bmp", 0, filename);
        Image.Dispose();
        Marshal.FreeHGlobal(p_bmp);
        return 0;
    }

    public int SavePicJPEG(Byte[] bmp, int width, int height, string filename)
    {
        try
        {
            HObject Image = null;
            int size = bmp.Length;
            IntPtr p_bmp = Marshal.AllocHGlobal(size);
            Marshal.Copy(bmp, 0, p_bmp, size);
            HOperatorSet.GenImage1Extern(out Image, "byte", width, height, p_bmp, 0);
            HOperatorSet.WriteImage(Image, "jpeg", 0, filename);
            Image.Dispose();
            Marshal.FreeHGlobal(p_bmp);
        }
        catch(Exception err)
        {

        }
        
        return 0;
    }
}
