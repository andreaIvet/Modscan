using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Linq.Expressions;
using System.Diagnostics.Eventing.Reader;

namespace Modscan
{

    public class Bingrid : Control
    {
        public int idm = 1;
        public int start = 0;
        public int count = 100;
        public SerialPort sp = new SerialPort();
        private uint TrasizioneR = 0;
        private int sctime = 0;
        public string ip;
        public int port;
        public bool Eth;
        public int fcode=3;

        public bool ShowBin=false;

        public class crc
        {
            private static byte[] array_crc_low =
                {
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
            0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
            0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
            0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
            0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40,
            0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1,
            0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0,
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40
            };


            private static byte[] array_crc_high =
            {
            0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06,
            0x07, 0xC7, 0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD,
            0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09,
            0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A,
            0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC, 0x14, 0xD4,
            0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
            0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3,
            0xF2, 0x32, 0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4,
            0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A,
            0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29,
            0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF, 0x2D, 0xED,
            0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
            0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60,
            0x61, 0xA1, 0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67,
            0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F,
            0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68,
            0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA, 0xBE, 0x7E,
            0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
            0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71,
            0x70, 0xB0, 0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92,
            0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C,
            0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B,
            0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89, 0x4B, 0x8B,
            0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
            0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42,
            0x43, 0x83, 0x41, 0x81, 0x80, 0x40
            };
            public byte crc_low;
            public byte crc_high;

            public crc()
            {
                crc_low = 0xFF;
                crc_high = 0xFF;
            }
            public void WriteCRC(byte buf)
            {
                int index = crc_low ^ buf;
                crc_low = (byte)(crc_high ^ array_crc_low[index]);
                crc_high = array_crc_high[index];
            }
        }

    public string byteString(byte b)
    {
        string st = "";

        switch(b & 0x0F){
            case 00 : st+="0000";break;
            case 01 : st+="1000";break;
            case 02 : st+="0100";break;
            case 03 : st+="1100";break;
            
            case 04 : st+="0010";break;
            case 05 : st+="1010";break;
            case 06 : st+="0110";break;
            case 07 : st+="1110";break;
            
            case 08 : st+="0001";break;
            case 09 : st+="1001";break;
            case 10 : st+="0101";break;
            case 11 : st+="1101";break;
            
            case 12 : st+="0011";break;
            case 13 : st+="1011";break;
            case 14 : st+="0111";break;
            case 15 : st+="1111";break;
        }

        switch((b & 0xF0) >> 4){
            case 00 : st+="0000";break;
            case 01 : st+="1000";break;
            case 02 : st+="0100";break;
            case 03 : st+="1100";break;
            
            case 04 : st+="0010";break;
            case 05 : st+="1010";break;
            case 06 : st+="0110";break;
            case 07 : st+="1110";break;
            
            case 08 : st+="0001";break;
            case 09 : st+="1001";break;
            case 10 : st+="0101";break;
            case 11 : st+="1101";break;
            
            case 12 : st+="0011";break;
            case 13 : st+="1011";break;
            case 14 : st+="0111";break;
            case 15 : st+="1111";break;
        }
        
        return st;
    }
        public async void refreshDataAsync()
        {
            Task<Bitmap> tt = new Task<Bitmap>(refreshData);
            tt.Start();
            Bitmap bb = await tt;
            BackgroundImage = bb;
        }

        public Bitmap refreshData()
        {
            int ln = 0;
            int lc = 0;


            Socket s = new Socket(System.Net.Sockets.SocketType.Stream, ProtocolType.Tcp);
            Font fn = new Font("Monospace", 12);
            Bitmap back = new Bitmap(ClientSize.Width, ClientSize.Height);

            float fn_Size=fn.Size;
            float fn_Height=fn.Height;
            int byteCount = 0;

            switch(fcode){
                case 1:
                case 2:
                    int cc =count;
                    while(cc>0){
                        cc-=8;
                        byteCount++;
                    }
                    break;
                case 3:
                case 4:
                    byteCount = count*2;
                    break;
            }

            byte[] buf_in = new byte[byteCount + 5];
            byte[] buf = new byte[8];

            if (Eth)
            {
                buf_in = new byte[byteCount + 9];
                buf = new byte[12];
                buf[0] = (byte)((TrasizioneR & 0xFF00) >> 8);
                buf[1] = (byte)(TrasizioneR & 0x00FF);
                buf[2] = (byte)0x00;
                buf[3] = (byte)0x00;
                buf[4] = (byte)0x00;
                buf[5] = (byte)6;
                buf[6] = (byte)idm;
                buf[7] = (byte)fcode;
                buf[8] = (byte)((start & 0xFF00) >> 8);
                buf[9] = (byte)(start & 0x00FF);
                buf[10] = (byte)((count & 0xFF00) >> 8);
                buf[11] = (byte)(count & 0x00FF);
                TrasizioneR++;
                if (TrasizioneR == uint.MaxValue) TrasizioneR = 0;
            }

            if (!Eth)
            {
                crc cc = new crc();
                buf[0] = (byte)idm;
                cc.WriteCRC(buf[0]);
                buf[1] = (byte)fcode;
                cc.WriteCRC(buf[1]);
                buf[2] = (byte)((start & 0xFF00) >> 8);
                cc.WriteCRC(buf[2]);
                buf[3] = (byte)(start & 0x00FF);
                cc.WriteCRC(buf[3]);
                buf[4] = (byte)((count & 0xFF00) >> 8);
                cc.WriteCRC(buf[4]);
                buf[5] = (byte)(count & 0x00FF);
                cc.WriteCRC(buf[5]);
                buf[6] = cc.crc_low;
                buf[7] = cc.crc_high;
            }

            try
            {
                if (!Eth)
                {
                    sp.Open();
                    sp.Write(buf, 0, 8);
                    System.Threading.Thread.Sleep(400);
                    int nb = sp.Read(buf_in, 0, buf_in.Length);
                    if (nb == 5 && buf_in[2] == 1) throw new Exception("FUNZIONE NON SUPPORTATA");
                    if (nb == 5 && buf_in[2] == 2) throw new Exception("REGISTRI INESTSTENTI");
                    if (nb == 5 && buf_in[2] == 3) throw new Exception("REGISTRI NON ACCESSIBILI");
                    if (nb == 5 && buf_in[2] == 4) throw new Exception("DISPOSITIVO ROTTO");
                    if (nb == 5 && buf_in[2] == 6) throw new Exception("DISPOSITIVO OCCUPATO busy");
                    if (nb == 5 && buf_in[2] == 8) throw new Exception("DISPOSITIVO MEMORIA CORROTTA");
                    if (nb != buf_in.Length) throw new Exception("byte persi");
                    sp.Close();
                }

                if (Eth)
                {
                    s.Connect(IPAddress.Parse(ip), port);
                    System.Threading.Thread.Sleep(1000);
                    if (s.Connected)
                    {
                        s.Send(buf);
                        int nb = s.Receive(buf_in);
                        if (nb == 9 && buf_in[8] == 1) throw new Exception("FUNZIONE NON SUPPORTATA");
                        if (nb == 9 && buf_in[8] == 2) throw new Exception("REGISTRI INESTSTENTI");
                        if (nb == 9 && buf_in[8] == 3) throw new Exception("REGISTRI NON ACCESSIBILI");
                        if (nb == 9 && buf_in[8] == 4) throw new Exception("DISPOSITIVO ROTTO");
                        if (nb == 9 && buf_in[8] == 6) throw new Exception("DISPOSITIVO OCCUPATO busy");
                        if (nb == 9 && buf_in[8] == 8) throw new Exception("DISPOSITIVO MEMORIA CORROTTA");
                        if (nb != buf_in.Length) throw new Exception("byte persi");
                        s.Disconnect(false);
                    }
                    s.Close();
                }

                Graphics g = Graphics.FromImage(back);
                g.FillRectangle(new SolidBrush(this.BackColor), 0, 0, back.Width, back.Height);
                switch(fn.Unit){
                    case GraphicsUnit.Point:
                        fn_Size = fn.Size*72F/g.DpiX;
                        fn_Height = fn.Height*72F/g.DpiY+2;
                        break;
                }
                int sp0 = (int)fn_Size * 4 + 5;
                int h0 = 20;
                int i=3;
                int ind=start;
                short data=0;
                int maxlc=11;
                if(Eth)i+=6;

                while(byteCount>0)
                {
                    if (lc == 0)
                    {
                        g.DrawString(ind.ToString(), fn, Brushes.Black, 0, (int)(fn_Height * ln) + h0);
                        g.DrawLine(Pens.Gray, sp0, (int)(fn_Height * ln) + h0, sp0, (int)(fn_Height * (ln + 1)) + h0);
                        g.DrawLine(Pens.Gray, sp0, (int)(fn_Height * (ln + 1)) + h0,ClientSize.Width, (int)(fn_Height * (ln + 1)) + h0);
                        lc++;
                    }

                    if(fcode==1 || fcode==2){
                        g.DrawString(byteString(buf_in[i]), fn, Brushes.Black, (lc - 1) * (fn_Size * 7) + sp0, (int)(fn_Height * ln) + h0);
                        i+=1;
                        byteCount-=1;
                        ind+=8;
                        maxlc=5;
                    }
                    if(fcode==3 || fcode==4){
                       if(ShowBin){
                            maxlc=3;
                            g.DrawString(byteString(buf_in[i+1])+byteString(buf_in[i]), fn, Brushes.Black, (lc - 1) * (fn_Size * 17) + sp0, (int)(fn_Height * ln) + h0);
                        }
                        else{
                            maxlc=11;
                            data = (short)((int)(buf_in[i] << 8) | (int)buf_in[i + 1]);
                            g.DrawString(data.ToString(), fn, Brushes.Black, (lc - 1) * (fn_Size * 7) + sp0, (int)(fn_Height * ln) + h0);
                        }
                        i+=2;
                        ind+=1;
                        byteCount-=2;
                    }

                    lc++;
                    if (lc == maxlc)
                    {
                        ln++;
                        lc = 0;
                    }
                }


                g.FillRectangle(Brushes.Green, sp0, 5, sctime, 10);
                sctime += 5;
                if (sctime > 100) sctime = 0;
                g.Dispose();
            }
            catch (Exception ee)
            {
                Graphics g = Graphics.FromImage(back);
                g.FillRectangle(Brushes.White, 0, 0, back.Width, back.Height);
                g.DrawString(ee.Message, fn, Brushes.Black, 10, 10);
                g.Dispose();
                if (!Eth) sp.Close();
                else
                {
                    if (s.Connected) s.Disconnect(false);
                    s.Close();
                }
            }
            return back;
        }
    }
}