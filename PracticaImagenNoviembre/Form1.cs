using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PracticaImagenNoviembre
{
    public partial class Form1 : Form
    {

        Image<Bgr, byte> imgInput;

        public Form1()
        {
            InitializeComponent();
        }

        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imgInput = new Image<Bgr, byte>(dialog.FileName);
                    pictureBox1.Image = imgInput.Bitmap;
                }
                else
                {
                    throw new Exception("Seleccione una imagen");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void faceDetectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (imgInput == null)
                {
                    throw new Exception("Selecciona una imagen");
                }
                DetectFaceHaar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DetectFaceHaar()
        {
            try
            {
                string facePath = Path.GetFullPath(@"../../data/haarcascade_frontalface_default.xml");

                CascadeClassifier classfierFace = new CascadeClassifier(facePath);

                var imgGray = imgInput.Convert<Gray, byte>().Clone();
                Rectangle[] faces = classfierFace.DetectMultiScale(imgGray, 1.1, 4);

                foreach (var face in faces)
                {
                    imgInput.Draw(face, new Bgr(0, 0, 255), 2);
                }
                pictureBox1.Image = imgInput.Bitmap;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}


