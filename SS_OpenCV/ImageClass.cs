using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV.Structure;
using Emgu.CV;

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
                byte blue, green, red, gray;

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
                byte blue, green, red, gray;

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
                byte blue, green, red, gray;

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
                byte blue, green, red, gray;

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
                byte blue, green, red, gray;

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
                // direct access to the image memory(sequencial)
                // direcion top left -> bottom right

                MIplImage m = img.MIplImage;
                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrWrite = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrRead = (byte*)mCopy.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

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
                byte blue, green, red, gray;

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

        public static void Mean(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrWrite = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrRead = (byte*)mCopy.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                int xo, yo;
                int step = m.widthStep;

                if (nChan == 3) // image in RGB
                {
                    for (y = 1; y < height - 1; y++)
                    {

                        for (x = 1; x < width - 1; x++)
                        {
                            blue = (byte)Math.Round(((dataPtrRead + nChan * x + step * y)[0] + (dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + (dataPtrRead + nChan * x + step * (y - 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[0] + (dataPtrRead + nChan * (x - 1) + step * y)[0] + (dataPtrRead + nChan * (x + 1) + step * y)[0] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0] + (dataPtrRead + nChan * x + step * (y + 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0]) / 9.0);
                            green = (byte)Math.Round(((dataPtrRead + nChan * x + step * y)[1] + (dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + (dataPtrRead + nChan * x + step * (y - 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1] + (dataPtrRead + nChan * (x - 1) + step * y)[1] + (dataPtrRead + nChan * (x + 1) + step * y)[1] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1] + (dataPtrRead + nChan * x + step * (y + 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1]) / 9.0);
                            red = (byte)Math.Round(((dataPtrRead + nChan * x + step * y)[2] + (dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + (dataPtrRead + nChan * x + step * (y - 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1] + (dataPtrRead + nChan * (x - 1) + step * y)[2] + (dataPtrRead + nChan * (x + 1) + step * y)[2] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2] + (dataPtrRead + nChan * x + step * (y + 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2]) / 9.0);

                            (dataPtrWrite + nChan * x + step * y)[0] = blue;
                            (dataPtrWrite + nChan * x + step * y)[1] = green;
                            (dataPtrWrite + nChan * x + step * y)[2] = red;
                        }
                    }

                    for (y = 0; y < height; y = height - 1)
                    {
                        for (x = 1; x < width - 1; x++)
                        {
                            if(y == 0)
                            {
                                blue = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[0]) + 2 * ((dataPtrRead + nChan * (x - 1) + step * y)[0]) + 2 * ((dataPtrRead + nChan * (x + 1) + step * y)[0]) + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0] + (dataPtrRead + nChan * x + step * (y + 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0]) / 9.0);
                                green = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[1]) + 2 * ((dataPtrRead + nChan * (x - 1) + step * y)[1]) + 2 * ((dataPtrRead + nChan * (x + 1) + step * y)[1]) + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1] + (dataPtrRead + nChan * x + step * (y + 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1]) / 9.0);
                                red = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[2]) + 2 * ((dataPtrRead + nChan * (x - 1) + step * y)[2]) + 2 * ((dataPtrRead + nChan * (x + 1) + step * y)[2]) + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2] + (dataPtrRead + nChan * x + step * (y + 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2]) / 9.0);
                            }
                            else {
                                blue = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[0]) + 2 * ((dataPtrRead + nChan * (x - 1) + step * y)[0]) + 2 * ((dataPtrRead + nChan * (x + 1) + step * y)[0]) + (dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + (dataPtrRead + nChan * x + step * (y - 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[0]) / 9.0);
                                green = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[1]) + 2 * ((dataPtrRead + nChan * (x - 1) + step * y)[1]) + 2 * ((dataPtrRead + nChan * (x + 1) + step * y)[1]) + (dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + (dataPtrRead + nChan * x + step * (y - 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1]) / 9.0);
                                red = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[2]) + 2 * ((dataPtrRead + nChan * (x - 1) + step * y)[2]) + 2 * ((dataPtrRead + nChan * (x + 1) + step * y)[2]) + (dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + (dataPtrRead + nChan * x + step * (y - 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[2]) / 9.0);
                            }


                            (dataPtrWrite + nChan * x + step * y)[0] = blue;
                            (dataPtrWrite + nChan * x + step * y)[1] = green;
                            (dataPtrWrite + nChan * x + step * y)[2] = red;
                        }
                    }

                    for (y = 1; y < height - 1; y++)
                    {
                        for (x = 0; x < width; x = width - 1)
                        {
                            if (x == 0)
                            {
                                blue = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[0]) + 2 * ((dataPtrRead + nChan * x + step * (y - 1))[0]) + 2 * ((dataPtrRead + nChan * x + step * (y + 1))[0]) + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0] + (dataPtrRead + nChan * (x + 1) + step * y)[0]) / 9.0);
                                green = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[1]) + 2 * ((dataPtrRead + nChan * x + step * (y - 1))[1]) + 2 * ((dataPtrRead + nChan * x + step * (y + 1))[1]) + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1] + (dataPtrRead + nChan * (x + 1) + step * y)[1]) / 9.0);
                                red = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[2]) + 2 * ((dataPtrRead + nChan * x + step * (y - 1))[2]) + 2 * ((dataPtrRead + nChan * x + step * (y + 1))[2]) + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2] + (dataPtrRead + nChan * (x + 1) + step * y)[2]) / 9.0);
                            }
                            else
                            {
                                blue = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[0]) + 2 * ((dataPtrRead + nChan * x + step * (y - 1))[0]) + 2 * ((dataPtrRead + nChan * x + step * (y + 1))[0]) + (dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0] + (dataPtrRead + nChan * (x - 1) + step * y)[0]) / 9.0);
                                green = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[1]) + 2 * ((dataPtrRead + nChan * x + step * (y - 1))[1]) + 2 * ((dataPtrRead + nChan * x + step * (y + 1))[1]) + (dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1] + (dataPtrRead + nChan * (x - 1) + step * y)[1]) / 9.0);
                                red = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[2]) + 2 * ((dataPtrRead + nChan * x + step * (y - 1))[2]) + 2 * ((dataPtrRead + nChan * x + step * (y + 1))[2]) + (dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2] + (dataPtrRead + nChan * (x - 1) + step * y)[2]) / 9.0);
                            }


                            (dataPtrWrite + nChan * x + step * y)[0] = blue;
                            (dataPtrWrite + nChan * x + step * y)[1] = green;
                            (dataPtrWrite + nChan * x + step * y)[2] = red;
                        }
                    }



                    (dataPtrWrite)[0] = (byte)Math.Round((4 * ((dataPtrRead)[0]) + 2 * ((dataPtrRead + nChan)[0]) + 2 * ((dataPtrRead + step * 1)[0]) + (dataPtrRead + nChan + step)[0]) / 9.0);
                    (dataPtrWrite)[1] = (byte)Math.Round((4 * ((dataPtrRead)[1]) + 2 * ((dataPtrRead + nChan)[1]) + 2 * ((dataPtrRead + step * 1)[1]) + (dataPtrRead + nChan + step)[1]) / 9.0);
                    (dataPtrWrite)[2] = (byte)Math.Round((4 * ((dataPtrRead)[2]) + 2 * ((dataPtrRead + nChan)[2]) + 2 * ((dataPtrRead + step * 1)[2]) + (dataPtrRead + nChan + step)[2]) / 9.0);

                    (dataPtrWrite + nChan * width)[0] = (byte)Math.Round((4 * ((dataPtrRead + nChan * width)[0]) + 2 * ((dataPtrRead + nChan * (width - 1))[0]) + 2 * ((dataPtrRead + nChan * width + step)[0]) + (dataPtrRead + nChan * (width - 1) + step)[0]) / 9.0);
                    (dataPtrWrite + nChan * width)[1] = (byte)Math.Round((4 * ((dataPtrRead + nChan * width)[1]) + 2 * ((dataPtrRead + nChan * (width - 1))[1]) + 2 * ((dataPtrRead + nChan * width + step)[1]) + (dataPtrRead + nChan * (width - 1) + step)[1]) / 9.0);
                    (dataPtrWrite + nChan * width)[2] = (byte)Math.Round((4 * ((dataPtrRead + nChan * width)[2]) + 2 * ((dataPtrRead + nChan * (width - 1))[2]) + 2 * ((dataPtrRead + nChan * width + step)[2]) + (dataPtrRead + nChan * (width - 1) + step)[2]) / 9.0);

                    (dataPtrWrite + step * height)[0] = (byte)Math.Round((4 * ((dataPtrRead + step * height)[0]) + 2 * ((dataPtrRead + step * (height - 1))[0]) + 2 * ((dataPtrRead + step * height + nChan)[0]) + (dataPtrRead + step * (height - 1) + nChan)[0]) / 9.0);
                    (dataPtrWrite + step * height)[1] = (byte)Math.Round((4 * ((dataPtrRead + step * height)[1]) + 2 * ((dataPtrRead + step * (height - 1))[1]) + 2 * ((dataPtrRead + step * height + nChan)[1]) + (dataPtrRead + step * (height - 1) + nChan)[1]) / 9.0);
                    (dataPtrWrite + step * height)[2] = (byte)Math.Round((4 * ((dataPtrRead + step * height)[2]) + 2 * ((dataPtrRead + step * (height - 1))[2]) + 2 * ((dataPtrRead + step * height + nChan)[2]) + (dataPtrRead + step * (height - 1) + nChan)[2]) / 9.0);

                    (dataPtrWrite + nChan * width + step * height)[0] = (byte)Math.Round((4 * ((dataPtrRead + nChan * width + step * height)[0]) + 2 * ((dataPtrRead + nChan * width + step * (height - 1))[0]) + 2 * ((dataPtrRead + nChan * (width - 1) + step * height)[0]) + (dataPtrRead + nChan * (width - 1) + step * (height - 1))[0]) / 9.0);
                    (dataPtrWrite + nChan * width + step * height)[1] = (byte)Math.Round((4 * ((dataPtrRead + nChan * width + step * height)[1]) + 2 * ((dataPtrRead + nChan * width + step * (height - 1))[1]) + 2 * ((dataPtrRead + nChan * (width - 1) + step * height)[1]) + (dataPtrRead + nChan * (width - 1) + step * (height - 1))[1]) / 9.0);
                    (dataPtrWrite + nChan * width + step * height)[2] = (byte)Math.Round((4 * ((dataPtrRead + nChan * width + step * height)[2]) + 2 * ((dataPtrRead + nChan * width + step * (height - 1))[2]) + 2 * ((dataPtrRead + nChan * (width - 1) + step * height)[2]) + (dataPtrRead + nChan * (width - 1) + step * (height - 1))[2]) / 9.0);
                }
            }
        }

        public static void NonUniform(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy, float[,] matrix, float matrixWeight)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrWrite = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrRead = (byte*)mCopy.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                int xo, yo;
                int step = m.widthStep;
                int aux;

                if (nChan == 3) // image in RGB
                {
                    for (y = 1; y < height - 1; y++)
                    {

                        for (x = 1; x < width - 1; x++)
                        {
                            aux = (int)Math.Round((matrix[1,1] * (dataPtrRead + nChan * x + step * y)[0] + matrix[0, 0] * (dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + matrix[0, 1] * (dataPtrRead + nChan * x + step * (y - 1))[0] + matrix[0, 2] * (dataPtrRead + nChan * (x + 1) + step * (y - 1))[0] + matrix[1, 0] * (dataPtrRead + nChan * (x - 1) + step * y)[0] + matrix[1, 2] * (dataPtrRead + nChan * (x + 1) + step * y)[0] + matrix[2, 0] * (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0] + matrix[2, 1] * (dataPtrRead + nChan * x + step * (y + 1))[0] + matrix[2, 2] * (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0]) / matrixWeight);
                            blue = (byte)(aux > 255 ? 255 : aux < 0 ? 0 : aux);
                            
                            aux = (int)Math.Round((matrix[1, 1] * (dataPtrRead + nChan * x + step * y)[1] + matrix[0, 0] * (dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + matrix[0, 1] * (dataPtrRead + nChan * x + step * (y - 1))[1] + matrix[0, 2] * (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1] + matrix[1, 0] * (dataPtrRead + nChan * (x - 1) + step * y)[1] + matrix[1, 2] * (dataPtrRead + nChan * (x + 1) + step * y)[1] + matrix[2, 0] * (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1] + matrix[2, 1] * (dataPtrRead + nChan * x + step * (y + 1))[1] + matrix[2, 2] * (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1]) / matrixWeight);
                            green = (byte)(aux > 255 ? 255 : aux < 0 ? 0 : aux);

                            aux = (int)Math.Round((matrix[1, 1] * (dataPtrRead + nChan * x + step * y)[2] + matrix[0, 0] * (dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + matrix[0, 1] * (dataPtrRead + nChan * x + step * (y - 1))[2] + matrix[0, 2] * (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1] + matrix[1, 0] * (dataPtrRead + nChan * (x - 1) + step * y)[2] + matrix[1, 2] * (dataPtrRead + nChan * (x + 1) + step * y)[2] + matrix[2, 0] * (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2] + matrix[2, 1] * (dataPtrRead + nChan * x + step * (y + 1))[2] + matrix[2, 2] * (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2]) / matrixWeight);                            
                            red = (byte)(aux > 255 ? 255 : aux < 0 ? 0 : aux);

                            (dataPtrWrite + nChan * x + step * y)[0] = blue;
                            (dataPtrWrite + nChan * x + step * y)[1] = green;
                            (dataPtrWrite + nChan * x + step * y)[2] = red;
                        }
                    }

                    for (y = 0; y < height; y = height - 1)
                    {
                        for (x = 1; x < width - 1; x++)
                        {
                            if (y == 0)
                            {
                                aux = (int)Math.Round(((matrix[1, 1] + matrix[0, 1]) * ((dataPtrRead + nChan * x + step * y)[0]) + (matrix[1, 0] + matrix[0, 0]) * ((dataPtrRead + nChan * (x - 1) + step * y)[0]) + (matrix[1, 2] + matrix[0, 2]) * ((dataPtrRead + nChan * (x + 1) + step * y)[0]) + matrix[2, 0] * (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0] + matrix[2, 1] * (dataPtrRead + nChan * x + step * (y + 1))[0] + matrix[2, 2] * (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0]) / matrixWeight);
                                blue = (byte)(aux > 255 ? 255 : aux < 0 ? 0 : aux);

                                aux = (int)Math.Round(((matrix[1, 1] + matrix[0, 1]) * ((dataPtrRead + nChan * x + step * y)[1]) + (matrix[1, 0] + matrix[0, 0]) * ((dataPtrRead + nChan * (x - 1) + step * y)[1]) + (matrix[1, 2] + matrix[0, 2]) * ((dataPtrRead + nChan * (x + 1) + step * y)[1]) + matrix[2, 0] * (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1] + matrix[2, 1] * (dataPtrRead + nChan * x + step * (y + 1))[1] + matrix[2, 2] * (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1]) / matrixWeight);
                                green = (byte)(aux > 255 ? 255 : aux < 0 ? 0 : aux);

                                aux = (int)Math.Round(((matrix[1, 1] + matrix[0, 1]) * ((dataPtrRead + nChan * x + step * y)[2]) + (matrix[1, 0] + matrix[0, 0]) * ((dataPtrRead + nChan * (x - 1) + step * y)[2]) + (matrix[1, 2] + matrix[0, 2]) * ((dataPtrRead + nChan * (x + 1) + step * y)[2]) + matrix[2, 0] * (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2] + matrix[2, 1] * (dataPtrRead + nChan * x + step * (y + 1))[2] + matrix[2, 2] * (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2]) / matrixWeight);
                                red = (byte)(aux > 255 ? 255 : aux < 0 ? 0 : aux);
                            }
                            else
                            {
                                aux = (int)Math.Round(((matrix[1, 1] + matrix[2, 1]) * ((dataPtrRead + nChan * x + step * y)[0]) + (matrix[1, 0] + matrix[2, 0]) * ((dataPtrRead + nChan * (x - 1) + step * y)[0]) + (matrix[1, 2] + matrix[2, 2]) * ((dataPtrRead + nChan * (x + 1) + step * y)[0]) + matrix[0, 0] * (dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + matrix[0, 1] * (dataPtrRead + nChan * x + step * (y - 1))[0] + matrix[0, 2] * (dataPtrRead + nChan * (x + 1) + step * (y - 1))[0]) / matrixWeight);
                                blue = (byte)(aux > 255 ? 255 : aux < 0 ? 0 : aux);

                                aux = (int)Math.Round(((matrix[1, 1] + matrix[2, 1]) * ((dataPtrRead + nChan * x + step * y)[1]) + (matrix[1, 0] + matrix[2, 0]) * ((dataPtrRead + nChan * (x - 1) + step * y)[1]) + (matrix[1, 2] + matrix[2, 2]) * ((dataPtrRead + nChan * (x + 1) + step * y)[1]) + matrix[0, 0] * (dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + matrix[0, 1] * (dataPtrRead + nChan * x + step * (y - 1))[1] + matrix[0, 2] * (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1]) / matrixWeight);
                                green = (byte)(aux > 255 ? 255 : aux < 0 ? 0 : aux);

                                aux = (int)Math.Round(((matrix[1, 1] + matrix[2, 1]) * ((dataPtrRead + nChan * x + step * y)[2]) + (matrix[1, 0] + matrix[2, 0]) * ((dataPtrRead + nChan * (x - 1) + step * y)[2]) + (matrix[1, 2] + matrix[2, 2]) * ((dataPtrRead + nChan * (x + 1) + step * y)[2]) + matrix[0, 0] * (dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + matrix[0, 1] * (dataPtrRead + nChan * x + step * (y - 1))[2] + matrix[0, 2] * (dataPtrRead + nChan * (x + 1) + step * (y - 1))[2]) / matrixWeight);
                                red = (byte)(red > 255 ? 255 : red < 0 ? 0 : red);
                            }


                            (dataPtrWrite + nChan * x + step * y)[0] = blue;
                            (dataPtrWrite + nChan * x + step * y)[1] = green;
                            (dataPtrWrite + nChan * x + step * y)[2] = red;
                        }
                    }

                    for (y = 1; y < height - 1; y++)
                    {
                        for (x = 0; x < width; x = width - 1)
                        {
                            if (x == 0)
                            {
                                blue = (byte)Math.Round(((matrix[1, 1] + matrix[1, 0]) * ((dataPtrRead + nChan * x + step * y)[0]) + (matrix[0, 1] + matrix[0, 0]) * ((dataPtrRead + nChan * x + step * (y - 1))[0]) + (matrix[2, 1] + matrix[2, 0]) * ((dataPtrRead + nChan * x + step * (y + 1))[0]) + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0] + (dataPtrRead + nChan * (x + 1) + step * y)[0]) / matrixWeight);
                                green = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[1]) + 2 * ((dataPtrRead + nChan * x + step * (y - 1))[1]) + 2 * ((dataPtrRead + nChan * x + step * (y + 1))[1]) + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1] + (dataPtrRead + nChan * (x + 1) + step * y)[1]) / matrixWeight);
                                red = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[2]) + 2 * ((dataPtrRead + nChan * x + step * (y - 1))[2]) + 2 * ((dataPtrRead + nChan * x + step * (y + 1))[2]) + (dataPtrRead + nChan * (x + 1) + step * (y - 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2] + (dataPtrRead + nChan * (x + 1) + step * y)[2]) / matrixWeight);
                            }
                            else
                            {
                                blue = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[0]) + 2 * ((dataPtrRead + nChan * x + step * (y - 1))[0]) + 2 * ((dataPtrRead + nChan * x + step * (y + 1))[0]) + (dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0] + (dataPtrRead + nChan * (x - 1) + step * y)[0]) / matrixWeight);
                                green = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[1]) + 2 * ((dataPtrRead + nChan * x + step * (y - 1))[1]) + 2 * ((dataPtrRead + nChan * x + step * (y + 1))[1]) + (dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1] + (dataPtrRead + nChan * (x - 1) + step * y)[1]) / matrixWeight);
                                red = (byte)Math.Round((2 * ((dataPtrRead + nChan * x + step * y)[2]) + 2 * ((dataPtrRead + nChan * x + step * (y - 1))[2]) + 2 * ((dataPtrRead + nChan * x + step * (y + 1))[2]) + (dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2] + (dataPtrRead + nChan * (x - 1) + step * y)[2]) / matrixWeight);
                            }

                            blue = (byte)(blue > 255 ? 255 : blue < 0 ? 0 : blue);
                            green = (byte)(green > 255 ? 255 : green < 0 ? 0 : green);
                            red = (byte)(red > 255 ? 255 : red < 0 ? 0 : red);


                            (dataPtrWrite + nChan * x + step * y)[0] = blue;
                            (dataPtrWrite + nChan * x + step * y)[1] = green;
                            (dataPtrWrite + nChan * x + step * y)[2] = red;
                        }
                    }



                    blue = (byte)Math.Round((4 * ((dataPtrRead)[0]) + 2 * ((dataPtrRead + nChan)[0]) + 2 * ((dataPtrRead + step * 1)[0]) + (dataPtrRead + nChan + step)[0]) / matrixWeight);
                    green = (byte)Math.Round((4 * ((dataPtrRead)[1]) + 2 * ((dataPtrRead + nChan)[1]) + 2 * ((dataPtrRead + step * 1)[1]) + (dataPtrRead + nChan + step)[1]) / matrixWeight);
                    red = (byte)Math.Round((4 * ((dataPtrRead)[2]) + 2 * ((dataPtrRead + nChan)[2]) + 2 * ((dataPtrRead + step * 1)[2]) + (dataPtrRead + nChan + step)[2]) / matrixWeight);

                    blue = (byte)(blue > 255 ? 255 : blue < 0 ? 0 : blue);
                    green = (byte)(green > 255 ? 255 : green < 0 ? 0 : green);
                    red = (byte)(red > 255 ? 255 : red < 0 ? 0 : red);

                    (dataPtrWrite)[0] = blue;
                    (dataPtrWrite)[1] = green;
                    (dataPtrWrite)[2] = red;


                    blue = (byte)Math.Round((4 * ((dataPtrRead + nChan * width)[0]) + 2 * ((dataPtrRead + nChan * (width - 1))[0]) + 2 * ((dataPtrRead + nChan * width + step)[0]) + (dataPtrRead + nChan * (width - 1) + step)[0]) / matrixWeight);
                    green = (byte)Math.Round((4 * ((dataPtrRead + nChan * width)[1]) + 2 * ((dataPtrRead + nChan * (width - 1))[1]) + 2 * ((dataPtrRead + nChan * width + step)[1]) + (dataPtrRead + nChan * (width - 1) + step)[1]) / matrixWeight);
                    red = (byte)Math.Round((4 * ((dataPtrRead + nChan * width)[2]) + 2 * ((dataPtrRead + nChan * (width - 1))[2]) + 2 * ((dataPtrRead + nChan * width + step)[2]) + (dataPtrRead + nChan * (width - 1) + step)[2]) / matrixWeight);

                    blue = (byte)(blue > 255 ? 255 : blue < 0 ? 0 : blue);
                    green = (byte)(green > 255 ? 255 : green < 0 ? 0 : green);
                    red = (byte)(red > 255 ? 255 : red < 0 ? 0 : red);

                    (dataPtrWrite + nChan * width)[0] = blue;
                    (dataPtrWrite + nChan * width)[1] = green;
                    (dataPtrWrite + nChan * width)[2] = red;


                    blue = (byte)Math.Round((4 * ((dataPtrRead + step * height)[0]) + 2 * ((dataPtrRead + step * (height - 1))[0]) + 2 * ((dataPtrRead + step * height + nChan)[0]) + (dataPtrRead + step * (height - 1) + nChan)[0]) / matrixWeight);
                    green = (byte)Math.Round((4 * ((dataPtrRead + step * height)[1]) + 2 * ((dataPtrRead + step * (height - 1))[1]) + 2 * ((dataPtrRead + step * height + nChan)[1]) + (dataPtrRead + step * (height - 1) + nChan)[1]) / matrixWeight);
                    red = (byte)Math.Round((4 * ((dataPtrRead + step * height)[2]) + 2 * ((dataPtrRead + step * (height - 1))[2]) + 2 * ((dataPtrRead + step * height + nChan)[2]) + (dataPtrRead + step * (height - 1) + nChan)[2]) / matrixWeight);

                    blue = (byte)(blue > 255 ? 255 : blue < 0 ? 0 : blue);
                    green = (byte)(green > 255 ? 255 : green < 0 ? 0 : green);
                    red = (byte)(red > 255 ? 255 : red < 0 ? 0 : red);

                    (dataPtrWrite + step * height)[0] = blue;
                    (dataPtrWrite + step * height)[1] = green;
                    (dataPtrWrite + step * height)[2] = red;

                    blue = (byte)Math.Round((4 * ((dataPtrRead + nChan * width + step * height)[0]) + 2 * ((dataPtrRead + nChan * width + step * (height - 1))[0]) + 2 * ((dataPtrRead + nChan * (width - 1) + step * height)[0]) + (dataPtrRead + nChan * (width - 1) + step * (height - 1))[0]) / matrixWeight);
                    green = (byte)Math.Round((4 * ((dataPtrRead + nChan * width + step * height)[1]) + 2 * ((dataPtrRead + nChan * width + step * (height - 1))[1]) + 2 * ((dataPtrRead + nChan * (width - 1) + step * height)[1]) + (dataPtrRead + nChan * (width - 1) + step * (height - 1))[1]) / matrixWeight);
                    red = (byte)Math.Round((4 * ((dataPtrRead + nChan * width + step * height)[2]) + 2 * ((dataPtrRead + nChan * width + step * (height - 1))[2]) + 2 * ((dataPtrRead + nChan * (width - 1) + step * height)[2]) + (dataPtrRead + nChan * (width - 1) + step * (height - 1))[2]) / matrixWeight);

                    blue = (byte)(blue > 255 ? 255 : blue < 0 ? 0 : blue);
                    green = (byte)(green > 255 ? 255 : green < 0 ? 0 : green);
                    red = (byte)(red > 255 ? 255 : red < 0 ? 0 : red);

                    (dataPtrWrite + nChan * width + step * height)[0] = blue;
                    (dataPtrWrite + nChan * width + step * height)[1] = green;
                    (dataPtrWrite + nChan * width + step * height)[2] = red;
                }
            }
        }
    
        public static void Sobel(Image<Bgr, byte> img, Image<Bgr, byte> imgCopy)
        {
            unsafe
            {
                MIplImage m = img.MIplImage;
                MIplImage mCopy = imgCopy.MIplImage;
                byte* dataPtrWrite = (byte*)m.imageData.ToPointer(); // Pointer to the image
                byte* dataPtrRead = (byte*)mCopy.imageData.ToPointer(); // Pointer to the image
                byte blue, green, red, gray;

                int width = img.Width;
                int height = img.Height;
                int nChan = m.nChannels; // number of channels - 3
                int padding = m.widthStep - m.nChannels * m.width; // alinhament bytes (padding)
                int x, y;
                int xo, yo;
                int step = m.widthStep;
                int sx, sy, s;

                if (nChan == 3) // image in RGB
                {
                    for (y = 1; y < height - 1; y++)
                    {

                        for (x = 1; x < width - 1; x++)
                        {
                            sx = Math.Abs((dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x - 1) + step * y)[0] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0] - (dataPtrRead + nChan * (x + 1) + step * (y - 1))[0] - 2 * (dataPtrRead + nChan * (x + 1) + step * y)[0] - (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0]);
                            sy = Math.Abs((-1) * (dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] - 2 * (dataPtrRead + nChan * x + step * (y - 1))[0] - (dataPtrRead + nChan * (x + 1) + step * (y - 1))[0] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0] + 2 * (dataPtrRead + nChan * x + step * (y + 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0]);
                            s = sx + sy;
                            blue = (byte)(s > 255 ? 255 : s);

                            sx = Math.Abs((dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x - 1) + step * y)[1] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1] - (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1] - 2 * (dataPtrRead + nChan * (x + 1) + step * y)[1] - (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1]);
                            sy = Math.Abs((-1) * (dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] - 2 * (dataPtrRead + nChan * x + step * (y - 1))[1] - (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1] + 2 * (dataPtrRead + nChan * x + step * (y + 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1]);
                            s = sx + sy;
                            green = (byte)(s > 255 ? 255 : s);

                            sx = Math.Abs((dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x - 1) + step * y)[2] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2] - (dataPtrRead + nChan * (x + 1) + step * (y - 1))[2] - 2 * (dataPtrRead + nChan * (x + 1) + step * y)[2] - (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2]);
                            sy = Math.Abs((-1) * (dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] - 2 * (dataPtrRead + nChan * x + step * (y - 1))[2] - (dataPtrRead + nChan * (x + 1) + step * (y - 1))[2] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2] + 2 * (dataPtrRead + nChan * x + step * (y + 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2]);
                            s = sx + sy;
                            red = (byte)(s > 255 ? 255 : s);

                            (dataPtrWrite + nChan * x + step * y)[0] = blue;
                            (dataPtrWrite + nChan * x + step * y)[1] = green;
                            (dataPtrWrite + nChan * x + step * y)[2] = red;
                        }
                    }

                    for (y = 0; y < height; y = height - 1)
                    {
                        for (x = 1; x < width - 1; x++)
                        {
                            if (y == 0)
                            {
                                sx = Math.Abs(3 * (dataPtrRead + nChan * (x - 1) + step * y)[0] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0] - 3 * (dataPtrRead + nChan * (x + 1) + step * y)[0] - (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0]);
                                sy = Math.Abs((-1) * (dataPtrRead + nChan * (x - 1) + step * y)[0] - 2 * (dataPtrRead + nChan * x + step * y)[0] - (dataPtrRead + nChan * (x + 1) + step * y)[0] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0] + 2 * (dataPtrRead + nChan * x + step * (y + 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0]);
                                s = sx + sy;
                                blue = (byte)(s > 255 ? 255 : s);

                                sx = Math.Abs(3 * (dataPtrRead + nChan * (x - 1) + step * y)[1] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1] - 3 * (dataPtrRead + nChan * (x + 1) + step * y)[1] - (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1]);
                                sy = Math.Abs((-1) * (dataPtrRead + nChan * (x - 1) + step * y)[1] - 2 * (dataPtrRead + nChan * x + step * y)[1] - (dataPtrRead + nChan * (x + 1) + step * y)[1] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1] + 2 * (dataPtrRead + nChan * x + step * (y + 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1]);
                                s = sx + sy;
                                green = (byte)(s > 255 ? 255 : s);

                                sx = Math.Abs(3 * (dataPtrRead + nChan * (x - 1) + step * y)[2] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2] - 3 * (dataPtrRead + nChan * (x + 1) + step * y)[2] - (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2]);
                                sy = Math.Abs((-1) * (dataPtrRead + nChan * (x - 1) + step * y)[2] - 2 * (dataPtrRead + nChan * x + step * y)[2] - (dataPtrRead + nChan * (x + 1) + step * y)[2] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2] + 2 * (dataPtrRead + nChan * x + step * (y + 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2]);
                                s = sx + sy;
                                red = (byte)(s > 255 ? 255 : s);
                            }
                            else
                            {
                                sx = Math.Abs(3 * (dataPtrRead + nChan * (x - 1) + step * y)[0] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0] - 3 * (dataPtrRead + nChan * (x + 1) + step * y)[0] - (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0]);
                                sy = Math.Abs((-1) * (dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] - 2 * (dataPtrRead + nChan * x + step * (y - 1))[0] - (dataPtrRead + nChan * (x + 1) + step * (y - 1))[0] + (dataPtrRead + nChan * (x - 1) + step * y)[0] + 2 * (dataPtrRead + nChan * x + step * y)[0] + (dataPtrRead + nChan * (x + 1) + step * y)[0]);
                                s = sx + sy;
                                blue = (byte)(s > 255 ? 255 : s);

                                sx = Math.Abs(3 * (dataPtrRead + nChan * (x - 1) + step * y)[1] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1] - 3 * (dataPtrRead + nChan * (x + 1) + step * y)[1] - (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1]);
                                sy = Math.Abs((-1) * (dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] - 2 * (dataPtrRead + nChan * x + step * (y - 1))[1] - (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1] + (dataPtrRead + nChan * (x - 1) + step * y)[1] + 2 * (dataPtrRead + nChan * x + step * y)[1] + (dataPtrRead + nChan * (x + 1) + step * y)[1]);
                                s = sx + sy;
                                green = (byte)(s > 255 ? 255 : s);

                                sx = Math.Abs(3 * (dataPtrRead + nChan * (x - 1) + step * y)[2] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2] - 3 * (dataPtrRead + nChan * (x + 1) + step * y)[2] - (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2]);
                                sy = Math.Abs((-1) * (dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] - 2 * (dataPtrRead + nChan * x + step * (y - 1))[2] - (dataPtrRead + nChan * (x + 1) + step * (y - 1))[2] + (dataPtrRead + nChan * (x - 1) + step * y)[2] + 2 * (dataPtrRead + nChan * x + step * y)[2] + (dataPtrRead + nChan * (x + 1) + step * y)[2]);
                                s = sx + sy;
                                red = (byte)(s > 255 ? 255 : s);
                            }


                            (dataPtrWrite + nChan * x + step * y)[0] = blue;
                            (dataPtrWrite + nChan * x + step * y)[1] = green;
                            (dataPtrWrite + nChan * x + step * y)[2] = red;
                        }
                    }

                    for (y = 1; y < height - 1; y++)
                    {
                        for (x = 0; x < width; x = width - 1)
                        {
                            if (x == 0)
                            {
                                sx = Math.Abs((dataPtrRead + nChan * x + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * x + step * y)[0] + (dataPtrRead + nChan * x + step * (y + 1))[0] - (dataPtrRead + nChan * (x + 1) + step * (y - 1))[0] - 2 * (dataPtrRead + nChan * (x + 1) + step * y)[0] - (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0]);
                                sy = Math.Abs((- 3) * (dataPtrRead + nChan * x + step * (y - 1))[0] - (dataPtrRead + nChan * (x + 1) + step * (y - 1))[0] + 3 * (dataPtrRead + nChan * x + step * (y + 1))[0] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[0]);
                                s = sx + sy;
                                blue = (byte)(s > 255 ? 255 : s);

                                sx = Math.Abs((dataPtrRead + nChan * x + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * x + step * y)[1] + (dataPtrRead + nChan * x + step * (y + 1))[1] - (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1] - 2 * (dataPtrRead + nChan * (x + 1) + step * y)[1] - (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1]);
                                sy = Math.Abs((-3) * (dataPtrRead + nChan * x + step * (y - 1))[1] - (dataPtrRead + nChan * (x + 1) + step * (y - 1))[1] + 3 * (dataPtrRead + nChan * x + step * (y + 1))[1] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[1]);
                                s = sx + sy;
                                green = (byte)(s > 255 ? 255 : s);

                                sx = Math.Abs((dataPtrRead + nChan * x + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * x + step * y)[2] + (dataPtrRead + nChan * x + step * (y + 1))[2] - (dataPtrRead + nChan * (x + 1) + step * (y - 1))[2] - 2 * (dataPtrRead + nChan * (x + 1) + step * y)[2] - (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2]);
                                sy = Math.Abs((-3) * (dataPtrRead + nChan * x + step * (y - 1))[2] - (dataPtrRead + nChan * (x + 1) + step * (y - 1))[2] + 3 * (dataPtrRead + nChan * x + step * (y + 1))[2] + (dataPtrRead + nChan * (x + 1) + step * (y + 1))[2]);
                                s = sx + sy;
                                red = (byte)(s > 255 ? 255 : s);
                            }
                            else
                            {
                                sx = Math.Abs((dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + 2 * (dataPtrRead + nChan * (x - 1) + step * y)[0] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0] - (dataPtrRead + nChan * x + step * (y - 1))[0] - 2 * (dataPtrRead + nChan * x + step * y)[0] - (dataPtrRead + nChan * x + step * (y + 1))[0]);
                                sy = Math.Abs((-3) * (dataPtrRead + nChan * x + step * (y - 1))[0] - (dataPtrRead + nChan * (x - 1) + step * (y - 1))[0] + 3 * (dataPtrRead + nChan * x + step * (y + 1))[0] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[0]);
                                s = sx + sy;
                                blue = (byte)(s > 255 ? 255 : s);

                                sx = Math.Abs((dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + 2 * (dataPtrRead + nChan * (x - 1) + step * y)[1] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1] - (dataPtrRead + nChan * x + step * (y - 1))[1] - 2 * (dataPtrRead + nChan * x + step * y)[1] - (dataPtrRead + nChan * x + step * (y + 1))[1]);
                                sy = Math.Abs((-3) * (dataPtrRead + nChan * x + step * (y - 1))[1] - (dataPtrRead + nChan * (x - 1) + step * (y - 1))[1] + 3 * (dataPtrRead + nChan * x + step * (y + 1))[1] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[1]);
                                s = sx + sy;
                                green = (byte)(s > 255 ? 255 : s);

                                sx = Math.Abs((dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + 2 * (dataPtrRead + nChan * (x - 1) + step * y)[2] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2] - (dataPtrRead + nChan * x + step * (y - 1))[2] - 2 * (dataPtrRead + nChan * x + step * y)[2] - (dataPtrRead + nChan * x + step * (y + 1))[2]);
                                sy = Math.Abs((-3) * (dataPtrRead + nChan * x + step * (y - 1))[2] - (dataPtrRead + nChan * (x - 1) + step * (y - 1))[2] + 3 * (dataPtrRead + nChan * x + step * (y + 1))[2] + (dataPtrRead + nChan * (x - 1) + step * (y + 1))[2]);
                                s = sx + sy;
                                red = (byte)(s > 255 ? 255 : s);
                            }


                            (dataPtrWrite + nChan * x + step * y)[0] = blue;
                            (dataPtrWrite + nChan * x + step * y)[1] = green;
                            (dataPtrWrite + nChan * x + step * y)[2] = red;
                        }
                    }



                    (dataPtrWrite)[0] = (byte)Math.Round((4 * ((dataPtrRead)[0]) + 2 * ((dataPtrRead + nChan)[0]) + 2 * ((dataPtrRead + step * 1)[0]) + (dataPtrRead + nChan + step)[0]) / 9.0);
                    (dataPtrWrite)[1] = (byte)Math.Round((4 * ((dataPtrRead)[1]) + 2 * ((dataPtrRead + nChan)[1]) + 2 * ((dataPtrRead + step * 1)[1]) + (dataPtrRead + nChan + step)[1]) / 9.0);
                    (dataPtrWrite)[2] = (byte)Math.Round((4 * ((dataPtrRead)[2]) + 2 * ((dataPtrRead + nChan)[2]) + 2 * ((dataPtrRead + step * 1)[2]) + (dataPtrRead + nChan + step)[2]) / 9.0);

                    (dataPtrWrite + nChan * width)[0] = (byte)Math.Round((4 * ((dataPtrRead + nChan * width)[0]) + 2 * ((dataPtrRead + nChan * (width - 1))[0]) + 2 * ((dataPtrRead + nChan * width + step)[0]) + (dataPtrRead + nChan * (width - 1) + step)[0]) / 9.0);
                    (dataPtrWrite + nChan * width)[1] = (byte)Math.Round((4 * ((dataPtrRead + nChan * width)[1]) + 2 * ((dataPtrRead + nChan * (width - 1))[1]) + 2 * ((dataPtrRead + nChan * width + step)[1]) + (dataPtrRead + nChan * (width - 1) + step)[1]) / 9.0);
                    (dataPtrWrite + nChan * width)[2] = (byte)Math.Round((4 * ((dataPtrRead + nChan * width)[2]) + 2 * ((dataPtrRead + nChan * (width - 1))[2]) + 2 * ((dataPtrRead + nChan * width + step)[2]) + (dataPtrRead + nChan * (width - 1) + step)[2]) / 9.0);

                    (dataPtrWrite + step * height)[0] = (byte)Math.Round((4 * ((dataPtrRead + step * height)[0]) + 2 * ((dataPtrRead + step * (height - 1))[0]) + 2 * ((dataPtrRead + step * height + nChan)[0]) + (dataPtrRead + step * (height - 1) + nChan)[0]) / 9.0);
                    (dataPtrWrite + step * height)[1] = (byte)Math.Round((4 * ((dataPtrRead + step * height)[1]) + 2 * ((dataPtrRead + step * (height - 1))[1]) + 2 * ((dataPtrRead + step * height + nChan)[1]) + (dataPtrRead + step * (height - 1) + nChan)[1]) / 9.0);
                    (dataPtrWrite + step * height)[2] = (byte)Math.Round((4 * ((dataPtrRead + step * height)[2]) + 2 * ((dataPtrRead + step * (height - 1))[2]) + 2 * ((dataPtrRead + step * height + nChan)[2]) + (dataPtrRead + step * (height - 1) + nChan)[2]) / 9.0);

                    (dataPtrWrite + nChan * width + step * height)[0] = (byte)Math.Round((4 * ((dataPtrRead + nChan * width + step * height)[0]) + 2 * ((dataPtrRead + nChan * width + step * (height - 1))[0]) + 2 * ((dataPtrRead + nChan * (width - 1) + step * height)[0]) + (dataPtrRead + nChan * (width - 1) + step * (height - 1))[0]) / 9.0);
                    (dataPtrWrite + nChan * width + step * height)[1] = (byte)Math.Round((4 * ((dataPtrRead + nChan * width + step * height)[1]) + 2 * ((dataPtrRead + nChan * width + step * (height - 1))[1]) + 2 * ((dataPtrRead + nChan * (width - 1) + step * height)[1]) + (dataPtrRead + nChan * (width - 1) + step * (height - 1))[1]) / 9.0);
                    (dataPtrWrite + nChan * width + step * height)[2] = (byte)Math.Round((4 * ((dataPtrRead + nChan * width + step * height)[2]) + 2 * ((dataPtrRead + nChan * width + step * (height - 1))[2]) + 2 * ((dataPtrRead + nChan * (width - 1) + step * height)[2]) + (dataPtrRead + nChan * (width - 1) + step * (height - 1))[2]) / 9.0);
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

        public static int[] Histogram_Gray(Emgu.CV.Image<Bgr, byte> img) {
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
                int[] h = new int[256]

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

                            h[gray]++;
                        }
                    }
                    return h;
                }
            }
        }

        public static void ConvertToBW_Otsu(Emgu.CV.Image<Bgr, byte> img) {
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
                int[] h = Histogram_Gray(img);
                int i, t, j;
                int threshold = 0;
                double sigma = 0, _sigma = 0;
                double q1 = 0, q2 = 0, u1 = 0, u2 = 0;
                double total = width * height;

                for (t = 0; y < 256; t++)
                {
                    q1 = 0;
                    a2 = 0;
                    u1 = 0;
                    u2 = 0;

                    for(i = 0; i <= t; i++) {
                        q1 += (h[i] / total);
                        u1 += (i * h[i] / total);
                    }
                    
                    u1 /= q1;

                    for(j = 0; j < 256; j++) {
                        q2 += (h[i] / total);
                        u2 += (i * h[i] / total);
                    }

                    u2 /= q2;

                    _sigma = q1 * q2 * ((u1 - u2) * (u1 - u2));

                    if (sigma < _sigma) {
                        sigma = _sigma;
                        threshold = t;
                    }
                }
                ConvertToBW(img, threshold);
            }
        }

        public static int[,] Histogram_RGB(Emgu.CV.Image<Bgr, byte> img) {
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
                int[,] h = new int[3,256]

                if (nChan == 3)
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
                    return h;
                }
            }
        }

        public static int[,] Histogram_All(Emgu.CV.Image<Bgr, byte> img) {
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
                int[,] h = new int[4,256]

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

                            h[0, blue]++;
                            h[1, green]++;
                            h[2, red]++;
                            h[3, gray]++;
                        }
                    }
                    return h;
                }
            }
        }
    }
}
