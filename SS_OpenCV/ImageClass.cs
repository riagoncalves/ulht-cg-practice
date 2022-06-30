using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Linq;
using System.IO;
using System.Drawing;

namespace CG_OpenCV
{
    class ImageClass
    {

        /// <summary>
        /// Image Negative using EmguCV library
        /// Slower method
        /// </summary>
        /// <param name="img">Image</param>
        public static void Negative(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                int step = m.widthStep;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {

                            // store in the image
                            (dataPtr + nChan * x + step * y)[0] = (byte)(255 - (dataPtr + nChan * x + step * y)[0]);
                            (dataPtr + nChan * x + step * y)[1] = (byte)(255 - (dataPtr + nChan * x + step * y)[1]);
                            (dataPtr + nChan * x + step * y)[2] = (byte)(255 - (dataPtr + nChan * x + step * y)[2]);

                            // advance the pointer to the next pixel
                            //dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        //dataPtr += padding;
                    }
                }
            }
        }

        /// <summary>
        /// Convert to gray
        /// Direct access to memory - faster method
        /// </summary>
        /// <param name="img">image</param>
        public static void ConvertToGray(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                int step = m.widthStep;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                         
                            // convert to gray
                            gray = (byte)Math.Round(((int)(dataPtr + nChan * x + step * y)[0] + (dataPtr + nChan * x + step * y)[1] + (dataPtr + nChan * x + step * y)[2]) / 3.0);

                            // store in the image
                            (dataPtr + nChan * x + step * y)[0] = gray;
                            (dataPtr + nChan * x + step * y)[1] = gray;
                            (dataPtr + nChan * x + step * y)[2] = gray;

                            // advance the pointer to the next pixel
                            //dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        //dataPtr += padding;
                    }
                }
            }
        }

        public static void BrightContrast(Image<Bgr, byte> img, int bright, double contrast)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                int step = m.widthStep;
                double aux = 0;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            // store in the image
                            blue = (dataPtr + nChan * x + step * y)[0];
                            green = (dataPtr + nChan * x + step * y)[1];
                            red = (dataPtr + nChan * x + step * y)[2];

                            aux = Math.Round(blue * contrast + bright);
                            (dataPtr + nChan * x + step * y)[0] = (byte)(aux > 255 ? 255 : aux < 0 ? 0 : aux);

                            aux = Math.Round(green * contrast + bright);
                            (dataPtr + nChan * x + step * y)[1] = (byte)(aux > 255 ? 255 : aux < 0 ? 0 : aux);

                            aux = Math.Round(red * contrast + bright);
                            (dataPtr + nChan * x + step * y)[2] = (byte)(aux > 255 ? 255 : aux < 0 ? 0 : aux);

                            // advance the pointer to the next pixel
                            //dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        //dataPtr += padding;
                    }
                }
            }
        }


        public static void RedChannel(Image<Bgr, byte> img)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                int step = m.widthStep;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {

                            // store in the image
                            (dataPtr + nChan * x + step * y)[0] = (dataPtr + nChan * x + step * y)[2];
                            (dataPtr + nChan * x + step * y)[1] = (dataPtr + nChan * x + step * y)[2]; ;

                            // advance the pointer to the next pixel
                            //dataPtr += nChan;
                        }

                        //at the end of the line advance the pointer by the aligment bytes (padding)
                        //dataPtr += padding;
                    }
                }
            }
        }


        public static void Translation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, int dx, int dy)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrWrite = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrRead = (byte*)mCopy.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                int xo, yo;
                int step = m.widthStep;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        yo = y - dy;
                        for (x = 0; x < width; x++)
                        {
                            xo = x - dx;

                            if (xo >= 0 && xo < width && yo >= 0 && yo < height)
                            {
                                blue = (dataPtrRead + nChan * xo + step * yo)[0];
                                green = (dataPtrRead + nChan * xo + step * yo)[1];
                                red = (dataPtrRead + nChan * xo + step * yo)[2];

                                (dataPtrWrite + nChan * x + step * y)[0] = blue;
                                (dataPtrWrite + nChan * x + step * y)[1] = green;
                                (dataPtrWrite + nChan * x + step * y)[2] = red;
                            }
                            else
                            {
                                (dataPtrWrite + nChan * x + step * y)[0] = 0;
                                (dataPtrWrite + nChan * x + step * y)[1] = 0;
                                (dataPtrWrite + nChan * x + step * y)[2] = 0;
                            }
                        }
                    }
                }
            }
        }

        public static void Rotation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float angle)
        {
            unsafe
            {

                MIplImage m = img.MIplImage;
                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrWrite = (byte*)m.imageData.ToPointer();
                byte* dataPtrRead = (byte*)mCopy.imageData.ToPointer();
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;
                int xo, yo;
                int step = m.widthStep;

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        
                        for (x = 0; x < width; x++)
                        {
                            yo = (int)Math.Round((height / 2.0) - (x - (width / 2.0)) * (Math.Sin(angle)) - ((height / 2.0) - y) * Math.Cos(angle));
                            xo = (int)Math.Round((x - (width / 2.0)) * Math.Cos(angle) - ((height / 2.0) - y) * Math.Sin(angle) + (width / 2.0));

                            if (xo >= 0 && xo < width && yo >= 0 && yo < height)
                            {
                                blue = (dataPtrRead + nChan * xo + step * yo)[0];
                                green = (dataPtrRead + nChan * xo + step * yo)[1];
                                red = (dataPtrRead + nChan * xo + step * yo)[2];

                                (dataPtrWrite + nChan * x + step * y)[0] = blue;
                                (dataPtrWrite + nChan * x + step * y)[1] = green;
                                (dataPtrWrite + nChan * x + step * y)[2] = red;
                            }
                            else
                            {
                                (dataPtrWrite + nChan * x + step * y)[0] = 0;
                                (dataPtrWrite + nChan * x + step * y)[1] = 0;
                                (dataPtrWrite + nChan * x + step * y)[2] = 0;
                            }
                        }
                    }
                }
            }
        }

        public static void Scale(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor)
        {
            unsafe
            {
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrWrite = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrRead = (byte*)mCopy.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                int xo, yo;
                int step = m.widthStep;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {

                        for (x = 0; x < width; x++)
                        {
                            xo = (int)Math.Round(((x - (width / 2.0)) / scaleFactor) + width);
                            yo = (int)Math.Round(((y - (height / 2.0)) / scaleFactor) + height);
                            
                            if (xo >= 0 && xo < width && yo >= 0 && yo < height)
                            {
                                blue = (dataPtrRead + nChan * xo + step * yo)[0];
                                green = (dataPtrRead + nChan * xo + step * yo)[1];
                                red = (dataPtrRead + nChan * xo + step * yo)[2];

                                (dataPtrWrite + nChan * x + step * y)[0] = blue;
                                (dataPtrWrite + nChan * x + step * y)[1] = green;
                                (dataPtrWrite + nChan * x + step * y)[2] = red;
                            }
                            else
                            {
                                (dataPtrWrite + nChan * x + step * y)[0] = 0;
                                (dataPtrWrite + nChan * x + step * y)[1] = 0;
                                (dataPtrWrite + nChan * x + step * y)[2] = 0;
                            }
                        }
                    }
                }
            }
        }

        public static void Scale_point_xy(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float scaleFactor, int centerX, int centerY)
        {
            unsafe
            {
                MIplImage imageCopy = imgCopy.MIplImage;
                byte* dataPtrRead = (byte*)imageCopy.imageData.ToPointer();

                MIplImage m = img.MIplImage;
                byte* dataPtrWrite = (byte*)m.imageData.ToPointer();

                byte red, green, blue;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y, xo, yo;
                int step = m.widthStep;
                int stepCopy = m.widthStep;

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            xo = (int)Math.Round((x - width / 2) / scaleFactor + centerX);
                            yo = (int)Math.Round((y - height / 2) / scaleFactor + centerY);

                            if (xo >= 0 && xo < width && yo >= 0 && yo < height)
                            {
                                blue = (dataPtrRead + nChan * xo + stepCopy * yo)[0];
                                green = (dataPtrRead + nChan * xo + stepCopy * yo)[1];
                                red = (dataPtrRead + nChan * xo + stepCopy * yo)[2];
                            }
                            else
                            {
                                red = green = blue = 0;
                            }

                            (dataPtrWrite + nChan * x + step * y)[0] = blue;
                            (dataPtrWrite + nChan * x + step * y)[1] = green;
                            (dataPtrWrite + nChan * x + step * y)[2] = red;
                        }
                    }
                }
            }
        }


        public static void Mean(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage imageCopy = imgCopy.MIplImage;
                byte* dataPtrRead = (byte*)imageCopy.imageData.ToPointer();

                MIplImage m = img.MIplImage;
                byte* dataPtrWrite = (byte*)m.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;
                int step = m.widthStep;
                int stepCopy = m.widthStep;

                if (nChan == 3)
                {
                    /* Canto superior esquerdo */
                    x = 0;
                    y = 0;
                    (dataPtrWrite + nChan * x + step * y)[0] = (byte)Math.Round(((dataPtrRead + nChan * (x) + stepCopy * (y))[0] * 4 + (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] * 2 + (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] * 2 + (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0]) / 9.0);
                    (dataPtrWrite + nChan * x + step * y)[1] = (byte)Math.Round(((dataPtrRead + nChan * (x) + stepCopy * (y))[1] * 4 + (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] * 2 + (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] * 2 + (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1]) / 9.0);
                    (dataPtrWrite + nChan * x + step * y)[2] = (byte)Math.Round(((dataPtrRead + nChan * (x) + stepCopy * (y))[2] * 4 + (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] * 2 + (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] * 2 + (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2]) / 9.0);

                    /* Canto superior direito */
                    x = width - 1;
                    y = 0;
                    (dataPtrWrite + nChan * x + step * y)[0] = (byte)Math.Round(((dataPtrRead + nChan * (x) + stepCopy * (y))[0] * 4 + (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] * 2 + (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] * 2 + (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0]) / 9.0);
                    (dataPtrWrite + nChan * x + step * y)[1] = (byte)Math.Round(((dataPtrRead + nChan * (x) + stepCopy * (y))[1] * 4 + (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] * 2 + (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] * 2 + (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1]) / 9.0);
                    (dataPtrWrite + nChan * x + step * y)[2] = (byte)Math.Round(((dataPtrRead + nChan * (x) + stepCopy * (y))[2] * 4 + (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] * 2 + (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] * 2 + (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2]) / 9.0);

                    /* Canto inferior esquerdo */
                    x = 0;
                    y = height - 1;
                    (dataPtrWrite + nChan * x + step * y)[0] = (byte)Math.Round(((dataPtrRead + nChan * (x) + stepCopy * (y))[0] * 4 + (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] * 2 + (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] * 2 + (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0]) / 9.0);
                    (dataPtrWrite + nChan * x + step * y)[1] = (byte)Math.Round(((dataPtrRead + nChan * (x) + stepCopy * (y))[1] * 4 + (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] * 2 + (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] * 2 + (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1]) / 9.0);
                    (dataPtrWrite + nChan * x + step * y)[2] = (byte)Math.Round(((dataPtrRead + nChan * (x) + stepCopy * (y))[2] * 4 + (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] * 2 + (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] * 2 + (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2]) / 9.0);

                    /* Canto inferior direito */
                    x = width - 1;
                    y = height - 1;
                    (dataPtrWrite + nChan * x + step * y)[0] = (byte)Math.Round(((dataPtrRead + nChan * (x) + stepCopy * (y))[0] * 4 + (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] * 2 + (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] * 2 + (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0]) / 9.0);
                    (dataPtrWrite + nChan * x + step * y)[1] = (byte)Math.Round(((dataPtrRead + nChan * (x) + stepCopy * (y))[1] * 4 + (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] * 2 + (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] * 2 + (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1]) / 9.0);
                    (dataPtrWrite + nChan * x + step * y)[2] = (byte)Math.Round(((dataPtrRead + nChan * (x) + stepCopy * (y))[2] * 4 + (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] * 2 + (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] * 2 + (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2]) / 9.0);

                    /* Bordas verticais */
                    for (y = 1; y < height - 1; y++)
                    {
                        /* Borda esquerda */
                        x = 0;
                        (dataPtrWrite + nChan * x + step * y)[0] = (byte)Math.Round((
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] * 2 +
                            (dataPtrRead + nChan * (x) + stepCopy * y)[0] * 2 +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] * 2 +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0]) / 9.0);

                        (dataPtrWrite + nChan * x + step * y)[1] = (byte)Math.Round((
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] * 2 +
                            (dataPtrRead + nChan * (x) + stepCopy * y)[1] * 2 +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] * 2 +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1]) / 9.0);

                        (dataPtrWrite + nChan * x + step * y)[2] = (byte)Math.Round((
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] * 2 +
                            (dataPtrRead + nChan * (x) + stepCopy * y)[2] * 2 +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] * 2 +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2]) / 9.0);

                        /* Borda direita */
                        x = width - 1;
                        (dataPtrWrite + nChan * x + step * y)[0] = (byte)Math.Round((
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] * 2 +
                            (dataPtrRead + nChan * (x) + stepCopy * y)[0] * 2 +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] * 2 +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0] +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0]) / 9.0);

                        (dataPtrWrite + nChan * x + step * y)[1] = (byte)Math.Round((
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] * 2 +
                            (dataPtrRead + nChan * (x) + stepCopy * y)[1] * 2 +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] * 2 +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1] +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1]) / 9.0);

                        (dataPtrWrite + nChan * x + step * y)[2] = (byte)Math.Round((
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] * 2 +
                            (dataPtrRead + nChan * (x) + stepCopy * y)[2] * 2 +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] * 2 +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2] +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2]) / 9.0);

                    }

                    /* Bordas horizontais */
                    for (x = 1; x < width - 1; x++)
                    {
                        /* Borda superior */
                        y = 0;
                        (dataPtrWrite + nChan * x + step * y)[0] = (byte)Math.Round((
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] * (2) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y))[0] * (2) +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] * (2) +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0] +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0]) / 9.0);

                        (dataPtrWrite + nChan * x + step * y)[1] = (byte)Math.Round((
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] * (2) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y))[1] * (2) +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] * (2) +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1] +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1]) / 9.0);

                        (dataPtrWrite + nChan * x + step * y)[2] = (byte)Math.Round((
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] * (2) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y))[2] * (2) +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] * (2) +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2] +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2]) / 9.0);

                        /* Borda inferior */
                        y = height - 1;
                        (dataPtrWrite + nChan * x + step * y)[0] = (byte)Math.Round((
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] * (2) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y))[0] * (2) +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] * (2) +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0] +
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0]) / 9.0);

                        (dataPtrWrite + nChan * x + step * y)[1] = (byte)Math.Round((
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] * (2) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y))[1] * (2) +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] * (2) +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1] +
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1]) / 9.0);

                        (dataPtrWrite + nChan * x + step * y)[2] = (byte)Math.Round((
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] * (2) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y))[2] * (2) +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] * (2) +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2] +
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2]) / 9.0);

                    }

                    for (y = 1; y < height - 1; y++)
                    {
                        for (x = 1; x < width - 1; x++)
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = (byte)Math.Round((
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0] +
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y))[0] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] +
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0]) / 9.0);

                            (dataPtrWrite + nChan * x + step * y)[1] = (byte)Math.Round((
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1] +
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y))[1] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] +
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1]) / 9.0);

                            (dataPtrWrite + nChan * x + step * y)[2] = (byte)Math.Round((
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2] +
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y))[2] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] +
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2]) / 9.0);
                        }
                    }
                }
            }
        }

        public static void NonUniform(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float[,] matrix, float matrixWeight)
        {
            unsafe
            {
                MIplImage imageCopy = imgCopy.MIplImage;
                byte* dataPtrRead = (byte*)imageCopy.imageData.ToPointer();

                MIplImage m = img.MIplImage;
                byte* dataPtrWrite = (byte*)m.imageData.ToPointer();

                int red, green, blue;
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;
                int step = m.widthStep;
                int stepCopy = m.widthStep;

                if (nChan == 3)
                {
                    /* Canto superior esquerdo */
                    x = 0;
                    y = 0;
                    blue = (int)Math.Round((
                        (dataPtrRead + nChan * (x) + stepCopy * (y))[0] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]) +
                        (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] * (matrix[0, 2] + matrix[1, 2]) +
                        (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] * (matrix[2, 0] + matrix[2, 1]) +
                        (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0] * matrix[2, 2]) / matrixWeight);

                    green = (int)Math.Round((
                        (dataPtrRead + nChan * (x) + stepCopy * (y))[1] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]) +
                        (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] * (matrix[0, 2] + matrix[1, 2]) +
                        (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] * (matrix[2, 0] + matrix[2, 1]) +
                        (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1] * matrix[2, 2]) / matrixWeight);

                    red = (int)Math.Round((
                        (dataPtrRead + nChan * (x) + stepCopy * (y))[2] * (matrix[0, 0] + matrix[0, 1] + matrix[1, 0] + matrix[1, 1]) +
                        (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] * (matrix[0, 2] + matrix[1, 2]) +
                        (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] * (matrix[2, 0] + matrix[2, 1]) +
                        (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2] * matrix[2, 2]) / matrixWeight);

                    if (blue < 0)
                    {
                        blue = 0;
                    }
                    else if (blue > 255)
                    {
                        blue = 255;
                    }

                    if (green < 0)
                    {
                        green = 0;
                    }
                    else if (green > 255)
                    {
                        green = 255;
                    }

                    if (red < 0)
                    {
                        red = 0;
                    }
                    else if (red > 255)
                    {
                        red = 255;
                    }

                    (dataPtrWrite + nChan * x + step * y)[0] = (byte)blue;
                    (dataPtrWrite + nChan * x + step * y)[1] = (byte)green;
                    (dataPtrWrite + nChan * x + step * y)[2] = (byte)red;

                    /* Canto superior direito */
                    x = width - 1;
                    y = 0;
                    blue = (int)Math.Round((
                        (dataPtrRead + nChan * (x) + stepCopy * (y))[0] * (matrix[0, 1] + matrix[0, 2] + matrix[1, 1] + matrix[1, 2]) +
                        (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] * (matrix[0, 0] + matrix[1, 0]) +
                        (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] * (matrix[2, 1] + matrix[2, 2]) +
                        (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0] * matrix[2, 0]) / matrixWeight);

                    green = (int)Math.Round((
                        (dataPtrRead + nChan * (x) + stepCopy * (y))[1] * (matrix[0, 1] + matrix[0, 2] + matrix[1, 1] + matrix[1, 2]) +
                        (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] * (matrix[0, 0] + matrix[1, 0]) +
                        (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] * (matrix[2, 1] + matrix[2, 2]) +
                        (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1] * matrix[2, 0]) / matrixWeight);

                    red = (int)Math.Round((
                        (dataPtrRead + nChan * (x) + stepCopy * (y))[2] * (matrix[0, 1] + matrix[0, 2] + matrix[1, 1] + matrix[1, 2]) +
                        (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] * (matrix[0, 0] + matrix[1, 0]) +
                        (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] * (matrix[2, 1] + matrix[2, 2]) +
                        (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2] * matrix[2, 0]) / matrixWeight);

                    if (blue < 0)
                    {
                        blue = 0;
                    }
                    else if (blue > 255)
                    {
                        blue = 255;
                    }

                    if (green < 0)
                    {
                        green = 0;
                    }
                    else if (green > 255)
                    {
                        green = 255;
                    }

                    if (red < 0)
                    {
                        red = 0;
                    }
                    else if (red > 255)
                    {
                        red = 255;
                    }

                    (dataPtrWrite + nChan * x + step * y)[0] = (byte)blue;
                    (dataPtrWrite + nChan * x + step * y)[1] = (byte)green;
                    (dataPtrWrite + nChan * x + step * y)[2] = (byte)red;

                    /* Canto inferior esquerdo */
                    x = 0;
                    y = height - 1;
                    blue = (int)Math.Round((
                        (dataPtrRead + nChan * (x) + stepCopy * (y))[0] * (matrix[1, 0] + matrix[1, 1] + matrix[2, 0] + matrix[2, 1]) +
                        (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] * (matrix[1, 2] + matrix[2, 2]) +
                        (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] * (matrix[0, 0] + matrix[0, 1]) +
                        (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0] * matrix[0, 2]) / matrixWeight);

                    green = (int)Math.Round((
                        (dataPtrRead + nChan * (x) + stepCopy * (y))[1] * (matrix[1, 0] + matrix[1, 1] + matrix[2, 0] + matrix[2, 1]) +
                        (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] * (matrix[1, 2] + matrix[2, 2]) +
                        (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] * (matrix[0, 0] + matrix[0, 1]) +
                        (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1] * matrix[0, 2]) / matrixWeight);

                    red = (int)Math.Round((
                        (dataPtrRead + nChan * (x) + stepCopy * (y))[2] * (matrix[1, 0] + matrix[1, 1] + matrix[2, 0] + matrix[2, 1]) +
                        (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] * (matrix[1, 2] + matrix[2, 2]) +
                        (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] * (matrix[0, 0] + matrix[0, 1]) +
                        (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2] * matrix[0, 2]) / matrixWeight);

                    if (blue < 0)
                    {
                        blue = 0;
                    }
                    else if (blue > 255)
                    {
                        blue = 255;
                    }

                    if (green < 0)
                    {
                        green = 0;
                    }
                    else if (green > 255)
                    {
                        green = 255;
                    }

                    if (red < 0)
                    {
                        red = 0;
                    }
                    else if (red > 255)
                    {
                        red = 255;
                    }

                    (dataPtrWrite + nChan * x + step * y)[0] = (byte)blue;
                    (dataPtrWrite + nChan * x + step * y)[1] = (byte)green;
                    (dataPtrWrite + nChan * x + step * y)[2] = (byte)red;

                    /* Canto inferior direito */
                    x = width - 1;
                    y = height - 1;
                    blue = (int)Math.Round((
                        (dataPtrRead + nChan * (x) + stepCopy * (y))[0] * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2]) +
                        (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] * (matrix[1, 0] + matrix[2, 0]) +
                        (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] * (matrix[0, 1] + matrix[0, 2]) +
                        (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0] * matrix[0, 0]) / matrixWeight);

                    green = (int)Math.Round((
                        (dataPtrRead + nChan * (x) + stepCopy * (y))[1] * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2]) +
                        (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] * (matrix[1, 0] + matrix[2, 0]) +
                        (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] * (matrix[0, 1] + matrix[0, 2]) +
                        (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1] * matrix[0, 0]) / matrixWeight);

                    red = (int)Math.Round((
                        (dataPtrRead + nChan * (x) + stepCopy * (y))[2] * (matrix[1, 1] + matrix[1, 2] + matrix[2, 1] + matrix[2, 2]) +
                        (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] * (matrix[1, 0] + matrix[2, 0]) +
                        (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] * (matrix[0, 1] + matrix[0, 2]) +
                        (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2] * matrix[0, 0]) / matrixWeight);

                    if (blue < 0)
                    {
                        blue = 0;
                    }
                    else if (blue > 255)
                    {
                        blue = 255;
                    }

                    if (green < 0)
                    {
                        green = 0;
                    }
                    else if (green > 255)
                    {
                        green = 255;
                    }

                    if (red < 0)
                    {
                        red = 0;
                    }
                    else if (red > 255)
                    {
                        red = 255;
                    }

                    (dataPtrWrite + nChan * x + step * y)[0] = (byte)blue;
                    (dataPtrWrite + nChan * x + step * y)[1] = (byte)green;
                    (dataPtrWrite + nChan * x + step * y)[2] = (byte)red;

                    /* Bordas verticais */
                    for (y = 1; y < height - 1; y++)
                    {
                        /* Borda esquerda */
                        x = 0;
                        blue = (int)Math.Round((
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] * (matrix[0, 0] + matrix[0, 1]) +
                            (dataPtrRead + nChan * (x) + stepCopy * y)[0] * (matrix[1, 0] + matrix[1, 1]) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] * (matrix[2, 0] + matrix[2, 1]) +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0] * matrix[0, 2] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] * matrix[1, 2] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0] * matrix[2, 2]) / matrixWeight);

                        green = (int)Math.Round((
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] * (matrix[0, 0] + matrix[0, 1]) +
                            (dataPtrRead + nChan * (x) + stepCopy * y)[1] * (matrix[1, 0] + matrix[1, 1]) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] * (matrix[2, 0] + matrix[2, 1]) +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1] * matrix[0, 2] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] * matrix[1, 2] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1] * matrix[2, 2]) / matrixWeight);

                        red = (int)Math.Round((
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] * (matrix[0, 0] + matrix[0, 1]) +
                            (dataPtrRead + nChan * (x) + stepCopy * y)[2] * (matrix[1, 0] + matrix[1, 1]) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] * (matrix[2, 0] + matrix[2, 1]) +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2] * matrix[0, 2] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] * matrix[1, 2] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2] * matrix[2, 2]) / matrixWeight);

                        if (blue < 0)
                        {
                            blue = 0;
                        }
                        else if (blue > 255)
                        {
                            blue = 255;
                        }

                        if (green < 0)
                        {
                            green = 0;
                        }
                        else if (green > 255)
                        {
                            green = 255;
                        }

                        if (red < 0)
                        {
                            red = 0;
                        }
                        else if (red > 255)
                        {
                            red = 255;
                        }

                        (dataPtrWrite + nChan * x + step * y)[0] = (byte)blue;
                        (dataPtrWrite + nChan * x + step * y)[1] = (byte)green;
                        (dataPtrWrite + nChan * x + step * y)[2] = (byte)red;

                        /* Borda direita */
                        x = width - 1;
                        blue = (int)Math.Round((
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] * (matrix[0, 1] + matrix[0, 2]) +
                            (dataPtrRead + nChan * (x) + stepCopy * y)[0] * (matrix[1, 1] + matrix[1, 2]) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] * (matrix[2, 1] + matrix[2, 2]) +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0] * matrix[0, 0] +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] * matrix[1, 0] +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0] * matrix[2, 0]) / matrixWeight);

                        green = (int)Math.Round((
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] * (matrix[0, 1] + matrix[0, 2]) +
                            (dataPtrRead + nChan * (x) + stepCopy * y)[1] * (matrix[1, 1] + matrix[1, 2]) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] * (matrix[2, 1] + matrix[2, 2]) +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1] * matrix[0, 0] +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] * matrix[1, 0] +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1] * matrix[2, 0]) / matrixWeight);

                        red = (int)Math.Round((
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] * (matrix[0, 1] + matrix[0, 2]) +
                            (dataPtrRead + nChan * (x) + stepCopy * y)[2] * (matrix[1, 1] + matrix[1, 2]) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] * (matrix[2, 1] + matrix[2, 2]) +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2] * matrix[0, 0] +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] * matrix[1, 0] +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2] * matrix[2, 0]) / matrixWeight);

                        if (blue < 0)
                        {
                            blue = 0;
                        }
                        else if (blue > 255)
                        {
                            blue = 255;
                        }

                        if (green < 0)
                        {
                            green = 0;
                        }
                        else if (green > 255)
                        {
                            green = 255;
                        }

                        if (red < 0)
                        {
                            red = 0;
                        }
                        else if (red > 255)
                        {
                            red = 255;
                        }

                        (dataPtrWrite + nChan * x + step * y)[0] = (byte)blue;
                        (dataPtrWrite + nChan * x + step * y)[1] = (byte)green;
                        (dataPtrWrite + nChan * x + step * y)[2] = (byte)red;

                    }

                    /* Bordas horizontais */
                    for (x = 1; x < width - 1; x++)
                    {
                        /* Borda superior */
                        y = 0;
                        blue = (int)Math.Round((
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] * (matrix[0, 0] + matrix[1, 0]) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y))[0] * (matrix[0, 1] + matrix[1, 1]) +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] * (matrix[0, 2] + matrix[1, 2]) +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0] * matrix[2, 0] +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] * matrix[2, 1] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0] * matrix[2, 2]) / matrixWeight);

                        green = (int)Math.Round((
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] * (matrix[0, 0] + matrix[1, 0]) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y))[1] * (matrix[0, 1] + matrix[1, 1]) +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] * (matrix[0, 2] + matrix[1, 2]) +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1] * matrix[2, 0] +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] * matrix[2, 1] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1] * matrix[2, 2]) / matrixWeight);

                        red = (int)Math.Round((
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] * (matrix[0, 0] + matrix[1, 0]) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y))[2] * (matrix[0, 1] + matrix[1, 1]) +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] * (matrix[0, 2] + matrix[1, 2]) +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2] * matrix[2, 0] +
                            (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] * matrix[2, 1] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2] * matrix[2, 2]) / matrixWeight);

                        if (blue < 0)
                        {
                            blue = 0;
                        }
                        else if (blue > 255)
                        {
                            blue = 255;
                        }

                        if (green < 0)
                        {
                            green = 0;
                        }
                        else if (green > 255)
                        {
                            green = 255;
                        }

                        if (red < 0)
                        {
                            red = 0;
                        }
                        else if (red > 255)
                        {
                            red = 255;
                        }

                        (dataPtrWrite + nChan * x + step * y)[0] = (byte)blue;
                        (dataPtrWrite + nChan * x + step * y)[1] = (byte)green;
                        (dataPtrWrite + nChan * x + step * y)[2] = (byte)red;

                        /* Borda inferior */
                        y = height - 1;
                        blue = (int)Math.Round((
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] * (matrix[1, 0] + matrix[2, 0]) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y))[0] * (matrix[1, 1] + matrix[2, 1]) +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] * (matrix[1, 2] + matrix[2, 2]) +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0] * matrix[0, 0] +
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] * matrix[0, 1] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0] * matrix[0, 2]) / matrixWeight);

                        green = (int)Math.Round((
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] * (matrix[1, 0] + matrix[2, 0]) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y))[1] * (matrix[1, 1] + matrix[2, 1]) +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] * (matrix[1, 2] + matrix[2, 2]) +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1] * matrix[0, 0] +
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] * matrix[0, 1] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1] * matrix[0, 2]) / matrixWeight);

                        red = (int)Math.Round((
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] * (matrix[1, 0] + matrix[2, 0]) +
                            (dataPtrRead + nChan * (x) + stepCopy * (y))[2] * (matrix[1, 1] + matrix[2, 1]) +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] * (matrix[1, 2] + matrix[2, 2]) +
                            (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2] * matrix[0, 0] +
                            (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] * matrix[0, 1] +
                            (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2] * matrix[0, 2]) / matrixWeight);

                        if (blue < 0)
                        {
                            blue = 0;
                        }
                        else if (blue > 255)
                        {
                            blue = 255;
                        }

                        if (green < 0)
                        {
                            green = 0;
                        }
                        else if (green > 255)
                        {
                            green = 255;
                        }

                        if (red < 0)
                        {
                            red = 0;
                        }
                        else if (red > 255)
                        {
                            red = 255;
                        }

                        (dataPtrWrite + nChan * x + step * y)[0] = (byte)blue;
                        (dataPtrWrite + nChan * x + step * y)[1] = (byte)green;
                        (dataPtrWrite + nChan * x + step * y)[2] = (byte)red;
                    }

                    for (y = 1; y < height - 1; y++)
                    {
                        for (x = 1; x < width - 1; x++)
                        {
                            blue = (int)Math.Round((
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0] * matrix[0, 0] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] * matrix[0, 1] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0] * matrix[0, 2] +
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] * matrix[1, 0] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y))[0] * matrix[1, 1] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] * matrix[1, 2] +
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0] * matrix[2, 0] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] * matrix[2, 1] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0] * matrix[2, 2]) / matrixWeight);

                            green = (int)Math.Round((
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1] * matrix[0, 0] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] * matrix[0, 1] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1] * matrix[0, 2] +
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] * matrix[1, 0] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y))[1] * matrix[1, 1] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] * matrix[1, 2] +
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1] * matrix[2, 0] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] * matrix[2, 1] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1] * matrix[2, 2]) / matrixWeight);

                            red = (int)Math.Round((
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2] * matrix[0, 0] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] * matrix[0, 1] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2] * matrix[0, 2] +
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] * matrix[1, 0] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y))[2] * matrix[1, 1] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] * matrix[1, 2] +
                                (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2] * matrix[2, 0] +
                                (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] * matrix[2, 1] +
                                (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2] * matrix[2, 2]) / matrixWeight);


                            if (blue < 0)
                            {
                                blue = 0;
                            }
                            else if (blue > 255)
                            {
                                blue = 255;
                            }

                            if (green < 0)
                            {
                                green = 0;
                            }
                            else if (green > 255)
                            {
                                green = 255;
                            }

                            if (red < 0)
                            {
                                red = 0;
                            }
                            else if (red > 255)
                            {
                                red = 255;
                            }

                            (dataPtrWrite + nChan * x + step * y)[0] = (byte)blue;
                            (dataPtrWrite + nChan * x + step * y)[1] = (byte)green;
                            (dataPtrWrite + nChan * x + step * y)[2] = (byte)red;
                        }
                    }
                }
            }
        }
    
        public static void Sobel(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage imageCopy = imgCopy.MIplImage;
                byte* dataPtrRead = (byte*)imageCopy.imageData.ToPointer();

                MIplImage m = img.MIplImage;
                byte* dataPtrWrite = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y, sx, sy;
                int step = m.widthStep;

                if (nChan == 3) // image in RGB
                {
                    for (y = 1; y < height - 1; y++)
                    {
                        //core
                        for (x = 1; x < width - 1; x++)
                        {
                            sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[0] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0]) -
                                ((dataPtrRead + nChan * (x + 1) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[0] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0])
                                );

                            sy = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y + 1))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0]) -
                                ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[0])
                                );

                            if (sx + sy > 255)
                            {
                                (dataPtrWrite + nChan * x + step * y)[0] = 255;
                            }
                            else
                            {
                                (dataPtrWrite + nChan * x + step * y)[0] = (byte)(sx + sy);
                            }


                            sx = (int)Math.Abs(
                                    ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[1] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1]) -
                                    ((dataPtrRead + nChan * (x + 1) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[1] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1])
                                    );

                            sy = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y + 1))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1]) -
                                ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1])
                                );

                            if (sx + sy > 255)
                            {
                                (dataPtrWrite + nChan * x + step * y)[1] = 255;
                            }
                            else
                            {
                                (dataPtrWrite + nChan * x + step * y)[1] = (byte)(sx + sy);
                            }

                            sx = (int)Math.Abs(
                                    ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[2] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2]) -
                                    ((dataPtrRead + nChan * (x + 1) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[2] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2])
                                    );

                            sy = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y + 1))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2]) -
                                ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[2])
                                );

                            if (sx + sy > 255)
                            {
                                (dataPtrWrite + nChan * x + step * y)[2] = 255;
                            }
                            else
                            {
                                (dataPtrWrite + nChan * x + step * y)[2] = (byte)(sx + sy);
                            }
                        }
                    }

                    for (x = 1; x < width - 1; x++)
                    {
                        //linha inicial
                        y = 0;

                        sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y))[0] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[0] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0]) -
                                ((dataPtrRead + nChan * (x + 1) + step * (y))[0] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[0] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0])
                                );

                        sy = (int)Math.Abs(
                            ((dataPtrRead + nChan * (x - 1) + step * (y + 1))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0]) -
                            ((dataPtrRead + nChan * (x - 1) + step * (y))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y))[0] + (dataPtrRead + nChan * (x + 1) + step * (y))[0])
                            );

                        if (sx + sy > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = (byte)(sx + sy);
                        }

                        sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y))[1] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[1] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1]) -
                                ((dataPtrRead + nChan * (x + 1) + step * (y))[1] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[1] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1])
                                );

                        sy = (int)Math.Abs(
                            ((dataPtrRead + nChan * (x - 1) + step * (y + 1))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1]) -
                            ((dataPtrRead + nChan * (x - 1) + step * (y))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y))[1] + (dataPtrRead + nChan * (x + 1) + step * (y))[1])
                            );

                        if (sx + sy > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[1] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[1] = (byte)(sx + sy);
                        }

                        sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y))[2] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[2] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2]) -
                                ((dataPtrRead + nChan * (x + 1) + step * (y))[2] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[2] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2])
                                );

                        sy = (int)Math.Abs(
                            ((dataPtrRead + nChan * (x - 1) + step * (y + 1))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2]) -
                            ((dataPtrRead + nChan * (x - 1) + step * (y))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y))[2] + (dataPtrRead + nChan * (x + 1) + step * (y))[2])
                            );

                        if (sx + sy > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[2] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[2] = (byte)(sx + sy);
                        }

                        // linha final
                        y = height - 1;
                        sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[0] + (dataPtrRead + nChan * (x - 1) + step * (y))[0]) -
                                ((dataPtrRead + nChan * (x + 1) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[0] + (dataPtrRead + nChan * (x + 1) + step * (y))[0])
                                );

                        sy = (int)Math.Abs(
                            ((dataPtrRead + nChan * (x - 1) + step * (y))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y))[0] + (dataPtrRead + nChan * (x + 1) + step * (y))[0]) -
                            ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[0])
                            );

                        if (sx + sy > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = (byte)(sx + sy);
                        }

                        sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[1] + (dataPtrRead + nChan * (x - 1) + step * (y))[1]) -
                                ((dataPtrRead + nChan * (x + 1) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[1] + (dataPtrRead + nChan * (x + 1) + step * (y))[1])
                                );

                        sy = (int)Math.Abs(
                            ((dataPtrRead + nChan * (x - 1) + step * (y))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y))[1] + (dataPtrRead + nChan * (x + 1) + step * (y))[1]) -
                            ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1])
                            );

                        if (sx + sy > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[1] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[1] = (byte)(sx + sy);
                        }

                        sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[2] + (dataPtrRead + nChan * (x - 1) + step * (y))[2]) -
                                ((dataPtrRead + nChan * (x + 1) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[2] + (dataPtrRead + nChan * (x + 1) + step * (y))[2])
                                );

                        sy = (int)Math.Abs(
                            ((dataPtrRead + nChan * (x - 1) + step * (y))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y))[2] + (dataPtrRead + nChan * (x + 1) + step * (y))[2]) -
                            ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[2])
                            );

                        if (sx + sy > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[2] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[2] = (byte)(sx + sy);
                        }
                    }

                    for (y = 1; y < height - 1; y++)
                    {
                        x = 0;
                        //coluna inicial                        
                        sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y))[0] + (dataPtrRead + nChan * (x) + step * (y + 1))[0]) -
                                ((dataPtrRead + nChan * (x + 1) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[0] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0])
                                );

                        sy = (int)Math.Abs(
                            ((dataPtrRead + nChan * (x) + step * (y + 1))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0]) -
                            ((dataPtrRead + nChan * (x) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[0])
                            );

                        if (sx + sy > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = (byte)(sx + sy);
                        }

                        sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y))[1] + (dataPtrRead + nChan * (x) + step * (y + 1))[1]) -
                                ((dataPtrRead + nChan * (x + 1) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[1] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1])
                                );

                        sy = (int)Math.Abs(
                            ((dataPtrRead + nChan * (x) + step * (y + 1))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1]) -
                            ((dataPtrRead + nChan * (x) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1])
                            );

                        if (sx + sy > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[1] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[1] = (byte)(sx + sy);
                        }

                        sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y))[2] + (dataPtrRead + nChan * (x) + step * (y + 1))[2]) -
                                ((dataPtrRead + nChan * (x + 1) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[2] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2])
                                );

                        sy = (int)Math.Abs(
                            ((dataPtrRead + nChan * (x) + step * (y + 1))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2]) -
                            ((dataPtrRead + nChan * (x) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[2])
                            );

                        if (sx + sy > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[2] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[2] = (byte)(sx + sy);
                        }

                        x = width - 1;
                        //coluna final
                        sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[0] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0]) -
                                ((dataPtrRead + nChan * (x) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y))[0] + (dataPtrRead + nChan * (x) + step * (y + 1))[0])
                                );

                        sy = (int)Math.Abs(
                            ((dataPtrRead + nChan * (x - 1) + step * (y + 1))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[0] + (dataPtrRead + nChan * (x) + step * (y + 1))[0]) -
                            ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[0] + (dataPtrRead + nChan * (x) + step * (y - 1))[0])
                            );

                        if (sx + sy > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = (byte)(sx + sy);
                        }

                        sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[1] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1]) -
                                ((dataPtrRead + nChan * (x) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y))[1] + (dataPtrRead + nChan * (x) + step * (y + 1))[1])
                                );

                        sy = (int)Math.Abs(
                            ((dataPtrRead + nChan * (x - 1) + step * (y + 1))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[1] + (dataPtrRead + nChan * (x) + step * (y + 1))[1]) -
                            ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[1] + (dataPtrRead + nChan * (x) + step * (y - 1))[1])
                            );

                        if (sx + sy > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[1] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[1] = (byte)(sx + sy);
                        }

                        sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[2] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2]) -
                                ((dataPtrRead + nChan * (x) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y))[2] + (dataPtrRead + nChan * (x) + step * (y + 1))[2])
                                );

                        sy = (int)Math.Abs(
                            ((dataPtrRead + nChan * (x - 1) + step * (y + 1))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[2] + (dataPtrRead + nChan * (x) + step * (y + 1))[2]) -
                            ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[2] + (dataPtrRead + nChan * (x) + step * (y - 1))[2])
                            );

                        if (sx + sy > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[2] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[2] = (byte)(sx + sy);
                        }

                    }
                    //canto superior esquerdo
                    x = 0;
                    y = 0;
                    sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x) + step * (y))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y))[0] + (dataPtrRead + nChan * (x) + step * (y + 1))[0]) -
                                ((dataPtrRead + nChan * (x + 1) + step * (y))[0] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[0] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0])
                                );

                    sy = (int)Math.Abs(
                        ((dataPtrRead + nChan * (x) + step * (y + 1))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0]) -
                        ((dataPtrRead + nChan * (x) + step * (y))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y))[0] + (dataPtrRead + nChan * (x + 1) + step * (y))[0])
                        );

                    if (sx + sy > 255)
                    {
                        (dataPtrWrite + nChan * x + step * y)[0] = 255;
                    }
                    else
                    {
                        (dataPtrWrite + nChan * x + step * y)[0] = (byte)(sx + sy);
                    }

                    sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x) + step * (y))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y))[1] + (dataPtrRead + nChan * (x) + step * (y + 1))[1]) -
                                ((dataPtrRead + nChan * (x + 1) + step * (y))[1] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[1] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1])
                                );

                    sy = (int)Math.Abs(
                        ((dataPtrRead + nChan * (x) + step * (y + 1))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1]) -
                        ((dataPtrRead + nChan * (x) + step * (y))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y))[1] + (dataPtrRead + nChan * (x + 1) + step * (y))[1])
                        );

                    if (sx + sy > 255)
                    {
                        (dataPtrWrite + nChan * x + step * y)[1] = 255;
                    }
                    else
                    {
                        (dataPtrWrite + nChan * x + step * y)[1] = (byte)(sx + sy);
                    }

                    sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x) + step * (y))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y))[2] + (dataPtrRead + nChan * (x) + step * (y + 1))[2]) -
                                ((dataPtrRead + nChan * (x + 1) + step * (y))[2] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[2] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2])
                                );

                    sy = (int)Math.Abs(
                        ((dataPtrRead + nChan * (x) + step * (y + 1))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2]) -
                        ((dataPtrRead + nChan * (x) + step * (y))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y))[2] + (dataPtrRead + nChan * (x + 1) + step * (y))[2])
                        );

                    if (sx + sy > 255)
                    {
                        (dataPtrWrite + nChan * x + step * y)[2] = 255;
                    }
                    else
                    {
                        (dataPtrWrite + nChan * x + step * y)[2] = (byte)(sx + sy);
                    }

                    //canto superior direito
                    x = width - 1;
                    y = 0;
                    sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y))[0] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[0] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0]) -
                                ((dataPtrRead + nChan * (x) + step * (y))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y))[0] + (dataPtrRead + nChan * (x) + step * (y + 1))[0])
                                );

                    sy = (int)Math.Abs(
                        ((dataPtrRead + nChan * (x - 1) + step * (y + 1))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[0] + (dataPtrRead + nChan * (x) + step * (y + 1))[0]) -
                        ((dataPtrRead + nChan * (x - 1) + step * (y))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y))[0] + (dataPtrRead + nChan * (x) + step * (y))[0])
                        );

                    if (sx + sy > 255)
                    {
                        (dataPtrWrite + nChan * x + step * y)[0] = 255;
                    }
                    else
                    {
                        (dataPtrWrite + nChan * x + step * y)[0] = (byte)(sx + sy);
                    }

                    sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y))[1] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[1] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1]) -
                                ((dataPtrRead + nChan * (x) + step * (y))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y))[1] + (dataPtrRead + nChan * (x) + step * (y + 1))[1])
                                );

                    sy = (int)Math.Abs(
                        ((dataPtrRead + nChan * (x - 1) + step * (y + 1))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[1] + (dataPtrRead + nChan * (x) + step * (y + 1))[1]) -
                        ((dataPtrRead + nChan * (x - 1) + step * (y))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y))[1] + (dataPtrRead + nChan * (x) + step * (y))[1])
                        );

                    if (sx + sy > 255)
                    {
                        (dataPtrWrite + nChan * x + step * y)[1] = 255;
                    }
                    else
                    {
                        (dataPtrWrite + nChan * x + step * y)[1] = (byte)(sx + sy);
                    }

                    sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y))[2] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[2] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2]) -
                                ((dataPtrRead + nChan * (x) + step * (y))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y))[2] + (dataPtrRead + nChan * (x) + step * (y + 1))[2])
                                );

                    sy = (int)Math.Abs(
                        ((dataPtrRead + nChan * (x - 1) + step * (y + 1))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y + 1))[2] + (dataPtrRead + nChan * (x) + step * (y + 1))[2]) -
                        ((dataPtrRead + nChan * (x - 1) + step * (y))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y))[2] + (dataPtrRead + nChan * (x) + step * (y))[2])
                        );

                    if (sx + sy > 255)
                    {
                        (dataPtrWrite + nChan * x + step * y)[2] = 255;
                    }
                    else
                    {
                        (dataPtrWrite + nChan * x + step * y)[2] = (byte)(sx + sy);
                    }

                    //canto inferior esquerdo
                    x = 0;
                    y = height - 1;
                    sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y))[0] + (dataPtrRead + nChan * (x) + step * (y))[0]) -
                                ((dataPtrRead + nChan * (x + 1) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[0] + (dataPtrRead + nChan * (x + 1) + step * (y))[0])
                                );

                    sy = (int)Math.Abs(
                        ((dataPtrRead + nChan * (x) + step * (y))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y))[0] + (dataPtrRead + nChan * (x + 1) + step * (y))[0]) -
                        ((dataPtrRead + nChan * (x) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[0])
                        );

                    if (sx + sy > 255)
                    {
                        (dataPtrWrite + nChan * x + step * y)[0] = 255;
                    }
                    else
                    {
                        (dataPtrWrite + nChan * x + step * y)[0] = (byte)(sx + sy);
                    }

                    sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y))[1] + (dataPtrRead + nChan * (x) + step * (y))[1]) -
                                ((dataPtrRead + nChan * (x + 1) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[1] + (dataPtrRead + nChan * (x + 1) + step * (y))[1])
                                );

                    sy = (int)Math.Abs(
                        ((dataPtrRead + nChan * (x) + step * (y))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y))[1] + (dataPtrRead + nChan * (x + 1) + step * (y))[1]) -
                        ((dataPtrRead + nChan * (x) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1])
                        );

                    if (sx + sy > 255)
                    {
                        (dataPtrWrite + nChan * x + step * y)[1] = 255;
                    }
                    else
                    {
                        (dataPtrWrite + nChan * x + step * y)[1] = (byte)(sx + sy);
                    }

                    sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y))[2] + (dataPtrRead + nChan * (x) + step * (y))[2]) -
                                ((dataPtrRead + nChan * (x + 1) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x + 1) + step * (y))[2] + (dataPtrRead + nChan * (x + 1) + step * (y))[2])
                                );

                    sy = (int)Math.Abs(
                        ((dataPtrRead + nChan * (x) + step * (y))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y))[2] + (dataPtrRead + nChan * (x + 1) + step * (y))[2]) -
                        ((dataPtrRead + nChan * (x) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[2])
                        );

                    if (sx + sy > 255)
                    {
                        (dataPtrWrite + nChan * x + step * y)[2] = 255;
                    }
                    else
                    {
                        (dataPtrWrite + nChan * x + step * y)[2] = (byte)(sx + sy);
                    }

                    //canto inferior direito
                    x = width - 1;
                    y = height - 1;
                    sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[0] + (dataPtrRead + nChan * (x - 1) + step * (y))[0]) -
                                ((dataPtrRead + nChan * (x) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y))[0] + (dataPtrRead + nChan * (x) + step * (y))[0])
                                );

                    sy = (int)Math.Abs(
                        ((dataPtrRead + nChan * (x - 1) + step * (y))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y))[0] + (dataPtrRead + nChan * (x) + step * (y))[0]) -
                        ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[0] + (dataPtrRead + nChan * (x) + step * (y - 1))[0])
                        );

                    if (sx + sy > 255)
                    {
                        (dataPtrWrite + nChan * x + step * y)[0] = 255;
                    }
                    else
                    {
                        (dataPtrWrite + nChan * x + step * y)[0] = (byte)(sx + sy);
                    }

                    sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[1] + (dataPtrRead + nChan * (x - 1) + step * (y))[1]) -
                                ((dataPtrRead + nChan * (x) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y))[1] + (dataPtrRead + nChan * (x) + step * (y))[1])
                                );

                    sy = (int)Math.Abs(
                        ((dataPtrRead + nChan * (x - 1) + step * (y))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y))[1] + (dataPtrRead + nChan * (x) + step * (y))[1]) -
                        ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[1] + (dataPtrRead + nChan * (x) + step * (y - 1))[1])
                        );

                    if (sx + sy > 255)
                    {
                        (dataPtrWrite + nChan * x + step * y)[1] = 255;
                    }
                    else
                    {
                        (dataPtrWrite + nChan * x + step * y)[1] = (byte)(sx + sy);
                    }

                    sx = (int)Math.Abs(
                                ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x - 1) + step * (y))[2] + (dataPtrRead + nChan * (x - 1) + step * (y))[2]) -
                                ((dataPtrRead + nChan * (x) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y))[2] + (dataPtrRead + nChan * (x) + step * (y))[2])
                                );

                    sy = (int)Math.Abs(
                        ((dataPtrRead + nChan * (x - 1) + step * (y))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y))[2] + (dataPtrRead + nChan * (x) + step * (y))[2]) -
                        ((dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x) + step * (y - 1))[2] + (dataPtrRead + nChan * (x) + step * (y - 1))[2])
                        );

                    if (sx + sy > 255)
                    {
                        (dataPtrWrite + nChan * x + step * y)[2] = 255;
                    }
                    else
                    {
                        (dataPtrWrite + nChan * x + step * y)[2] = (byte)(sx + sy);
                    }
                }
            }

        }

        public static void Diferentiation(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage imageCopy = imgCopy.MIplImage;
                byte* dataPtrRead = (byte*)imageCopy.imageData.ToPointer();

                MIplImage m = img.MIplImage;
                byte* dataPtrWrite = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y, g;
                int step = m.widthStep;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height - 1; y++)
                    {
                        //inicio
                        for (x = 0; x < width - 1; x++)
                        {
                            g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[0] - (dataPtrRead + nChan * (x + 1) + step * (y))[0]) +
                                (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[0] - (dataPtrRead + nChan * (x) + step * (y + 1))[0]);

                            if (g > 255)
                            {
                                (dataPtrWrite + nChan * x + step * y)[0] = 255;
                            }
                            else
                            {
                                (dataPtrWrite + nChan * x + step * y)[0] = (byte)(g);
                            }

                            g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[1] - (dataPtrRead + nChan * (x + 1) + step * (y))[1]) +
                                (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[1] - (dataPtrRead + nChan * (x) + step * (y + 1))[1]);

                            if (g > 255)
                            {
                                (dataPtrWrite + nChan * x + step * y)[1] = 255;
                            }
                            else
                            {
                                (dataPtrWrite + nChan * x + step * y)[1] = (byte)(g);
                            }

                            g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[2] - (dataPtrRead + nChan * (x + 1) + step * (y))[2]) +
                                (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[2] - (dataPtrRead + nChan * (x) + step * (y + 1))[2]);

                            if (g > 255)
                            {
                                (dataPtrWrite + nChan * x + step * y)[2] = 255;
                            }
                            else
                            {
                                (dataPtrWrite + nChan * x + step * y)[2] = (byte)(g);
                            }
                        }
                    }

                    for (x = 0; x < width - 1; x++)
                    {
                        // linha final
                        y = height - 1;
                        g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[0] - (dataPtrRead + nChan * (x + 1) + step * (y))[0]);

                        if (g > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = (byte)(g);
                        }

                        g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[1] - (dataPtrRead + nChan * (x + 1) + step * (y))[1]);

                        if (g > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[1] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[1] = (byte)(g);
                        }

                        g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[2] - (dataPtrRead + nChan * (x + 1) + step * (y))[2]);

                        if (g > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[2] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[2] = (byte)(g);
                        }
                    }

                    for (y = 0; y < height - 1; y++)
                    {
                        //coluna final
                        x = width - 1;
                        g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[0] - (dataPtrRead + nChan * (x) + step * (y + 1))[0]);

                        if (g > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = (byte)(g);
                        }

                        g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[1] - (dataPtrRead + nChan * (x) + step * (y + 1))[1]);

                        if (g > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[1] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[1] = (byte)(g);
                        }

                        g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[2] - (dataPtrRead + nChan * (x) + step * (y + 1))[2]);

                        if (g > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[2] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[2] = (byte)(g);
                        }

                    }

                    //canto inferior direito
                    x = width - 1;
                    y = height - 1;
                    (dataPtrWrite + nChan * x + step * y)[0] = 0;
                    (dataPtrWrite + nChan * x + step * y)[1] = 0;
                    (dataPtrWrite + nChan * x + step * y)[2] = 0;
                }
            }

        }

        public static void Roberts(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage imageCopy = imgCopy.MIplImage;
                byte* dataPtrRead = (byte*)imageCopy.imageData.ToPointer();

                MIplImage m = img.MIplImage;
                byte* dataPtrWrite = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y, g;
                int step = m.widthStep;

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height - 1; y++)
                    {
                        //inicio
                        for (x = 0; x < width - 1; x++)
                        {
                            g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[0] - (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0]) +
                                (int)Math.Abs((dataPtrRead + nChan * (x + 1) + step * (y))[0] - (dataPtrRead + nChan * (x) + step * (y + 1))[0]);

                            if (g > 255)
                            {
                                (dataPtrWrite + nChan * x + step * y)[0] = 255;
                            }
                            else
                            {
                                (dataPtrWrite + nChan * x + step * y)[0] = (byte)(g);
                            }

                            g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[1] - (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1]) +
                                (int)Math.Abs((dataPtrRead + nChan * (x + 1) + step * (y))[1] - (dataPtrRead + nChan * (x) + step * (y + 1))[1]);

                            if (g > 255)
                            {
                                (dataPtrWrite + nChan * x + step * y)[1] = 255;
                            }
                            else
                            {
                                (dataPtrWrite + nChan * x + step * y)[1] = (byte)(g);
                            }

                            g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[2] - (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2]) +
                                (int)Math.Abs((dataPtrRead + nChan * (x + 1) + step * (y))[2] - (dataPtrRead + nChan * (x) + step * (y + 1))[2]);

                            if (g > 255)
                            {
                                (dataPtrWrite + nChan * x + step * y)[2] = 255;
                            }
                            else
                            {
                                (dataPtrWrite + nChan * x + step * y)[2] = (byte)(g);
                            }
                        }
                    }

                    for (x = 0; x < width - 1; x++)
                    {
                        // linha final
                        y = height - 1;
                        g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[0] - (dataPtrRead + nChan * (x + 1) + step * (y))[0]) +
                            (int)Math.Abs((dataPtrRead + nChan * (x + 1) + step * (y))[0] - (dataPtrRead + nChan * (x) + step * (y))[0]);

                        if (g > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = (byte)(g);
                        }

                        g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[1] - (dataPtrRead + nChan * (x + 1) + step * (y))[1]) +
                            (int)Math.Abs((dataPtrRead + nChan * (x + 1) + step * (y))[1] - (dataPtrRead + nChan * (x) + step * (y))[1]);

                        if (g > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[1] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[1] = (byte)(g);
                        }

                        g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[2] - (dataPtrRead + nChan * (x + 1) + step * (y))[2]) +
                            (int)Math.Abs((dataPtrRead + nChan * (x + 1) + step * (y))[2] - (dataPtrRead + nChan * (x) + step * (y))[2]);

                        if (g > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[2] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[2] = (byte)(g);
                        }
                    }

                    for (y = 0; y < height - 1; y++)
                    {
                        //coluna final
                        x = width - 1;
                        g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[0] - (dataPtrRead + nChan * (x) + step * (y + 1))[0]) * 2;

                        if (g > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[0] = (byte)(g);
                        }

                        g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[1] - (dataPtrRead + nChan * (x) + step * (y + 1))[1]) * 2;

                        if (g > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[1] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[1] = (byte)(g);
                        }

                        g = (int)Math.Abs((dataPtrRead + nChan * (x) + step * (y))[2] - (dataPtrRead + nChan * (x) + step * (y + 1))[2]) * 2;

                        if (g > 255)
                        {
                            (dataPtrWrite + nChan * x + step * y)[2] = 255;
                        }
                        else
                        {
                            (dataPtrWrite + nChan * x + step * y)[2] = (byte)(g);
                        }

                    }

                    //canto inferior direito
                    x = width - 1;
                    y = height - 1;
                    (dataPtrWrite + nChan * x + step * y)[0] = 0;
                    (dataPtrWrite + nChan * x + step * y)[1] = 0;
                    (dataPtrWrite + nChan * x + step * y)[2] = 0;
                }
            }
        }

        public static void Median(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage imageCopy = imgCopy.MIplImage;
                byte* dataPtrRead = (byte*)imageCopy.imageData.ToPointer();

                MIplImage m = img.MIplImage;
                byte* dataPtrWrite = (byte*)m.imageData.ToPointer(); // Pointer to the image

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y, index;
                int[] p = new int[9];
                int step = m.widthStep;
                int stepCopy = imageCopy.widthStep;

                if (nChan == 3) // image in RGB
                {
                    for (y = 1; y < height - 1; y++)
                    {
                        //inicio
                        for (x = 1; x < width - 1; x++)
                        {   /* P1 - P2 */
                            p[0] = (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2]) +

                                   /* P1 - P3 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2]) +

                                   /* P1 - P4 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2]) +

                                   /* P1 - P5 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y))[2]) +

                                   /* P1 - P6 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2]) +

                                   /* P1 - P7 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2]) +

                                   /* P1 - P8 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2]) +

                                   /* P1 - P9 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2]);


                            /* P2 - P1 */
                            p[1] = (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2]) +

                                   /* P2 - P3 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2]) +

                                   /* P2 - P4 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2]) +

                                   /* P2 - P5 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y))[2]) +

                                   /* P2 - P6 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2]) +

                                   /* P2 - P7 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2]) +

                                   /* P2 - P8 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2]) +

                                   /* P2 - P9 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2]);


                            /* P3 - P1 */
                            p[2] = (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2]) +

                                   /* P3 - P2 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2]) +

                                   /* P3 - P4 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2]) +

                                   /* P3 - P5 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y))[2]) +

                                   /* P3 - P6 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2]) +

                                   /* P3 - P7 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2]) +

                                   /* P3 - P8 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2]) +

                                   /* P3 - P9 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2]);


                            /* P4 - P1 */
                            p[3] = (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2]) +

                                   /* P4 - P2 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2]) +

                                   /* P4 - P3 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2]) +

                                   /* P4 - P5 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y))[2]) +

                                   /* P4 - P6 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2]) +

                                   /* P4 - P7 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2]) +

                                   /* P4 - P8 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2]) +

                                   /* P4 - P9 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2]);


                            /* P5 - P1 */
                            p[4] = (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2]) +

                                   /* P5 - P2 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2]) +

                                   /* P5 - P3 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2]) +

                                   /* P5 - P4 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2]) +

                                   /* P5 - P6 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2]) +

                                   /* P5 - P7 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2]) +

                                   /* P5 - P8 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2]) +

                                   /* P5 - P9 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2]);


                            /* P6 - P1 */
                            p[5] = (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2]) +

                                   /* P6 - P2 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2]) +

                                   /* P6 - P3 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2]) +

                                   /* P6 - P4 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2]) +

                                   /* P6 - P5 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y))[2]) +

                                   /* P6 - P7 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2]) +

                                   /* P6 - P8 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2]) +

                                   /* P6 - P9 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2]);


                            /* P7 - P1 */
                            p[6] = (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2]) +

                                   /* P7 - P2 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2]) +

                                   /* P7 - P3 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2]) +

                                   /* P7 - P4 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2]) +

                                   /* P7 - P5 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y))[2]) +

                                   /* P7 - P6 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2]) +

                                   /* P7 - P8 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2]) +

                                   /* P7 - P9 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2]);


                            /* P8 - P1 */
                            p[7] = (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2]) +

                                   /* P8 - P2 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2]) +

                                   /* P8 - P3 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2]) +

                                   /* P8 - P4 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2]) +

                                   /* P8 - P5 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y))[2]) +

                                   /* P8 - P6 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2]) +

                                   /* P8 - P7 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2]) +

                                   /* P8 - P9 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2]);


                            /* P9 - P1 */
                            p[8] = (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2]) +

                                   /* P9 - P2 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2]) +

                                   /* P9 - P3 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2]) +

                                   /* P9 - P4 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2]) +

                                   /* P9 - P5 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y))[2]) +

                                   /* P9 - P6 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2]) +

                                   /* P9 - P7 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2]) +

                                   /* P9 - P8 */
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1]) +
                                   (int)Math.Abs((dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2] - (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2]);


                            index = Array.IndexOf(p, p.Min());
                            switch (index)
                            {
                                case 0:
                                    (dataPtrWrite + nChan * x + step * y)[0] = (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[0];
                                    (dataPtrWrite + nChan * x + step * y)[1] = (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[1];
                                    (dataPtrWrite + nChan * x + step * y)[2] = (dataPtrRead + nChan * (x - 1) + stepCopy * (y - 1))[2];
                                    break;

                                case 1:
                                    (dataPtrWrite + nChan * x + step * y)[0] = (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[0];
                                    (dataPtrWrite + nChan * x + step * y)[1] = (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[1];
                                    (dataPtrWrite + nChan * x + step * y)[2] = (dataPtrRead + nChan * (x) + stepCopy * (y - 1))[2];
                                    break;

                                case 2:
                                    (dataPtrWrite + nChan * x + step * y)[0] = (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[0];
                                    (dataPtrWrite + nChan * x + step * y)[1] = (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[1];
                                    (dataPtrWrite + nChan * x + step * y)[2] = (dataPtrRead + nChan * (x + 1) + stepCopy * (y - 1))[2];
                                    break;

                                case 3:
                                    (dataPtrWrite + nChan * x + step * y)[0] = (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[0];
                                    (dataPtrWrite + nChan * x + step * y)[1] = (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[1];
                                    (dataPtrWrite + nChan * x + step * y)[2] = (dataPtrRead + nChan * (x - 1) + stepCopy * (y))[2];
                                    break;

                                case 4:
                                    (dataPtrWrite + nChan * x + step * y)[0] = (dataPtrRead + nChan * (x) + stepCopy * (y))[0];
                                    (dataPtrWrite + nChan * x + step * y)[1] = (dataPtrRead + nChan * (x) + stepCopy * (y))[1];
                                    (dataPtrWrite + nChan * x + step * y)[2] = (dataPtrRead + nChan * (x) + stepCopy * (y))[2];
                                    break;

                                case 5:
                                    (dataPtrWrite + nChan * x + step * y)[0] = (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[0];
                                    (dataPtrWrite + nChan * x + step * y)[1] = (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[1];
                                    (dataPtrWrite + nChan * x + step * y)[2] = (dataPtrRead + nChan * (x + 1) + stepCopy * (y))[2];
                                    break;

                                case 6:
                                    (dataPtrWrite + nChan * x + step * y)[0] = (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[0];
                                    (dataPtrWrite + nChan * x + step * y)[1] = (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[1];
                                    (dataPtrWrite + nChan * x + step * y)[2] = (dataPtrRead + nChan * (x - 1) + stepCopy * (y + 1))[2];
                                    break;

                                case 7:
                                    (dataPtrWrite + nChan * x + step * y)[0] = (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[0];
                                    (dataPtrWrite + nChan * x + step * y)[1] = (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[1];
                                    (dataPtrWrite + nChan * x + step * y)[2] = (dataPtrRead + nChan * (x) + stepCopy * (y + 1))[2];
                                    break;

                                case 8:
                                    (dataPtrWrite + nChan * x + step * y)[0] = (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[0];
                                    (dataPtrWrite + nChan * x + step * y)[1] = (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[1];
                                    (dataPtrWrite + nChan * x + step * y)[2] = (dataPtrRead + nChan * (x + 1) + stepCopy * (y + 1))[2];
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public static void ConvertToBW(Emgu.CV.Image<Bgr, byte> img, int threshold) {
            unsafe {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;
                int step = m.widthStep;

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = (dataPtr + nChan * x + step * y)[0];
                            green = (dataPtr + nChan * x + step * y)[1];
                            red = (dataPtr + nChan * x + step * y)[2];

                            gray = (byte)((blue + green + red) / 3.0);

                            if(gray > threshold) {
                                (dataPtr + nChan * x + step * y)[0] = 255;
                                (dataPtr + nChan * x + step * y)[1] = 255;
                                (dataPtr + nChan * x + step * y)[2] = 255;
                            }
                            else {
                                (dataPtr + nChan * x + step * y)[0] = 0;
                                (dataPtr + nChan * x + step * y)[1] = 0;
                                (dataPtr + nChan * x + step * y)[2] = 0;
                            }
                        }
                    }
                }
            }
        }

        public static int[] Histogram_Gray(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y, blue, green, red, gray;
                int step = m.widthStep;
                int[] h = new int[256];

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = (dataPtr + nChan * x + step * y)[0];
                            green = (dataPtr + nChan * x + step * y)[1];
                            red = (dataPtr + nChan * x + step * y)[2];
                            gray = (int)Math.Round((blue + green + red) / 3.0);

                            h[gray]++;
                        }
                    }
                }
                return h;
            }
        }

        public static void ConvertToBW_Otsu(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                double q1, q2, u1, u2, sigma = 0, tmp_sigma = 0;
                double total = img.Width * img.Height;
                int i, j, t, threshold = 0;
                int[] h = Histogram_Gray(img);
                
                for (t = 0; t < 256; t++)
                {
                    q1 = q2 = u1 = u2 = 0;

                    for(i = 0; i <= t; i++)
                    {
                        q1 += h[i] / total;
                        u1 += i * h[i] / total;
                    }
                    u1 /= q1;

                    for (j = t; j < 256; j++)
                    {
                        q2 += h[j] / total;
                        u2 += j * h[j] / total;
                    }
                    u2 /= q2;
                    tmp_sigma = q1 * q2 * ((u1 - u2) * (u1 - u2));

                    if(sigma < tmp_sigma)
                    {
                        sigma = tmp_sigma;
                        threshold = t;
                    }
                }
                
                ConvertToBW(img, threshold);
            }
        }

        public static int[,] Histogram_RGB(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y, blue, green, red;
                int step = m.widthStep;
                int[,] h = new int[3,256];

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = (dataPtr + nChan * x + step * y)[0];
                            green = (dataPtr + nChan * x + step * y)[1];
                            red = (dataPtr + nChan * x + step * y)[2];

                            h[0, blue]++;
                            h[1, green]++;
                            h[2, red]++;
                        }
                    }
                }
                return h;
            }
        }

        public static int[,] Histogram_All(Emgu.CV.Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y, blue, green, red, gray;
                int step = m.widthStep;
                int[,] h = new int[4, 256];

                if (nChan == 3) // image in RGB
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            blue = (dataPtr + nChan * x + step * y)[0];
                            green = (dataPtr + nChan * x + step * y)[1];
                            red = (dataPtr + nChan * x + step * y)[2];
                            gray = (int)Math.Round((blue + green + red) / 3.0);

                            h[0, gray]++;
                            h[1, blue]++;
                            h[2, green]++;
                            h[3, red]++;
                        }
                    }
                }
                return h;
            }
        }

        public static Image<Bgr, byte> Signs(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, out List<string[]> limitSign, out List<string[]> warningSign, out List<string[]> prohibitionSign, int level)
        {
            unsafe {
                limitSign = new List<string[]>();
                warningSign = new List<string[]>();
                prohibitionSign = new List<string[]>();

                int width = img.Width;
                int height = img.Height;

                // Numbers Import
                Image[] numberFiles = new Image[10];

                for(int i = 0; i < 10; i++) {
                    numberFiles[i] = Image.FromFile("F:\\Faculdade\\Trabalhos\\computacao-grafica\\Digitos-20220625\\" + i + ".png");
                }

                doHSV(img);

                int[,] tagsMatrix = doTag(img);

                // Write_CSV(tagsMatrix);

                List<int[]> corners = getPosicoes(tagsMatrix, height, width);
                clearNoise(corners, height, width);


                // Warning Signs
                List<int[]> filterWarningsVector = filterWarnings(corners);

                foreach(int[] singleWarningSign in filterWarningsVector) {
                    string[] warningSignVector = new string[5];
                    warningSignVector[0] = "-1";
                    warningSignVector[1] = singleWarningSign[1] + ""; // Left-x
                    warningSignVector[2] = singleWarningSign[3] + ""; // Top-y
                    warningSignVector[3] = singleWarningSign[2] + ""; // Right-x
                    warningSignVector[4] = singleWarningSign[4] + ""; // Bottom-y

                    warningSign.Add(warningSignVector);
                }

                List<Image<Bgr, byte>> listSignsOriginal = listSigns(imgCopy, corners);
                doHSVBlack(listSignsOriginal);

                for(int i = 0; i < listSignsOriginal.Count; i++) {
                    int value = compareNumbers(listSignsOriginal[i], numberFiles);
                    
                    if(value < 10) {
                        string[] prohibitionSignVector = new string[5];
                        prohibitionSignVector[0] = "-1";
                        prohibitionSignVector[1] = corners[i][1] + ""; // Left-x
                        prohibitionSignVector[2] = corners[i][3] + ""; // Top-y
                        prohibitionSignVector[3] = corners[i][2] + ""; // Right-x
                        prohibitionSignVector[4] = corners[i][4] + ""; // Bottom-y

                        prohibitionSign.Add(prohibitionSignVector);
                    }
                    else {
                        string[] limitSignVector = new string[5];
                        limitSignVector[0] = value + "";
                        limitSignVector[1] = corners[i][1] + ""; // Left-x
                        limitSignVector[2] = corners[i][3] + ""; // Top-y
                        limitSignVector[3] = corners[i][2] + ""; // Right-x
                        limitSignVector[4] = corners[i][4] + ""; // Bottom-y

                        limitSign.Add(limitSignVector);
                    }
                }

                drawSquares(imgCopy, warningSign);
                drawSquares(imgCopy, prohibitionSign);
                drawSquares(imgCopy, limitSign);

                return imgCopy;
            }
        }

        public static void drawSquares(Image<Bgr, byte> img, List<string[]> signList) {
            unsafe {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;
                int step = m.widthStep;

                foreach(string[] singleSign in signList) {
                    int xleft = Int32.Parse(singleSign[1]) - 5;
                    int ytop = Int32.Parse(singleSign[2]) - 5;
                    int xright = Int32.Parse(singleSign[3]) + 5;
                    int ybottom = Int32.Parse(singleSign[4]) + 5;

                    if(singleSign[0] != "-1") {
                        Console.WriteLine("Velocidade Máxima: " + singleSign[0]);
                    }

                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            if((((x >= xleft - 2 && x <= xleft + 2) || (x >= xright - 2 && x <= xright + 2)) && y >= ytop && y <= ybottom) || (((y >= ytop - 2 && y <= ytop + 2) || (y >= ybottom - 2 && y <= ybottom + 2)) && x >= xleft && x <= xright)) {
                                (dataPtr + nChan * x + step * y)[0] = 14;
                                (dataPtr + nChan * x + step * y)[1] = 56;
                                (dataPtr + nChan * x + step * y)[2] = 255;
                            } 
                        }
                    }
                }
            }
        }

        public static void doHSV(Image<Bgr, byte> img) {
            unsafe {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int x, y;
                int step = m.widthStep;

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            double[] hsv = BGR_To_HSV((dataPtr + nChan * x + step * y)[0] / 255.0, (dataPtr + nChan * x + step * y)[1] / 255.0, (dataPtr + nChan * x + step * y)[2] / 255.0);

                            if(hsv[1] > 0.4 && hsv[2] > 0.2 && ((hsv[0] >= 0 && hsv[0] <= 25) || (hsv[0] >= 335 && hsv[0] <= 360))) {
                                (dataPtr + nChan * x + step * y)[0] = 255;
                                (dataPtr + nChan * x + step * y)[1] = 255;
                                (dataPtr + nChan * x + step * y)[2] = 255;
                            }
                            else {
                                (dataPtr + nChan * x + step * y)[0] = 0;
                                (dataPtr + nChan * x + step * y)[1] = 0;
                                (dataPtr + nChan * x + step * y)[2] = 0;
                            }   
                        }
                    }
                }
            }
        }

        public static double[] BGR_To_HSV(double blue, double green, double red) {
            double[] hsv = new double[3];
            double h = 0.0;
            double max = Math.Max(red, Math.Max(blue, green));
            double min = Math.Min(red, Math.Max(blue, green));

            if((red > green && red > blue) && green >= blue) {
                h = 60 * ((green - blue) / (max - min));
            }
            else if((red > green && red > blue) && green < blue) {
                h = 60 * ((green - blue) / (max - min)) + 360;
            }
            else if(green > red && green > blue) {
                h = 60 * ((blue - red) / (max - min)) + 120;
            }
            else if(blue > red && blue > green) {
                h = 60 * ((red - green) / (max - min)) + 240;
            }

            hsv[0] = h;
            hsv[1] = max == 0 ? 0 : (max - min) / max;
            hsv[2] = max;

            return hsv;
        }

        public static int[,] doTag(Image<Bgr, byte> img, bool margin = false) {
            unsafe {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int marginH = margin ?(int)(height * 0.15) : 0;
                int marginW = margin ?(int)(width * 0.15) : 0;
                int nChan = m.nChannels;
                int x, y;
                int step = m.widthStep;
                int [,] matrix = new int[height,width];
                int num = 1;
                bool changed = false;

                for (y = marginH; y < height - marginH; y++)
                {
                    for (x = marginW; x < width - marginW; x++)
                    {
                        if((dataPtr + nChan * x + step * y)[0] == 255) {
                            matrix[y,x] = num;
                            num++;
                        }
                        else {
                            matrix[y,x] = 0;
                        }
                    }
                }

                do {
                    changed = false;

                    for (y = 1 + marginH; y < height - 1 - marginH; y++)
                    {
                        for (x = 1 + marginW; x < width - 1 - marginW; x++)
                        {
                            if (matrix[y,x] == 0) continue;

                            int aux = matrix[y,x];

                            // Top Left
                            matrix[y,x] = matrix[y-1,x-1] != 0 && matrix[y-1,x-1] < matrix[y,x] ? matrix[y-1,x-1] : matrix[y,x];

                            // Mid Left
                            matrix[y,x] = matrix[y,x-1] != 0 && matrix[y,x-1] < matrix[y,x] ? matrix[y,x-1] : matrix[y,x];

                            // Bot Left
                            matrix[y,x] = matrix[y+1,x-1] != 0 && matrix[y+1,x-1] < matrix[y,x] ? matrix[y+1,x-1] : matrix[y,x];
                            
                            // Top Center
                            matrix[y,x] = matrix[y-1,x] != 0 && matrix[y-1,x] < matrix[y,x] ? matrix[y-1,x] : matrix[y,x];

                            // Bot Center
                            matrix[y,x] = matrix[y+1,x] != 0 && matrix[y+1,x] < matrix[y,x] ? matrix[y+1,x] : matrix[y,x];

                            // Top Right
                            matrix[y,x] = matrix[y-1,x+1] != 0 && matrix[y-1,x+1] < matrix[y,x] ? matrix[y-1,x+1] : matrix[y,x];

                            // Mid Right
                            matrix[y,x] = matrix[y,x+1] != 0 && matrix[y,x+1] < matrix[y,x] ? matrix[y,x+1] : matrix[y,x];

                            // Bot Right
                            matrix[y,x] = matrix[y+1,x+1] != 0 && matrix[y+1,x+1] < matrix[y,x] ? matrix[y+1,x+1] : matrix[y,x];

                            if(changed == false && matrix[y,x] != aux) {
                                changed = true;
                            }
                        }
                    }
                    
                    if(!changed) break;

                    changed = false;

                    for (y = height - 2 - marginH; y > marginH; y--)
                    {
                        for (x = width - 2 - marginW; x > marginW; x--)
                        {
                            if (matrix[y,x] == 0) continue;

                            int aux = matrix[y,x];

                            // Top Left
                            matrix[y,x] = matrix[y-1,x-1] != 0 && matrix[y-1,x-1] < matrix[y,x] ? matrix[y-1,x-1] : matrix[y,x];

                            // Mid Left
                            matrix[y,x] = matrix[y,x-1] != 0 && matrix[y,x-1] < matrix[y,x] ? matrix[y,x-1] : matrix[y,x];

                            // Bot Left
                            matrix[y,x] = matrix[y+1,x-1] != 0 && matrix[y+1,x-1] < matrix[y,x] ? matrix[y+1,x-1] : matrix[y,x];
                            
                            // Top Center
                            matrix[y,x] = matrix[y-1,x] != 0 && matrix[y-1,x] < matrix[y,x] ? matrix[y-1,x] : matrix[y,x];

                            // Bot Center
                            matrix[y,x] = matrix[y+1,x] != 0 && matrix[y+1,x] < matrix[y,x] ? matrix[y+1,x] : matrix[y,x];

                            // Top Right
                            matrix[y,x] = matrix[y-1,x+1] != 0 && matrix[y-1,x+1] < matrix[y,x] ? matrix[y-1,x+1] : matrix[y,x];

                            // Mid Right
                            matrix[y,x] = matrix[y,x+1] != 0 && matrix[y,x+1] < matrix[y,x] ? matrix[y,x+1] : matrix[y,x];

                            // Bot Right
                            matrix[y,x] = matrix[y+1,x+1] != 0 && matrix[y+1,x+1] < matrix[y,x] ? matrix[y+1,x+1] : matrix[y,x];

                            if(changed == false && matrix[y,x] != aux) {
                                changed = true;
                            }
                        }
                    }
                } while(changed);

                return matrix;
            }
        }

        public static void Write_CSV(int[,] matrix, string name = "tags") {
            using (System.IO.StreamWriter outfile = new System.IO.StreamWriter(@"F:\Faculdade\Trabalhos\2º Ano\2º Semestre\Computação Gráfica\CSV-Export\" + name + ".csv"))
            {
                for (int y = 0; y < matrix.GetUpperBound(0); y++)
                {
                    string content = "";
                    for (int x = 0; x < matrix.GetUpperBound(1); x++)
                    {
                        content += matrix[y,x] + ",";
                    }
                    outfile.WriteLine(content);
                }
            }
        }

        public static int compareNumbers(Image<Bgr, byte> sign, Image[] numberFiles) {
            int finalValue = -1;
            List<int> signValues = new List<int>();

            int[,] signMatrixTags = doTag(sign, true);

            List<int[]> corners = getPosicoes(signMatrixTags, sign.Height, sign.Width, true);
            clearNoise(corners, sign.Height, sign.Width);

            List<Image<Bgr, byte>> numberList = listSigns(sign, corners, true);

            int signValueIndex = 0;
            bool hasNum = false;
            
            // int refN = 0;

            foreach(Image<Bgr, byte> numberImage in numberList) {
                int width = numberImage.Width;
                int height = numberImage.Height;
                int totalPixels = height * width;
                int[,] numberImageTags = doTag(numberImage);
                double[] allDifs = new double[10];

                // Write_CSV(numberImageTags, refN + "");
                // refN++;


                for(int i = 0; i < numberFiles.Length; i++) {
                    Image<Bgr, byte> resizedImage = new Image<Bgr, Byte>(new Bitmap(numberFiles[i], new Size(width, height)));
                    int diff = 0;
                    
                    doHSVBlackSingle(resizedImage);
                    int[,] resizedImageTags = doTag(resizedImage);  

                    for(int y = 0; y < height; y++) {
                        for(int x = 0; x < width; x++) {
                            if(!(numberImageTags[y,x] == 0 && resizedImageTags[y,x] == 0)) {
                                if(numberImageTags[y,x] == 0 || resizedImageTags[y,x] == 0) diff++;
                            }
                        }
                    }

                   allDifs[i] = (diff / (double)totalPixels);

                }
                
                if(allDifs.Min() < 0.22) {
                    signValues.Add(Array.IndexOf(allDifs, allDifs.Min()));
                    signValueIndex++;
                    hasNum = true;
                }
            }

            if(hasNum) finalValue = 0;

            for(int i = 0; i < signValues.Count; i++) {
                int localValue = signValues[i];

                for(int j = 0; j < i; j++) {
                    localValue *= 10;
                }

                finalValue += localValue;
            }
            return finalValue;
        }

        public static List<int[]> filterWarnings(List<int[]> corners)
        {
            List<int[]> warningSigns = new List<int[]>();
            List<int> removeIndexs = new List<int>();

            for(int i = 0; i < corners.Count; i++) {
                if (corners[i][5] == 1) {
                    warningSigns.Add(corners[i]);
                    removeIndexs.Add(i);
                }
            }

            if(removeIndexs.Count > 0) {
                for(int index = removeIndexs.Count - 1; index >= 0; index--) {
                    corners.RemoveAt(removeIndexs[index]);
                }
            }

            return warningSigns;
        }

        public static List<int[]> getPosicoes(int[,] matrix, int h, int w, bool margin = false)
        {
            List<int> etiquetas = new List<int>();
            List<int[]> posicoes = new List<int[]>();
            int x, y;
            int marginH = margin ? (int)(h * 0.15) : 0;
            int marginW = margin ? (int)(w * 0.15) : 0;

            for (y = marginH; y < h - 1 - marginH; y++)
            {
                for (x = marginW; x < w - 1 - marginW; x++)
                {
                    if (matrix[y,x] != 0 && !etiquetas.Contains(matrix[y,x])) {
                        etiquetas.Add(matrix[y,x]);
                    }
                }
            }

            foreach (int etiqueta in etiquetas) {
                int[] posicao = new int[6];
                posicao[0] = etiqueta;
                posicao[1] = w - 1; // x left
                posicao[2] = 0; // x right
                posicao[3] = h - 1; // y top
                posicao[4] = 0; // y bottom
                posicao[5] = 0; // (isTriangle: 0 = false, 1 = true)

                int auxYL = 0;
                int auxYR = 0;

                for (y = 0; y < h - 1; y++)
                {
                    for (x = 0; x < w - 1; x++)
                    {
                        if (matrix[y,x] == etiqueta) {
                            if (posicao[1] > x) {
                                posicao[1] = x;
                                auxYL = y;
                                break;
                            }
                        }
                    }
                }

                for (y = h - 2; y > 0; y--)
                {
                    for (x = w - 2; x > 0; x--)
                    {
                        if (matrix[y,x] == etiqueta) {
                            if (posicao[2] < x) {
                                posicao[2] = x;
                                auxYR = y;
                                break;
                            }
                        }
                    }
                }
                
                for (x = 0; x < w - 1; x++)
                {
                    for (y = 0; y < h - 1; y++)
                    {
                        if (matrix[y,x] == etiqueta) {
                            if (posicao[3] > y) {
                                posicao[3] = y;
                                break;
                            }
                        }
                    }
                }

                for (x = w - 2; x > 0; x--)
                {
                    for (y = h - 2; y > 0; y--)
                    {
                        if (matrix[y,x] == etiqueta) {
                            if (posicao[4] < y) {
                                posicao[4] = y;
                                break;
                            }
                        }
                    }
                }

                int positionAuxBot = posicao[3] + 3 * ((posicao[4] - posicao[3]) / 4);
                int positionAuxTop = posicao[3] + ((posicao[4] - posicao[3]) / 4);

                posicao[5] = (auxYL >= positionAuxBot && auxYL < posicao[4] || auxYR >= positionAuxBot && auxYR < posicao[4]) ||
                             (auxYL >= posicao[3] && auxYL < positionAuxTop || auxYR >= posicao[3] && auxYR < positionAuxTop) ? 1 : 0;

                posicoes.Add(posicao);
            }
            return posicoes;
        }

        public static void clearNoise(List<int[]> corners, int h, int w)
        {
            List<int> removeIndexs = new List<int>();

            double maxArea = 0;
            double left, top, localArea;

            for (int i = 0; i < corners.Count; i++)
            {
                left = Math.Abs(corners[i][4] - corners[i][3]);
                top = Math.Abs(corners[i][2] - corners[i][1]);
                maxArea = left * top > maxArea ? left * top : maxArea;
            }

            for (int i = 0; i < corners.Count; i++)
            {
                left = Math.Abs(corners[i][4] - corners[i][3]);
                top = Math.Abs(corners[i][2] - corners[i][1]);
                localArea = left * top;
                bool isOutside = true;

                foreach(int[] position in corners) {
                    if(position[0] != corners[i][0]) {
                        if(position[1] < corners[i][1] && position[2] > corners[i][2] && position[3] < corners[i][3] && position[4] > corners[i][4]) {
                            isOutside = false;
                            break;
                        }
                    }
                }

                if (!(isOutside && (localArea / maxArea) * 100.0 > 10 && (localArea / (h * w)) * 100.0 > 0.1))
                {
                    removeIndexs.Add(i);
                }

            }

            if(removeIndexs.Count > 0) {
                for(int index = removeIndexs.Count - 1; index >= 0; index--) {
                    corners.RemoveAt(removeIndexs[index]);
                }
            }
        }

        public static List<Image<Bgr, byte>> listSigns(Image<Bgr, byte> img, List<int[]> signs, bool sort = false)
        {
            if(sort) signs.Sort((a, b) => b[1].CompareTo(a[1]));
            List<Image<Bgr, byte>> signsList = new List<Image<Bgr, byte>>();

            for (int i = 0; i < signs.Count; i++)
            {
                Rectangle rectangulo = new Rectangle(signs[i][1], signs[i][3], signs[i][2] - signs[i][1], signs[i][4] - signs[i][3]);
                Image<Bgr, byte> img_ = img.Copy(rectangulo);
                signsList.Add(img_);
            }

            return signsList;
        }

        public static void doHSVBlack(List<Image<Bgr, byte>> listSigns)
        {
            unsafe
            {
                for (int i = 0; i < listSigns.Count; i++)
                {
                    doHSVBlackSingle(listSigns[i]);
                }
            }
        }

        public static void doHSVBlackSingle(Image<Bgr, byte> img)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                byte* dataPtr = (byte*)m.imageData.ToPointer();
                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels;
                int padding = m.widthStep - m.nChannels * m.width;
                int x, y;
                int step = m.widthStep;

                if (nChan == 3)
                {
                    for (y = 0; y < height; y++)
                    {
                        for (x = 0; x < width; x++)
                        {
                            double[] hsv = BGR_To_HSV((dataPtr + nChan * x + step * y)[0] / 255.0,
                                                        (dataPtr + nChan * x + step * y)[1] / 255.0,
                                                        (dataPtr + nChan * x + step * y)[2] / 255.0);

                            if (hsv[2] < 0.45)
                            {
                                (dataPtr + nChan * x + step * y)[0] = 255;
                                (dataPtr + nChan * x + step * y)[1] = 255;
                                (dataPtr + nChan * x + step * y)[2] = 255;
                            }
                            else
                            {
                                (dataPtr + nChan * x + step * y)[0] = 0;
                                (dataPtr + nChan * x + step * y)[1] = 0;
                                (dataPtr + nChan * x + step * y)[2] = 0;
                            }
                        }
                    }
                }
            }
        }
    }
}
