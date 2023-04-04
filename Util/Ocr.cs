using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using OpenCvSharp;
using Sdcb.PaddleInference;
using Sdcb.PaddleOCR;
using Sdcb.PaddleOCR.Models;
using Sdcb.PaddleOCR.Models.LocalV3;

namespace Util
{
    public class Ocr
    {
        public string TextRecognition(Bitmap bitmap)
        {
            var model = LocalFullModels.ChineseV3;
            using var all = new PaddleOcrAll(model, PaddleDevice.Mkldnn())
            {
                AllowRotateDetection = true, /* ����ʶ���нǶȵ����� */
                Enable180Classification = false, /* ����ʶ����ת�Ƕȴ���90�ȵ����� */
            };

            var memory = new MemoryStream();
            bitmap.Save(memory, ImageFormat.Png);

            using var src = Cv2.ImDecode(memory.GetBuffer(), ImreadModes.Color);
            var result = all.Run(src);

            return result.Text;
        }
    }
}
