using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassToProtoConvert
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        Dictionary<string, string> convertTypes = new Dictionary<string, string>()
        {
             { "double?", "double" },
            { "double", "double" },
            { "decimal?", "double /* decimal? */" },
            { "decimal", "double /* decimal */" },
            { "float?", "float" },
            { "float", "float" },
            { "int?", "int32" },
            { "int", "int32" },
            { "long?", "int64" },
            { "long", "int64" },
            { "uint", "uint32" },
            { "ulong", "uint64" },
             { "bool?", "bool" },
            { "bool", "bool" },
             { "System.String?", "string" },
            { "System.String", "string" },
            { "string?", "string" },
            { "string", "string" },
             { "System.DateTimeOffset?", "google.protobuf.Timestamp" },
            { "System.DateTimeOffset", "google.protobuf.Timestamp" },
             { "DateTimeOffset?", "google.protobuf.Timestamp" },
            { "DateTimeOffset", "google.protobuf.Timestamp" },
            { "System.DateTime?", "google.protobuf.Timestamp" },
            { "System.DateTime", "google.protobuf.Timestamp" },
            { "DateTime?", "google.protobuf.Timestamp" },
            { "DateTime", "google.protobuf.Timestamp" },
             { "System.TimeSpan?", "google.protobuf.Duration" },
            { "System.TimeSpan", "google.protobuf.Duration" },
             { "TimeSpan?", "google.protobuf.Duration" },
            { "TimeSpan", "google.protobuf.Duration" },
            { "class", "message" },
            { "public", "" },
            { "protected", ""},
            { "private", ""},
            { "virtual", "" },
            { "internal", "" }
        };

        string orginal = string.Empty;
        string output = string.Empty;
        private void btnConvert_Click(object sender, EventArgs e)
        {
            orginal = txtContent.Text;
            bool error = false;

            output = "\r\n" + txtContent.Text + "\r\n";


            foreach (var item in convertTypes)
            {
                output = Utils.ReplaceString(output, item.Key, item.Value, false);
            }

            try
            {
                Regex regex = new Regex(".*({.*get;.*set;.*})", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                Match result = regex.Match(this.output);
                int order = 1;

                while (result.Success)
                {
                    var current = result.Groups[0].Value.Replace(result.Groups[1].Value, " = " + order + ";");
                    output = Utils.ReplaceString(this.output, result.Groups[0].Value, current, false);
                    result = result.NextMatch();
                    ++order;
                }
            }
            catch (ArgumentException ex)
            {
                error = true;
                // Syntax error in the regular expression
            }

            txtContent.Text = output;
            txtContent.Focus();
            txtContent.SelectAll();
            if (error)
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "Error occur while convert !";
            }
            else
            {
                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = "Convert success !";
            }
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            lblStatus.Text = string.Empty;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblStatus.Text = string.Empty;
        }

        private void btnOrignal_Click(object sender, EventArgs e)
        {
            txtContent.Text = orginal;
        }
    }
}
