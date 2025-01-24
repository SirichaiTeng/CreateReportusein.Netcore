using CreateFileRDLC_DataSet.Model;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CreateFileRDLC_DataSet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnGenPdf_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string lasrname = txtName.Text;
            var result = new Example
            {
                Name = name,
                LastName = lasrname
            };

            LocalReport report = new LocalReport();
            string rdlcPath = Path.Combine(
            Directory.GetCurrentDirectory(), //C:\XXX\XXX\XXX\XXX\CreateFileRDLC&DataSet\bin\Debug\\
            "Reports",
            "Report1.rdlc");
            if (!File.Exists(rdlcPath))
            {
                throw new FileNotFoundException("ไม่พบไฟล์ RDLC", rdlcPath);
            }
            report.ReportPath = rdlcPath;
            report.DataSources.Add(new ReportDataSource("DataSet1", new List<Example> { result }));
            var deviceInfo = @"<DeviceInfo>
                    <EmbedFonts>None</EmbedFonts>
                   </DeviceInfo>";
            //byte[] result2 = report.Render("PDF", deviceInfo);
            string mimeType, encoding, fileNameExtension;
            string[] streams;
            Warning[] warnings;
            byte[] pdfContent = report.Render(
                "PDF", // รูปแบบที่ต้องการ
                deviceInfo, // DeviceInfo
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings
            );

            // บันทึกเป็นไฟล์ PDF
            string outputPath = Path.Combine(Application.StartupPath, "output.pdf");
            File.WriteAllBytes(outputPath, pdfContent);
            MessageBox.Show($"ไฟล์ PDF ถูกบันทึกไว้ที่: {outputPath}", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
