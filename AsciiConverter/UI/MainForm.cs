using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsciiConverter
{
    public partial class MainForm : Form
    {
        private AsciiConverter converter;
        public MainForm()
        {
            InitializeComponent();
        }

        private async void openImageButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                saveTextButton.Enabled = false;
                string fileName = openFileDialog.FileName;
                converter = new AsciiConverter((Bitmap) Image.FromFile(fileName));
                await converter.ConvertToAsciiAsync();
                MessageBox.Show($"Изображение было сконвертировано.\nНажмите \"Сохранить\"");
                saveTextButton.Enabled = true;
            }
        }

        private void saveTextButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                openImageButton.Enabled = false;
                using (FileStream outputFile = File.Open(Path.ChangeExtension(saveFileDialog.FileName, ".txt"),
                                                         FileMode.Create, FileAccess.Write, FileShare.Write))
                {
                    using (StreamWriter writer = new StreamWriter(outputFile))
                    {
                        writer.Write(converter.AsciiText);
                    }
                }
                MessageBox.Show($"Текст был уcпешно сохранён в\n{saveFileDialog.FileName}");
                Process.Start(saveFileDialog.FileName); //открываем файл с результатом
                openImageButton.Enabled = true;
            }
        }
    }
}
