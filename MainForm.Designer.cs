using System;
using System.Windows.Forms;

namespace Modscan
{

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    private ParamListControl wb =new ParamListControl();
    private TextBox mod_start =new TextBox();
    private TextBox mod_n =new TextBox();
    private TextBox mod_id =new TextBox();
    private TextBox mod_poll =new TextBox();
    private ComboBox cpfun =new ComboBox();
    private CheckBox bina =new CheckBox();
    private SplitContainer sc;
    private Bingrid plc =new Bingrid();
    private Timer tm =new Timer();

    //private ObservableCollection<string> almdb;
    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {  
        this.ClientSize = new System.Drawing.Size(900, 450);
        MenuStrip mme =new MenuStrip();
        ToolStripMenuItem mi =new ToolStripMenuItem("Comunicazione");
        mme.Items.Add(mi);
        mi.DropDownItems.Add("Apri Comunicazione Seriale",null,onser);
        mi.DropDownItems.Add("Apri Comunicazione Ethernet",null,oneth);
        mi.DropDownItems.Add("Stop",null,onstp);
        wb.Dock = DockStyle.Fill;
        wb.StartY = mme.Height+5;
        wb.AddParam(new ParamListItemControl("start",mod_start,25));
        wb.AddParam(new ParamListItemControl("n registri",mod_n,25));
        wb.AddParam(new ParamListItemControl("indirizzo",mod_id,25));
        wb.AddParam(new ParamListItemControl("scansione",mod_poll,25));        
        wb.AddParam(new ParamListItemControl("Funzione",cpfun,25));
        wb.AddParam(new ParamListItemControl("MostraBit",bina,25));
        cpfun.Items.Add("Coil 0x01");
        cpfun.Items.Add("Read Discrete Inputs 0x02");
        cpfun.Items.Add("Holding Register 0x03");
        cpfun.Items.Add("Read Input Registers 0x04");
        cpfun.SelectedIndex =0;        
        plc.BackgroundImageLayout = ImageLayout.Center;
        plc.Dock = DockStyle.Fill;
        sc =new SplitContainer();
        sc.Dock = DockStyle.Fill;
        sc.Panel1.Margin =new Padding(5);
        sc.Panel2.Margin =new Padding(5);
        sc.Width = 900;
        sc.Panel1MinSize = 230;
        sc.Panel2MinSize = 600;
        sc.Panel1.Controls.Add(wb);                         
        sc.Panel2.Controls.Add(plc);        
        tm.Interval =2000;
        tm.Tick += new EventHandler(updd);          
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Text = "ModScan";
        wb.Controls.Add(mme);
        this.Controls.Add(sc);
    }

    #endregion
}
}