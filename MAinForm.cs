using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Modscan
{


public partial class MainForm : Form
{

    
    private void onstp(object sender, EventArgs e)
    {
        tm.Stop();
    }


    private void oneth(object sender, EventArgs e)
    {

        ParamListDialog ethConfig =new ParamListDialog("Apri Comunicazione","Ethernet");

        //TextBox ip =new TextBox();
        TextBox port =new TextBox();
        TreeViewIP trip = new TreeViewIP();
   
        //ip.Text ="192.168.1.93";
        port.Text = "502";

        //ethConfig.pr.AddParam(new ParamListItemControl("indirizzo",ip,25));
        ethConfig.pr.AddParam(new ParamListItemControl("porta",port,25));
        ethConfig.pr.AddParam(new ParamListItemControl("indirizzo ip",trip,400));
        trip.searchIP();
            

        if(ethConfig.ShowDialog() == DialogResult.OK){
            plc.Eth=true;
            plc.ip = trip.SelectedNode.Tag.ToString();
            plc.port = Int32.Parse(port.Text);
            tm.Start();
        }
    }
    private void onser(object sender, EventArgs e)
    {
        string[] ports = SerialPort.GetPortNames();
        if(ports.Length==0){
            MessageBox.Show("Nessuna Porta Seriale","Modscanner",MessageBoxButtons.OK);
            return;
        }
        ParamListDialog ComConfig =new ParamListDialog("Apri Comunicazione","Seriale");
        
        ComboBox cbComName =new ComboBox();
        ComboBox cbComVel =new ComboBox();
        ComboBox cbComPar =new ComboBox();

        cbComName.Items.AddRange(ports);
        cbComName.SelectedIndex = 0;
        cbComVel.Items.Add(4800);       
        cbComVel.Items.Add(9600);
        cbComVel.Items.Add(19200);
        cbComVel.Items.Add(115200);
        cbComPar.Items.Add("None");
        cbComPar.Items.Add("Odd");
        cbComPar.Items.Add("Even");
        cbComPar.Items.Add("Mark");                        
        cbComPar.Items.Add("Space");
        cbComPar.SelectedIndex = 0;
        cbComVel.SelectedIndex = 2;

        ComConfig.pr.AddParam(new ParamListItemControl("nome Porta",cbComName,25));
        ComConfig.pr.AddParam(new ParamListItemControl("velocità",cbComVel,25));
        ComConfig.pr.AddParam(new ParamListItemControl("Parita",cbComPar,25));

        if(ComConfig.ShowDialog() == DialogResult.OK){
            plc.Eth=false;
            plc.Dock = DockStyle.Fill;

            plc.sp.PortName = (string)cbComName.SelectedItem;
            plc.sp.BaudRate = (int)cbComVel.SelectedItem;
            plc.sp.Parity = (Parity)cbComPar.SelectedIndex;
            plc.sp.Handshake = System.IO.Ports.Handshake.None;
            plc.sp.WriteTimeout = 400;
            plc.sp.ReadTimeout = 400;
            plc.sp.StopBits = StopBits.One;
            plc.sp.RtsEnable = false;
            plc.sp.DtrEnable = false;
            plc.sp.DataBits = 8;
            plc.sp.ParityReplace = 0x00;           
           
            tm.Start();
        }
        //else Close();
    }

    private void updd(object sender, EventArgs e)
    {
        if(mod_id.Text.Length==0)return;
        if(mod_start.Text.Length==0)return;
        if(mod_n.Text.Length==0)return;
        tm.Interval = Int32.Parse(mod_poll.Text)*1000;
        plc.count =Int32.Parse(mod_n.Text);
        plc.start =Int32.Parse(mod_start.Text);
        plc.idm =Int32.Parse(mod_id.Text);
        plc.fcode=cpfun.SelectedIndex+1;
        plc.ShowBin = bina.Checked;
        plc.refreshDataAsync();
    }

    public MainForm()
    {
        InitializeComponent();       
        mod_n.Text = plc.count.ToString();
        mod_start.Text = plc.start.ToString();
        mod_id.Text = plc.idm.ToString();
        mod_poll.Text ="2";
    }
}
}