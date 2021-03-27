using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Net;
using System.IO;
using api_database;


namespace Server
{
    public class FTPserver
    {
        public string HostFTP { get; private set; }
        public string LoginFTP { get; private set; }
        public string PasswordFTP { get; private set; }
        public string FTPFolder { get; private set; }
        public string UriFTP { get; private set; }

        public FTPserver()
        {
            HostFTP = "ftp60.hostland.ru";
            LoginFTP = "host1323541_itstep";
            PasswordFTP = "geV32nn8Sq";
            FTPFolder = "mnement";
            UriFTP = "ftp://" + HostFTP + "/" + FTPFolder + "/";
        }

        public FTPserver(string host, string login, string password, string ftpFolder)
        {
            HostFTP = host;
            LoginFTP = login;
            PasswordFTP = password;
            FTPFolder = ftpFolder;
            UriFTP = "ftp://" + HostFTP + "/" + FTPFolder + "/";
        }

        public string FTPUploadFile(string filename, string sender, string receiver)
        {
            WebClient wc = new WebClient();
            wc.BaseAddress = UriFTP;
            wc.Credentials = new NetworkCredential(LoginFTP, PasswordFTP);
            Console.WriteLine(DateTime.Now.ToString() + " - " + "Uploading..");
            wc.UploadProgressChanged += new UploadProgressChangedEventHandler(wc_UploadProgressChanged);
            wc.UploadFileCompleted += new UploadFileCompletedEventHandler(wc_UploadFileCompleted);
            wc.UploadFileAsync(new Uri(@UriFTP + Path.GetFileName(filename)), filename);
            var link = UriFTP + RenameFile(filename, sender, receiver);
            return link;
        }

        public string RenameFile(string filename, string sender, string receiver)
        {
            FtpWebRequest client = (FtpWebRequest)FtpWebRequest.Create(new Uri(@UriFTP + Path.GetFileName(filename)));
            client.Credentials = new NetworkCredential(LoginFTP, PasswordFTP);
            client.KeepAlive = true;
            client.UseBinary = true;
            string dateTime = System.Text.RegularExpressions.Regex.Replace(DateTime.Now.ToString(), @"\s+", "_");
            dateTime = System.Text.RegularExpressions.Regex.Replace(dateTime, @"[^0-9a-zA-Z.]+", "_");
            string newName = dateTime + "_" + sender + "_" + receiver + Path.GetExtension(filename);
            client.Method = WebRequestMethods.Ftp.Rename;
            client.RenameTo = newName;
            filename = newName;
            FtpWebResponse renameResponse = (FtpWebResponse)client.GetResponse();
            renameResponse.Close();
            return filename;
        }

        public void FTPDownloadFile(string filename)
        {
            WebClient wc = new WebClient();
            wc.BaseAddress = UriFTP;
            wc.Credentials = new NetworkCredential(LoginFTP, PasswordFTP);

            bool flag = true;

            do
            {
                if (!wc.IsBusy)
                {
                    Console.WriteLine(DateTime.Now.ToString() + " - " + "Downloading..");
                    wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
                    wc.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(wc_DownloadFileCompleted);
                    wc.DownloadFileAsync(new Uri(filename), "adaddsf.txt");
                    flag = false;
                }
            } while (flag);
        }
        

        public void ServerContents()
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(UriFTP);

            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = new NetworkCredential(LoginFTP, PasswordFTP);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Console.WriteLine("Содержимое сервера:");
            Console.WriteLine();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            Console.WriteLine(reader.ReadToEnd());

            reader.Close();
            responseStream.Close();
            response.Close();
            Console.ReadKey();
        }
        public void wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString() + " - " + "Process completed");
        }

        public void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString() + " - " + e.ProgressPercentage.ToString() + "% - " + e.BytesReceived / 1024 + @"kb/" + e.TotalBytesToReceive / 1024 + "kb");
        }

        public void wc_UploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString() + " - " + e.ProgressPercentage.ToString() + "% - " + e.BytesSent / 1024 + @"kb/" + e.TotalBytesToSend / 1024 + "kb");
        }

        public void wc_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToString() + " - " + "Uploaded!");
        }

    }
}
