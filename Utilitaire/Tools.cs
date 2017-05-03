using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.IO;
using Ecole.UI;
using System.Xml;
using System.Threading;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace Ecole.Utilitaire
{
    class Tools
    {
        public static int CENTAINE = 2;
        public static int DIZAINE = 1;
        public static int UNITE = 0;
        public static String SEPARATOR = " ";

        private string uriserveur { get; set; }
        private string numero { get; set; }
        private string usernameSms { get; set; }
        private string passwordSms { get; set; }

        private static int STATUS_INDEX = 0;
        private static int MESSAGE_ID_INDEX = 1;
        private static int DESTINATION_INDEX = 2;

        public Tools()
        { }

        public Tools(string app_folder)
        {
            try
            {
                if (Tools.DecryptFile(app_folder + "serveurNotification.xml", app_folder + "serveurNotification1.xml"))
                    Console.WriteLine("decrytion succeed");
                else
                    Console.WriteLine("decrytion failed");

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(app_folder + "serveurNotification1.xml");

                XmlNodeList nodeList = xmlDoc.GetElementsByTagName("SMS");
                
                foreach (XmlNode node in nodeList)
                {
                    uriserveur = node.ChildNodes.Item(0).InnerText;
                    numero = node.ChildNodes.Item(1).InnerText;
                    usernameSms = node.ChildNodes.Item(2).InnerText;
                    passwordSms = node.ChildNodes.Item(3).InnerText;
                }

                if (Tools.EncryptFile(app_folder + "serveurNotification1.xml", app_folder + "serveurNotification.xml"))
                    Console.WriteLine("encrytion succeed");
                else
                    Console.WriteLine("encrytion failed");
                File.Delete(System.IO.Path.GetFullPath(app_folder + "serveurNotification1.xml"));
            }
            catch (Exception e) { Console.WriteLine("Erreur dans la création de Tools --- " + e.Message); }
        }

        /**
         * 
         * Retourne une chaine numerique representant le nombre en integrant le separateur de bloc
         * 
         */
        public static String getFormattedNumber(ulong number)
        {
            if (number == 0)
                return "0";
            String str = "";
            while (number != 0)
            {
                int bloc = (int)(number % 1000);
                if (bloc == 0 && number >= 1000)
                    str = "000" + SEPARATOR + str;
                else if (bloc < 10 && number > 1000)
                    str = "00" + bloc + SEPARATOR + str;
                else if (bloc < 100 && number > 1000)
                    str = "0" + bloc + SEPARATOR + str;
                else
                    str = bloc + SEPARATOR + str;
                number /= 1000;
            }
            return str;
        }

        public static String numberToString(ulong number)
        {
            if (number == 0)
                return "Zero";
            String[] bloc_labels = {"."," Mille"," Million"," Milliard"," Billion", " Billiard", 
								    " Trillion", " Trilliard", " Quadrillion", " Quadrilliard"};
            int nb_bloc = 0;
            String str = "";
            while (number != 0)
            {
                int bloc_3 = (int)(number % 1000);
                String plural_suffix = "";
                if (bloc_3 > 1 && nb_bloc > 1)
                {
                    plural_suffix = "s";
                }
                number /= 1000;
                int centaine = bloc_3 / 100;
                int bloc_2 = bloc_3 % 100;
                String bloc_str = "";

                if (bloc_3 != 1 || nb_bloc != 1)
                {// cas different de 1000
                    if (bloc_2 == 0 && centaine > 1)// multiple strict de 100
                        bloc_str += digitToString(centaine, CENTAINE) + "s";
                    else
                        bloc_str += digitToString(centaine, CENTAINE) + " " + twoDigitToString(bloc_2);
                    //System.Console.Write( bloc_3 + " : " + bloc_str + "\n");
                }
                if (bloc_3 != 0)
                    str = bloc_str + bloc_labels[nb_bloc] + plural_suffix + " " + str;

                nb_bloc++;
            }
            return str;

        }

        public static String twoDigitToString(int digit_2)
        {
            int dizaine = digit_2 / 10;
            int unite = digit_2 % 10;
            switch (dizaine)
            {
                case 0:
                    return digitToString(unite, UNITE);
                case 1:
                    switch (unite)
                    {
                        case 0:
                            return "dix";
                        case 1:
                            return "onze";
                        case 2:
                            return "douze";
                        case 3:
                            return "treize";
                        case 4:
                            return "quatorze";
                        case 5:
                            return "quinze";
                        case 6:
                            return "seize";

                        default:
                            return "dix-" + digitToString(unite, UNITE);
                    }
                case 7:
                    return digitToString(dizaine - 1, DIZAINE) + "-" + twoDigitToString(10 + unite);
                case 9:
                    return digitToString(dizaine - 1, DIZAINE) + "-" + twoDigitToString(10 + unite);
                default:
                    if (unite != 0)
                        return digitToString(dizaine, DIZAINE) + "-" + digitToString(unite, UNITE);
                    else if (dizaine == 8)
                        return digitToString(dizaine, DIZAINE) + "s";
                    else
                        return digitToString(dizaine, DIZAINE);
            }
        }

        public static String digitToString(int digit, int type)
        {
            switch (digit)
            {

                case 1:
                    if (type == UNITE)
                        return "un";
                    if (type == DIZAINE)
                        return "dix";
                    if (type == CENTAINE)
                        return "cent";
                    break;
                case 2:
                    if (type == UNITE)
                        return "deux";
                    if (type == DIZAINE)
                        return "vingt";
                    if (type == CENTAINE)
                        return "deux cent";
                    break;
                case 3:
                    if (type == UNITE)
                        return "trois";
                    if (type == DIZAINE)
                        return "trente";
                    if (type == CENTAINE)
                        return "trois cent";
                    break;
                case 4:
                    if (type == UNITE)
                        return "quatre";
                    if (type == DIZAINE)
                        return "quarante";
                    if (type == CENTAINE)
                        return "quatre cent";
                    break;
                case 5:
                    if (type == UNITE)
                        return "cinq";
                    if (type == DIZAINE)
                        return "cinquante";
                    if (type == CENTAINE)
                        return "cinq cent";
                    break;
                case 6:
                    if (type == UNITE)
                        return "six";
                    if (type == DIZAINE)
                        return "soixante";
                    if (type == CENTAINE)
                        return "six cent";
                    break;
                case 7:
                    if (type == UNITE)
                        return "sept";
                    if (type == DIZAINE)
                        return "soixante dix";
                    if (type == CENTAINE)
                        return "sept cent";
                    break;
                case 8:
                    if (type == UNITE)
                        return "huit";
                    if (type == DIZAINE)
                        return "quatre-vingt";
                    if (type == CENTAINE)
                        return "huit cent";
                    break;
                case 9:
                    if (type == UNITE)
                        return "neuf";
                    if (type == DIZAINE)
                        return "quatre-vingt dix";
                    if (type == CENTAINE)
                        return "neuf cent";
                    break;
                default:
                    return "";
            }
            return "";
        }

        public static string initialeChaine(string chaine)
        {
            int n;
            if (chaine == "")
                return "-";
            string[] tab = chaine.Split(' ');
            n = tab.Length;
            string result = "";

            for (int i = 0; i < n; i++)
                result = result + tab[i][0];

            return result;
        }

        public string buildContent(string sender, string text, List<string[]> adresses)
        {
            string json = "{\"from\":\"" + sender + "\",";
            if (adresses.Count > 1)
            {
                json += "\"to\":[";
                for(int i = 0; i< adresses.Count - 1; i++)
                {
                    string[] recipient = adresses.ElementAt(i);
                    json += "\"" + format_number(recipient[1]) + "\",";
                }
                json += "\"" + format_number(adresses.ElementAt(adresses.Count - 1)[1]) + "\"";
                json += "],";
            }
            else
            {
                foreach (string[] recipient in adresses)
                {
                    json += "\"to\":\"" + format_number(recipient[1]) + "\",";
                }
            }

            json += "\"text\":\"" +  text +"\" }";

            return json;
        }

        public string buildContent(string sender, List<string[]> adresses)
        {
            string json = "{";
            json += "\"messages\":[";
            for (int i = 0; i < adresses.Count - 1; i++)
            {
                string[] tab = adresses.ElementAt(i);
                json += "{";
                json += "\"from\":\"" + sender + "\",";
                json += "\"to\":\"" + format_number(tab[1]) + "\",";
                json += "\"text\":\"" + tab[2] + "\"";
                json += "},";
            }
            string[] tab1 = adresses.ElementAt(adresses.Count - 1);
            json += "{";
            json += "\"from\":\"" + sender + "\",";
            json += "\"to\":\"" + format_number(tab1[1]) + "\",";
            json += "\"text\":\"" + tab1[2] + "\"";
            json += "}";

            json += "]";
            json += "}";

            return json;
        }

        public Dictionary<string, int> getSendingStatus(string response)
        {
            Dictionary<string, int> resultats = new Dictionary<string, int>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("result");

            foreach (XmlNode node in nodeList)
            {
                string status = node.ChildNodes.Item(STATUS_INDEX).InnerText;
                string messageId = node.ChildNodes.Item(MESSAGE_ID_INDEX).InnerText;
                string destination = node.ChildNodes.Item(DESTINATION_INDEX).InnerText;
                Console.WriteLine("\n\tstatus: " + status + "\n\tid:" + messageId + "\n\t+destination:" + destination);
                resultats.Add(messageId, Convert.ToInt32(status));
            }

            return resultats;
        }

        public string SendingSMS(string content, string api)
        {

            /* Préparation de la requête */

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(this.uriserveur + api);
            request.Method = "POST";
            request.AllowAutoRedirect = false;
            // Convert POST data to a byte array.
            byte[] byteArray = Encoding.UTF8.GetBytes(content);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/json";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Set the accept header
            request.Accept = "application/json";
            // Set the authorization
            request.Headers.Add("authorization", "Basic " + getAuthorization(this.usernameSms, this.passwordSms));

            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();


            /* envoi de la requête */

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                Console.WriteLine("Web exception occurred. Status code: {0}", ex.Status);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            /* traitement de la reponse */

            Stream dataStream_out = null;
            StreamReader reader = null;
            string responseFromServer = null;

            try
            {
                // Get the stream containing content returned by the server.
                dataStream_out = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream_out);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                // Cleanup the streams and the response.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (dataStream_out != null)
                {
                    dataStream_out.Close();
                }
                response.Close();
            }
            return responseFromServer;
        }

        public Dictionary<string, int> sendSMS(List<string[]> adresse, string message)
        {
            try
            {
                string api = "/sms/1/text/single";
                string content = buildContent(numero, message, adresse);
                string response = SendingSMS(content, api);
                return getSendingStatus(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal Dictionary<string, int> sendSMS(List<string[]> destinataireSMS)
        {
            try
            {
                string api = "/sms/1/text/multi";
                string content = buildContent(numero, destinataireSMS);
                string response = SendingSMS(content, api);
                return getSendingStatus(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal Dictionary<string, int> sendSMS(string[] adresse, string message)
        {
            try
            {
                string api = "/sms/1/text/single";
                List<string[]> adresses = new List<string[]>();
                adresses.Add(adresse);
                string content = buildContent(numero, message, adresses);
                string response = SendingSMS(content, api);
                return getSendingStatus(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string getAuthorization(string username, string password)
        {
            string result = "";

            var bytes = Encoding.UTF8.GetBytes(username + ":" + password);
            result = Convert.ToBase64String(bytes);

            return result;
        }

        public static string format_number(string number)
        {
            if (number.Length < 9)
                return "";
            if (number.Length == 13)
                return number;
            if (number.Length == 12 & !number.Contains('+'))
                return "+" + number;
            if (number.Length == 9)
                return "+237" + number;

            return "";
        }

        public static Dictionary<string, string> getBalance(string username, string pwd)
        {
            Dictionary<string, string> retour = new Dictionary<string, string>();

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://api.infobip.com/account/1/balance");
            request.Method = "POST";
            request.Accept = "application/json";
            request.Headers.Add("authorization", "Basic "+getAuthorization(username,pwd));

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                Console.WriteLine("Web exception occurred. Status code: {0}", ex.Status);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            /* traitement de la reponse */

            Stream dataStream_out = null;
            StreamReader reader = null;
            string responseFromServer = null;

            try
            {
                // Get the stream containing content returned by the server.
                dataStream_out = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                reader = new StreamReader(dataStream_out);
                // Read the content.
                responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);
                string[] tab = responseFromServer.Split(',');
                retour.Add("balance",tab[0].Split(':')[1].ToString());
                retour.Add("currency", tab[1].Split(':')[1].ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (dataStream_out != null)
                {
                    dataStream_out.Close();
                }
                response.Close();
            }

            return retour;
        }

        public static bool HasConnection()
        {
            try
            {
                System.Net.IPHostEntry i = System.Net.Dns.GetHostEntry("www.google.com");
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region cryptaograhie

        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Encrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        public static bool EncryptFile(string inputFile, string outputFile)
        {

            try
            {
                string password = @"myKey123"; // Your Key Here
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
                return true;
            }
            catch
            {
                Console.WriteLine("Encryption failed!", "Error");
                return false;
            }
        }

        ///<summary>
        /// Steve Lydford - 12/05/2008.
        ///
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        public static bool DecryptFile(string inputFile, string outputFile)
        {
            try
            {
                string password = @"myKey123"; // Your Key Here

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();
                return true;
            }
            catch
            {
                Console.WriteLine("Decryption failed!", "Error");
                return false;
            }
        }

        #endregion


        public static void createchart(Dictionary<List<string>, List<double>> moyennesSequentielles)
        {
            
            string[] xvals = new string[moyennesSequentielles.ElementAt(0).Key.Count];
            double[] yvals = new double[moyennesSequentielles.ElementAt(0).Key.Count];
            for (int i = 0; i < moyennesSequentielles.ElementAt(0).Key.Count; i++)
            {
                xvals[i] = moyennesSequentielles.ElementAt(0).Key.ElementAt(i);
                yvals[i] = moyennesSequentielles.ElementAt(0).Value.ElementAt(i);
            }
            
            // create the chart
            var chart = new Chart();
            chart.Size = new Size(600, 300);

            var chartArea = new ChartArea();
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.LabelStyle.Font = new Font("Consolas", 8);
            chartArea.AxisY.LabelStyle.Font = new Font("Consolas", 8);
            chart.ChartAreas.Add(chartArea);

            var series = new Series();
            series.Name = "Series1";
            series.ChartType = SeriesChartType.Column;
            series.XValueType = ChartValueType.String;
            series.YValueType = ChartValueType.Double;
            chart.Series.Add(series);

            // bind the datapoints
            chart.Series["Series1"].Points.DataBindXY(xvals, yvals);

            // draw!
            chart.Invalidate();

            // write out a file
            chart.SaveImage(ConnexionUI.DOSSIER_BULLETINS + "chart.png", ChartImageFormat.Png);
        }
    }
}
