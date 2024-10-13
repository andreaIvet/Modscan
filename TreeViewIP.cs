using System;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Modscan
{
    public class TreeViewIP : TreeView
    {
        public void searchIP()
        {
            Nodes.Clear();
            foreach (NetworkInterface i in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (i.OperationalStatus == OperationalStatus.Up && i.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    TreeNode tii = Nodes.Add(i.Name);
                    foreach (UnicastIPAddressInformation ip in i.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {                        
                            string[] mm = ip.Address.ToString().Split(".");                           
                            searchIPAS(mm[0]+"."+mm[1]+"."+mm[2]+".",tii);                                 
                        }
                    }
                }
            }
        }

        async void searchIPAS(string str,TreeNode adap){
            TreeNode nr =new TreeNode("rete "+str);        
            adap.Nodes.Add(nr);
            Ping p =new Ping();   
            for(int c=0;c<255;c++){            
                int perc = (int)((double)c/255.0*100.0);
                nr.Text = "scansione rete "+str +" "+perc+"%";                                                    
                string ipp =str+c;     
                PingReply pr = await p.SendPingAsync(ipp,2);    
                if(pr.Status == IPStatus.Success){
                    TreeNode tr =new TreeNode(nr.Nodes.Count + " : "+ipp);     
                    tr.Tag = ipp;     
                    nr.Nodes.Add(tr);                                                             
                }                            
            }
            if(nr.Nodes.Count==0)adap.Nodes.Remove(nr); 
            else  nr.Text = "rete "+str;        
        }
    }
}