using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace SplitFiles
{
    public partial class Form1 : Form
    {
        int numberOfFile = 10;
        String fileFullName;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fileFullName = txtfileSelected.Text;
            numberOfFile = int.Parse(txtNumberOfFiles.Text);
            long fileLength = new FileInfo(fileFullName).Length / numberOfFile;
            String filePath = Path.GetDirectoryName(fileFullName);

            SplitFile(fileFullName, Convert.ToInt32(fileLength) + 1, filePath);
            
            label3.Text = "Completed!";
        }

        public static void SplitFile(string inputFile, int chunkSize, string path)
        {
            byte[] buffer = new byte[chunkSize];
            string fileBaseName = inputFile.Substring(path.Length+1);
            using (Stream input = File.OpenRead(inputFile))
            {
                int index = 0;
                while (input.Position < input.Length)
                {
                    using (Stream output = File.Create(path + "\\" + fileBaseName + "_" + index))
                    {
                        int chunkBytesRead = 0;
                        while (chunkBytesRead < chunkSize)
                        {
                            int bytesRead = input.Read(buffer,
                                                       chunkBytesRead,
                                                       chunkSize - chunkBytesRead);

                            if (bytesRead == 0)
                            {
                                break;
                            }
                            chunkBytesRead += bytesRead;
                        }
                        output.Write(buffer, 0, chunkBytesRead);
                    }
                    index++;
                }
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            txtfileSelected.Text = openFileDialog1.FileName;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtNumberOfFiles.Text = "10";
        }
    }
}
