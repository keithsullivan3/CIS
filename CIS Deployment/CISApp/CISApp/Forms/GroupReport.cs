using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CISApp.DLL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace CISApp
{
    public partial class GroupReport : Form
    {
        string groupId;
        //this is to launch the report
        ReportDocument repDoc = new ReportDocument();

        public GroupReport(string group)
        {
            InitializeComponent();
            groupId = group;
        }

        private void GroupReport_Load(object sender, EventArgs e)
        {
            repDoc = new ReportDocument();
            ParameterFields rptParams;
            ParameterValues paramValues;
            ParameterDiscreteValue paramDiscreteValue;
            ParameterField paramField;

            try
            {
                repDoc.Load("../../Reports/Group.rpt");
                //repDoc.Load("C:\\Users\\keith.sullivan\\Documents\\CIS\\CISApp\\CISApp\\Reports\\Group.rpt");
                rptParams = repDoc.ParameterFields;
                paramValues = new ParameterValues();
                paramDiscreteValue = new ParameterDiscreteValue();
                paramDiscreteValue.Value = groupId;
                paramValues.Add(paramDiscreteValue);
                paramField = rptParams["studentgroup"];
                paramField.CurrentValues = paramValues;                
                crystalReportViewer1.ReportSource = repDoc;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Could not connect to report");
            }
        }

        //this code is if we need a export to pdf button...put it in a button click event
        /*try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = "c:\\CISgroup" + cbGroup.SelectedItem.ToString() + ".pdf";
                CrExportOptions = repDoc.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                repDoc.Export();
                MessageBox.Show("The group has been exported to a pdf file");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }*/
    }
}
