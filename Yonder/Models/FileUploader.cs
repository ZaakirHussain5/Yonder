using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Yonder.Models
{
    public class FileUploader
    {
        public static string UploadFile(string filename,byte[] File)
        {
            string ftpfullpath = string.Format("{0}yon-img{1}{2}{3}",
                ConfigurationManager.AppSettings["FTPURL"].ToString(),DateTime.Now.Year,DateTime.Now.Millisecond,filename);
            FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
            ftp.Credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["FTPU"].ToString(), 
                ConfigurationManager.AppSettings["FTPP"].ToString());

            ftp.KeepAlive = true;
            ftp.UseBinary = true;
            ftp.Method = WebRequestMethods.Ftp.UploadFile;

            byte[] buffer = File;

            Stream ftpstream = ftp.GetRequestStream();
            ftpstream.Write(buffer, 0, buffer.Length);
            ftpstream.Close();

            return ftpfullpath.Replace("ftp", "http").Replace("/yon-", "/Yonder/yon-");
        }
    }
}