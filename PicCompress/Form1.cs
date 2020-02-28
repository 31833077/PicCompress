using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Encoder = System.Drawing.Imaging.Encoder;

namespace PicCompress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //folderBrowserDialog1.ShowDialog();
            //var path= folderBrowserDialog1.SelectedPath;
            //folderBrowserDialog1.SelectedPath = "D:\\用户文件\\Desktop\\文档\\物流枢纽\\9.审计报告\\新建文件夹";
            // var files = Directory.GetFiles("D:\\用户文件\\Desktop\\文档\\物流枢纽\\9.审计报告\\新建文件夹").ToList();
            
            var path = (folderBrowserDialog1.SelectedPath == "" ? openFileDialog1.FileName : folderBrowserDialog1.SelectedPath);
            if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                Compress(new Bitmap(path), Application.StartupPath + "\\" + path.Substring(path.LastIndexOf("\\") + 1), (long)numericUpDown1.Value);
            }
            else
            {
                var files = Directory.GetFiles(path).ToList();

                foreach (var item in files)
                {
                    Compress(new Bitmap(item), Application.StartupPath + "\\" + item.Substring(item.LastIndexOf("\\") + 1), (long)numericUpDown1.Value);
                    this.Text = files.IndexOf(item) + "";
                }
            }
        }

        #region 图片压缩

        /// <summary>

        /// 图片压缩(降低质量以减小文件的大小)

        /// </summary>

        /// <param name="srcBitMap">传入的Bitmap对象</param>

        /// <param name="destFile">压缩后的图片保存路径</param>

        /// <param name="level">压缩等级，0到100，0 最差质量，100 最佳</param>

        public static void Compress(Bitmap srcBitMap, string destFile, long level)

        {

            Stream s = new FileStream(destFile, FileMode.Create);

            Compress(srcBitMap, s, level);

            s.Close();

        }

        /// <summary>

        /// 编码信息

        /// </summary>

        /// <param name="mimeType"></param>

        /// <returns></returns>

        private static ImageCodecInfo GetEncoderInfo(String mimeType)

        {

            int j;

            ImageCodecInfo[] encoders;

            encoders = ImageCodecInfo.GetImageEncoders();

            for (j = 0; j < encoders.Length; ++j)

            {

                if (encoders[j].MimeType == mimeType)

                    return encoders[j];

            }

            return null;

        }

        /// <summary>

        /// 图片压缩(降低质量以减小文件的大小)

        /// </summary>

        /// <param name="srcBitmap">传入的Bitmap对象</param>

        /// <param name="destStream">压缩后的Stream对象</param>

        /// <param name="level">压缩等级，0到100，0 最差质量，100 最佳</param>

        private static void Compress(Bitmap srcBitmap, Stream destStream, long level)

        {

            ImageCodecInfo myImageCodecInfo;

            Encoder myEncoder;

            EncoderParameter myEncoderParameter;

            EncoderParameters myEncoderParameters;

            // Get an ImageCodecInfo object that represents the JPEG codec.

            myImageCodecInfo = GetEncoderInfo("image/jpeg");

            // Create an Encoder object based on the GUID

            // for the Quality parameter category.

            myEncoder = Encoder.Quality;

            // Create an EncoderParameters object.

            // An EncoderParameters object has an array of EncoderParameter

            // objects. In this case, there is only one

            // EncoderParameter object in the array.

            myEncoderParameters = new EncoderParameters(1);

            // Save the bitmap as a JPEG file with 给定的 quality level

            myEncoderParameter = new EncoderParameter(myEncoder, level);

            myEncoderParameters.Param[0] = myEncoderParameter;

            srcBitmap.Save(destStream, myImageCodecInfo, myEncoderParameters);

        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
                var client = new RestClient("https://localhost:44395/token");
                var request = new RestRequest(Method.POST);
                string encodedBody = "grant_type=password&username=admin&password=123";
                   
                request.AddParameter("application/x-www-form-urlencoded", encodedBody, ParameterType.RequestBody);
                request.AddParameter("Content-Type", "application/x-www-form-urlencoded", ParameterType.HttpHeader);
                var response = client.Execute(request);
                MessageBox.Show( response.Content);
           
        }
    }
}
