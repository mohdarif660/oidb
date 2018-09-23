using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace OIDBMVCWEBSITE.CustomCode
{
    /// <summary>
    /// Summary description for CaptchaImage
    /// </summary>
    public class CaptchaImage : IHttpHandler, IRequiresSessionState
    {
        MemoryStream myMemoryStream = new MemoryStream();
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string salt = CreateImage();

                context.Session.Add("captcha", salt.ToLower());
                context.Response.ContentType = "image/jpeg";
                context.Response.Cache.SetCacheability(HttpCacheability.Public);
                context.Response.BufferOutput = false;

                context.Response.OutputStream.Write(myMemoryStream.GetBuffer(), 0, myMemoryStream.GetBuffer().Length);
            }
            catch (Exception ex)
            {

            }

        }

        public bool IsReusable { get { return true; } }
        public string CreateRandomCode(int codeCount)
        {
            string randomCode = "";
            try
            {
                //!,@,&,*,#,$,
                string allChar1 = "1,2,3,4,5,6,7,8,9,!,@,&,*,#,$,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z";
                string[] allCharArray1 = allChar1.Split(',');
                string allChar2 = "1,2,3,4,5,6,7,8,9,!,@,&,*,#,$,A,B,C,D,E,F,G,H,I,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z";
                //string allChar2 = "!,@,&,*,#,$";
                string[] allCharArray2 = allChar2.Split(',');
                int temp = -1;
                Random rand = new Random();
                for (int i = 0; i < codeCount; i++)
                {
                    if ((i < 2) || (i >= 4 && i < 6))
                    {
                        if (temp != -1) { rand = new Random(i * temp * ((int)DateTime.Now.Ticks)); }
                        int t = rand.Next(40);
                        if (temp != -1 && temp == t) { return CreateRandomCode(codeCount); }
                        temp = t;
                        randomCode += allCharArray1[t];
                    }
                    if (i >= 2 && i < 4)
                    {
                        if (temp != -1) { rand = new Random(i * temp * ((int)DateTime.Now.Ticks)); }
                        int t = rand.Next(6);
                        if (temp != -1 && temp == t) { return CreateRandomCode(codeCount); }
                        temp = t;
                        randomCode += allCharArray2[t];
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return randomCode;

        }
        private string CreateImage()
        {
            string checkCode = CreateRandomCode(6);

            System.Drawing.Bitmap image = new System.Drawing.Bitmap(Convert.ToInt32(Math.Ceiling((decimal)(checkCode.Length * 22))), 38);
            Graphics g = Graphics.FromImage(image);
            try
            {
                Random random = new Random();
                g.Clear(Color.AliceBlue);
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new System.Drawing.Font("Comic Sans MS", 20, System.Drawing.FontStyle.Bold);
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                image.Save(myMemoryStream, System.Drawing.Imaging.ImageFormat.Gif);
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
            return checkCode;
        }

    }
}