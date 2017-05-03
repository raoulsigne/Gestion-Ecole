using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections;
using System.Windows.Controls.Primitives;
using Ecole.BusinessEntity;
using Ecole.DataAccess;
using Ecole.ClasseConception;
using Ecole.BusinessLogic;
using System.Diagnostics;
using System.IO;

using Ecole.UI;
using System.Collections.ObjectModel;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Media.Imaging;

namespace Ecole.Utilitaire
{
    public class CreerEtat

    {
        //Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        //static string logo = ConnexionUI.DOSSIER_IMAGES + "logo.png";
        public string docname { get; set; }
        public string title { get; set; }

        public CreerEtat(string name, string titre)
        {
            this.docname = ConnexionUI.DOSSIER_ETATS + name + ".pdf";
            this.title = titre;

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
        }

        public CreerEtat()
        {
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
        }

        private PdfPTable creerEntete()
        {
            ParametresDA parametreDA = new ParametresDA();
            ParametresBE parametre = parametreDA.rechercherDerniereValeur();

            PdfPTable entete = new PdfPTable(3);
            float[] widths = new float[] { 3.5f, 3f, 3.5f };
            entete.SetWidths(widths);
            entete.WidthPercentage = 95f;
            entete.DefaultCell.Border = Rectangle.NO_BORDER;
            entete.HorizontalAlignment = Element.ALIGN_CENTER;

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times = new Font(bfTimes, 6);
            Font timeheader = new Font(bfTimes, 8, Font.BOLD);
            PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;


            cell.Phrase = new iTextSharp.text.Phrase(parametre.ministere.ToUpper(), times);
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            entete.AddCell(cell);

            //logo
            try
            {
                iTextSharp.text.Image imglogo = iTextSharp.text.Image.GetInstance(ConnexionUI.DOSSIER_IMAGES + parametre.logo);
                imglogo.ScalePercent(40f);
                PdfPCell imageCell = new PdfPCell(imglogo);
                imageCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                imageCell.Rowspan = 3;
                imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                imageCell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
                entete.AddCell(imageCell);
            }
            catch (Exception)
            {
                cell = new PdfPCell(new Phrase(" ", times));
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.Rowspan = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                entete.AddCell(cell);
            }

            cell = new PdfPCell(new Phrase("Republique du Cameroun".ToUpper(), times));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            entete.AddCell(cell);

            cell.Phrase = new iTextSharp.text.Phrase("Délégation Régionale du ".ToUpper() + parametre.region.ToUpper(), times);
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            entete.AddCell(cell);

            cell = new PdfPCell(new Phrase("Paix--Travail--Patrie", times));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            entete.AddCell(cell);

            cell.Phrase = new iTextSharp.text.Phrase("Délégation Départementale de ".ToUpper() + parametre.departement.ToUpper(), times);
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            entete.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", times));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            entete.AddCell(cell);

            cell = new PdfPCell(new Phrase(parametre.nomEcole.ToUpper(), timeheader));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            entete.AddCell(cell);

            cell = new PdfPCell(new Phrase("N Immatriculation : 5LC2GSFD112567076", times));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            entete.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", times));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            entete.AddCell(cell);

            cell.Phrase = new iTextSharp.text.Phrase("BP " + parametre.adresse.ToUpper() + " " + parametre.ville.ToUpper() + " / TEL " + parametre.tel, times);
            cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT; 
            entete.AddCell(cell);

            cell = new PdfPCell(new Phrase("Discipline-Travail-Réussite", times));
            cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            entete.AddCell(cell);

            cell = new PdfPCell(new Phrase(" ", times));
            cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            entete.AddCell(cell);

            return entete;
        }

        private PdfPTable creerEnteteReduit_old()
        {
            ParametresDA parametreDA = new ParametresDA();
            ParametresBE parametre = parametreDA.rechercherDerniereValeur();

            PdfPTable entete = new PdfPTable(3);
            float[] widths = new float[] { 3.5f, 2f, 4.5f };
            entete.SetWidths(widths);
            entete.WidthPercentage = 95f;
            entete.DefaultCell.Border = Rectangle.NO_BORDER;
            entete.HorizontalAlignment = Element.ALIGN_CENTER;

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times = new Font(bfTimes, 10);
            Font timeheader = new Font(bfTimes, 12, Font.BOLD);
            PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            cell = new PdfPCell(new Phrase("Republique du Cameroun".ToUpper(), times));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            entete.AddCell(cell);
            try
            {
                iTextSharp.text.Image imglogo = iTextSharp.text.Image.GetInstance(ConnexionUI.DOSSIER_IMAGES + parametre.logo);
                imglogo.ScalePercent(30f);
                PdfPCell imageCell = new PdfPCell(imglogo);
                imageCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                imageCell.Rowspan = 4;
                imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                imageCell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
                entete.AddCell(imageCell);
            }
            catch (Exception)
            {
                cell = new PdfPCell(new Phrase(" ", times));
                cell.Border = Rectangle.NO_BORDER;
                cell.Rowspan = 4;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                entete.AddCell(cell);
            }

            cell.Phrase = new iTextSharp.text.Phrase("Délégation Départementale de " + parametre.departement, times);
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            entete.AddCell(cell);
            cell.Phrase = new iTextSharp.text.Phrase(parametre.ministere, times);
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            entete.AddCell(cell);
            cell = new PdfPCell(new Phrase(parametre.nomEcole.ToUpper(), timeheader));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            entete.AddCell(cell);
            cell.Phrase = new iTextSharp.text.Phrase("Délégation Régionale du ".ToUpper() + parametre.region.ToUpper(), times);
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            entete.AddCell(cell);
            cell.Phrase = new iTextSharp.text.Phrase(parametre.ville, times);
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            entete.AddCell(cell);
            cell.Phrase = new iTextSharp.text.Phrase(parametre.email + " / " + parametre.siteWeb, times);
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            entete.AddCell(cell);
            cell.Phrase = new iTextSharp.text.Phrase("BP : " + parametre.adresse + " / Tel " + parametre.tel + " / Fax " + parametre.fax, times);
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            entete.AddCell(cell);
            //cell = new PdfPCell(new Phrase(" ", timeheader));
            //cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            //entete.AddCell(cell);
            //cell.Phrase = new iTextSharp.text.Phrase(parametre.email + " / " + parametre.siteWeb, times);
            //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //entete.AddCell(cell);
            
            return entete;
        }

        private PdfPTable creerEnteteReduit()
        {
            ParametresDA parametreDA = new ParametresDA();
            ParametresBE parametre = parametreDA.rechercherDerniereValeur();

            PdfPTable entete = new PdfPTable(3);
            float[] widths = new float[] { 5f, 2f, 3f };
            entete.SetWidths(widths);
            entete.WidthPercentage = 95f;
            entete.DefaultCell.Border = Rectangle.NO_BORDER;
            entete.HorizontalAlignment = Element.ALIGN_CENTER;

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times = new Font(bfTimes, 8);
            Font time_ministere = new Font(bfTimes, 8, Font.BOLD);
            Font time_delegation = new Font(bfTimes, 8, Font.ITALIC);
            Font times_dept = new Font(bfTimes, 7, Font.BOLD);
            Font times_light = new Font(bfTimes, 6);
            Font timeheader = new Font(bfTimes, 10, Font.BOLD);
            PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            cell = new PdfPCell(new Phrase(parametre.ministere.ToUpper(), time_ministere));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            entete.AddCell(cell);
            try
            {
                iTextSharp.text.Image imglogo = iTextSharp.text.Image.GetInstance(ConnexionUI.DOSSIER_IMAGES + parametre.logo);
                imglogo.ScalePercent(30f);
                PdfPCell imageCell = new PdfPCell(imglogo);
                imageCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                imageCell.Rowspan = 5;
                imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                imageCell.VerticalAlignment = Rectangle.ALIGN_MIDDLE;
                entete.AddCell(imageCell);
            }
            catch (Exception)
            {
                cell = new PdfPCell(new Phrase(" ", times));
                cell.Border = Rectangle.NO_BORDER;
                cell.Rowspan = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                entete.AddCell(cell);
            }

            cell.Phrase = new iTextSharp.text.Phrase("Republique du Cameroun".ToUpper(), times);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            entete.AddCell(cell);
            cell.Phrase = new iTextSharp.text.Phrase("Délégation Régionale ".ToUpper() + parametre.region.ToUpper(), time_delegation);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            entete.AddCell(cell);
            cell.Phrase = new iTextSharp.text.Phrase("Paix--Travail--Patrie", times_light);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            entete.AddCell(cell);
            cell.Phrase = new iTextSharp.text.Phrase("Département ".ToUpper() + parametre.departement.ToUpper(), times_dept);
            entete.AddCell(cell);
            cell.Phrase = new iTextSharp.text.Phrase(" ", timeheader);
            entete.AddCell(cell);
            cell.Phrase = new iTextSharp.text.Phrase(parametre.nomEcole.ToUpper(), timeheader);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            entete.AddCell(cell);
            cell.Phrase = new iTextSharp.text.Phrase(" ", timeheader);
            entete.AddCell(cell);
            cell.Phrase = new iTextSharp.text.Phrase("BP : " + parametre.adresse + " " + parametre.ville + " / Tel " + parametre.tel + " / "+ parametre.siteWeb, times_dept);
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            entete.AddCell(cell);
            cell.Phrase = new iTextSharp.text.Phrase(" ", timeheader);
            entete.AddCell(cell);

            return entete;
        }


        //private PdfPTable creerEnteteBulletin(EleveBE eleve, ClasseBE classe, int anneeScolaire, int effectifClasse)
        //{
        //    //********************* DEBUT partie mettant la photo de l'élève sur le bulletin

        //    PdfPTable infosEleve = new PdfPTable(3);
        //    float[] widths = new float[] { 1f, 3f, 1.5f };
        //    infosEleve.SetWidths(widths);
        //    infosEleve.WidthPercentage = 95f;
        //    infosEleve.DefaultCell.Border = Rectangle.NO_BORDER;
        //    infosEleve.HorizontalAlignment = Element.ALIGN_CENTER;

        //    BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
        //    Font times = new Font(bfTimes, 10, Font.BOLD);
        //    PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
        //    cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

        //    cell = new PdfPCell(new Phrase(" ", times));
        //    cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        //    cell.VerticalAlignment = Element.ALIGN_BOTTOM;
        //    cell.Colspan = 3;
        //    infosEleve.AddCell(cell);

        //    if (eleve != null && eleve.photo != null)
        //    {
        //        try
        //        {
        //            iTextSharp.text.Image imglogo = iTextSharp.text.Image.GetInstance(new Uri(ConnexionUI.DOSSIER_PHOTO + eleve.photo));
        //            imglogo.ScalePercent(15f);
        //            PdfPCell imageCell = new PdfPCell(imglogo);
        //            imageCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //            imageCell.Rowspan = 3; // either 1 if you need to insert one cell
        //            imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            imageCell.VerticalAlignment = Element.ALIGN_CENTER;
        //            infosEleve.AddCell(imageCell);
        //        }
        //        catch (Exception)
        //        {
        //            cell = new PdfPCell(new Phrase(" ", times));
        //            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //            cell.Rowspan = 3; // either 1 if you need to insert one cell
        //            cell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            cell.VerticalAlignment = Element.ALIGN_CENTER;
        //            infosEleve.AddCell(cell);
        //        }
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase(" ", times));
        //        cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //        cell.Rowspan = 3;
        //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //        cell.VerticalAlignment = Element.ALIGN_CENTER;
        //        infosEleve.AddCell(cell);
        //    }

        //    cell = new PdfPCell(new Phrase(" Nom : " + eleve.nom.ToUpper() + " \n Matricule :  " + eleve.matricule + " \n Née le " + eleve.dateNaissance.ToShortDateString() + " à " + eleve.lieuNaissance, times));
        //    cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //    cell.Rowspan = 3;
        //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //    cell.VerticalAlignment = Element.ALIGN_CENTER;
        //    infosEleve.AddCell(cell);

        //    EleveDA eleveDA = new EleveDA();

        //    //si l'élève est redoublant
        //    if (eleveDA.estRedoublant(eleve, classe, anneeScolaire))
        //    {
        //        cell = new PdfPCell(new Phrase(" Classe : " + classe.codeClasse + " \n Effectif : " + effectifClasse + " \n Redoublant : OUI" + " \n Année scolaire : " + (anneeScolaire - 1) + "/" + anneeScolaire, times));
        //        cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //        cell.Rowspan = 3;
        //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //        cell.VerticalAlignment = Element.ALIGN_CENTER;
        //        infosEleve.AddCell(cell);
        //    }
        //    else
        //    {
        //        cell = new PdfPCell(new Phrase(" Classe : " + classe.codeClasse + " \n Effectif : " + effectifClasse + " \n Redoublant : NON" + " \n Année scolaire : " + (anneeScolaire - 1) + "/" + anneeScolaire, times));
        //        cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //        cell.Rowspan = 3;
        //        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        //        cell.VerticalAlignment = Element.ALIGN_CENTER;
        //        infosEleve.AddCell(cell);
        //    }

        //    /*cell = new PdfPCell(new Phrase(" ", times));
        //    cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
        //    cell.VerticalAlignment = Element.ALIGN_BOTTOM;
        //    cell.Colspan = 3;
        //    infosEleve.AddCell(cell);*/

        //    //********************* FIN partie mettant la photo de l'élève sur le bulletin

        //    return infosEleve;
        //}

        private PdfPTable creerEnteteBulletin(EleveBE eleve, ClasseBE classe, int anneeScolaire, int effectifClasse)
        {
            //********************* DEBUT partie mettant la photo de l'élève sur le bulletin
            PdfPTable infosEleve = new PdfPTable(2);
            float[] widths = new float[] { 3f, 2f };
            infosEleve.SetWidths(widths);
            infosEleve.WidthPercentage = 70f;
            infosEleve.DefaultCell.Border = Rectangle.NO_BORDER;
            infosEleve.HorizontalAlignment = Element.ALIGN_RIGHT;

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times = new Font(bfTimes, 8, Font.BOLD);
            PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            cell = new PdfPCell(new Phrase(" ", times));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_BOTTOM;
            cell.Colspan = 4;
            infosEleve.AddCell(cell);

            cell = new PdfPCell(new Phrase(" Nom : " + eleve.nom.ToUpper() + " \n Matricule :  " + eleve.matricule + " \n Née le " + eleve.dateNaissance.ToShortDateString() + " à " + eleve.lieuNaissance, times));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Rowspan = 3;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_CENTER;
            infosEleve.AddCell(cell);

            EleveDA eleveDA = new EleveDA();

            //si l'élève est redoublant
            if (eleveDA.estRedoublant(eleve, classe, anneeScolaire))
            {
                cell = new PdfPCell(new Phrase(" Classe : " + classe.codeClasse + " \n Effectif : " + effectifClasse + " \n Redoublant : OUI", times));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.Rowspan = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                infosEleve.AddCell(cell);
            }
            else
            {
                cell = new PdfPCell(new Phrase(" Classe : " + classe.codeClasse + " \n Effectif : " + effectifClasse + " \n Redoublant : NON", times));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.Rowspan = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                infosEleve.AddCell(cell);
            }

            cell = new PdfPCell(new Phrase(" ", times));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            infosEleve.AddCell(cell);
            cell = new PdfPCell(new Phrase(" ", times));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            infosEleve.AddCell(cell);

            return infosEleve;
        }

        private PdfPTable creerEnteteProfilAcademique(EleveBE eleve, ClasseBE classe)
        {
            //********************* DEBUT partie mettant la photo de l'élève sur le bulletin

            PdfPTable infosEleve = new PdfPTable(3);
            float[] widths = new float[] { 1f, 2f, 1f };
            infosEleve.SetWidths(widths);
            infosEleve.WidthPercentage = 90f;
            infosEleve.DefaultCell.Border = Rectangle.NO_BORDER;
            infosEleve.HorizontalAlignment = Element.ALIGN_CENTER;

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times = new Font(bfTimes, 12, Font.BOLD);
            PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            cell = new PdfPCell(new Phrase(" ", times));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_BOTTOM;
            cell.Colspan = 3;
            infosEleve.AddCell(cell);
            try
            {
                iTextSharp.text.Image imglogo = iTextSharp.text.Image.GetInstance(new Uri(ConnexionUI.DOSSIER_PHOTO + eleve.photo));
                imglogo.ScalePercent(15f);
                PdfPCell imageCell = new PdfPCell(imglogo);
                imageCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                imageCell.Rowspan = 3; // either 1 if you need to insert one cell
                imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                imageCell.VerticalAlignment = Element.ALIGN_CENTER;
                infosEleve.AddCell(imageCell);
            }
            catch (Exception)
            {
                cell = new PdfPCell(new Phrase(" ",times));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.Rowspan = 3; // either 1 if you need to insert one cell
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                infosEleve.AddCell(cell);
            }

            cell = new PdfPCell(new Phrase("Nom : " + eleve.nom.ToUpper() + " \n Matricule :  " + eleve.matricule.ToUpper() + " \n Née le " + eleve.dateNaissance.ToShortDateString() + " à " + eleve.lieuNaissance, times));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.Rowspan = 3;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.VerticalAlignment = Element.ALIGN_CENTER;
            infosEleve.AddCell(cell);

            if (classe != null)
            {
                cell = new PdfPCell(new Phrase("Classe : " + classe.codeClasse.ToUpper(), times));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.Rowspan = 3;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                infosEleve.AddCell(cell);
            }
            else
            {
                cell = new PdfPCell(new Phrase(" ", times));
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.Rowspan = 3;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                infosEleve.AddCell(cell);
            }

            cell = new PdfPCell(new Phrase(" ", times));
            cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.VerticalAlignment = Element.ALIGN_BOTTOM;
            cell.Colspan = 3;
            infosEleve.AddCell(cell);

            //********************* FIN partie mettant la photo de l'élève sur le bulletin

            return infosEleve;
        }


        private PdfPTable creerPieds()
        {
            ParametresDA parametreDA = new ParametresDA();
            ParametresBE parametre = parametreDA.rechercherDerniereValeur();

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times = new Font(bfTimes, 8);
            PdfPTable pieds = new PdfPTable(2);
            pieds.WidthPercentage = 90f;
            pieds.DefaultCell.Border = Rectangle.NO_BORDER;
            PdfPCell cellule = new iTextSharp.text.pdf.PdfPCell();
            cellule.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cellule.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cellule.Phrase = new iTextSharp.text.Phrase("Le responsable de l'opération", times);
            pieds.AddCell(cellule);
            cellule.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cellule.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cellule.Phrase = new iTextSharp.text.Phrase("Le " + parametre.titreDuChef, times);
            pieds.AddCell(cellule);
            cellule.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cellule.HorizontalAlignment = Element.ALIGN_LEFT;
            cellule.Phrase = new iTextSharp.text.Phrase("The charge of the operation", times);
            pieds.AddCell(cellule);
            cellule.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cellule.HorizontalAlignment = Element.ALIGN_RIGHT;
            cellule.Phrase = new iTextSharp.text.Phrase("The " + parametre.titleOfChief, times);
            pieds.AddCell(cellule);

            return pieds;
        }

        private PdfPTable creerPieds_caisse()
        {
            ParametresDA parametreDA = new ParametresDA();
            ParametresBE parametre = parametreDA.rechercherDerniereValeur();

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times = new Font(bfTimes, 8);
            PdfPTable pieds = new PdfPTable(2);
            pieds.WidthPercentage = 90f;
            pieds.DefaultCell.Border = Rectangle.NO_BORDER;
            PdfPCell cellule = new iTextSharp.text.pdf.PdfPCell();
            cellule.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cellule.HorizontalAlignment = Rectangle.ALIGN_LEFT;
            cellule.Phrase = new iTextSharp.text.Phrase("Le responsable de l'opération", times);
            pieds.AddCell(cellule);
            cellule.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cellule.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cellule.Phrase = new iTextSharp.text.Phrase("Le Fondateur", times);
            pieds.AddCell(cellule);
            cellule.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cellule.HorizontalAlignment = Element.ALIGN_LEFT;
            cellule.Phrase = new iTextSharp.text.Phrase("The charge of the operation", times);
            pieds.AddCell(cellule);
            cellule.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cellule.HorizontalAlignment = Element.ALIGN_RIGHT;
            cellule.Phrase = new iTextSharp.text.Phrase("The Founder", times);
            pieds.AddCell(cellule);

            return pieds;
        }

        private PdfPTable creerPiedsBulletin(String nomProfTitulaire, ParametresBE parametre)
        {
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times = new Font(bfTimes, 7);
            PdfPTable pieds = new PdfPTable(3);
            pieds.WidthPercentage = 90f;
            pieds.DefaultCell.Border = Rectangle.NO_BORDER;

            PdfPCell cellule = new iTextSharp.text.pdf.PdfPCell();
            cellule.Border = iTextSharp.text.Rectangle.NO_BORDER;

            cellule.HorizontalAlignment = Element.ALIGN_LEFT;
            cellule.Phrase = new iTextSharp.text.Phrase("Visa des parents", times);
            pieds.AddCell(cellule);
            cellule.HorizontalAlignment = Element.ALIGN_CENTER;
            cellule.Phrase = new iTextSharp.text.Phrase("L'enseignant Principal", times);
            pieds.AddCell(cellule);
            if (parametre != null)
            {
                cellule.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellule.Phrase = new iTextSharp.text.Phrase("Le " + parametre.titreDuChef, times);
                pieds.AddCell(cellule);
            }
            else
            {
                cellule.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellule.Phrase = new iTextSharp.text.Phrase(" ", times);
                pieds.AddCell(cellule);
            }

            cellule.HorizontalAlignment = Element.ALIGN_RIGHT;
            cellule.Phrase = new iTextSharp.text.Phrase(" ", times);
            cellule.Colspan = 3;
            pieds.AddCell(cellule);
            pieds.AddCell(cellule);
            pieds.AddCell(cellule);

            cellule.Colspan = 1;
            cellule.HorizontalAlignment = Element.ALIGN_LEFT;
            cellule.Phrase = new iTextSharp.text.Phrase(" ", times);
            pieds.AddCell(cellule);
            cellule.HorizontalAlignment = Element.ALIGN_CENTER;
            cellule.Phrase = new iTextSharp.text.Phrase(nomProfTitulaire, times);
            pieds.AddCell(cellule);
            if (parametre != null)
            {
                cellule.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellule.Phrase = new iTextSharp.text.Phrase(parametre.directeur, times);
                pieds.AddCell(cellule);
            }
            else
            {
                cellule.HorizontalAlignment = Element.ALIGN_RIGHT;
                cellule.Phrase = new iTextSharp.text.Phrase(" ", times);
                pieds.AddCell(cellule);
            }

            return pieds;
        }


        public void SendToPrinter(string file)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = @file;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            p.WaitForInputIdle();
            p.WaitForExit();

            System.Threading.Thread.Sleep(3000);
            if (false == p.CloseMainWindow())
                p.Kill();
        }

        /**
         * fonction generique qui retourne un etat en se basant de la datagrid en parametre
        */
        public void obtenirEtat(DataGrid grid)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8);
                Font timestitre = new Font(bfTimes, 9);
                Font timeheader = new Font(bfTimes, 9, Font.BOLD);
                var FontColour_header = new BaseColor(134, 181, 232);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region contenu concernant les grid
                PdfPTable table = new PdfPTable(grid.Columns.Count);

                PdfPCell cell = new PdfPCell();
                table.WidthPercentage = 90f;
                
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    cell.Phrase = new Phrase(" ", timeheader);
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);
                }
                cell = new PdfPCell();
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    cell.Phrase = new Phrase(grid.Columns[j].Header.ToString(), timeheader);
                    cell.BackgroundColor = FontColour_header;
                    table.AddCell(cell);
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt;
                                try
                                {
                                    txt = cell1.Content as TextBlock;
                                    if (txt != null)
                                    {
                                        table.AddCell(new Phrase(txt.Text, times));
                                    }
                                }
                                catch (Exception)
                                {
                                    table.AddCell(new Phrase("", times));
                                }
                            }
                        }
                    }
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(titre);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void obtenirEtatModePaysage(DataGrid grid)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9, Font.BOLD);
                Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region contenu concernant les grid
                PdfPTable table = new PdfPTable(grid.Columns.Count);

                table.WidthPercentage = 95f;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString(), timeheader));
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt;
                                try
                                {
                                    txt = cell1.Content as TextBlock;
                                    if (txt != null)
                                    {
                                        table.AddCell(new Phrase(txt.Text, times));
                                    }
                                }
                                catch (Exception)
                                {
                                    table.AddCell(new Phrase("", times));
                                }
                            }
                        }
                    }
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void obtenirEtatAppreciationMoyenne(DataGrid grid)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region contenu concernant les grid
                PdfPTable table = new PdfPTable(grid.Columns.Count);

                float[] widths = new float[] { 2.5f, 1.3f, 1f, 1f, 1f, 1.3f, 1f, 1f, 1f, 1f, 1f, 3f };
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                table.SetWidths(widths);

                table.WidthPercentage = 95f;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString(), timeheader));
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt;
                                try
                                {
                                    txt = cell1.Content as TextBlock;
                                    if (txt != null)
                                    {
                                        table.AddCell(new Phrase(txt.Text, times));
                                    }
                                }
                                catch (Exception)
                                {
                                    table.AddCell(new Phrase("", times));
                                }
                            }
                        }
                    }
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void obtenirEtatAppreciationResultat(DataGrid grid)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region contenu concernant les grid
                PdfPTable table = new PdfPTable(grid.Columns.Count);

                float[] widths = new float[] { 2.5f, 1.3f, 1f, 1f, 1f, 1.3f, 1f, 1f, 1f, 3f };
                //table.HorizontalAlignment = Element.ALIGN_CENTER;
                table.SetWidths(widths);

                table.WidthPercentage = 95f;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString(), timeheader));
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt;
                                try
                                {
                                    txt = cell1.Content as TextBlock;
                                    if (txt != null)
                                    {
                                        table.AddCell(new Phrase(txt.Text, times));
                                    }
                                }
                                catch (Exception)
                                {
                                    table.AddCell(new Phrase("", times));
                                }
                            }
                        }
                    }
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void etatProgrammeClasse(DataGrid grid, string classe, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur la classe
                PdfPTable infos = new PdfPTable(4);
                float[] widths1 = new float[] { 1.5f, 4f, 1.5f, 4f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 90f;
                PdfPCell cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Classe:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(classe, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Année:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(annee + "/" + (annee + 1), times);
                infos.AddCell(cell);
                #endregion

                #region contenu concernant les grid
                PdfPTable table = new PdfPTable(grid.Columns.Count);
                float[] widths = new float[] { 4f, 2f, 4f };
                table.SetWidths(widths);
                table.WidthPercentage = 90f;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString().ToUpper(), times));
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt;
                                try
                                {
                                    txt = cell1.Content as TextBlock;
                                    if (txt != null)
                                    {
                                        table.AddCell(new Phrase(txt.Text, timeheader));
                                    }
                                }
                                catch (Exception)
                                {
                                    table.AddCell(new Phrase("", times));
                                }
                            }
                        }
                    }
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname)); 
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        /**
          * fonction generique qui retourne un etat en se basant de la datagrid en parametre
         */
        public void etatCaisse(DataGrid grid, string datedeb, string datefin, decimal entree, decimal sortie, decimal soldetotal, int type)
        {
            try
            {
                CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper() + " ENTRE " + datedeb + " ET " + datefin, timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region contenu concernant les operations
                PdfPTable table = new PdfPTable(grid.Columns.Count);
                float[] widths;
                if(type == 0)
                    widths = new float[] { 5f, 2f, 2f, 2f, 5f, 2f };
                else if (type == 1)
                    widths = new float[] { 1f, 5f, 2f, 2f, 4f };
                else
                    widths = new float[] { 1f, 5f, 2f, 2f, 2f, 4f };
                table.SetWidths(widths);
                table.WidthPercentage = 100f;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString().ToUpper(), timeheader));
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt;
                                try
                                {
                                    txt = cell1.Content as TextBlock;
                                    if (txt != null)
                                    {
                                        table.AddCell(new Phrase(txt.Text, times));
                                    }
                                }
                                catch (Exception)
                                {
                                    table.AddCell(new Phrase("", times));
                                }
                            }
                        }
                    }
                }
                #endregion

                Chunk c;
                Paragraph p = new Paragraph();
                c = new Chunk("Total des entrées (En toutes lettres) :" + entree.ToString("0,0", elGR) + " (" + Tools.numberToString(Convert.ToUInt64(entree)) + ")\n", timeheader);
                p.Add(c);
                c = new Chunk("Total des sorties (En toutes lettres) :" + sortie.ToString("0,0", elGR) + " (" + Tools.numberToString(Convert.ToUInt64(sortie)) + ")\n", timeheader);
                p.Add(c);
                if (soldetotal < 0)
                    c = new Chunk("Solde total de la caisse (En toutes lettres):" + soldetotal.ToString("0,0", elGR) + " ( moins " + Tools.numberToString(Convert.ToUInt64(-soldetotal)) + ")\n", timeheader);
                else
                    c = new Chunk("Solde total de la caisse (En toutes lettres):" + soldetotal.ToString("0,0", elGR) + " (" + Tools.numberToString(Convert.ToUInt64(soldetotal)) + ")\n", timeheader);
                p.Add(c);

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(p);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds_caisse());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        /**
          * fonction generique qui retourne un etat en se basant de la datagrid en parametre
         */
        public void etatVersement(DataGrid grid, string datedeb, string datefin, decimal entree)
        {
            try
            {
                CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper() + " ENTRE " + datedeb + " ET " + datefin, timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region contenu concernant les operations
                PdfPTable table = new PdfPTable(grid.Columns.Count);
                float[] widths = new float[] { 5f, 2f, 2f, 4f, 2f };
                table.SetWidths(widths);
                table.WidthPercentage = 100f;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString().ToUpper(), timeheader));
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt;
                                try
                                {
                                    txt = cell1.Content as TextBlock;
                                    if (txt != null)
                                    {
                                        table.AddCell(new Phrase(txt.Text, times));
                                    }
                                }
                                catch (Exception)
                                {
                                    table.AddCell(new Phrase("", times));
                                }
                            }
                        }
                    }
                }
                #endregion

                Chunk c;
                Paragraph p = new Paragraph();
                c = new Chunk("Total des versements (En toutes lettres) :" + entree.ToString("0,0", elGR) + " (" + Tools.numberToString(Convert.ToUInt64(entree)) + ")\n", timeheader);
                p.Add(c);
                
                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(p);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds_caisse());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        /**
         * fonction qui retourne la liste des anonymats d'une classe pour une matiere pendant une sequence
         */
        public void etatAnonymat(DataGrid grid, string classe, string matiere, string sequence, string evaluation, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur la classe
                PdfPTable infos = new PdfPTable(4);
                float[] widths1 = new float[] { 1.5f, 4f, 1.5f, 4f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 90f;
                PdfPCell cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Classe:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(classe, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Matière:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(matiere, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Année:", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(annee.ToString(), times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Sequence:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(sequence, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Evaluation:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(evaluation, times);
                infos.AddCell(cell);
                #endregion

                #region contenu concernant les anonymats
                PdfPTable table = new PdfPTable(grid.Columns.Count);
                float[] widths = new float[] { 1f, 6f, 2f, 2f };
                table.SetWidths(widths);
                table.WidthPercentage = 90f;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString().ToUpper(), times));
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt = cell1.Content as TextBlock;
                                if (txt != null)
                                {
                                    table.AddCell(new Phrase(txt.Text, timeheader));
                                }
                                else
                                    table.AddCell(new Phrase("", times));
                            }
                        }
                    }
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void etatAnonymat(List<LigneSaisieAnonymat> lignes, List<string> headers, string classe, string matiere, string sequence, string evaluation, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur la classe
                PdfPTable infos = new PdfPTable(4);
                float[] widths1 = new float[] { 1.5f, 4f, 1.5f, 4f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 90f;
                PdfPCell cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Classe:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(classe, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Matière:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(matiere, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Année:", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase((annee - 1) + " / " + annee, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Sequence:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(sequence, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Evaluation:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(evaluation, times);
                infos.AddCell(cell);
                #endregion

                #region contenu concernant les anonymats
                PdfPTable table = new PdfPTable(headers.Count);
                float[] widths = new float[] { 1f, 6f, 2f, 2f };
                table.SetWidths(widths);
                table.WidthPercentage = 90f;
                for (int j = 0; j < headers.Count; j++)
                {
                    table.AddCell(new Phrase(headers.ElementAt(j), times));
                }
                table.HeaderRows = 1;
                if (lignes != null)
                {
                    foreach (var item in lignes)
                    {
                        table.AddCell(new Phrase(item.numero.ToString(), timeheader));
                        table.AddCell(new Phrase(item.nom, timeheader));
                        table.AddCell(new Phrase(item.matricule, timeheader));
                        table.AddCell(new Phrase(item.anonymat, timeheader));
                    }
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        /**
         * fonction qui retourne la liste des notes d'une classe pour une matiere pendant une sequence
         * cette fonction est aussi utile pour l'affichage des notes des etudiants
         */
        public void etatNotes(DataGrid grid, string classe, string matiere, string sequence, string evaluation, int annee, int bareme, int type)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur la classe
                PdfPTable infos = new PdfPTable(4);
                float[] widths1 = new float[] { 1f, 1f, 1f, 1f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Classe:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(classe, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Matière:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(matiere, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Année:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(annee.ToString(), times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Sequence:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(sequence, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Evaluation:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(evaluation, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Barème:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(bareme.ToString(), times);
                infos.AddCell(cell);
                #endregion

                #region contenu concernant les matieres
                PdfPTable table = new PdfPTable(grid.Columns.Count);
                float[] widths;
                if (type == 1)
                {
                    widths = new float[] { 1f, 6f, 2f, 2f };
                }
                else
                {
                    widths = new float[] { 2f, 2f, 2f };
                }
                table.SetWidths(widths);
                table.WidthPercentage = 90f;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString().ToUpper(), times));
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt = cell1.Content as TextBlock;
                                if (txt != null)
                                {
                                    table.AddCell(new Phrase(txt.Text, timeheader));
                                }
                            }
                        }
                    }
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void statutFinancier(DataGrid grid, EleveBE eleve, string categorie, double total_a_payer, double totalVerser, double resteAPayer, double totalRemise, List<PrestationBE> prestations)
        {
            try
            {
                CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                Paragraph reste = new Paragraph("Reste à payer : " + " " + resteAPayer.ToString("0,0", elGR), timeheader);
                Paragraph total = new Paragraph("Total à payer : " + " " + total_a_payer.ToString("0,0", elGR), timeheader);
                Paragraph remise = new Paragraph("Bourse / Dispense : " + " " + totalRemise.ToString("0,0", elGR), timeheader);
                Paragraph dejaPaye = new Paragraph("Montant déjà versé : " + " " + totalVerser.ToString("0,0", elGR), timeheader);
                reste.Alignment = Element.ALIGN_LEFT;
                reste.IndentationLeft = 10f;
                total.Alignment = Element.ALIGN_LEFT;
                total.IndentationLeft = 10f;
                remise.Alignment = Element.ALIGN_LEFT;
                remise.IndentationLeft = 10f;
                dejaPaye.Alignment = Element.ALIGN_LEFT;
                dejaPaye.IndentationLeft = 10f;

                #region information sur l'eleve
                PdfPTable infos = new PdfPTable(4);
                float[] widths1 = new float[] { 2.5f, 3f, 1.5f, 3f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 95f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Nom(s) et prénom(s):".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(eleve.nom, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Matricule:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(eleve.matricule, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Catégorie:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(categorie, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Date:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(DateTime.Today.ToShortDateString(), times);
                infos.AddCell(cell);
                #endregion

                #region contenu concernant les prestations
                PdfPTable table = new PdfPTable(grid.Columns.Count);
                table.WidthPercentage = 95f;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString(), times));
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt = cell1.Content as TextBlock;
                                if (txt != null)
                                {
                                    table.AddCell(new Phrase(txt.Text, timeheader));
                                }
                            }
                        }
                    }
                }
                #endregion

                #region legende concernant les prestations
                PdfPTable legende = new PdfPTable(2);
                float[] widths2 = new float[] { 2f, 3f };
                legende.HorizontalAlignment = Element.ALIGN_CENTER;
                legende.SetWidths(widths2);
                legende.WidthPercentage = 50f;
                PdfPCell cell2 = new iTextSharp.text.pdf.PdfPCell();
                cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                foreach (PrestationBE p in prestations)
                {
                    cell2.Phrase = new iTextSharp.text.Phrase(p.codePrestation, timeheader);
                    legende.AddCell(cell2);
                    cell2.Phrase = new iTextSharp.text.Phrase(p.nomPrestation, times);
                    legende.AddCell(cell2);
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(total);
                doc.Add(dejaPaye);
                doc.Add(remise);
                doc.Add(reste);
                doc.Add(Chunk.NEWLINE);
                doc.Add(legende);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds_caisse());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void statutFinancier(DataGrid grid, EleveBE eleve, string categorie, double total_a_payer, double totalVerser, double resteAPayer, List<PrestationBE> prestations)
        {
            try
            {
                CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                Paragraph reste = new Paragraph("Reste à payer : " + " " + resteAPayer.ToString("0,0", elGR), timeheader);
                Paragraph total = new Paragraph("Total à payer : " + " " + total_a_payer.ToString("0,0", elGR), timeheader);
                Paragraph dejaPaye = new Paragraph("Montant déjà versé : " + " " + totalVerser.ToString("0,0", elGR), timeheader);
                reste.Alignment = Element.ALIGN_LEFT;
                reste.IndentationLeft = 10f;
                total.Alignment = Element.ALIGN_LEFT;
                total.IndentationLeft = 10f;
                dejaPaye.Alignment = Element.ALIGN_LEFT;
                dejaPaye.IndentationLeft = 10f;

                #region information sur l'eleve
                PdfPTable infos = new PdfPTable(4);
                float[] widths1 = new float[] { 2.5f, 3f, 1.5f, 3f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 95f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Nom(s) et prénom(s):".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(eleve.nom, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Matricule:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(eleve.matricule, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Catégorie:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(categorie, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Date:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(DateTime.Today.ToShortDateString(), times);
                infos.AddCell(cell);
                #endregion

                #region contenu concernant les prestations
                PdfPTable table = new PdfPTable(grid.Columns.Count);
                table.WidthPercentage = 95f;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString(), times));
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt = cell1.Content as TextBlock;
                                if (txt != null)
                                {
                                    table.AddCell(new Phrase(txt.Text, timeheader));
                                }
                            }
                        }
                    }
                }
                #endregion

                #region legende concernant les prestations
                PdfPTable legende = new PdfPTable(2);
                float[] widths2 = new float[] { 2f, 3f };
                legende.HorizontalAlignment = Element.ALIGN_CENTER;
                legende.SetWidths(widths2);
                legende.WidthPercentage = 50f;
                PdfPCell cell2 = new iTextSharp.text.pdf.PdfPCell();
                cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                foreach (PrestationBE p in prestations)
                {
                    cell2.Phrase = new iTextSharp.text.Phrase(p.codePrestation, timeheader);
                    legende.AddCell(cell2);
                    cell2.Phrase = new iTextSharp.text.Phrase(p.nomPrestation, times);
                    legende.AddCell(cell2);
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(total);
                doc.Add(dejaPaye);
                doc.Add(reste);
                doc.Add(Chunk.NEWLINE);
                doc.Add(legende);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds_caisse());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        /**
         * fonction qui retourne la liste des sanctions d'une classe en fonction de la sequence et de la sanction
         */
        public void etatSanctionClasse(DataGrid grid, string classe, string sequence, string sanction, string variable)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur la sanction
                PdfPTable infos = new PdfPTable(4);
                float[] widths1 = new float[] { 1f, 2f, 1f, 3f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Classe:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(classe, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Sequence:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(sequence, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Sanction:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(sanction, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Unite:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(variable, times);
                infos.AddCell(cell);
                #endregion

                #region contenu concernant les prestations
                PdfPTable table = new PdfPTable(grid.Columns.Count);
                float[] widths = new float[] { 1f, 4f, 1.5f, 1.5f, 2f, 2f };
                table.SetWidths(widths);
                table.WidthPercentage = 90f;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString().ToUpper(), times));
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt = cell1.Content as TextBlock;
                                if (txt != null)
                                {
                                    table.AddCell(new Phrase(txt.Text, timeheader));
                                }
                            }
                        }
                    }
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        /**
         * fonction qui retourne la liste des sanctions d'un eleve
         */
        public void etatSanctionEleve(DataGrid grid, EleveBE eleve, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur l'eleve
                PdfPTable infos = new PdfPTable(4);
                float[] widths1 = new float[] { 3f, 4f, 2f, 2f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Nom(s) et prénom(s):".ToUpper(), times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(eleve.nom, timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Matricule:".ToUpper(), times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(eleve.matricule, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Année:".ToUpper(), times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(annee.ToString(), times);
                infos.AddCell(cell);
                /*cell.Phrase = new iTextSharp.text.Phrase("Classe:", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(classe, times);
                infos.AddCell(cell);*/
                #endregion

                #region contenu concernant les sanction
                PdfPTable table = new PdfPTable(grid.Columns.Count);
                float[] widths = new float[] { 1f, 1f, 1f, 1f, 1f };
                table.SetWidths(widths);
                table.WidthPercentage = 90f;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString().ToUpper(), timeheader));
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt = cell1.Content as TextBlock;
                                if (txt != null)
                                {
                                    table.AddCell(new Phrase(txt.Text, times));
                                }
                            }
                        }
                    }
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void couponInscription(EleveBE eleve, string categorie, string classe, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper() + "\nDate : " + DateTime.Today.ToString("dd-MM-yyyy"), timeheader);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur l'inscription
                PdfPTable infos = new PdfPTable(2);
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;

                cell.Phrase = new iTextSharp.text.Phrase("Nom(s) et prénom(s):", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(eleve.nom, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Matricule:", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(eleve.matricule, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Categorie:", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(categorie, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Classe:", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(classe, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Année", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase((annee - 1) +" / " + annee, times);
                infos.AddCell(cell);
                #endregion

                doc.Add(creerEntete());
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void bordoreauOperation(RealiserBE realiser)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timeheader);
                titre.Alignment = Element.ALIGN_CENTER;

                Paragraph date = new Paragraph("Date : " + realiser.dateop.ToShortDateString(), times);
                date.Alignment = Element.ALIGN_CENTER;

                #region information sur l'operation
                PdfPTable infos = new PdfPTable(2);
                float[] widths = new float[] { 4f, 5f };
                infos.SetWidths(widths);
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Nom(s) et prénom(s): ".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(realiser.concerne, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Montant (en toute lettre): ".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(realiser.montant + " (" + Tools.numberToString(Convert.ToUInt64(realiser.montant)) + ")", times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Motif: ".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(realiser.motif.Split('-')[1], times);
                infos.AddCell(cell);

                PdfPTable signature = new PdfPTable(3);
                signature.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.WidthPercentage = 90f;
                PdfPCell cell1 = new iTextSharp.text.pdf.PdfPCell();
                cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell1.Phrase = new iTextSharp.text.Phrase("BENEFICIAIRE", timeheader);
                cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                signature.AddCell(cell1);
                cell1.Phrase = new iTextSharp.text.Phrase("L'AGENT COMPTABLE", timeheader);
                cell1.HorizontalAlignment = Element.ALIGN_MIDDLE;
                signature.AddCell(cell1);
                cell1.Phrase = new iTextSharp.text.Phrase("LA PRINCIPALE", timeheader);
                cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
                signature.AddCell(cell1);
                #endregion

                doc.Add(creerEntete());
                doc.Add(titre);
                doc.Add(date);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(signature);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerEntete());
                doc.Add(titre);
                doc.Add(date);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(signature);

                doc.Close();
                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }
        public void etatConseilClasse(DataGrid grid, string classe, string jury, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 10, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 10);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur la classe
                PdfPTable infos = new PdfPTable(2);
                float[] widths1 = new float[] { 2f, 9f };
                infos.SetWidths(widths1);
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Classe:", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(classe, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Jury :", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(jury, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Année:", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(annee.ToString(), times);
                infos.AddCell(cell);
                #endregion

                #region contenu concernant les decisions du conseil
                PdfPTable table = new PdfPTable(grid.Columns.Count);
                float[] widths = new float[] { 1f, 6f, 2f, 2f };

                table.SetWidths(widths);
                table.WidthPercentage = 90f;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString(), timeheader));
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt = cell1.Content as TextBlock;
                                if (txt != null)
                                {
                                    table.AddCell(new Phrase(txt.Text, times));
                                }
                            }
                        }
                    }
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void factureAchatArticle(AcheterBE acheter, EleveBE eleve, SetarticleBE setarticle)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A5.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                //writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timeheader);
                titre.Alignment = Element.ALIGN_CENTER;

                Paragraph date = new Paragraph("Date : " + acheter.dateAchat, timeheader);
                date.Alignment = Element.ALIGN_CENTER;

                #region information sur l'operation
                PdfPTable infos = new PdfPTable(2);
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Nom(s) et prénom(s): ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(eleve.nom.ToUpper(), times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Matricule: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(eleve.matricule.ToUpper(), times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Code de l'article: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(setarticle.codesetarticle, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Nom de l'article: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(setarticle.nomsetarticle, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Quantité: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(acheter.quantite.ToString(), times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Prix unitaire: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(acheter.montant.ToString(), times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Total à payer (En toutes lettres): ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase((acheter.montant * acheter.quantite).ToString() + " (" + Tools.numberToString(Convert.ToUInt64(acheter.montant * acheter.quantite)) + ")", times);
                infos.AddCell(cell);

                cell.Border = Rectangle.NO_BORDER;
                cell.Phrase = new iTextSharp.text.Phrase("  ");
                infos.AddCell(cell);
                infos.AddCell(cell);

                PdfPTable signature = new PdfPTable(2);
                signature.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.WidthPercentage = 90f;
                PdfPCell cell1 = new iTextSharp.text.pdf.PdfPCell();
                cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell1.Phrase = new iTextSharp.text.Phrase("L'élève / Parent".ToUpper(), timeheader);
                cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                signature.AddCell(cell1);
                cell1.Phrase = new iTextSharp.text.Phrase("Le(a) Caissier(e) : " + ConnexionUI.utilisateur.nom, timeheader);
                cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
                signature.AddCell(cell1);
                #endregion

                doc.Add(creerEntete());
                doc.Add(titre);
                doc.Add(date);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(signature);
                //doc.Add(Chunk.NEWLINE);
                //doc.Add(creerEntete());
                //doc.Add(titre);
                //doc.Add(date);
                //doc.Add(Chunk.NEWLINE);
                //doc.Add(infos);
                //doc.Add(signature);

                doc.Close();
                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void facturePrestation(PayerBE payer, EleveBE eleve, string classe, string prestation, double remise, string tranche)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A5.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                //writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur l'operation
                PdfPTable infos = new PdfPTable(2);
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Nom(s) et prénom(s): ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(eleve.nom.ToUpper(), times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Matricule - Classe :", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(eleve.matricule.ToUpper() + " - " + classe, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Prestation: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(payer.codePrestation + " - " + prestation, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Tranche: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(payer.codeTranche + " - " + tranche, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Montant (En toutes lettres): ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase((payer.montant + remise).ToString() + " (" + Tools.numberToString(Convert.ToUInt64(payer.montant + remise)) + ")", times);
                infos.AddCell(cell);
                if (remise > 0)
                {
                    cell.Phrase = new iTextSharp.text.Phrase("Remise: ", timeheader);
                    infos.AddCell(cell);
                    cell.Phrase = new iTextSharp.text.Phrase(remise.ToString(), times);
                    infos.AddCell(cell);
                    cell.Phrase = new iTextSharp.text.Phrase("Montant versé: ", timeheader);
                    infos.AddCell(cell);
                    cell.Phrase = new iTextSharp.text.Phrase(payer.montant.ToString(), times);
                    infos.AddCell(cell);
                }
                cell.Phrase = new iTextSharp.text.Phrase("  ", timeheader);
                cell.Border = Rectangle.NO_BORDER;
                infos.AddCell(cell);
                infos.AddCell(cell);

                PdfPTable signature = new PdfPTable(2);
                signature.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.WidthPercentage = 90f;
                PdfPCell cell1 = new iTextSharp.text.pdf.PdfPCell();
                cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell1.Phrase = new iTextSharp.text.Phrase("L'élève / Parent".ToUpper(), timeheader);
                cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                signature.AddCell(cell1);
                cell1.Phrase = new iTextSharp.text.Phrase("Le(a) Caissier(e) : " + ConnexionUI.utilisateur.nom, timeheader);
                cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
                signature.AddCell(cell1);
                #endregion

                doc.Add(creerEntete());
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(signature);
                //doc.Add(Chunk.NEWLINE);
                //doc.Add(creerEntete());
                //doc.Add(titre);
                //doc.Add(Chunk.NEWLINE);
                //doc.Add(infos);
                //doc.Add(signature);

                doc.Close();
                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void facturePrestation(PrestationBE prestation, EleveBE eleve, string classe, double montant, double remise)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A5.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                //writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timeheader);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur l'operation
                PdfPTable infos = new PdfPTable(2);
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Nom(s) et prénom(s): ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(eleve.nom.ToUpper(), times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Matricule  -  Classe: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(eleve.matricule.ToUpper() + " - " + classe, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Code de la prestation: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(prestation.codePrestation, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Nom de la prestation: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(prestation.nomPrestation, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Montant (En toutes lettres): ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(montant.ToString() + " (" + Tools.numberToString(Convert.ToUInt64(montant)) + ")", times);
                infos.AddCell(cell);
                if (remise > 0)
                {
                    cell.Phrase = new iTextSharp.text.Phrase("Remise (En toutes lettres): ", timeheader);
                    infos.AddCell(cell);
                    cell.Phrase = new iTextSharp.text.Phrase(remise.ToString() + " (" + Tools.numberToString(Convert.ToUInt64(remise)) + ")", times);
                    infos.AddCell(cell);
                }

                PdfPTable signature = new PdfPTable(2);
                signature.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.WidthPercentage = 90f;
                PdfPCell cell1 = new iTextSharp.text.pdf.PdfPCell();
                cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell1.Phrase = new iTextSharp.text.Phrase("L'élève / Parent".ToUpper(), timeheader);
                cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                signature.AddCell(cell1);
                cell1.Phrase = new iTextSharp.text.Phrase("Le(a) Caissier(e) : " + ConnexionUI.utilisateur.nom, timeheader);
                cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
                signature.AddCell(cell1);
                #endregion

                doc.Add(creerEntete());
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(signature);
                //doc.Add(Chunk.NEWLINE);
                //doc.Add(creerEntete());
                //doc.Add(titre);
                //doc.Add(Chunk.NEWLINE);
                //doc.Add(infos);
                //doc.Add(Chunk.NEWLINE);
                //doc.Add(signature);

                doc.Close();
                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void factureDepot(double p, double montant, double total, double totalverse, double resteAVerser, EleveBE eleve, string classe)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A5.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                //writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur l'operation
                PdfPTable infos = new PdfPTable(2);
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;

                cell.Phrase = new iTextSharp.text.Phrase("  ", timeheader);
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                infos.AddCell(cell);
                infos.AddCell(cell);
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.Phrase = new iTextSharp.text.Phrase("Nom(s) et prénom(s): ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(eleve.nom.ToUpper(), times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Matricule - Classe : ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(eleve.matricule.ToUpper() + " -  " + classe, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Montant versé: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(p.ToString(), times);
                infos.AddCell(cell);
                
                cell.Phrase = new iTextSharp.text.Phrase("Total à payer (En toutes lettres): ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(total.ToString() + " (" + Tools.numberToString(Convert.ToUInt64(total)) + ")", times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Montant déjà versé (En toutes lettres): ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(totalverse.ToString() + " (" + Tools.numberToString(Convert.ToUInt64(totalverse)) + ")", times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Reste à payer (En toutes lettres): ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(resteAVerser.ToString() + " (" + Tools.numberToString(Convert.ToUInt64(resteAVerser)) + ")", times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Montant à rembourser: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(montant.ToString(), times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("  ", timeheader);
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                infos.AddCell(cell);
                infos.AddCell(cell);

                PdfPTable signature = new PdfPTable(2);
                signature.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.WidthPercentage = 90f;
                PdfPCell cell1 = new iTextSharp.text.pdf.PdfPCell();
                cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell1.Phrase = new iTextSharp.text.Phrase("L'élève / Parent", timeheader);
                cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                signature.AddCell(cell1);
                cell1.Phrase = new iTextSharp.text.Phrase("Le(a) Caissier(e) : " + ConnexionUI.utilisateur.nom , timeheader);
                cell1.HorizontalAlignment = Element.ALIGN_RIGHT;
                signature.AddCell(cell1);
                #endregion

                doc.Add(creerEntete());
                doc.Add(titre);
                doc.Add(infos);
                doc.Add(signature);
                //doc.Add(Chunk.NEWLINE);
                //doc.Add(creerEntete());
                //doc.Add(titre);
                //doc.Add(infos);
                //doc.Add(signature);

                doc.Close();
                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void etatMoyennes(List<LigneEtatMoyenne> lignes, List<string> headers, ClasseBE classe, string annee, SequenceBE sequence, TrimestreBE trimestre, double moyenne)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur la classe
                PdfPTable infos = new PdfPTable(2);
                float[] widths1 = new float[] { 2f, 5f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Classe:", timeheader);
                infos.AddCell(cell);
                if (classe != null)
                    cell.Phrase = new iTextSharp.text.Phrase(classe.codeClasse + " - " + classe.nomClasse, timeheader);
                else
                    cell.Phrase = new iTextSharp.text.Phrase(" Toutes les Classes ", timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Année:", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase((Convert.ToInt32(annee) - 1) + " / " + annee, timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);
                
                if (sequence != null)
                {
                    cell.Phrase = new iTextSharp.text.Phrase("Sequence:", timeheader);
                    infos.AddCell(cell);
                    cell.Phrase = new iTextSharp.text.Phrase(sequence.codeseq + " - " + sequence.nomseq, timeheader);
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    infos.AddCell(cell);

                }

                if (trimestre != null)
                {
                    cell.Phrase = new iTextSharp.text.Phrase("Trimestre:", timeheader);
                    infos.AddCell(cell);
                    cell.Phrase = new iTextSharp.text.Phrase(trimestre.codetrimestre + " - " + trimestre.nomtrimestre, timeheader);
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    infos.AddCell(cell);

                }

                cell.Phrase = new iTextSharp.text.Phrase("Moyenne générale de la classe:", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(Convert.ToString(moyenne), timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                #endregion

                #region contenu concernant les prestations
                PdfPTable table = new PdfPTable(headers.Count);
                float[] widths = new float[] { 1f, 4.5f, 1f, 1f, 1f, 1f, 1f, 3f };
                table.SetWidths(widths);
                table.WidthPercentage = 98f;
                for (int j = 0; j < headers.Count; j++)
                {
                    table.AddCell(new Phrase(headers.ElementAt(j).ToString(), times));
                }
                table.HeaderRows = 1;
                if (lignes != null)
                {
                    foreach (LigneEtatMoyenne l in lignes)
                    {
                        table.AddCell(new Phrase(l.matricule, timeheader));
                        table.AddCell(new Phrase(l.nom, timeheader));
                        table.AddCell(new Phrase(l.note.ToString(), timeheader));
                        table.AddCell(new Phrase(l.coef.ToString(), timeheader));
                        table.AddCell(new Phrase(l.total.ToString(), timeheader));
                        table.AddCell(new Phrase(l.rang.ToString(), timeheader));
                        table.AddCell(new Phrase(l.appreciation, timeheader));
                        table.AddCell(new Phrase(l.appreciationEnseignant, timeheader));
                    }
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void etatListeEleve(List<LigneEleve> lignes, List<string> headers, ClasseBE classe, string annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur la sanction
                PdfPTable infos = new PdfPTable(2);
                float[] widths1 = new float[] { 1f, 6f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Classe:".ToUpper(), timeheader);
                infos.AddCell(cell);
                if (classe != null)
                    cell.Phrase = new iTextSharp.text.Phrase(classe.codeClasse + " - " + classe.nomClasse, timeheader);
                else
                    cell.Phrase = new iTextSharp.text.Phrase(" Toutes les Classes ", timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Année:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(annee, timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);
                #endregion

                #region contenu concernant les prestations
                PdfPTable table = new PdfPTable(headers.Count);
                float[] widths = new float[] { 0.5f, 2f, 3f, 2f, 2f, 2f, 3f };
                table.SetWidths(widths);
                table.WidthPercentage = 98f;
                for (int j = 0; j < headers.Count; j++)
                {
                    table.AddCell(new Phrase(headers.ElementAt(j).ToString().ToUpper(), times));
                }
                table.HeaderRows = 1;
                if (lignes != null)
                {
                    foreach (LigneEleve l in lignes)
                    {
                        table.AddCell(new Phrase(l.numero.ToString(), timeheader));
                        table.AddCell(new Phrase(l.matricule, timeheader));
                        table.AddCell(new Phrase(l.nom.ToString(), timeheader));
                        table.AddCell(new Phrase(l.datenaissance.ToString(), timeheader));
                        table.AddCell(new Phrase(l.telephone.ToString(), timeheader));
                        table.AddCell(new Phrase(l.telparent.ToString(), timeheader));
                        table.AddCell(new Phrase(l.adresse, timeheader));
                    }
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void etatListeEleveDuneClasse(ObservableCollection<EleveBE> ListEleve, ClasseBE classe, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur la sanction
                PdfPTable infos = new PdfPTable(2);
                float[] widths1 = new float[] { 1f, 6f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Classe: ".ToUpper(), timeheader);
                infos.AddCell(cell);
                if (classe != null)
                    cell.Phrase = new iTextSharp.text.Phrase(classe.codeClasse + " - " + classe.nomClasse, timeheader);
                else
                    cell.Phrase = new iTextSharp.text.Phrase(" Toutes les Classes ", timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Année Scolaire: ".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(Convert.ToString(annee - 1) + "/" + Convert.ToString(annee), timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);
                #endregion

                #region contenu concernant les prestations
                PdfPTable table = new PdfPTable(7);
                float[] widths = new float[] { 1.1f, 4.8f, 1.5f, 1.4f, 1f, 1.6f, 2f };
                table.SetWidths(widths);
                table.WidthPercentage = 98f;
                //for (int j = 0; j < 9; j++)
                //{
                //    table.AddCell(new Phrase(" ", times));
                //}

                table.HeaderRows = 1;

                cell = new PdfPCell(new Phrase("No", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Nom", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Matricule", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Catégorie", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Sexe", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Date de Naiss", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                /*cell = new PdfPCell(new Phrase("téléphone", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);*/

                cell = new PdfPCell(new Phrase("tel Parent", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                /*cell = new PdfPCell(new Phrase("Email", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Adresse", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);*/

                if (ListEleve != null && ListEleve.Count != 0)
                {

                    for (int i = 0; i < ListEleve.Count; i++)
                    {
                        cell = new PdfPCell(new Phrase(ListEleve.ElementAt(i).numero.ToString(), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(ListEleve.ElementAt(i).nom.ToString(), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(ListEleve.ElementAt(i).matricule.ToString(), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        if (ListEleve.ElementAt(i).categorie != null)
                        {
                            cell = new PdfPCell(new Phrase(ListEleve.ElementAt(i).categorie.ToString(), timeheader));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);
                        }
                        else
                        {
                            cell = new PdfPCell(new Phrase(" ", timeheader));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);
                        }

                        if (ListEleve.ElementAt(i).sexe != null && ListEleve.ElementAt(i).sexe.Count() != 0)
                        {
                            cell = new PdfPCell(new Phrase(ListEleve.ElementAt(i).sexe.ElementAt(0).ToString().ToUpper(), timeheader));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);
                        }
                        else
                        {
                            cell = new PdfPCell(new Phrase(" ", timeheader));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);
                        }

                        cell = new PdfPCell(new Phrase(ListEleve.ElementAt(i).dateNaissanceString, timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        /*cell = new PdfPCell(new Phrase(ListEleve.ElementAt(i).telephone.ToString(), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);*/

                        cell = new PdfPCell(new Phrase(ListEleve.ElementAt(i).telParent.ToString(), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        /*cell = new PdfPCell(new Phrase(ListEleve.ElementAt(i).email.ToString(), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(ListEleve.ElementAt(i).adresse.ToString(), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);*/
                    }
                }


                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void etatListeEleveDuneClasse_new(ObservableCollection<EleveBE> ListEleve, ClasseBE classe, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 9, Font.BOLD);
                Font timeheader = new Font(bfTimes, 7);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur la sanction
                PdfPTable infos = new PdfPTable(2);
                float[] widths1 = new float[] { 1f, 6f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 80f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Classe: ".ToUpper(), timeheader);
                infos.AddCell(cell);
                if (classe != null)
                    cell.Phrase = new iTextSharp.text.Phrase(classe.codeClasse + " - " + classe.nomClasse, timeheader);
                else
                    cell.Phrase = new iTextSharp.text.Phrase(" Toutes les Classes ", timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Année Scolaire: ".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(Convert.ToString(annee - 1) + "/" + Convert.ToString(annee), timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);
                #endregion

                #region contenu concernant les prestations
                PdfPTable table = new PdfPTable(6);
                float[] widths = new float[] { 0.5f, 5f, 3f, 1f, 5f, 3f };
                table.SetWidths(widths);
                table.WidthPercentage = 95f;
                
                table.HeaderRows = 1;

                cell = new PdfPCell(new Phrase("No", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Nom", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Matricule", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Sexe", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Nom du père", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Téléphone du père", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                if (ListEleve != null && ListEleve.Count != 0)
                {

                    for (int i = 0; i < ListEleve.Count; i++)
                    {
                        cell = new PdfPCell(new Phrase(ListEleve.ElementAt(i).numero.ToString(), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(ListEleve.ElementAt(i).nom.ToString(), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(ListEleve.ElementAt(i).matricule.ToString(), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        if (ListEleve.ElementAt(i).sexe != null && ListEleve.ElementAt(i).sexe.Count() != 0)
                        {
                            cell = new PdfPCell(new Phrase(ListEleve.ElementAt(i).sexe.ElementAt(0).ToString().ToUpper(), timeheader));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);
                        }
                        else
                        {
                            cell = new PdfPCell(new Phrase(" ", timeheader));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);
                        }

                        cell = new PdfPCell(new Phrase(ListEleve.ElementAt(i).nomPere.ToString(), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(ListEleve.ElementAt(i).telParent.ToString(), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        table.AddCell(cell);
                    }
                }


                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void etatListeDesArticles(ObservableCollection<ArticleBE> ListArticle)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;


                #region
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();

                PdfPTable table = new PdfPTable(3);
                float[] widths = new float[] { 2f, 2f, 2f };
                table.SetWidths(widths);
                table.WidthPercentage = 90f;
                //for (int j = 0; j < 9; j++)
                //{
                //    table.AddCell(new Phrase(" ", times));
                //}

                table.HeaderRows = 1;

                cell = new PdfPCell(new Phrase("Code Article", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Catégorie", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Désignation", times));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);


                if (ListArticle != null && ListArticle.Count != 0)
                {

                    for (int i = 0; i < ListArticle.Count; i++)
                    {
                        cell = new PdfPCell(new Phrase(ListArticle.ElementAt(i).codeArticle.ToString(), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(ListArticle.ElementAt(i).codeCatArticle.ToString(), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(ListArticle.ElementAt(i).designation.ToString(), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                    }
                }


                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void recapitulatifNotes(List<LigneRecapitulatif> recapitulatif, ClasseBE classe, string nomprof, List<string> codematieres, List<string> codegroupes, int annee, double moyenne)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                var FontColour_header = new BaseColor(134, 181, 232);
                Document doc = new Document(PageSize.A4.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur la classe et le prof titulaire
                PdfPTable infos = new PdfPTable(4);
                float[] widths1 = new float[] { 2f, 3f, 2f, 3f};
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;

                cell.Phrase = new iTextSharp.text.Phrase("Classe:".ToUpper(), timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(classe.codeClasse + " - " + classe.nomClasse, times);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Année scolaire:".ToUpper(), timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase((annee - 1) + " / " + annee, times);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Professeur titulaire:".ToUpper(), timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(nomprof, times);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Moyenne générale de la classe:", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(Convert.ToString(moyenne), times);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                #endregion

                #region contenu concernant les lignes
                int nbColonne = codegroupes.Count() + codematieres.Count() + 5;
                List<string> headers = new List<string>();
                PdfPTable table = new PdfPTable(nbColonne);
                double[] totaux = new double[codematieres.Count() + 1];
                int[] diviseur = new int[codematieres.Count() + 1];
                int i = 0;
                for (i = 0; i < codematieres.Count() + 1; i++)
                {
                    totaux[i] = 0;
                    diviseur[i] = 0;
                }
                i = 0;

                /*definition des valeurs des entetes*/
                headers.Add("Nom et prénom");
                foreach (string code in codegroupes)
                    headers.Add(code);
                headers.Add("Moy");
                headers.Add("Rang");
                for (i = 0; i < codematieres.Count(); i++)
                    headers.Add(codematieres.ElementAt(i));
                headers.Add("Total");
                headers.Add("Mention");

                /*definition de la largeur des colonnes*/
                float[] widths = new float[headers.Count()];
                widths[0] = 3f;
                for (i = 1; i < nbColonne; i++)
                {
                    widths[i] = 1f;
                }
                table.SetWidths(widths);
                table.WidthPercentage = 100f;
                for (int j = 0; j < headers.Count; j++)
                {
                    table.AddCell(new Phrase(headers.ElementAt(j).ToString().ToUpper(), times));
                }
                table.HeaderRows = 1;

                cell = new PdfPCell();
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                if (recapitulatif != null)
                {
                    foreach (LigneRecapitulatif l in recapitulatif)
                    {
                        i = 0;
                        table.AddCell(new Phrase(l.nom, timeheader));

                        foreach (string code in codegroupes)
                        {
                            cell = new PdfPCell(); 
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            try
                            {
                                cell.Phrase = new Phrase(Math.Round(l.moyennesGroupe[code], 2).ToString(), timeheader);
                                if (l.moyennesGroupe[code] < 10)
                                    cell.BackgroundColor = FontColour_header;
                                table.AddCell(cell);
                            }
                            catch (Exception)
                            {
                                cell.Phrase = new Phrase(" - ", timeheader);
                                cell.BackgroundColor = FontColour_header;
                                table.AddCell(cell);
                            }
                        }

                        cell.Phrase = new iTextSharp.text.Phrase(Math.Round(l.moyenne, 2).ToString(), timeheader);
                        table.AddCell(cell);

                        cell.Phrase = new iTextSharp.text.Phrase(l.rang.ToString(), timeheader);
                        table.AddCell(cell);

                        i = 0;
                        foreach (string code in codematieres)
                        {
                            cell = new PdfPCell();
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            try
                            {
                                cell.Phrase = new Phrase(Math.Round(l.moyennesSequentielles[code], 2).ToString(), timeheader);
                                if (l.moyennesSequentielles[code] < 10)
                                    cell.BackgroundColor = FontColour_header;
                                table.AddCell(cell);
                                diviseur[i] += 1;
                                totaux[i++] += l.moyennesSequentielles[code];
                            }
                            catch (Exception)
                            {
                                cell.Phrase = new Phrase(" - ", timeheader);
                                cell.BackgroundColor = FontColour_header;
                                table.AddCell(cell);
                                i++;
                            }
                        }

                        cell.Phrase = new iTextSharp.text.Phrase(Math.Round(l.total, 2).ToString(), timeheader);
                        table.AddCell(cell);

                        cell.Phrase = new iTextSharp.text.Phrase(Tools.initialeChaine(l.mention.ToString()), timeheader);
                        table.AddCell(cell);

                        totaux[i] += l.total;
                        if (l.total > 0)
                            diviseur[i] += 1;
                    }

                    cell = new PdfPCell();
                    cell.Colspan = codegroupes.Count() + 3;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.Phrase = new iTextSharp.text.Phrase("Moyennes".ToUpper(), timeheader);
                    cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                    table.AddCell(cell);

                    cell = new PdfPCell();
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    i = 0;
                    foreach (double t in totaux)
                    {
                        if (diviseur[i] > 0)
                        {
                            cell.Phrase = new iTextSharp.text.Phrase(Math.Round((t / diviseur[i++]), 2).ToString(), timeheader);
                            table.AddCell(cell);
                        }
                        else
                        {
                            cell.Phrase = new iTextSharp.text.Phrase(" - ", timeheader);
                            table.AddCell(cell);
                            i++;
                        }
                    }

                    cell.Phrase = new iTextSharp.text.Phrase(" - ".ToUpper(), timeheader);
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }
        /**
         * fonction qui génère l'état du journal de l'application
        */
        public void etatJournal(DataGrid grid, string login, string nomUser, string datedeb, string datefin)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfHelvetic = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Font timesUser = new Font(bfHelvetic, 10, Font.BOLD);
                //Document document = new Document(PageSize.A4.rotate()); changer lorientation des pages= paysage ou portrait
                // Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 5);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 10, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title + " entre le " + datedeb + " et le " + datefin, timestitre);
                titre.Alignment = Element.ALIGN_CENTER;
                titre.SpacingAfter = 1f;
                //Phrase user = new Phrase("Utilisateur concerné : ", new Font(bfTimes,12,Font.UNDERLINE)) ; 
                Phrase user = new Phrase();
                user.Add(Chunk.NEWLINE);
                user.Add(new Chunk("Utilisateur concerné :", new Font(bfTimes, 11, Font.UNDERLINE)));
                user.Add(new Chunk(" " + login + " => " + nomUser, new Font(bfHelvetic, 12, Font.BOLD)));
                user.Add(Chunk.NEWLINE);
                user.Add(new Chunk("Générer le : " + DateTime.Today.ToShortDateString() + " à " + DateTime.Now.ToShortTimeString(), new Font(bfTimes, 10, Font.ITALIC)));
                titre.Add(user);
                //Paragraph user = new Paragraph( "Utilisateur concerné : " + login + " -> " + nomUser, timesUser);
                //titre.Alignment = Element.ALIGN_CENTER;

                // Chunk monChunk = new Chunk("Hello World", FontFactory.GetFont(FontFactory.COURRIER, 20, Font.ITALIC, new Color(255, 0, 0))); 

                #region contenu du journal
                PdfPTable table = new PdfPTable(grid.Columns.Count);
                float[] widths = new float[] { 0.95f, 1.85f, 2.5f, 9f, 1.85f, 1.5f };
                table.SetWidths(widths);
                table.WidthPercentage = 90f;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString(), timeheader));
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt;
                                try
                                {
                                    txt = cell1.Content as TextBlock;
                                    if (txt != null)
                                    {
                                        table.AddCell(new Phrase(txt.Text, times));
                                    }
                                }
                                catch (Exception)
                                {
                                    table.AddCell(new Phrase("", times));
                                }
                            }
                        }
                    }
                }
                #endregion

                Chunk c;
                Paragraph p = new Paragraph();
                Paragraph numPage = new Paragraph();
                //c = new Chunk("Total des entrées :"+entree,timeheader);
                //p.Add(c);
                //numPage.Add(new Chunk("Page " + doc.PageNumber.ToString() + "/", new Font(bfTimes, 10, Font.ITALIC)));
                //c = new Chunk("Fin Journal", new Font(bfTimes, 10, Font.ITALIC));
                //p.Add("---------------------------------------------------------");
                //p.Add(c);


                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                // doc.Add(Chunk.NEWLINE);
                //doc.Add(new Chunk("Générer le : ", new Font(bfTimes, 10, Font.UNDERLINE)));
                doc.AddCreationDate();
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(p);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Add(numPage);

                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void etatFicheDePresence(List<LigneFichePresence> lignes, List<string> headers, ClasseBE classe, string annee, string type)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc;
                string varJour = "jour";

                if (type == varJour)
                    doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                else
                    doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);

                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.SpacingBefore = 2;
                titre.Alignment = Element.ALIGN_CENTER;
                titre.SpacingAfter = 1;
                Paragraph periode;
                if (type == varJour)
                    periode = new Paragraph("Journée du ___________________ ", timestitre);
                else
                    periode = new Paragraph("Semaine du ________________ au ________________ ", timestitre);

                periode.Alignment = Element.ALIGN_CENTER;
                Paragraph sequence = new Paragraph("Séquence : _________________", timestitre);
                sequence.Alignment = Element.ALIGN_CENTER;

                #region information sur la sanction
                PdfPTable infos = new PdfPTable(2);
                float[] widths1 = new float[] { 1.5f, 6f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Classe: ", timeheader);
                infos.AddCell(cell);
                if (classe != null)
                    cell.Phrase = new iTextSharp.text.Phrase(classe.codeClasse + " - " + classe.nomClasse, timestitre);
                else
                    cell.Phrase = new iTextSharp.text.Phrase(" Toutes les Classes ", timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Année Scolaire: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase((Convert.ToInt32(annee) - 1) + "/" + annee, timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);
                #endregion

                #region contenu concernant les prestations
                PdfPTable table = new PdfPTable(headers.Count);
                float[] widths;

                if (type == varJour)
                    widths = new float[] { 1f, 6f, 1.85f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };

                else
                    widths = new float[] { 1f, 6f, 1.75f, 1.3f, 1.3f, 1.5f, 1.3f, 1.5f, 1.3f, 1.3f };


                table.SetWidths(widths);
                table.WidthPercentage = 98f;
                for (int j = 0; j < headers.Count; j++)
                {
                    table.AddCell(new Phrase(headers.ElementAt(j).ToString(), times));
                }
                table.HeaderRows = 2;
                if (lignes != null)
                {
                    if (type == varJour)
                        foreach (LigneFichePresence l in lignes)
                        {
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(new Phrase(l.numero.ToString(), timeheader)); cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(new Phrase(l.nom.ToString(), timeheader)); cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(new Phrase(l.matricule, timeheader));
                            table.AddCell(new Phrase("", timeheader));
                            table.AddCell(new Phrase("", timeheader));
                            table.AddCell(new Phrase("", timeheader));
                            table.AddCell(new Phrase("", timeheader));
                            table.AddCell(new Phrase("", timeheader));
                            table.AddCell(new Phrase("", timeheader));
                            table.AddCell(new Phrase("", timeheader));
                            table.AddCell(new Phrase("", timeheader));

                        }
                    else
                        foreach (LigneFichePresence l in lignes)
                        {
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(new Phrase(l.numero.ToString(), timeheader)); cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(new Phrase(l.nom.ToString(), timeheader)); cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(new Phrase(l.matricule, timeheader));
                            table.AddCell(new Phrase("", timeheader));
                            table.AddCell(new Phrase("", timeheader));
                            table.AddCell(new Phrase("", timeheader));
                            table.AddCell(new Phrase("", timeheader));
                            table.AddCell(new Phrase("", timeheader));
                            table.AddCell(new Phrase("", timeheader));
                            table.AddCell(new Phrase("", timeheader));
                        }

                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(periode);
                doc.Add(sequence);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                //  doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }


        //----------------------------etat fiche Discipline--------------------------------------------------

        public void etatFicheDeDiscipline(List<LigneFicheDiscipline> lignes, List<string> headers, ClasseBE classe, string annee, string type)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc;
                string varJour = "jour";

                //if (type == varJour)
                doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                // else
                doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);

                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.SpacingBefore = 2;
                titre.Alignment = Element.ALIGN_CENTER;
                titre.SpacingAfter = 1;
                Paragraph periode;
                if (type == varJour)
                    periode = new Paragraph("Journée du ___________________ ", timestitre);
                else
                    periode = new Paragraph("Semaine du ________________ au ________________ ", timestitre);

                periode.Alignment = Element.ALIGN_CENTER;
                Paragraph sequence = new Paragraph("Séquence : _________________", timestitre);
                sequence.Alignment = Element.ALIGN_CENTER;

                #region information sur la sanction
                PdfPTable infos = new PdfPTable(2);
                float[] widths1 = new float[] { 1.5f, 6f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Classe: ", timeheader);
                infos.AddCell(cell);
                if (classe != null)
                    cell.Phrase = new iTextSharp.text.Phrase(classe.codeClasse + " - " + classe.nomClasse, timestitre);
                else
                    cell.Phrase = new iTextSharp.text.Phrase(" Toutes les Classes ", timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Année Scolaire: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase((Convert.ToInt32(annee) - 1) + "/" + annee, timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);
                #endregion

                #region contenu concernant les prestations
                PdfPTable table = new PdfPTable(headers.Count);
                int dim = headers.Count;
                float[] widths = new float[dim];

                // if (type == varJour)
                // {
                widths[0] = 1f;
                widths[1] = 6f;
                widths[2] = 1.85f;
                for (int i = 3; i < headers.Count; i++)
                    widths[i] = 1.75f;
                // }

                table.SetWidths(widths);
                table.WidthPercentage = 98f;
                for (int j = 0; j < headers.Count; j++)
                {
                    table.AddCell(new Phrase(headers.ElementAt(j).ToString(), times));
                }
                table.HeaderRows = 1;
                if (lignes != null)
                {
                    if (type == varJour)
                        foreach (LigneFicheDiscipline l in lignes)
                        {
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(new Phrase(l.numero.ToString(), timeheader)); cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(new Phrase(l.nom.ToString(), timeheader)); cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(new Phrase(l.matricule, timeheader));
                            for (int i = 0; i < l.discipline.Count; i++)
                                table.AddCell(new Phrase("", timeheader));
                        }
                    else
                        foreach (LigneFicheDiscipline l in lignes)
                        {
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(new Phrase(l.numero.ToString(), timeheader)); cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(new Phrase(l.nom.ToString(), timeheader)); cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(new Phrase(l.matricule, timeheader));
                            for (int i = 0; i < l.discipline.Count + 1; i++)
                                table.AddCell(new Phrase("", timeheader));

                        }

                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(periode);
                doc.Add(sequence);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                //  doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        //----------------------------etat fiche Relevé de notes-----------------------------------------------
        public void etatFicheDeReleveNote(List<LigneFicheReleveNote> lignes, List<string> headers, ClasseBE classe, string annee, string sequence)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc;

                string varJour = "jour";

                //if (type == varJour)
                doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                // else
                doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);

                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                PdfContentByte cb = writer.DirectContent;  // pour le dessin

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.SpacingBefore = 2;
                titre.Alignment = Element.ALIGN_CENTER;
                titre.SpacingAfter = 1;
                Paragraph periode;

                periode = new Paragraph("Journée du ___________________ ", timestitre);
                periode = new Paragraph("Semaine du ________________ au ________________ ", timestitre);

                periode.Alignment = Element.ALIGN_CENTER;
                //Paragraph sequence = new Paragraph("Séquence : _________________", timestitre);
                // sequence.Alignment = Element.ALIGN_CENTER;

                #region information sur la sanction
                PdfPTable infos = new PdfPTable(4);
                float[] widths1 = new float[] { 4f, 6f, 3f, 10f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 95f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;

                cell.Phrase = new iTextSharp.text.Phrase("Classe: ", timeheader);
                infos.AddCell(cell);
                if (classe != null)
                    cell.Phrase = new iTextSharp.text.Phrase(classe.codeClasse + " - " + classe.nomClasse, timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Département : ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("______________________________________\n ", timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Année Scolaire: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase((Convert.ToInt32(annee) - 1) + "/" + annee, timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Discipline : ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("______________________________________\n ", timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Séquence : ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(sequence, timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Coef : ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("__________\n ", timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase(" ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(" ", timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Enseignant : ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("______________________________________\n ", timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);


                #endregion

                #region contenu concernant les prestations
                PdfPTable table = new PdfPTable(headers.Count);

                int dim = headers.Count;
                float[] widths = new float[dim];

                widths[0] = 1f;
                widths[1] = 6f;
                widths[2] = 1.85f;
                for (int i = 3; i < headers.Count; i++)
                    widths[i] = 2f;

                table.SetWidths(widths);
                table.WidthPercentage = 95f;
                for (int j = 0; j < headers.Count; j++)
                {
                    table.AddCell(new Phrase(headers.ElementAt(j).ToString(), times));
                }
                table.HeaderRows = 1;
                if (lignes != null)
                {
                    foreach (LigneFicheReleveNote l in lignes)
                    {
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(new Phrase(l.numero.ToString(), timeheader)); cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(new Phrase(l.nom.ToString().ToUpper(), timeheader)); cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(new Phrase(l.matricule, timeheader));
                        for (int i = 0; i < l.evaluation.Count; i++)
                            table.AddCell(new Phrase("", timeheader));
                    }

                }
                #endregion

                #region cadre Appréciation
                PdfPTable cadre = new PdfPTable(1);
                cadre.WidthPercentage = 95f;
                cell = new PdfPCell(new Phrase("APPRECIATIONS : "));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;

                cell.Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cadre.AddCell(cell);

                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                for (int j = 0; j < 10; j++)
                    cadre.AddCell(cell);
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                cadre.AddCell(cell);
                #endregion

                //cb.Rectangle(20, 600f, 100f, 100f);
                //cb.Stroke();

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                //doc.Add(periode);
                //doc.Add(sequence);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                //doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(cadre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void etatFicheDeReleveNoteSimplifiee(List<LigneFicheReleveNote> lignes, List<string> headers, ClasseBE classe, string annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 9, Font.BOLD);
                Font timeheader = new Font(bfTimes, 9);
                Document doc;

                //if (type == varJour)
                doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                // else
                doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);

                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                PdfContentByte cb = writer.DirectContent;  // pour le dessin

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.SpacingBefore = 2;
                titre.Alignment = Element.ALIGN_CENTER;
                titre.SpacingAfter = 1;
                Paragraph periode;

                periode = new Paragraph("Journée du ___________________ ", timestitre);
                periode = new Paragraph("Semaine du ________________ au ________________ ", timestitre);

                periode.Alignment = Element.ALIGN_CENTER;
                //Paragraph sequence = new Paragraph("Séquence : _________________", timestitre);
                // sequence.Alignment = Element.ALIGN_CENTER;

                #region information sur la sanction
                PdfPTable infos = new PdfPTable(4);
                float[] widths1 = new float[] { 4f, 6f, 3f, 10f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 95f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;

                cell.Phrase = new iTextSharp.text.Phrase("Classe: ", timeheader);
                infos.AddCell(cell);
                if (classe != null)
                    cell.Phrase = new iTextSharp.text.Phrase(classe.codeClasse + " - " + classe.nomClasse, timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Département : ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("______________________________________\n ", timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Année Scolaire: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase((Convert.ToInt32(annee) - 1) + "/" + annee, timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Discipline : ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("______________________________________\n ", timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase(" ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(" ", timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Coef : ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("__________\n ", timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase(" ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(" ", timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Enseignant : ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("______________________________________\n ", timestitre);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);


                #endregion

                #region contenu concernant les prestations
                PdfPTable table = new PdfPTable(headers.Count);

                int dim = headers.Count;
                float[] widths = new float[dim];

                widths[0] = 1f;
                widths[1] = 6.5f;
                widths[2] = 1f;
                for (int i = 3; i < headers.Count; i++)
                    widths[i] = 1f;

                table.SetWidths(widths);
                table.WidthPercentage = 95f;
                for (int j = 0; j < headers.Count; j++)
                {
                    table.AddCell(new Phrase(headers.ElementAt(j).ToString(), times));
                }
                table.HeaderRows = 1;

                //cell = new PdfPCell(new Phrase(codeMatiere));
                //cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                //cell.Colspan = headers.Count;
                //table.AddCell(cell);

                if (lignes != null)
                {
                    cell = new PdfPCell();
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.FixedHeight = 20f;
                    foreach (LigneFicheReleveNote l in lignes)
                    {
                        cell.Phrase = new Phrase(l.numero.ToString(), timeheader);
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        table.AddCell(cell);
                        cell.Phrase = new Phrase(l.nom.ToString().ToUpper(), timeheader);
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        table.AddCell(cell);
                        cell.Phrase = new Phrase(l.matricule, timeheader);
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        for (int i = 3; i < headers.Count; i++)
                            table.AddCell(new Phrase("", timeheader));
                    }

                }
                #endregion



                //cb.Rectangle(20, 600f, 100f, 100f);
                //cb.Stroke();

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                //doc.Add(periode);
                //doc.Add(sequence);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                //doc.Add(Chunk.NEWLINE);
                doc.Add(table);

                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        //************************ DEBUT AJOUT YUYA
        public void EtatFicheInscription(EleveBE eleve, String codeClasse, String categorieEleve)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;



                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE); titre.Alignment = Element.ALIGN_CENTER;
                doc.Add(new iTextSharp.text.Phrase("l'élève " + eleve.nom.ToUpper() + " de matricule " + eleve.matricule + " est autorisé à payer ses frais d'inscription pour la classe " + codeClasse + "", times));
                doc.Add(Chunk.NEWLINE);
                doc.Add(new iTextSharp.text.Phrase("Catégorie de l'élève :  " + categorieEleve.ToUpper() + "", times));
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);

                doc.Add(new iTextSharp.text.Phrase("Date de Délivrance : ".ToUpper() + " " + DateTime.Today.ToShortDateString(), times));
                //Paragraph paragraphe = new Paragraph("Date de Délivrance : ".ToUpper()+ " " +DateTime.Today.ToShortDateString(), timestitre);
                //titre.Alignment = Element.ALIGN_CENTER;

                //doc.Add(paragraphe);
                doc.Add(Chunk.NEWLINE);

                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        //*** Fonction qui génère l'état du profil académique d'in élève
        public void etatProfilAcadémique(List<MoyennesBE> listMoyennes)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region contenu concernant les grid
                PdfPTable table = new PdfPTable(6);
                //la table possède 6 colonnes
                /*
                 * 0 : année
                 * 1 : matricule
                 * 2 : codematière
                 * 3 : codesequence
                 * 4 : moyenne
                 * 5:  mention
                 */
                //float[] widths = new float[] { 1.5f, 8f, 1f, 1f, 1f, 1f, 1f, 1.5f };
                //table.SetWidths(widths);
                table.WidthPercentage = 100f;

                table.AddCell(new Phrase("Année", timeheader));
                table.AddCell(new Phrase("Matricule", timeheader));
                table.AddCell(new Phrase("Code Matière", timeheader));
                table.AddCell(new Phrase("Séquence", timeheader));
                table.AddCell(new Phrase("Moyenne", timeheader));
                table.AddCell(new Phrase("Mention", timeheader));

                /*for (int j = 0; j < table.NumberOfColumns; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString(), timeheader));
                }*/
                table.HeaderRows = 1;

                for (int i = 0; i < listMoyennes.Count; i++)
                {
                    table.AddCell(new Phrase(Convert.ToString(listMoyennes.ElementAt(i).annee), times));
                    table.AddCell(new Phrase(listMoyennes.ElementAt(i).matricule, times));
                    table.AddCell(new Phrase(listMoyennes.ElementAt(i).codeMat, times));
                    table.AddCell(new Phrase(listMoyennes.ElementAt(i).codeSeq, times));
                    table.AddCell(new Phrase(Convert.ToString(listMoyennes.ElementAt(i).moyenne), times));
                    table.AddCell(new Phrase(Convert.ToString(listMoyennes.ElementAt(i).mention), times));

                }

                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        //*** Fonction qui génère l'état du bulletin trimestriel d'un élève
        public void etatBulletinTrimestrielEleve(BulletinTrimestriel bulletinTrimestriel, List<String> ListCodeSequence, int effectifClasse, EnseignantBE profTitulaire, ParametresBE parametre, string photo)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfTimes2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

                Font times = new Font(bfTimes, 8);
                Font timesMarque = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10, Font.BOLD);
                Font timeheader = new Font(bfTimes2, 6);
                Font timeBlock = new Font(bfTimes2, 7);
                var FontColour_header = new BaseColor(134, 181, 232);
                var FontColour_groupe = new BaseColor(200, 221, 226);
                Font TextFont_Header = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontColour_header);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter1();
                doc.Open();

                Phrase phrase = new Phrase();
                phrase.Add(new Chunk(title.ToUpper() + "\n", timestitre));
                phrase.Add(new Chunk("Année scolaire : ".ToUpper() + (bulletinTrimestriel.annee - 1) + "/" + bulletinTrimestriel.annee, timesMarque));
                Paragraph titre = new Paragraph(phrase);
                titre.Alignment = Element.ALIGN_CENTER;


                //photo de l'etudiant
                if (photo != null)
                {
                    try
                    {
                        iTextSharp.text.Image imglogo = iTextSharp.text.Image.GetInstance(new Uri(ConnexionUI.DOSSIER_PHOTO + photo));
                        imglogo.ScaleAbsolute(80f, 70f);
                        imglogo.SetAbsolutePosition(100f, PageSize.A4.Height - 160f);
                        doc.Add(imglogo);
                    }
                    catch (Exception)
                    {
                    }
                }

                #region contenu concernant les grid

                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                int nbColonneStatic = 9;

                PdfPTable table = new PdfPTable(nbColonneStatic + bulletinTrimestriel.nbSequence);
                int dim = nbColonneStatic + bulletinTrimestriel.nbSequence;
                float[] largeur = new float[dim];
                largeur[0] = 4.5f;

                for (int i = 0; i < bulletinTrimestriel.nbSequence; i++)
                    largeur[i + 1] = 0.9f;

                largeur[bulletinTrimestriel.nbSequence + 1] = 0.8f;
                largeur[bulletinTrimestriel.nbSequence + 2] = 0.8f;
                largeur[bulletinTrimestriel.nbSequence + 3] = 0.9f;
                largeur[bulletinTrimestriel.nbSequence + 4] = 0.8f;
                largeur[bulletinTrimestriel.nbSequence + 5] = 0.8f;
                largeur[bulletinTrimestriel.nbSequence + 6] = 0.8f;
                largeur[bulletinTrimestriel.nbSequence + 7] = 0.8f;
                largeur[bulletinTrimestriel.nbSequence + 8] = 3.5f;

                table.SetWidths(largeur);
                table.WidthPercentage = 95f;
                //la table possède 6 colonnes
                /*
                 * 0 : année
                 * 1 : matricule
                 * 2 : codematière
                 * 3 : codesequence
                 * 4 : moyenne
                 * 5:  mention
                 */
                //float[] widths = new float[] { 1.5f, 8f, 1f, 1f, 1f, 1f, 1f, 1.5f };
                //table.SetWidths(widths);
                //table.WidthPercentage = 100f;
                cell = new PdfPCell(new Phrase("Matières", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                for (int j = 0; j < bulletinTrimestriel.nbSequence; j++)
                {
                    cell = new PdfPCell(new Phrase("" + ListCodeSequence.ElementAt(j), timeheader));
                    cell.BackgroundColor = FontColour_header;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);
                }

                cell = new PdfPCell(new Phrase("Moy", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Coef", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Total", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rang", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Cl", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Min", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Max", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Appréciation", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                /*for (int j = 0; j < table.NumberOfColumns; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString(), timeheader));
                }*/
                table.HeaderRows = 1;

                int nbEvalution = bulletinTrimestriel.nbSequence;
                int compteur = bulletinTrimestriel.nbSequence; //servira à chaque fois à indiquer si on a terminé avec une matière
                //NB pour une matière, on a 'bulletinSequentiel.nbEvaluation' ligne dans la liste

                if (bulletinTrimestriel.listLigneBulletinTrimestriel != null && bulletinTrimestriel.listLigneBulletinTrimestriel.Count != 0)
                {

                    String indiceCodeGroupe = bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(0).codeGroupe;

                    int i = 0;
                    while (i < bulletinTrimestriel.listLigneBulletinTrimestriel.Count)
                    {
                        LigneBulletinTrimestriel ligneBulletin = bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i);

                        //le nom de la matière et de l'enseignant
                        //le nom de la matière et de l'enseignant
                        phrase = new Phrase();
                        phrase.Add(new Chunk(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).nomMat + "\n", timesMarque));
                        phrase.Add(new Chunk(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).nomProf, times));

                        cell = new PdfPCell(phrase);
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        table.AddCell(cell);

                        int nb = 0; //compte le nombre de fois qu'on a trouvé une ligne avec un code de trimestre identique
                        int nb1 = 0; //compte le nombre de fois que l'opération i+j est sortie des limites de la liste des ligne de Bulletin Annuel
                        int k = 0; //k permet de déterminer si on doit avancer dans la liste 'listLigneBulletinTrimestriel'
                        //k = 0 (on avance); k = 1 (on reste sur la même position)
                        //on avance seulement si on a trouvé la note correspondante à la séquence que l'on cherchait
                        //on rempli les notes de tous les trimestres
                        for (int j = 0; j < compteur; j++)
                        {
                            if (i + j < bulletinTrimestriel.listLigneBulletinTrimestriel.Count)
                            {
                                if (j == 0)
                                {
                                    if (bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j).codeSeq == ListCodeSequence.ElementAt(j))
                                    {
                                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j).moyenneSeq, 2)), times));
                                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                        table.AddCell(cell);
                                        k = 0;
                                    }
                                    else
                                    {
                                        cell = new PdfPCell(new Phrase(Convert.ToString(""), times));
                                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                        table.AddCell(cell);
                                        k = 1;

                                    }

                                }
                                else
                                {
                                    if (k == 0)
                                    {
                                        if ((bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j - k).codeSeq != bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).codeSeq)
                                        && (bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j - k).codeMat == bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).codeMat))
                                        {
                                            cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j - k).moyenneSeq, 2)), times));
                                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                            table.AddCell(cell);
                                            k = 0;
                                        }
                                        else
                                        {
                                            cell = new PdfPCell(new Phrase(Convert.ToString(" "), times));
                                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                            table.AddCell(cell);
                                            k = 1;
                                            nb++;
                                        }
                                    }
                                    else
                                    {
                                        if (bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j - k).codeSeq == ListCodeSequence.ElementAt(j))
                                        {
                                            cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j - k).moyenneSeq, 2)), times));
                                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                            table.AddCell(cell);
                                            k = 0;
                                            nb++;
                                        }
                                        else
                                        {
                                            cell = new PdfPCell(new Phrase(Convert.ToString(" "), times));
                                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                            table.AddCell(cell);
                                            k = 1;
                                            nb++;
                                        }
                                    }


                                }

                            }
                            else
                            {
                                if ((k == 1) && (i + j - k < bulletinTrimestriel.listLigneBulletinTrimestriel.Count)
                                    && (bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j - k).codeSeq == ListCodeSequence.ElementAt(j)))
                                {
                                    cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j - k).moyenneSeq, 2)), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);
                                    k = 0;
                                }
                                else
                                {
                                    cell = new PdfPCell(new Phrase(Convert.ToString(""), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);
                                    k = 1;

                                }

                                /*cell = new PdfPCell(new Phrase(Convert.ToString(" "), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);*/

                                nb1++;
                            }

                        }

                        //la moyenne annuelles de l'élève sur la matière
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).moyenneTrim, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //le coeficient de la matière
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).coef), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne annuelle coeficié de l'élève pour la matière
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).moyenneTrim * bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).coef, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //le rang de l'élève
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).rangTrim), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne de la classe
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).moyenneClasseTrim), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne minimale
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).moyenneMinClasseTrim, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne maximale
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).moyenneMaxClasseTrim, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //l'appréciation (ou encore la mention)
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).mention + " \n  - " + bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).appreciation), timesMarque));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        i = (i + compteur) - nb - nb1;

                        //on vérifi si on n'est pas passé dans un autre groupe de matière
                        if (i < bulletinTrimestriel.listLigneBulletinTrimestriel.Count)
                        {
                            if (bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).codeGroupe != bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).codeGroupe)
                            {
                                //on insère la ligne contenant la moyenne du groupe
                                //le nom du groupe
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).nomGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                table.AddCell(cell);

                                //le total des points du groupe ainsi que le total max des points possibles
                                cell = new PdfPCell(new Phrase("Points : " + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalPointGroupe, 2) + "/" + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalPointMaxGroupe, 2), timeheader));
                                cell.Colspan = nbEvalution + 1;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le total des coeficients
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalCoefGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //la moyenne du groupe de l'élève
                                cell = new PdfPCell(new Phrase("Moyenne : " + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).moyenneGroupe, 2), timeheader));
                                cell.Colspan = 6;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                        }
                        else
                        {
                            if (i - 1 < bulletinTrimestriel.listLigneBulletinTrimestriel.Count)
                            {
                                //on insère la ligne contenant la moyenne du groupe
                                //le nom du groupe
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).nomGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                table.AddCell(cell);
                                //le total des points du groupe ainsi que le total max des points possibles
                                cell = new PdfPCell(new Phrase("Points : " + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalPointGroupe, 2) + "/" + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalPointMaxGroupe, 2), timeheader));
                                cell.Colspan = nbEvalution + 1;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le total des coeficients
                                cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalCoefGroupe, 2)), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //la moyenne du groupe de l'élève
                                cell = new PdfPCell(new Phrase("Moyenne : " + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).moyenneGroupe, 2), timeheader));
                                cell.Colspan = 6;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                            else
                            {
                                int index = bulletinTrimestriel.listLigneBulletinTrimestriel.Count - 1;
                                //on insère la ligne contenant la moyenne du groupe
                                //le nom du groupe
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).nomGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                table.AddCell(cell);
                                //le total des points du groupe ainsi que le total max des points possibles
                                cell = new PdfPCell(new Phrase("Points : " + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalPointGroupe, 2) + "/" + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalPointMaxGroupe, 2), timeheader));
                                cell.Colspan = nbEvalution + 1;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le total des coeficients
                                cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalCoefGroupe, 2)), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //la moyenne du groupe de l'élève
                                cell = new PdfPCell(new Phrase("Moyenne : " + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).moyenneGroupe, 2), timeheader));
                                cell.Colspan = 6;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                        }
                    }
                }

                #endregion

                //**********************Debut de la partie bilan travail sequentiel
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                cell.Colspan = nbEvalution + 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("TRAVAIL", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Points", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Coef", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);


                cell = new PdfPCell(new Phrase("Moyenne", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rang", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Cl", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Min", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Max", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Appréciation", timeBlock));
                cell.Colspan = nbEvalution + 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                if (bulletinTrimestriel.listLigneBulletinTrimestriel != null)
                {
                    if (bulletinTrimestriel.resultattrimestriel != null)
                    {
                        cell = new PdfPCell(new Phrase(" "));
                        cell.Border = Rectangle.NO_BORDER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.resultattrimestriel.point), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.resultattrimestriel.coef), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.resultattrimestriel.moyenne, 2)), timesMarque));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.resultattrimestriel.rang), timesMarque));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.resultattrimestriel.moyenneclasse, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.moyenneMin, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.moyenneMax, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.resultattrimestriel.mention + " \n  - " + bulletinTrimestriel.resultattrimestriel.appreciation), timesMarque));
                        cell.Colspan = nbEvalution + 1;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }

                //***********************Fin de la partie bilan sequentiel

                //******************************** Debut de la partie discipline

                //on liste les codes des sanctions
                DisciplineDA disciplineDA = new DisciplineDA();
                List<DisciplineBE> LDiscipline = disciplineDA.listerTous();
                int nbDiscipline = LDiscipline.Count;

                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                cell.Colspan = nbEvalution + 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("DISCIPLINE", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                int nbColonne = 1; //compte le nbre de colonne crée ds le tableau pour la partie Discipline

                for (int i = 0; i < nbDiscipline; i++)
                {
                    if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                    {
                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + ".inj(" + LDiscipline.ElementAt(i).unite + ")", timeBlock));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        nbColonne++;

                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + ".j(" + LDiscipline.ElementAt(i).unite + ")", timeBlock));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        nbColonne++;
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + "(" + LDiscipline.ElementAt(i).unite + ")", timeBlock));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        nbColonne++;
                    }
                }

                /*cell = new PdfPCell(new Phrase("Abs.inj(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Abs.j(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Retards(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Avert.", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Blâmes", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Exclu.(J)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);*/

                cell = new PdfPCell(new Phrase("Mention Conseil de classe", timeBlock));
                //cell.Colspan = nbEvalution + 2;
                //cell.Colspan = 2;
                if (nbColonneStatic + nbEvalution - nbColonne > 0) //pour fusionner le reste de colonne libres
                    cell.Colspan = nbColonneStatic + nbEvalution - nbColonne;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);




                cell = new PdfPCell(new Phrase(" ", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell();

                if (bulletinTrimestriel.ListSanction != null)
                {
                    int i = 0;
                    while (i < nbDiscipline)
                    {
                        //représente le code de sanction avec 'inj' (non justifié)
                        //String sanctionCourante = LDiscipline.ElementAt(i).codeSanction + ".inj(" + LDiscipline.ElementAt(i).unite + ")";

                        //représente le code de sanction avec 'j' (jusrifié)
                        //String sanctionCouranteSuivante = LDiscipline.ElementAt(i).codeSanction + ".j(" + LDiscipline.ElementAt(i).unite + ")";

                        if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                        {
                            //on cherche pour le code de sanction en cours, le nombre de sanctions non justifiées
                            bool trouve = false;
                            int nbSanctionJustifiee = 0;
                            int nbSanctionNonJustifiee = 0;
                            for (int j = 0; j < bulletinTrimestriel.ListSanction.Count; j++)
                            {
                                if (bulletinTrimestriel.ListSanction.ElementAt(j).codesanction == LDiscipline.ElementAt(i).codeSanction)
                                {
                                    trouve = true;
                                    if (bulletinTrimestriel.ListSanction.ElementAt(j).etat.Equals("NON JUSTIFIEE"))
                                    {
                                        nbSanctionNonJustifiee = nbSanctionNonJustifiee + bulletinTrimestriel.ListSanction.ElementAt(j).quantité;
                                    }
                                    else
                                        nbSanctionJustifiee = nbSanctionJustifiee + bulletinTrimestriel.ListSanction.ElementAt(j).quantité;
                                }
                            }

                            //si on a trouver parmis les sanctions de l'élève une correspondant au code de sanction en cours
                            if (trouve == true)
                            {
                                //le nombre de sanction non justifiées
                                cell = new PdfPCell(new Phrase(Convert.ToString(nbSanctionNonJustifiee - nbSanctionJustifiee), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);


                                //le nombre de sanction justifiées
                                cell = new PdfPCell(new Phrase(Convert.ToString(nbSanctionJustifiee), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                            else
                            {
                                //le nombre de sanction non justifiées (on le met à 0)
                                cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le nombre de sanction justifiées (on le met à 0)
                                cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }

                            i = i + 1;
                        }
                        else
                        {
                            //on cherche pour le code de sanction en cours, le nombre de sanctions non justifiées
                            bool trouve = false;
                            int nbSanction = 0;
                            // int nbSanctionNonJustifiee = 0;
                            for (int j = 0; j < bulletinTrimestriel.ListSanction.Count; j++)
                            {
                                if (bulletinTrimestriel.ListSanction.ElementAt(j).codesanction == LDiscipline.ElementAt(i).codeSanction)
                                {
                                    trouve = true;
                                    nbSanction = nbSanction + bulletinTrimestriel.ListSanction.ElementAt(j).quantité;
                                }
                            }

                            //si on a trouver parmis les sanctions de l'élève une correspondant au code de sanction en cours
                            if (trouve == true)
                            {
                                //le nombre de sanction non justifiées
                                cell = new PdfPCell(new Phrase(Convert.ToString(nbSanction), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                            }
                            else
                            {
                                //le nombre de sanction non justifiées (on le met à 0)
                                cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                            }

                            i = i + 1;
                        }

                    }
                }

                //on écrit la mention du conseil de classe
                if (bulletinTrimestriel.resultattrimestriel != null)
                {
                    cell = new PdfPCell(new Phrase(bulletinTrimestriel.resultattrimestriel.decision + ". \n     " + bulletinTrimestriel.resultattrimestriel.remarque, timeBlock));
                    //cell.Colspan = nbEvalution + 2;
                    //cell.Colspan =  2;
                    if (nbColonneStatic + nbEvalution - nbColonne > 0) //pour fusionner le nombre de colonne libre
                        cell.Colspan = nbColonneStatic + nbEvalution - nbColonne;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);
                }

                //Fin de la partie discipline

                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                cell.Colspan = nbEvalution + 1;
                table.AddCell(cell);


                doc.Add(creerEnteteReduit());
                doc.Add(titre);
                //doc.Add(Chunk.NEWLINE);
                doc.Add(creerEnteteBulletin(bulletinTrimestriel.eleve, bulletinTrimestriel.classe, bulletinTrimestriel.annee, effectifClasse));
                doc.Add(table);
                //doc.Add(Chunk.NEWLINE);
                //doc.Add(table2);
                //doc.Add(Chunk.NEWLINE);
                if (profTitulaire != null)
                    doc.Add(creerPiedsBulletin(profTitulaire.nomProf, parametre));
                else
                    doc.Add(creerPiedsBulletin("", parametre));

                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void etatBulletinTrimestrielEleve(Document doc, PdfWriter writer, BulletinTrimestriel bulletinTrimestriel, List<String> ListCodeSequence, int effectifClasse,
                                EnseignantBE profTitulaire, ParametresBE parametre, string photo, Dictionary<List<string>, List<double>> moyennesSequentielles)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfTimes2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

                Font times = new Font(bfTimes, 8);
                Font timesMarque = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10, Font.BOLD);
                Font timeheader = new Font(bfTimes2, 8);
                Font timeBlock = new Font(bfTimes2, 8);
                var FontColour_header = new BaseColor(134, 181, 232);
                var FontColour_groupe = new BaseColor(200, 221, 226);
                Font TextFont_Header = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontColour_header);

                Tools.createchart(moyennesSequentielles);

                try
                {
                    PdfContentByte canvas = writer.DirectContentUnder;
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(ConnexionUI.DOSSIER_IMAGES + parametre.logo);
                    image.SetAbsolutePosition((PageSize.A4.Width - image.ScaledWidth) / 2, (PageSize.A4.Height - image.ScaledHeight) / 2);
                    image.ScaleToFit(200, 200);
                    canvas.SaveState();
                    PdfGState state = new PdfGState();
                    state.FillOpacity = 0.2f;
                    canvas.SetGState(state);
                    canvas.AddImage(image);
                    canvas.RestoreState();
                }
                catch (Exception)
                {
                }
                iTextSharp.text.Image imagearrow = iTextSharp.text.Image.GetInstance(ConnexionUI.DOSSIER_IMAGES + "arrow.png");
                imagearrow.ScaleToFit(10, 10);

                if (photo != null)
                {
                    try
                    {
                        iTextSharp.text.Image imglogo = iTextSharp.text.Image.GetInstance(new Uri(ConnexionUI.DOSSIER_PHOTO + photo));
                        imglogo.ScaleAbsolute(80f, 70f);
                        imglogo.SetAbsolutePosition(100f, PageSize.A4.Height - 160f);
                        doc.Add(imglogo);
                    }
                    catch (Exception)
                    {
                    }
                }

                Phrase phrase = new Phrase();
                phrase.Add(new Chunk(title.ToUpper() + "\n", timestitre));
                phrase.Add(new Chunk("Année scolaire : ".ToUpper() + (bulletinTrimestriel.annee - 1) + "/" + bulletinTrimestriel.annee, timesMarque));
                Paragraph titre = new Paragraph(phrase);
                titre.Alignment = Element.ALIGN_CENTER;

                #region contenu concernant les grid

                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                int nbColonneStatic = 10;

                PdfPTable table = new PdfPTable(nbColonneStatic + bulletinTrimestriel.nbSequence);
                int dim = nbColonneStatic + bulletinTrimestriel.nbSequence;
                float[] largeur = new float[dim];
                int j = 0;
                largeur[j++] = 0.3f;
                largeur[j++] = 4.5f;
                for (int i = 1; i <= bulletinTrimestriel.nbSequence; i++)
                    largeur[j++] = 0.9f;
                while (j < dim - 1)
                    largeur[j++] = 0.8f;
                largeur[dim - 1] = 3.5f;
                table.SetWidths(largeur);
                table.WidthPercentage = 95f;

                cell = new PdfPCell(new Phrase("Matières", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.Colspan = 2;
                cell.FixedHeight = 20f;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                for (j = 0; j < bulletinTrimestriel.nbSequence; j++)
                {
                    cell = new PdfPCell(new Phrase("" + ListCodeSequence.ElementAt(j), timeheader));
                    cell.BackgroundColor = FontColour_header;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);
                }

                cell = new PdfPCell(new Phrase("Moy", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Coef", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Total", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rang", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Cl", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Min", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Max", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Appréciation", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                /*for (int j = 0; j < table.NumberOfColumns; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString(), timeheader));
                }*/
                table.HeaderRows = 1;

                int nbEvalution = bulletinTrimestriel.nbSequence;
                int compteur = bulletinTrimestriel.nbSequence; //servira à chaque fois à indiquer si on a terminé avec une matière
                //NB pour une matière, on a 'bulletinSequentiel.nbEvaluation' ligne dans la liste

                if (bulletinTrimestriel.listLigneBulletinTrimestriel != null && bulletinTrimestriel.listLigneBulletinTrimestriel.Count != 0)
                {

                    String indiceCodeGroupe = bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(0).codeGroupe;

                    int i = 0;
                    while (i < bulletinTrimestriel.listLigneBulletinTrimestriel.Count)
                    {
                        LigneBulletinTrimestriel ligneBulletin = bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i);

                        //petit logo
                        cell = new PdfPCell(imagearrow);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.FixedHeight = 20f;
                        cell.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                        table.AddCell(cell);

                        //le nom de la matière et de l'enseignant
                        //le nom de la matière et de l'enseignant
                        phrase = new Phrase();
                        phrase.Add(new Chunk(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).nomMat + "\n", timesMarque));
                        phrase.Add(new Chunk(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).nomProf, times));

                        cell = new PdfPCell(phrase);
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
                        table.AddCell(cell);

                        int nb = 0; //compte le nombre de fois qu'on a trouvé une ligne avec un code de trimestre identique
                        int nb1 = 0; //compte le nombre de fois que l'opération i+j est sortie des limites de la liste des ligne de Bulletin Annuel
                        int k = 0; //k permet de déterminer si on doit avancer dans la liste 'listLigneBulletinTrimestriel'
                        //k = 0 (on avance); k = 1 (on reste sur la même position)
                        //on avance seulement si on a trouvé la note correspondante à la séquence que l'on cherchait
                        //on rempli les notes de tous les trimestres
                        for (j = 0; j < compteur; j++)
                        {
                            if (i + j < bulletinTrimestriel.listLigneBulletinTrimestriel.Count)
                            {
                                if (j == 0)
                                {
                                    if (bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j).codeSeq == ListCodeSequence.ElementAt(j))
                                    {
                                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j).moyenneSeq, 2)), times));
                                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                        table.AddCell(cell);
                                        k = 0;
                                    }
                                    else
                                    {
                                        cell = new PdfPCell(new Phrase(Convert.ToString(""), times));
                                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                        table.AddCell(cell);
                                        k = 1;

                                    }

                                }
                                else
                                {
                                    if (k == 0)
                                    {
                                        if ((bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j - k).codeSeq != bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).codeSeq)
                                        && (bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j - k).codeMat == bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).codeMat))
                                        {
                                            cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j - k).moyenneSeq, 2)), times));
                                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                            table.AddCell(cell);
                                            k = 0;
                                        }
                                        else
                                        {
                                            cell = new PdfPCell(new Phrase(Convert.ToString(" "), times));
                                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                            table.AddCell(cell);
                                            k = 1;
                                            nb++;
                                        }
                                    }
                                    else
                                    {
                                        if (bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j - k).codeSeq == ListCodeSequence.ElementAt(j))
                                        {
                                            cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j - k).moyenneSeq, 2)), times));
                                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                            table.AddCell(cell);
                                            k = 0;
                                            nb++;
                                        }
                                        else
                                        {
                                            cell = new PdfPCell(new Phrase(Convert.ToString(" "), times));
                                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                            table.AddCell(cell);
                                            k = 1;
                                            nb++;
                                        }
                                    }


                                }

                            }
                            else
                            {
                                if ((k == 1) && (i + j - k < bulletinTrimestriel.listLigneBulletinTrimestriel.Count)
                                    && (bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j - k).codeSeq == ListCodeSequence.ElementAt(j)))
                                {
                                    cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i + j - k).moyenneSeq, 2)), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);
                                    k = 0;
                                }
                                else
                                {
                                    cell = new PdfPCell(new Phrase(Convert.ToString(""), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);
                                    k = 1;

                                }

                                /*cell = new PdfPCell(new Phrase(Convert.ToString(" "), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);*/

                                nb1++;
                            }

                        }

                        //la moyenne annuelles de l'élève sur la matière
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).moyenneTrim, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //le coeficient de la matière
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).coef), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne annuelle coeficié de l'élève pour la matière
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).moyenneTrim * bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).coef, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //le rang de l'élève
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).rangTrim), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne de la classe
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).moyenneClasseTrim), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne minimale
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).moyenneMinClasseTrim, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne maximale
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).moyenneMaxClasseTrim, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //l'appréciation (ou encore la mention)
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).mention + " \n  - " + bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).appreciation), timesMarque));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        i = (i + compteur) - nb - nb1;

                        //on vérifi si on n'est pas passé dans un autre groupe de matière
                        if (i < bulletinTrimestriel.listLigneBulletinTrimestriel.Count)
                        {
                            if (bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i).codeGroupe != bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).codeGroupe)
                            {
                                //on insère la ligne contenant la moyenne du groupe
                                //le nom du groupe
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).nomGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                cell.Colspan = 2;
                                table.AddCell(cell);

                                //le total des points du groupe ainsi que le total max des points possibles
                                cell = new PdfPCell(new Phrase("Points : " + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalPointGroupe, 2) + "/" + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalPointMaxGroupe, 2), timeheader));
                                cell.Colspan = nbEvalution + 1;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le total des coeficients
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalCoefGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                cell.Colspan = 3;
                                table.AddCell(cell);

                                //la moyenne du groupe de l'élève
                                cell = new PdfPCell(new Phrase("Moyenne : " + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).moyenneGroupe, 2), timeheader));
                                cell.Colspan = 6;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                        }
                        else
                        {
                            if (i - 1 < bulletinTrimestriel.listLigneBulletinTrimestriel.Count)
                            {
                                //on insère la ligne contenant la moyenne du groupe
                                //le nom du groupe
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).nomGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                cell.Colspan = 2;
                                table.AddCell(cell);

                                //le total des points du groupe ainsi que le total max des points possibles
                                cell = new PdfPCell(new Phrase("Points : " + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalPointGroupe, 2) + "/" + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalPointMaxGroupe, 2), timeheader));
                                cell.Colspan = nbEvalution + 1;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le total des coeficients
                                cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalCoefGroupe, 2)), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                cell.Colspan = 3;
                                table.AddCell(cell);

                                //la moyenne du groupe de l'élève
                                cell = new PdfPCell(new Phrase("Moyenne : " + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).moyenneGroupe, 2), timeheader));
                                cell.Colspan = 6;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                            else
                            {
                                int index = bulletinTrimestriel.listLigneBulletinTrimestriel.Count - 1;
                                //on insère la ligne contenant la moyenne du groupe
                                //le nom du groupe
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).nomGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                cell.Colspan = 2;
                                table.AddCell(cell);

                                //le total des points du groupe ainsi que le total max des points possibles
                                cell = new PdfPCell(new Phrase("Points : " + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalPointGroupe, 2) + "/" + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalPointMaxGroupe, 2), timeheader));
                                cell.Colspan = nbEvalution + 1;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                cell.Colspan = 3;
                                table.AddCell(cell);

                                //le total des coeficients
                                cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).totalCoefGroupe, 2)), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //la moyenne du groupe de l'élève
                                cell = new PdfPCell(new Phrase("Moyenne : " + Math.Round(bulletinTrimestriel.listLigneBulletinTrimestriel.ElementAt(i - 1).moyenneGroupe, 2), timeheader));
                                cell.Colspan = 6;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                        }
                    }
                }

                #endregion

                //**********************Debut de la partie bilan travail sequentiel
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = dim;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("TRAVAIL", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Points", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Coef", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);


                cell = new PdfPCell(new Phrase("Moyenne", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rang", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Cl", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Min", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Max", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Appréciation", timeBlock));
                cell.Colspan = nbEvalution + 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                if (bulletinTrimestriel.listLigneBulletinTrimestriel != null)
                {
                    if (bulletinTrimestriel.resultattrimestriel != null)
                    {
                        cell = new PdfPCell(new Phrase(" "));
                        cell.Border = Rectangle.NO_BORDER;
                        cell.Colspan = 2;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.resultattrimestriel.point), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.resultattrimestriel.coef), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.resultattrimestriel.moyenne, 2)), timesMarque));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.resultattrimestriel.rang), timesMarque));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.resultattrimestriel.moyenneclasse, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.moyenneMin, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinTrimestriel.moyenneMax, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinTrimestriel.resultattrimestriel.mention + " \n  - " + bulletinTrimestriel.resultattrimestriel.appreciation), timesMarque));
                        cell.Colspan = nbEvalution + 1;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }

                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = dim;
                table.AddCell(cell);
                table.AddCell(cell);

                //***********************Fin de la partie bilan sequentiel

                //******************************** Debut de la partie discipline
                
                DisciplineDA disciplineDA = new DisciplineDA();
                List<DisciplineBE> LDiscipline = disciplineDA.listerTous();
                int nbDiscipline = LDiscipline.Count;

                PdfPTable table2 = new PdfPTable(nbDiscipline + 4);
                float[] largeur2 = new float[nbDiscipline + 4];
                largeur2[0] = 3f;
                for (int i = 1; i < nbDiscipline + 3; i++)
                    largeur2[i] = 1f;
                largeur2[nbDiscipline + 3] = 3f;
                table2.SetWidths(largeur2);
                
                cell = new PdfPCell(new Phrase("DISCIPLINE", timeBlock));
                cell.FixedHeight = 20f;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table2.AddCell(cell);

                int nbColonne = 1; //compte le nbre de colonne crée ds le tableau pour la partie Discipline

                for (int i = 0; i < nbDiscipline; i++)
                {
                    if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                    {
                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + ".inj(" + LDiscipline.ElementAt(i).unite + ")", timeBlock));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table2.AddCell(cell);

                        nbColonne++;

                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + ".j(" + LDiscipline.ElementAt(i).unite + ")", timeBlock));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table2.AddCell(cell);

                        nbColonne++;
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + "(" + LDiscipline.ElementAt(i).unite + ")", timeBlock));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table2.AddCell(cell);

                        nbColonne++;
                    }
                }

                cell = new PdfPCell(new Phrase("Mention Conseil de classe", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table2.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell();

                if (bulletinTrimestriel.ListSanction != null)
                {
                    int i = 0;
                    while (i < nbDiscipline)
                    {
                        //représente le code de sanction avec 'inj' (non justifié)
                        //String sanctionCourante = LDiscipline.ElementAt(i).codeSanction + ".inj(" + LDiscipline.ElementAt(i).unite + ")";

                        //représente le code de sanction avec 'j' (jusrifié)
                        //String sanctionCouranteSuivante = LDiscipline.ElementAt(i).codeSanction + ".j(" + LDiscipline.ElementAt(i).unite + ")";

                        if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                        {
                            //on cherche pour le code de sanction en cours, le nombre de sanctions non justifiées
                            bool trouve = false;
                            int nbSanctionJustifiee = 0;
                            int nbSanctionNonJustifiee = 0;
                            for (j = 0; j < bulletinTrimestriel.ListSanction.Count; j++)
                            {
                                if (bulletinTrimestriel.ListSanction.ElementAt(j).codesanction == LDiscipline.ElementAt(i).codeSanction)
                                {
                                    trouve = true;
                                    if (bulletinTrimestriel.ListSanction.ElementAt(j).etat.Equals("NON JUSTIFIEE"))
                                    {
                                        nbSanctionNonJustifiee = nbSanctionNonJustifiee + bulletinTrimestriel.ListSanction.ElementAt(j).quantité;
                                    }
                                    else
                                        nbSanctionJustifiee = nbSanctionJustifiee + bulletinTrimestriel.ListSanction.ElementAt(j).quantité;
                                }
                            }

                            //si on a trouver parmis les sanctions de l'élève une correspondant au code de sanction en cours
                            if (trouve == true)
                            {
                                //le nombre de sanction non justifiées
                                cell = new PdfPCell(new Phrase(Convert.ToString(nbSanctionNonJustifiee - nbSanctionJustifiee), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);


                                //le nombre de sanction justifiées
                                cell = new PdfPCell(new Phrase(Convert.ToString(nbSanctionJustifiee), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);
                            }
                            else
                            {
                                //le nombre de sanction non justifiées (on le met à 0)
                                cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);

                                //le nombre de sanction justifiées (on le met à 0)
                                cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);
                            }

                            i = i + 1;
                        }
                        else
                        {
                            //on cherche pour le code de sanction en cours, le nombre de sanctions non justifiées
                            bool trouve = false;
                            int nbSanction = 0;
                            // int nbSanctionNonJustifiee = 0;
                            for (j = 0; j < bulletinTrimestriel.ListSanction.Count; j++)
                            {
                                if (bulletinTrimestriel.ListSanction.ElementAt(j).codesanction == LDiscipline.ElementAt(i).codeSanction)
                                {
                                    trouve = true;
                                    nbSanction = nbSanction + bulletinTrimestriel.ListSanction.ElementAt(j).quantité;
                                }
                            }

                            //si on a trouver parmis les sanctions de l'élève une correspondant au code de sanction en cours
                            if (trouve == true)
                            {
                                //le nombre de sanction non justifiées
                                cell = new PdfPCell(new Phrase(Convert.ToString(nbSanction), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);

                            }
                            else
                            {
                                //le nombre de sanction non justifiées (on le met à 0)
                                cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);

                            }

                            i = i + 1;
                        }

                    }
                }

                //on écrit la mention du conseil de classe
                if (bulletinTrimestriel.resultattrimestriel != null)
                {
                    cell = new PdfPCell(new Phrase(bulletinTrimestriel.resultattrimestriel.decision + ". \n     " + bulletinTrimestriel.resultattrimestriel.remarque, timesMarque));
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table2.AddCell(cell);
                }

                cell = new PdfPCell(table2);
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = dim;
                table.AddCell(cell);
                

                doc.Add(creerEntete());
                doc.Add(titre);
                doc.Add(creerEnteteBulletin(bulletinTrimestriel.eleve, bulletinTrimestriel.classe, bulletinTrimestriel.annee, effectifClasse));
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                try
                {
                    iTextSharp.text.Image imgprogression = iTextSharp.text.Image.GetInstance(new Uri(ConnexionUI.DOSSIER_BULLETINS + "chart.png"));
                    imgprogression.ScaleToFit(new Rectangle(300, 150));
                    doc.Add(imgprogression);
                }
                catch (Exception)
                {
                }
                if (profTitulaire != null)
                    doc.Add(creerPiedsBulletin(profTitulaire.nomProf, parametre));
                else
                    doc.Add(creerPiedsBulletin("", parametre));

                doc.NewPage();
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        //*** Fonction qui génère l'état du bulletin Annuel d'un élève
        public void etatBulletinAnnuelEleve(BulletinAnnuel bulletinAnnuel, List<String> ListCodeSequence, int effectifClasse, EnseignantBE profTitulaire, ParametresBE parametre, String codeClasse, string photo)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfTimes2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

                Font times = new Font(bfTimes, 8);
                Font timesMarque = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10, Font.BOLD);
                Font timeheader = new Font(bfTimes2, 6);
                Font timeBlock = new Font(bfTimes2, 7);
                var FontColour_header = new BaseColor(134, 181, 232);
                var FontColour_groupe = new BaseColor(200, 221, 226);
                Font TextFont_Header = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontColour_header);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter1();
                doc.Open();

                //photo de l'etudiant
                if (photo != null)
                {
                    try
                    {
                        iTextSharp.text.Image imglogo = iTextSharp.text.Image.GetInstance(new Uri(ConnexionUI.DOSSIER_PHOTO + photo));
                        imglogo.ScaleAbsolute(80f, 70f);
                        imglogo.SetAbsolutePosition(100f, PageSize.A4.Height - 160f);
                        doc.Add(imglogo);
                    }
                    catch (Exception)
                    {
                    }
                }

                Phrase phrase = new Phrase();
                phrase.Add(new Chunk(title.ToUpper() + "\n", timestitre));
                phrase.Add(new Chunk("Année scolaire : ".ToUpper() + (bulletinAnnuel.annee - 1) + "/" + bulletinAnnuel.annee, timesMarque));
                Paragraph titre = new Paragraph(phrase);
                titre.Alignment = Element.ALIGN_CENTER;

                #region contenu concernant les grid

                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                int nbColonneStatic = 9;

                PdfPTable table = new PdfPTable(nbColonneStatic + bulletinAnnuel.nbSequence);
                int dim = nbColonneStatic + bulletinAnnuel.nbSequence;
                float[] largeur = new float[dim];
                largeur[0] = 4f;

                for (int i = 0; i < bulletinAnnuel.nbSequence; i++)
                    largeur[i + 1] = 0.9f;

                largeur[bulletinAnnuel.nbSequence + 1] = 0.8f;
                largeur[bulletinAnnuel.nbSequence + 2] = 0.8f;
                largeur[bulletinAnnuel.nbSequence + 3] = 0.9f;
                largeur[bulletinAnnuel.nbSequence + 4] = 0.8f;
                largeur[bulletinAnnuel.nbSequence + 5] = 0.8f;
                largeur[bulletinAnnuel.nbSequence + 6] = 0.8f;
                largeur[bulletinAnnuel.nbSequence + 7] = 0.8f;
                largeur[bulletinAnnuel.nbSequence + 8] = 3.5f;

                table.SetWidths(largeur);
                table.WidthPercentage = 95f;
                //la table possède 6 colonnes
                /*
                 * 0 : année
                 * 1 : matricule
                 * 2 : codematière
                 * 3 : codesequence
                 * 4 : moyenne
                 * 5:  mention
                 */
                //float[] widths = new float[] { 1.5f, 8f, 1f, 1f, 1f, 1f, 1f, 1.5f };
                //table.SetWidths(widths);
                //table.WidthPercentage = 100f;
                cell = new PdfPCell(new Phrase("Matières", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                for (int j = 0; j < bulletinAnnuel.nbSequence; j++)
                {
                    cell = new PdfPCell(new Phrase("" + ListCodeSequence.ElementAt(j), timeheader));
                    cell.BackgroundColor = FontColour_header;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);
                }

                cell = new PdfPCell(new Phrase("Moy", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Coef", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Total", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rang", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Cl", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Min", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Max", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Appréciation", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                /*for (int j = 0; j < table.NumberOfColumns; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString(), timeheader));
                }*/
                table.HeaderRows = 1;

                int nbEvalution = bulletinAnnuel.nbSequence;
                int compteur = bulletinAnnuel.nbSequence; //servira à chaque fois à indiquer si on a terminé avec une matière
                //NB pour une matière, on a 'bulletinSequentiel.nbEvaluation' ligne dans la liste

                if (bulletinAnnuel.listLigneBulletinAnnuel != null && bulletinAnnuel.listLigneBulletinAnnuel.Count != 0)
                {

                    String indiceCodeGroupe = bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(0).codeGroupe;

                    int i = 0;
                    while (i < bulletinAnnuel.listLigneBulletinAnnuel.Count)
                    {
                        LigneBulletinAnnuel ligneBulletin = bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i);

                        //le nom de la matière et de l'enseignant
                        phrase = new Phrase();
                        phrase.Add(new Chunk(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i).nomMat + "\n", timesMarque));
                        phrase.Add(new Chunk(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i).nomProf, times));
                        cell = new PdfPCell(phrase);
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        table.AddCell(cell);

                        int nb = 0; //compte le nombre de fois qu'on a trouvé une ligne avec un code de trimestre identique
                        int nb1 = 0; //compte le nombre de fois que l'opération i+j est sortie des limites de la liste des ligne de Bulletin Annuel
                        int k = 0; //k permet de déterminer si on doit avancer dans la liste 'listLigneBulletinTrimestriel'
                        //k = 0 (on avance); k = 1 (on reste sur la même position)
                        //on avance seulement si on a trouvé la note correspondante à la séquence que l'on cherchait

                        //on rempli les notes de tous les trimestres
                        for (int j = 0; j < compteur; j++)
                        {
                            if (i + j < bulletinAnnuel.listLigneBulletinAnnuel.Count)
                            {
                                if (j == 0)
                                {


                                    if (bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i + j).codeSequence == ListCodeSequence.ElementAt(j))
                                    {
                                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i + j).moyenneSequence, 2)), times));
                                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                        table.AddCell(cell);
                                        k = 0;
                                    }
                                    else
                                    {
                                        cell = new PdfPCell(new Phrase(Convert.ToString(""), times));
                                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                        table.AddCell(cell);
                                        k = 1;

                                    }

                                }
                                else
                                {
                                    if (k == 0)
                                    {
                                        if ((bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i + j - k).codeSequence != bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i).codeSequence)
                                            && (bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i + j - k).codeMat == bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i).codeMat))
                                        {
                                            cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i + j - k).moyenneSequence, 2)), times));
                                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                            table.AddCell(cell);
                                            k = 0;
                                        }
                                        else
                                        {
                                            cell = new PdfPCell(new Phrase(Convert.ToString(" "), times));
                                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                            table.AddCell(cell);
                                            k = 1;
                                            nb++;
                                        }
                                    }
                                    else
                                    {
                                        if (bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i + j - k).codeSequence == ListCodeSequence.ElementAt(j))
                                        {
                                            cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i + j - k).moyenneSequence, 2)), times));
                                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                            table.AddCell(cell);
                                            k = 0;
                                            nb++;
                                        }
                                        else
                                        {
                                            cell = new PdfPCell(new Phrase(Convert.ToString(" "), times));
                                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                            table.AddCell(cell);
                                            k = 1;
                                            nb++;
                                        }
                                    }


                                }
                            }
                            else
                            {
                                if ((k == 1) && (i + j - k < bulletinAnnuel.listLigneBulletinAnnuel.Count)
                                    && (bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i + j - k).codeSequence == ListCodeSequence.ElementAt(j)))
                                {
                                    cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i + j - k).moyenneSequence, 2)), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);
                                    k = 0;
                                }
                                else
                                {
                                    cell = new PdfPCell(new Phrase(Convert.ToString(""), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);
                                    k = 1;

                                }

                                /*cell = new PdfPCell(new Phrase(Convert.ToString(" "), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);*/

                                nb1++;
                            }

                        }

                        //la moyenne annuelles de l'élève sur la matière
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i).moyenneAnnuelle, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //le coeficient de la matière
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i).coef), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne annuelle coeficié de l'élève pour la matière
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i).moyenneAnnuelle * bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i).coef, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //le rang de l'élève
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i).rangAnnuel), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne de la classe
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i).moyenneClasseAnnuelle, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne minimale
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i).moyenneMinClasseAnnuelle, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne maximale
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i).moyenneMaxClasseAnnuelle, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //l'appréciation (ou encore la mention)
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i).mention + "\n   - " + bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i).appreciation), timesMarque));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        i = (i + compteur) - nb - nb1;

                        //on vérifi si on n'est pas passé dans un autre groupe de matière
                        if (i < bulletinAnnuel.listLigneBulletinAnnuel.Count)
                        {
                            if (bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i).codeGroupe != bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i - 1).codeGroupe)
                            {
                                //on insère la ligne contenant la moyenne du groupe
                                //le nom du groupe
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i - 1).nomGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                table.AddCell(cell);

                                //le total des points du groupe ainsi que le total max des points possibles
                                cell = new PdfPCell(new Phrase("Points : " + Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i - 1).totalPointGroupe, 2) + "/" + Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i - 1).totalPointMaxGroupe, 2), timeheader));
                                cell.Colspan = nbEvalution + 1;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le total des coeficients
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i - 1).totalCoefGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //la moyenne du groupe de l'élève
                                cell = new PdfPCell(new Phrase("Moyenne : " + Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i - 1).moyenneGroupe, 2), timeheader));
                                cell.Colspan = 6;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                        }
                        else
                        {
                            if (i - 1 < bulletinAnnuel.listLigneBulletinAnnuel.Count)
                            {
                                //on insère la ligne contenant la moyenne du groupe
                                //le nom du groupe
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i - 1).nomGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                table.AddCell(cell);
                                //le total des points du groupe ainsi que le total max des points possibles
                                cell = new PdfPCell(new Phrase("Points : " + Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i - 1).totalPointGroupe, 2) + "/" + Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i - 1).totalPointMaxGroupe, 2), timeheader));
                                cell.Colspan = nbEvalution + 1;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le total des coeficients
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i - 1).totalCoefGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //la moyenne du groupe de l'élève
                                cell = new PdfPCell(new Phrase("Moyenne : " + Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i - 1).moyenneGroupe, 2), timeheader));
                                cell.Colspan = 6;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                            else
                            {
                                int index = bulletinAnnuel.listLigneBulletinAnnuel.Count - 1;
                                //on insère la ligne contenant la moyenne du groupe
                                //le nom du groupe
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i - 1).nomGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                table.AddCell(cell);
                                //le total des points du groupe ainsi que le total max des points possibles
                                cell = new PdfPCell(new Phrase("Points : " + Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i - 1).totalPointGroupe, 2) + "/" + Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i - 1).totalPointMaxGroupe, 2), timeheader));
                                cell.Colspan = nbEvalution + 1;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le total des coeficients
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i - 1).totalCoefGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //la moyenne du groupe de l'élève
                                cell = new PdfPCell(new Phrase("Moyenne : " + Math.Round(bulletinAnnuel.listLigneBulletinAnnuel.ElementAt(i - 1).moyenneGroupe, 2), timeheader));
                                cell.Colspan = 6;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                        }
                    }
                }

                #endregion

                //**************Debut de la partie bilan travail sequentiel
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                cell.Colspan = nbEvalution + 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("TRAVAIL", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Points", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Coef", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);


                cell = new PdfPCell(new Phrase("Moyenne", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rang", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Cl", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Min", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Max", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Appréciation", timeBlock));
                cell.Colspan = nbEvalution + 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                if (bulletinAnnuel.resultatannuel != null)
                {
                    if (bulletinAnnuel.resultatannuel != null)
                    {
                        cell = new PdfPCell(new Phrase(" "));
                        cell.Border = Rectangle.NO_BORDER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinAnnuel.resultatannuel.point, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell();

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinAnnuel.resultatannuel.coef), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinAnnuel.resultatannuel.moyenne, 2)), timesMarque));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinAnnuel.resultatannuel.rang), timesMarque));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinAnnuel.resultatannuel.moyenneclasse, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinAnnuel.moyenneMin, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinAnnuel.moyenneMax, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinAnnuel.resultatannuel.mention + "\n  - " + bulletinAnnuel.resultatannuel.appreciation), timesMarque));
                        cell.Colspan = nbEvalution + 1;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }

                //Fin de la partie bilan sequentiel

                //******************************** Debut de la partie discipline

                //on liste les codes des sanctions
                DisciplineDA disciplineDA = new DisciplineDA();
                List<DisciplineBE> LDiscipline = disciplineDA.listerTous();
                int nbDiscipline = LDiscipline.Count;

                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                cell.Colspan = nbEvalution + 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("DISCIPLINE", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                int nbColonne = 1; //compte le nbre de colonne crée ds le tableau pour la partie Discipline

                for (int i = 0; i < nbDiscipline; i++)
                {
                    if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                    {
                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + ".inj(" + LDiscipline.ElementAt(i).unite + ")", timeBlock));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        nbColonne++;

                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + ".j(" + LDiscipline.ElementAt(i).unite + ")", timeBlock));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        nbColonne++;
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + "(" + LDiscipline.ElementAt(i).unite + ")", timeBlock));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        nbColonne++;
                    }
                }

                /*cell = new PdfPCell(new Phrase("Abs.inj(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Abs.j(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Retards(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Avert.", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Blâmes", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Exclu.(J)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);*/

                cell = new PdfPCell(new Phrase("Mention Conseil de classe", timeBlock));
                //cell.Colspan = nbEvalution + 2;
                //cell.Colspan = 2;
                if (nbColonneStatic + nbEvalution - nbColonne > 0) //pour fusionner le reste de colonne libres
                    cell.Colspan = nbColonneStatic + nbEvalution - nbColonne;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);




                cell = new PdfPCell(new Phrase(" ", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell();

                if (bulletinAnnuel.ListSanction != null)
                {
                    int i = 0;
                    while (i < nbDiscipline)
                    {
                        //représente le code de sanction avec 'inj' (non justifié)
                        //String sanctionCourante = LDiscipline.ElementAt(i).codeSanction + ".inj(" + LDiscipline.ElementAt(i).unite + ")";

                        //représente le code de sanction avec 'j' (jusrifié)
                        //String sanctionCouranteSuivante = LDiscipline.ElementAt(i).codeSanction + ".j(" + LDiscipline.ElementAt(i).unite + ")";

                        if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                        {
                            //on cherche pour le code de sanction en cours, le nombre de sanctions non justifiées
                            bool trouve = false;
                            int nbSanctionJustifiee = 0;
                            int nbSanctionNonJustifiee = 0;
                            for (int j = 0; j < bulletinAnnuel.ListSanction.Count; j++)
                            {
                                if (bulletinAnnuel.ListSanction.ElementAt(j).codesanction == LDiscipline.ElementAt(i).codeSanction)
                                {
                                    trouve = true;
                                    if (bulletinAnnuel.ListSanction.ElementAt(j).etat.Equals("NON JUSTIFIEE"))
                                    {
                                        nbSanctionNonJustifiee = nbSanctionNonJustifiee + bulletinAnnuel.ListSanction.ElementAt(j).quantité;
                                    }
                                    else
                                        nbSanctionJustifiee = nbSanctionJustifiee + bulletinAnnuel.ListSanction.ElementAt(j).quantité;
                                }
                            }

                            //si on a trouver parmis les sanctions de l'élève une correspondant au code de sanction en cours
                            if (trouve == true)
                            {
                                //le nombre de sanction non justifiées
                                cell = new PdfPCell(new Phrase(Convert.ToString(nbSanctionNonJustifiee - nbSanctionJustifiee), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le nombre de sanction justifiées
                                cell = new PdfPCell(new Phrase(Convert.ToString(nbSanctionJustifiee), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                            else
                            {
                                //le nombre de sanction non justifiées (on le met à 0)
                                cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le nombre de sanction justifiées (on le met à 0)
                                cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }

                            i = i + 1;
                        }
                        else
                        {
                            //on cherche pour le code de sanction en cours, le nombre de sanctions non justifiées
                            bool trouve = false;
                            int nbSanction = 0;
                            // int nbSanctionNonJustifiee = 0;
                            for (int j = 0; j < bulletinAnnuel.ListSanction.Count; j++)
                            {
                                if (bulletinAnnuel.ListSanction.ElementAt(j).codesanction == LDiscipline.ElementAt(i).codeSanction)
                                {
                                    trouve = true;
                                    nbSanction = nbSanction + bulletinAnnuel.ListSanction.ElementAt(j).quantité;
                                }
                            }

                            //si on a trouver parmis les sanctions de l'élève une correspondant au code de sanction en cours
                            if (trouve == true)
                            {
                                //le nombre de sanction non justifiées
                                cell = new PdfPCell(new Phrase(Convert.ToString(nbSanction), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                            }
                            else
                            {
                                //le nombre de sanction non justifiées (on le met à 0)
                                cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                            }

                            i = i + 1;
                        }

                    }
                }

                //on écrit la mention du conseil de classe
                if (bulletinAnnuel.resultatannuel != null)
                {
                    String decision = getInfosNewNiveauEleve(bulletinAnnuel.resultatannuel, codeClasse);

                    cell = new PdfPCell(new Phrase(decision + ". \n - " + bulletinAnnuel.resultatannuel.remarque, timeBlock));
                    //cell.Colspan = nbEvalution + 2;
                    //cell.Colspan =  2;
                    if (nbColonneStatic + nbEvalution - nbColonne > 0) //pour fusionner le nombre de colonne libre
                        cell.Colspan = nbColonneStatic + nbEvalution - nbColonne;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);
                }

                //Fin de la partie discipline

                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                cell.Colspan = nbEvalution + 1;
                table.AddCell(cell);

                doc.Add(creerEntete());
                doc.Add(titre);
                //doc.Add(Chunk.NEWLINE);
                doc.Add(creerEnteteBulletin(bulletinAnnuel.eleve, bulletinAnnuel.classe, bulletinAnnuel.annee, effectifClasse));
                doc.Add(table);
                // doc.Add(Chunk.NEWLINE);
                //doc.Add(table2);
                //doc.Add(Chunk.NEWLINE);
                if (profTitulaire != null)
                    doc.Add(creerPiedsBulletin(profTitulaire.nomProf, parametre));
                else
                    doc.Add(creerPiedsBulletin("", parametre));

                doc.Close();

                SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                //System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        //*** Fonction qui génère l'état du bulletin Séquentiel d'un élève
        public void etatBulletinSequentielEleve(BullettinSequentiel bulletinSequentiel, List<String> ListCodeEvaluation, int effectifClasse, EnseignantBE profTitulaire, ParametresBE parametre, string photo)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfTimes2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

                Font times = new Font(bfTimes, 8);
                Font timesMarque = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10, Font.BOLD);
                Font timeheader = new Font(bfTimes2, 6);
                Font timeBlock = new Font(bfTimes2, 7);
                var FontColour_header = new BaseColor(134, 181, 232);
                var FontColour_groupe = new BaseColor(200, 221, 226);
                Font TextFont_Header = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontColour_header);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter1();
                doc.Open();

                PdfContentByte canvas = writer.DirectContentUnder;
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(ConnexionUI.DOSSIER_IMAGES + parametre.logo);
                image.SetAbsolutePosition((PageSize.A4.Width - image.ScaledWidth) / 2, (PageSize.A4.Height - image.ScaledHeight) / 2);
                image.ScaleToFit(200, 200);
                canvas.SaveState();
                PdfGState state = new PdfGState();
                state.FillOpacity = 0.2f;
                canvas.SetGState(state);
                canvas.AddImage(image);
                canvas.RestoreState();

                iTextSharp.text.Image imagearrow = iTextSharp.text.Image.GetInstance(ConnexionUI.DOSSIER_IMAGES + "arrow.png");
                imagearrow.ScaleToFit(10,10);

                if (photo != null)
                {
                    try
                    {
                        iTextSharp.text.Image imglogo = iTextSharp.text.Image.GetInstance(new Uri(ConnexionUI.DOSSIER_PHOTO + photo));
                        imglogo.ScaleAbsolute(80f, 70f);
                        imglogo.SetAbsolutePosition(100f, PageSize.A4.Height - 160f);
                        doc.Add(imglogo);
                    }
                    catch (Exception)
                    {
                    }
                }
                Phrase phrase = new Phrase();
                phrase.Add(new Chunk(title.ToUpper() + "\n", timestitre));
                phrase.Add(new Chunk("Année scolaire : ".ToUpper() + (bulletinSequentiel.annee - 1) + "/" + bulletinSequentiel.annee, timesMarque));
                Paragraph titre = new Paragraph(phrase);
                titre.Alignment = Element.ALIGN_CENTER;

                int nbColonneStatic = 9; //le nbre de colonne static du tableau, à laquelle ont ajoutera des colonnes qui seront crée dynamiquement

                #region contenu concernant les grid

                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                PdfPTable table = new PdfPTable(nbColonneStatic + 1);
                int dim = nbColonneStatic + 1;
                float[] largeur = new float[dim];
                largeur[0] = 0.3f; 
                largeur[1] = 4.5f;

                //for (int i = 0; i < bulletinSequentiel.nbEvaluation; i++)
                //    largeur[i + 1] = 0.9f;

                largeur[2] = 0.8f;
                largeur[3] = 0.8f;
                largeur[4] = 0.9f;
                largeur[5] = 0.8f;
                largeur[6] = 0.8f;
                largeur[7] = 0.8f;
                largeur[8] = 0.8f;
                largeur[9] = 3.5f;

                table.SetWidths(largeur);
                table.WidthPercentage = 95f;

                //****************** debut de la déclaration du tableau des disciplines

                PdfPTable table2 = new PdfPTable(nbColonneStatic + 2);
                int dim2 = nbColonneStatic + 2;
                float[] largeur2 = new float[dim2];
                largeur2[0] = 3f;

                //for (int i = 0; i < bulletinSequentiel.nbEvaluation; i++)
                //    largeur[i + 1] = 0.9f;

                largeur2[1] = 0.8f;
                largeur2[2] = 0.8f;
                largeur2[3] = 0.9f;
                largeur2[4] = 0.8f;
                largeur2[5] = 0.8f;
                largeur2[6] = 0.8f;
                largeur2[7] = 0.8f;
                largeur2[8] = 0.8f;
                largeur2[9] = 0.8f;
                largeur2[10] = 4.5f;

                table2.SetWidths(largeur2);
                table2.WidthPercentage = 95f;

                //****************** fin de la déclaration du tableau des disciplines

                //la table possède 6 colonnes
                /*
                 * 0 : année
                 * 1 : matricule
                 * 2 : codematière
                 * 3 : codesequence
                 * 4 : moyenne
                 * 5:  mention
                 */
                //float[] widths = new float[] { 1.5f, 8f, 1f, 1f, 1f, 1f, 1f, 1.5f };
                //table.SetWidths(widths);
                //table.WidthPercentage = 100f;
                cell = new PdfPCell(new Phrase("Matières", timeheader));
                cell.Colspan = 2;
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                //for (int j = 0; j < bulletinSequentiel.nbEvaluation; j++)
                //{
                //    cell = new PdfPCell(new Phrase("" + ListCodeEvaluation.ElementAt(j), timeheader));
                //    cell.BackgroundColor = FontColour_header;
                //    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                //    table.AddCell(cell);
                //}

                cell = new PdfPCell(new Phrase("Moy", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Coef", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Total", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rang", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Cl", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Min", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Max", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Appréciation", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                /*for (int j = 0; j < table.NumberOfColumns; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString(), timeheader));
                }*/
                table.HeaderRows = 1;

                int nbEvalution = bulletinSequentiel.nbEvaluation;
                int compteur = bulletinSequentiel.nbEvaluation; //servira à chaque fois à indiquer si on a terminé avec une matière
                //NB pour une matière, on a 'bulletinSequentiel.nbEvaluation' ligne dans la liste

                if (bulletinSequentiel.listLigneBulletinSequentiel != null && bulletinSequentiel.listLigneBulletinSequentiel.Count != 0)
                {

                    String indiceCodeGroupe = bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(0).codeGroupe;

                    int i = 0;
                    while (i < bulletinSequentiel.listLigneBulletinSequentiel.Count)
                    {
                        LigneBulletinSequentiel ligneBulletin = bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i);
                        
                        //petit logo
                        cell = new PdfPCell(imagearrow);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                        table.AddCell(cell);

                        //le nom de la matière et de l'enseignant
                        phrase = new Phrase();
                        phrase.Add(new Chunk(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).nomMat + "\n", timesMarque));
                        phrase.Add(new Chunk(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).nomProf, times));

                        cell = new PdfPCell(phrase);
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
                        table.AddCell(cell);

                        int nb = 0; //compte le nombre de fois qu'on a trouvé une ligne avec un code de trimestre identique
                        int nb1 = 0; //compte le nombre de fois que l'opération i+j est sortie des limites de la liste des ligne de Bulletin Annuel
                        
                        //la moyenne séquentielle de l'élève sur la matière
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).moyenneSequence, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //le coeficient de la matière
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).coef), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne annuelle coeficié de l'élève pour la matière
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).moyenneSequence * bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).coef, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //le rang de l'élève
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).rangSequentiel), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne de la classe
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).moyenneSeqClasse, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne minimale
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).moyenneSeqMin, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne maximale
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).moyenneSeqMax, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //l'appréciation (ou encore la mention)
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).mention + "\n  - " + bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).appreciation), timesMarque));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        i = (i + compteur) - nb - nb1;
                        //i = i + nb - nb1;

                        //on vérifi si on n'est pas passé dans un autre groupe de matière
                        if (i < bulletinSequentiel.listLigneBulletinSequentiel.Count)
                        {
                            if (bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).codeGroupe != bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).codeGroupe)
                            {
                                //on insère la ligne contenant la moyenne du groupe
                                //le nom du groupe
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).nomGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                cell.Colspan = 2;
                                table.AddCell(cell);

                                //le total des points du groupe ainsi que le total max des points possibles
                                cell = new PdfPCell(new Phrase("Points : " + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalPointGroupe, 2) + "/" + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalPointMaxGroupe, 2), timeheader));
                                cell.Colspan = nbEvalution + 1;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le total des coeficients
                                cell = new PdfPCell(new Phrase("Total Coef : " + Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalCoefGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                cell.Colspan = 3;
                                table.AddCell(cell);

                                //la moyenne du groupe de l'élève
                                cell = new PdfPCell(new Phrase("Moyenne : " + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).moyenneGroupe, 2), timeheader));
                                cell.Colspan = 4;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                        }
                        else
                        {
                            if (i - 1 < bulletinSequentiel.listLigneBulletinSequentiel.Count)
                            {
                                //on insère la ligne contenant la moyenne du groupe
                                //le nom du groupe
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).nomGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                cell.Colspan = 2;
                                table.AddCell(cell);
                                //le total des points du groupe ainsi que le total max des points possibles
                                cell = new PdfPCell(new Phrase("Points : " + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalPointGroupe, 2) + "/" + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalPointMaxGroupe, 2), timeheader));
                                cell.Colspan = nbEvalution + 1;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le total des coeficients
                                cell = new PdfPCell(new Phrase("Total Coef : " + Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalCoefGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                cell.Colspan = 3;
                                table.AddCell(cell);

                                //la moyenne du groupe de l'élève
                                cell = new PdfPCell(new Phrase("Moyenne : " + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).moyenneGroupe, 2), timeheader));
                                cell.Colspan = 4;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                            else
                            {
                                int index = bulletinSequentiel.listLigneBulletinSequentiel.Count - 1;
                                //on insère la ligne contenant la moyenne du groupe
                                //le nom du groupe
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).nomGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                cell.Colspan = 2;
                                table.AddCell(cell);
                                //le total des points du groupe ainsi que le total max des points possibles
                                cell = new PdfPCell(new Phrase("Points : " + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalPointGroupe, 2) + "/" + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalPointMaxGroupe, 2), timeheader));
                                cell.Colspan = nbEvalution + 1;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le total des coeficients
                                cell = new PdfPCell(new Phrase("Total Coef : " + Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalCoefGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                cell.Colspan = 3;
                                table.AddCell(cell);

                                //la moyenne du groupe de l'élève
                                cell = new PdfPCell(new Phrase("Moyenne : " + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).moyenneGroupe, 2), timeheader));
                                cell.Colspan = 4;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                        }
                    }
                }

                #endregion

                //**********************Debut de la partie bilan travail sequentiel
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                cell.Colspan = nbEvalution + 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("TRAVAIL", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Points", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Coef", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);


                cell = new PdfPCell(new Phrase("Moyenne", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rang", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Cl", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Min", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Max", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Appréciation", timeBlock));
                cell.Colspan = nbEvalution + 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                if (bulletinSequentiel.listLigneBulletinSequentiel != null)
                {
                    if (bulletinSequentiel.resultatSequentiel != null)
                    {
                        cell = new PdfPCell(new Phrase(" "));
                        cell.Border = Rectangle.NO_BORDER;
                        cell.Colspan = 2;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.resultatSequentiel.point), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.resultatSequentiel.coef), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.resultatSequentiel.moyenne, 2)), timesMarque));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.resultatSequentiel.rang), timesMarque));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.resultatSequentiel.moyenneclasse, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.moyenneMin, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.moyenneMax, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.resultatSequentiel.mention + " \n  - " + bulletinSequentiel.resultatSequentiel.appreciation), timesMarque));
                        cell.Colspan = nbEvalution + 1;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }

                //***********************Fin de la partie bilan sequentiel

                //******************************** Debut de la partie discipline

                //on liste les codes des sanctions
                DisciplineDA disciplineDA = new DisciplineDA();
                List<DisciplineBE> LDiscipline = disciplineDA.listerTous();
                int nbDiscipline = LDiscipline.Count;

                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                cell.Colspan = nbEvalution + 1;
                table.AddCell(cell);


                cell = new PdfPCell(new Phrase("DISCIPLINE", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table2.AddCell(cell);

                int nbColonne = 1; //compte le nbre de colonne crée ds le tableau pour la partie Discipline

                for (int i = 0; i < nbDiscipline; i++)
                {
                    if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                    {
                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + ".inj(" + LDiscipline.ElementAt(i).unite + ")", timeBlock));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table2.AddCell(cell);

                        nbColonne++;

                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + ".j(" + LDiscipline.ElementAt(i).unite + ")", timeBlock));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table2.AddCell(cell);

                        nbColonne++;
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + "(" + LDiscipline.ElementAt(i).unite + ")", timeBlock));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table2.AddCell(cell);

                        nbColonne++;
                    }
                }

                /*cell = new PdfPCell(new Phrase("Abs.inj(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Abs.j(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Retards(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Avert.", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Blâmes", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Exclu.(J)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);*/

                cell = new PdfPCell(new Phrase("Mention Conseil de classe", timeBlock));
                //cell.Colspan = nbEvalution + 2;
                //cell.Colspan = 2;
                if (nbColonneStatic + nbEvalution - nbColonne > 0) //pour fusionner le reste de colonne libres
                    cell.Colspan = nbColonneStatic + nbEvalution - nbColonne;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table2.AddCell(cell);




                cell = new PdfPCell(new Phrase(" ", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell();

                if (bulletinSequentiel.ListSanction != null)
                {
                    int i = 0;
                    while (i < nbDiscipline)
                    {
                        //représente le code de sanction avec 'inj' (non justifié)
                        //String sanctionCourante = LDiscipline.ElementAt(i).codeSanction + ".inj(" + LDiscipline.ElementAt(i).unite + ")";

                        //représente le code de sanction avec 'j' (jusrifié)
                        //String sanctionCouranteSuivante = LDiscipline.ElementAt(i).codeSanction + ".j(" + LDiscipline.ElementAt(i).unite + ")";

                        if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                        {
                            //on cherche pour le code de sanction en cours, le nombre de sanctions non justifiées
                            bool trouve = false;
                            int nbSanctionJustifiee = 0;
                            int nbSanctionNonJustifiee = 0;
                            for (int j = 0; j < bulletinSequentiel.ListSanction.Count; j++)
                            {
                                if (bulletinSequentiel.ListSanction.ElementAt(j).codesanction == LDiscipline.ElementAt(i).codeSanction)
                                {
                                    trouve = true;
                                    if (bulletinSequentiel.ListSanction.ElementAt(j).etat.Equals("NON JUSTIFIEE"))
                                    {
                                        nbSanctionNonJustifiee = nbSanctionNonJustifiee + bulletinSequentiel.ListSanction.ElementAt(j).quantité;
                                    }
                                    else
                                        nbSanctionJustifiee = nbSanctionJustifiee + bulletinSequentiel.ListSanction.ElementAt(j).quantité;
                                }
                            }

                            //si on a trouver parmis les sanctions de l'élève une correspondant au code de sanction en cours
                            if (trouve == true)
                            {
                                //le nombre de sanction non justifiées
                                cell = new PdfPCell(new Phrase(Convert.ToString(nbSanctionNonJustifiee - nbSanctionJustifiee), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);


                                //le nombre de sanction justifiées
                                cell = new PdfPCell(new Phrase(Convert.ToString(nbSanctionJustifiee), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);
                            }
                            else
                            {
                                //le nombre de sanction non justifiées (on le met à 0)
                                cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);

                                //le nombre de sanction justifiées (on le met à 0)
                                cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);
                            }

                            i = i + 1;
                        }
                        else
                        {
                            //on cherche pour le code de sanction en cours, le nombre de sanctions non justifiées
                            bool trouve = false;
                            int nbSanction = 0;
                            // int nbSanctionNonJustifiee = 0;
                            for (int j = 0; j < bulletinSequentiel.ListSanction.Count; j++)
                            {
                                if (bulletinSequentiel.ListSanction.ElementAt(j).codesanction == LDiscipline.ElementAt(i).codeSanction)
                                {
                                    trouve = true;
                                    nbSanction = nbSanction + bulletinSequentiel.ListSanction.ElementAt(j).quantité;
                                }
                            }

                            //si on a trouver parmis les sanctions de l'élève une correspondant au code de sanction en cours
                            if (trouve == true)
                            {
                                //le nombre de sanction non justifiées
                                cell = new PdfPCell(new Phrase(Convert.ToString(nbSanction), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);

                            }
                            else
                            {
                                //le nombre de sanction non justifiées (on le met à 0)
                                cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);

                            }

                            i = i + 1;
                        }

                    }
                }

                //on écrit la mention du conseil de classe
                if (bulletinSequentiel.resultatSequentiel != null)
                {
                    cell = new PdfPCell(new Phrase(bulletinSequentiel.resultatSequentiel.decision + ". \n     " + bulletinSequentiel.resultatSequentiel.remarque, timeBlock));
                    //cell.Colspan = nbEvalution + 2;
                    //cell.Colspan =  2;
                    if (nbColonneStatic + nbEvalution - nbColonne > 0) //pour fusionner le nombre de colonne libre
                        cell.Colspan = nbColonneStatic + nbEvalution - nbColonne;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table2.AddCell(cell);
                }

                //Fin de la partie discipline

                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table2.AddCell(cell);
                table2.AddCell(cell);
                table2.AddCell(cell);
                table2.AddCell(cell);
                table2.AddCell(cell);
                table2.AddCell(cell);
                table2.AddCell(cell);
                table2.AddCell(cell);
                table2.AddCell(cell);
                table2.AddCell(cell);
                cell.Colspan = nbEvalution + 1;
                table2.AddCell(cell);

                doc.Add(creerEntete());
                doc.Add(titre);
                doc.Add(creerEnteteBulletin(bulletinSequentiel.eleve, bulletinSequentiel.classe, bulletinSequentiel.annee, effectifClasse));
                doc.Add(table);
                doc.Add(table2);
                
                if (profTitulaire != null)
                    doc.Add(creerPiedsBulletin(profTitulaire.nomProf, parametre));
                else
                    doc.Add(creerPiedsBulletin("", parametre));

                doc.Close();

                SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                //System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void etatBulletinSequentielEleve(Document doc, PdfWriter writer, BullettinSequentiel bulletinSequentiel, List<String> ListCodeEvaluation, int effectifClasse, EnseignantBE profTitulaire, ParametresBE parametre, string photo)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfTimes2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

                Font times = new Font(bfTimes, 8);
                Font timesMarque = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10, Font.BOLD);
                Font timeheader = new Font(bfTimes2, 8);
                Font timeBlock = new Font(bfTimes2, 8);
                var FontColour_header = new BaseColor(134, 181, 232);
                var FontColour_groupe = new BaseColor(200, 221, 226);
                Font TextFont_Header = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontColour_header);

                try
                {
                    PdfContentByte canvas = writer.DirectContentUnder;
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(ConnexionUI.DOSSIER_IMAGES + parametre.logo);
                    image.SetAbsolutePosition((PageSize.A4.Width - image.ScaledWidth) / 2, (PageSize.A4.Height - image.ScaledHeight) / 2);
                    image.ScaleToFit(200, 200);
                    canvas.SaveState();
                    PdfGState state = new PdfGState();
                    state.FillOpacity = 0.2f;
                    canvas.SetGState(state);
                    canvas.AddImage(image);
                    canvas.RestoreState();
                }
                catch (Exception)
                {
                }
                iTextSharp.text.Image imagearrow = iTextSharp.text.Image.GetInstance(ConnexionUI.DOSSIER_IMAGES + "arrow.png");
                imagearrow.ScaleToFit(10, 10);

                if (photo != null)
                {
                    try
                    {
                        iTextSharp.text.Image imglogo = iTextSharp.text.Image.GetInstance(new Uri(ConnexionUI.DOSSIER_PHOTO + photo));
                        imglogo.ScaleAbsolute(80f, 70f);
                        imglogo.SetAbsolutePosition(100f, PageSize.A4.Height - 160f);
                        doc.Add(imglogo);
                    }
                    catch (Exception)
                    {
                    }
                }
                Phrase phrase = new Phrase();
                phrase.Add(new Chunk(title.ToUpper() + "\n", timestitre));
                phrase.Add(new Chunk("Année scolaire : ".ToUpper() + (bulletinSequentiel.annee - 1) + "/" + bulletinSequentiel.annee, timesMarque));
                Paragraph titre = new Paragraph(phrase);
                titre.Alignment = Element.ALIGN_CENTER;

                int nbColonneStatic = 9; //le nbre de colonne static du tableau, à laquelle ont ajoutera des colonnes qui seront crée dynamiquement

                #region contenu concernant les grid

                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                PdfPTable table = new PdfPTable(nbColonneStatic + 1);
                int dim = nbColonneStatic + 1;
                float[] largeur = new float[dim];
                largeur[0] = 0.3f;
                largeur[1] = 4.5f;

                //for (int i = 0; i < bulletinSequentiel.nbEvaluation; i++)
                //    largeur[i + 1] = 0.9f;

                largeur[2] = 0.8f;
                largeur[3] = 0.8f;
                largeur[4] = 0.9f;
                largeur[5] = 0.8f;
                largeur[6] = 0.8f;
                largeur[7] = 0.8f;
                largeur[8] = 0.8f;
                largeur[9] = 3.5f;

                table.SetWidths(largeur);
                table.WidthPercentage = 95f;

                //****************** debut de la déclaration du tableau des disciplines

                PdfPTable table2 = new PdfPTable(nbColonneStatic + 2);
                int dim2 = nbColonneStatic + 2;
                float[] largeur2 = new float[dim2];
                largeur2[0] = 3f;

                //for (int i = 0; i < bulletinSequentiel.nbEvaluation; i++)
                //    largeur[i + 1] = 0.9f;

                largeur2[1] = 0.8f;
                largeur2[2] = 0.8f;
                largeur2[3] = 0.9f;
                largeur2[4] = 0.8f;
                largeur2[5] = 0.8f;
                largeur2[6] = 0.8f;
                largeur2[7] = 0.8f;
                largeur2[8] = 0.8f;
                largeur2[9] = 0.8f;
                largeur2[10] = 4.5f;

                table2.SetWidths(largeur2);
                table2.WidthPercentage = 95f;

                //****************** fin de la déclaration du tableau des disciplines

                //la table possède 6 colonnes
                /*
                 * 0 : année
                 * 1 : matricule
                 * 2 : codematière
                 * 3 : codesequence
                 * 4 : moyenne
                 * 5:  mention
                 */
                //float[] widths = new float[] { 1.5f, 8f, 1f, 1f, 1f, 1f, 1f, 1.5f };
                //table.SetWidths(widths);
                //table.WidthPercentage = 100f;
                cell = new PdfPCell(new Phrase("Matières", timeheader));
                cell.Colspan = 2;
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                //for (int j = 0; j < bulletinSequentiel.nbEvaluation; j++)
                //{
                //    cell = new PdfPCell(new Phrase("" + ListCodeEvaluation.ElementAt(j), timeheader));
                //    cell.BackgroundColor = FontColour_header;
                //    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                //    table.AddCell(cell);
                //}

                cell = new PdfPCell(new Phrase("Moy", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Coef", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Total", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rang", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Cl", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Min", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Max", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Appréciation", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                /*for (int j = 0; j < table.NumberOfColumns; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString(), timeheader));
                }*/
                table.HeaderRows = 1;

                int nbEvalution = bulletinSequentiel.nbEvaluation;
                int compteur = bulletinSequentiel.nbEvaluation; //servira à chaque fois à indiquer si on a terminé avec une matière
                //NB pour une matière, on a 'bulletinSequentiel.nbEvaluation' ligne dans la liste

                if (bulletinSequentiel.listLigneBulletinSequentiel != null && bulletinSequentiel.listLigneBulletinSequentiel.Count != 0)
                {

                    String indiceCodeGroupe = bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(0).codeGroupe;

                    int i = 0;
                    while (i < bulletinSequentiel.listLigneBulletinSequentiel.Count)
                    {
                        LigneBulletinSequentiel ligneBulletin = bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i);

                        //petit logo
                        cell = new PdfPCell(imagearrow);
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                        cell.Border = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                        table.AddCell(cell);

                        //le nom de la matière et de l'enseignant
                        phrase = new Phrase();
                        phrase.Add(new Chunk(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).nomMat + "\n", timesMarque));
                        phrase.Add(new Chunk(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).nomProf, times));

                        cell = new PdfPCell(phrase);
                        cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                        cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
                        table.AddCell(cell);

                        int nb = 0; //compte le nombre de fois qu'on a trouvé une ligne avec un code de trimestre identique
                        int nb1 = 0; //compte le nombre de fois que l'opération i+j est sortie des limites de la liste des ligne de Bulletin Annuel

                        //la moyenne séquentielle de l'élève sur la matière
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).moyenneSequence, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //le coeficient de la matière
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).coef), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne annuelle coeficié de l'élève pour la matière
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).moyenneSequence * bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).coef, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //le rang de l'élève
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).rangSequentiel), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne de la classe
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).moyenneSeqClasse, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne minimale
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).moyenneSeqMin, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //la moyenne maximale
                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).moyenneSeqMax, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                        //l'appréciation (ou encore la mention)
                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).mention + "\n  - " + bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).appreciation), timesMarque));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        i = (i + compteur) - nb - nb1;
                        //i = i + nb - nb1;

                        //on vérifi si on n'est pas passé dans un autre groupe de matière
                        if (i < bulletinSequentiel.listLigneBulletinSequentiel.Count)
                        {
                            if (bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i).codeGroupe != bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).codeGroupe)
                            {
                                //on insère la ligne contenant la moyenne du groupe
                                //le nom du groupe
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).nomGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                cell.Colspan = 2;
                                table.AddCell(cell);

                                //le total des points du groupe ainsi que le total max des points possibles
                                cell = new PdfPCell(new Phrase("Points : " + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalPointGroupe, 2) + "/" + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalPointMaxGroupe, 2), timeheader));
                                cell.Colspan = nbEvalution + 1;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le total des coeficients
                                cell = new PdfPCell(new Phrase("Total Coef : " + Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalCoefGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                cell.Colspan = 3;
                                table.AddCell(cell);

                                //la moyenne du groupe de l'élève
                                cell = new PdfPCell(new Phrase("Moyenne : " + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).moyenneGroupe, 2), timeheader));
                                cell.Colspan = 4;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                        }
                        else
                        {
                            if (i - 1 < bulletinSequentiel.listLigneBulletinSequentiel.Count)
                            {
                                //on insère la ligne contenant la moyenne du groupe
                                //le nom du groupe
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).nomGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                cell.Colspan = 2;
                                table.AddCell(cell);
                                //le total des points du groupe ainsi que le total max des points possibles
                                cell = new PdfPCell(new Phrase("Points : " + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalPointGroupe, 2) + "/" + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalPointMaxGroupe, 2), timeheader));
                                cell.Colspan = nbEvalution + 1;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le total des coeficients
                                cell = new PdfPCell(new Phrase("Total Coef : " + Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalCoefGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                cell.Colspan = 3;
                                table.AddCell(cell);

                                //la moyenne du groupe de l'élève
                                cell = new PdfPCell(new Phrase("Moyenne : " + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).moyenneGroupe, 2), timeheader));
                                cell.Colspan = 4;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                            else
                            {
                                int index = bulletinSequentiel.listLigneBulletinSequentiel.Count - 1;
                                //on insère la ligne contenant la moyenne du groupe
                                //le nom du groupe
                                cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).nomGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                cell.Colspan = 2;
                                table.AddCell(cell);
                                //le total des points du groupe ainsi que le total max des points possibles
                                cell = new PdfPCell(new Phrase("Points : " + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalPointGroupe, 2) + "/" + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalPointMaxGroupe, 2), timeheader));
                                cell.Colspan = nbEvalution + 1;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le total des coeficients
                                cell = new PdfPCell(new Phrase("Total Coef : " + Convert.ToString(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).totalCoefGroupe), timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                cell.Colspan = 3;
                                table.AddCell(cell);

                                //la moyenne du groupe de l'élève
                                cell = new PdfPCell(new Phrase("Moyenne : " + Math.Round(bulletinSequentiel.listLigneBulletinSequentiel.ElementAt(i - 1).moyenneGroupe, 2), timeheader));
                                cell.Colspan = 4;
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                        }
                    }
                }

                #endregion

                //**********************Debut de la partie bilan travail sequentiel
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                cell.Colspan = nbEvalution + 1;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("TRAVAIL", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Points", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Coef", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);


                cell = new PdfPCell(new Phrase("Moyenne", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rang", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Cl", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Min", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Max", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Appréciation", timeBlock));
                cell.Colspan = nbEvalution + 1;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                if (bulletinSequentiel.listLigneBulletinSequentiel != null)
                {
                    if (bulletinSequentiel.resultatSequentiel != null)
                    {
                        cell = new PdfPCell(new Phrase(" "));
                        cell.Border = Rectangle.NO_BORDER;
                        cell.Colspan = 2;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.resultatSequentiel.point), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.resultatSequentiel.coef), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.resultatSequentiel.moyenne, 2)), timesMarque));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.resultatSequentiel.rang), timesMarque));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.resultatSequentiel.moyenneclasse, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.moyenneMin, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(Math.Round(bulletinSequentiel.moyenneMax, 2)), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        cell = new PdfPCell(new Phrase(Convert.ToString(bulletinSequentiel.resultatSequentiel.mention + " \n  - " + bulletinSequentiel.resultatSequentiel.appreciation), timesMarque));
                        cell.Colspan = nbEvalution + 1;
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }

                //***********************Fin de la partie bilan sequentiel

                //******************************** Debut de la partie discipline

                //on liste les codes des sanctions
                DisciplineDA disciplineDA = new DisciplineDA();
                List<DisciplineBE> LDiscipline = disciplineDA.listerTous();
                int nbDiscipline = LDiscipline.Count;

                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                table.AddCell(cell);
                cell.Colspan = nbEvalution + 1;
                table.AddCell(cell);


                cell = new PdfPCell(new Phrase("DISCIPLINE", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table2.AddCell(cell);

                int nbColonne = 1; //compte le nbre de colonne crée ds le tableau pour la partie Discipline

                for (int i = 0; i < nbDiscipline; i++)
                {
                    if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                    {
                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + ".inj(" + LDiscipline.ElementAt(i).unite + ")", timeBlock));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table2.AddCell(cell);

                        nbColonne++;

                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + ".j(" + LDiscipline.ElementAt(i).unite + ")", timeBlock));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table2.AddCell(cell);

                        nbColonne++;
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + "(" + LDiscipline.ElementAt(i).unite + ")", timeBlock));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table2.AddCell(cell);

                        nbColonne++;
                    }
                }

                cell = new PdfPCell(new Phrase("Mention Conseil de classe", timeBlock));
                //cell.Colspan = nbEvalution + 2;
                //cell.Colspan = 2;
                if (nbColonneStatic + nbEvalution - nbColonne > 0) //pour fusionner le reste de colonne libres
                    cell.Colspan = nbColonneStatic + nbEvalution - nbColonne;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table2.AddCell(cell);




                cell = new PdfPCell(new Phrase(" ", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                table2.AddCell(cell);

                cell = new PdfPCell();

                if (bulletinSequentiel.ListSanction != null)
                {
                    int i = 0;
                    while (i < nbDiscipline)
                    {
                        //représente le code de sanction avec 'inj' (non justifié)
                        //String sanctionCourante = LDiscipline.ElementAt(i).codeSanction + ".inj(" + LDiscipline.ElementAt(i).unite + ")";

                        //représente le code de sanction avec 'j' (jusrifié)
                        //String sanctionCouranteSuivante = LDiscipline.ElementAt(i).codeSanction + ".j(" + LDiscipline.ElementAt(i).unite + ")";

                        if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                        {
                            //on cherche pour le code de sanction en cours, le nombre de sanctions non justifiées
                            bool trouve = false;
                            int nbSanctionJustifiee = 0;
                            int nbSanctionNonJustifiee = 0;
                            for (int j = 0; j < bulletinSequentiel.ListSanction.Count; j++)
                            {
                                if (bulletinSequentiel.ListSanction.ElementAt(j).codesanction == LDiscipline.ElementAt(i).codeSanction)
                                {
                                    trouve = true;
                                    if (bulletinSequentiel.ListSanction.ElementAt(j).etat.Equals("NON JUSTIFIEE"))
                                    {
                                        nbSanctionNonJustifiee = nbSanctionNonJustifiee + bulletinSequentiel.ListSanction.ElementAt(j).quantité;
                                    }
                                    else
                                        nbSanctionJustifiee = nbSanctionJustifiee + bulletinSequentiel.ListSanction.ElementAt(j).quantité;
                                }
                            }

                            //si on a trouver parmis les sanctions de l'élève une correspondant au code de sanction en cours
                            if (trouve == true)
                            {
                                //le nombre de sanction non justifiées
                                cell = new PdfPCell(new Phrase(Convert.ToString(nbSanctionNonJustifiee - nbSanctionJustifiee), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);


                                //le nombre de sanction justifiées
                                cell = new PdfPCell(new Phrase(Convert.ToString(nbSanctionJustifiee), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);
                            }
                            else
                            {
                                //le nombre de sanction non justifiées (on le met à 0)
                                cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);

                                //le nombre de sanction justifiées (on le met à 0)
                                cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);
                            }

                            i = i + 1;
                        }
                        else
                        {
                            //on cherche pour le code de sanction en cours, le nombre de sanctions non justifiées
                            bool trouve = false;
                            int nbSanction = 0;
                            // int nbSanctionNonJustifiee = 0;
                            for (int j = 0; j < bulletinSequentiel.ListSanction.Count; j++)
                            {
                                if (bulletinSequentiel.ListSanction.ElementAt(j).codesanction == LDiscipline.ElementAt(i).codeSanction)
                                {
                                    trouve = true;
                                    nbSanction = nbSanction + bulletinSequentiel.ListSanction.ElementAt(j).quantité;
                                }
                            }

                            //si on a trouver parmis les sanctions de l'élève une correspondant au code de sanction en cours
                            if (trouve == true)
                            {
                                //le nombre de sanction non justifiées
                                cell = new PdfPCell(new Phrase(Convert.ToString(nbSanction), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);

                            }
                            else
                            {
                                //le nombre de sanction non justifiées (on le met à 0)
                                cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table2.AddCell(cell);

                            }

                            i = i + 1;
                        }

                    }
                }

                //on écrit la mention du conseil de classe
                if (bulletinSequentiel.resultatSequentiel != null)
                {
                    cell = new PdfPCell(new Phrase(bulletinSequentiel.resultatSequentiel.decision + ". \n     " + bulletinSequentiel.resultatSequentiel.remarque, timesMarque));
                    //cell.Colspan = nbEvalution + 2;
                    //cell.Colspan =  2;
                    if (nbColonneStatic + nbEvalution - nbColonne > 0) //pour fusionner le nombre de colonne libre
                        cell.Colspan = nbColonneStatic + nbEvalution - nbColonne;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table2.AddCell(cell);
                }

                //Fin de la partie discipline

                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table2.AddCell(cell);
                table2.AddCell(cell);
                table2.AddCell(cell);
                table2.AddCell(cell);
                table2.AddCell(cell);
                table2.AddCell(cell);
                table2.AddCell(cell);
                table2.AddCell(cell);
                table2.AddCell(cell);
                table2.AddCell(cell);
                cell.Colspan = nbEvalution + 1;
                table2.AddCell(cell);

                doc.Add(creerEntete());
                doc.Add(titre);
                doc.Add(creerEnteteBulletin(bulletinSequentiel.eleve, bulletinSequentiel.classe, bulletinSequentiel.annee, effectifClasse));
                doc.Add(table);
                doc.Add(table2);

                if (profTitulaire != null)
                    doc.Add(creerPiedsBulletin(profTitulaire.nomProf, parametre));
                else
                    doc.Add(creerPiedsBulletin("", parametre));

                doc.NewPage();

            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        //fonction qui détermine la prochaine classe d'un élève en fonction de son résultat annuel
        public String getInfosNewNiveauEleve(ResultatAnnuelBE resultat, String codeClasse)
        {
            NiveauDA niveauDA = new NiveauDA();
            List<NiveauBE> LNiveau = niveauDA.listerSuivantCritere("niveau = " + resultat.newNiveau);
            //on recherche le nom de la prochaine classe de l'élève
            if (LNiveau != null && LNiveau.Count != 0)
            {
                String nomFutureNiveau = LNiveau.ElementAt(0).nomNiveau;

                //on recherche la série actuelle de l'élève
                ClasseDA classeDA = new ClasseDA();
                ClasseBE classe = new ClasseBE();
                classe.codeClasse = codeClasse;
                classe = classeDA.rechercher(classe);

                if (classe != null)
                {

                    String nomSerieActuel = classe.codeSerie;
                    NiveauBE niveauActuel = new NiveauBE();
                    niveauActuel.codeNiveau = classe.codeNiveau;

                    niveauActuel = niveauDA.rechercher(niveauActuel);

                    String ChaineResultat = "";

                    //si l'élève est Admis
                    if (resultat.decision.Equals("Admis"))
                    {
                        switch (niveauActuel.niveau)
                        {
                            case 2:
                                ChaineResultat = "Admis en " + nomFutureNiveau;
                                break;
                            case 4:
                                ChaineResultat = "Admis en " + nomFutureNiveau;
                                break;
                            case 5:
                                ChaineResultat = "Admis en " + nomFutureNiveau;
                                break;
                            case 6:
                                ChaineResultat = "Admis en " + nomFutureNiveau + "si examen (PROBATOIRE)";
                                break;
                            case 7:
                                ChaineResultat = "Admis au supérieur si examen (BAC)";
                                break;
                            default:
                                ChaineResultat = "Admis en " + nomFutureNiveau + " " + nomSerieActuel;
                                break;

                        }

                        return ChaineResultat;

                    }
                    else
                    { //si l'élève a Echec
                        ChaineResultat = "Reprend la " + niveauActuel.nomNiveau + " " + nomSerieActuel;
                        return ChaineResultat;
                    }
                }
                else return null;
            }
            else if (resultat.newNiveau == 8)
            { //si l'élève était en terminal et il est Admis
                String ChaineResultat;
                ChaineResultat = "Admis au supérieur si examen (BAC)";
                return ChaineResultat;
            }
            return null;
        }

        //*** Fonction qui génère l'état du bulletin Séquentiel d'un élève
        public void etatDesSanctionsSequentiellesDuneClasse(List<EleveBE> LEleve, String codeClasse, String codeSequence, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfTimes2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Font timesMarque = new Font(bfTimes, 10, Font.BOLD);
                Font timeBlock = new Font(bfTimes2, 9);
                var FontColour_header = new BaseColor(134, 181, 232);
                var FontColour_groupe = new BaseColor(200, 221, 226);
                Font TextFont_Header = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontColour_header);
                Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                int nbColonneStatic = 3; //le nbre de colonne static du tableau, à laquelle ont ajoutera des colonnes qui seront crée dynamiquement

                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                //************ DEBUT Partie renseignant les infos d'entete du fichier

                ClasseDA classeDA = new ClasseDA();
                ClasseBE classe = new ClasseBE();
                classe.codeClasse = codeClasse;
                classe = classeDA.rechercher(classe);
                PdfPTable tableEntete = new PdfPTable(2);
                float[] largeurEntete = new float[2];
                largeurEntete[0] = 0.2f;
                largeurEntete[1] = 0.8f;

                tableEntete.SetWidths(largeurEntete);

                tableEntete.WidthPercentage = 95f;

                if (classe != null)
                {
                    cell = new PdfPCell(new Phrase("Classe : ", times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    tableEntete.AddCell(cell);

                    cell = new PdfPCell(new Phrase(classe.codeClasse + " - " + classe.nomClasse, timeheader));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    tableEntete.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Année Académique : ", times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    tableEntete.AddCell(cell);

                    cell = new PdfPCell(new Phrase((annee - 1) + "/" + annee, timeheader));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    tableEntete.AddCell(cell);
                }

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 2;
                tableEntete.AddCell(cell);

                //************ FIN Partie renseignant les infos d'entete du fichier


                //on liste les codes des sanctions
                DisciplineDA disciplineDA = new DisciplineDA();
                List<DisciplineBE> LDiscipline = disciplineDA.listerTous();
                int nbDiscipline = LDiscipline.Count;

                int nbColonneDynamique = 0; //compte le nbre de colonne crée ds le tableau pour la partie Discipline

                for (int i = 0; i < nbDiscipline; i++)
                {
                    if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                    {
                        nbColonneDynamique = nbColonneDynamique + 2;
                    }
                    else
                    {
                        nbColonneDynamique = nbColonneDynamique + 1;
                    }
                }

                PdfPTable table = new PdfPTable(nbColonneStatic + nbColonneDynamique);
                int dim = nbColonneStatic + nbColonneDynamique;
                float[] largeur = new float[dim];
                largeur[0] = 0.2f;
                largeur[1] = 0.6f;
                largeur[2] = 1.3f;

                for (int i = 0; i < nbColonneDynamique; i++)
                    largeur[i + 3] = 0.5f;

                table.SetWidths(largeur);
                table.WidthPercentage = 95f;
                //la table possède 6 colonnes
                /*
                 * 0 : année
                 * 1 : matricule
                 * 2 : codematière
                 * 3 : codesequence
                 * 4 : moyenne
                 * 5:  mention
                 */
                //float[] widths = new float[] { 1.5f, 8f, 1f, 1f, 1f, 1f, 1f, 1.5f };
                //table.SetWidths(widths);

                //******************************** Debut de la partie discipline



                //cell = new PdfPCell(new Phrase(" "));
                //cell.Border = Rectangle.NO_BORDER;
                //table.AddCell(cell);

                //table.AddCell(cell);
                //table.AddCell(cell);
                //table.AddCell(cell);
                //table.AddCell(cell);
                //table.AddCell(cell);
                //table.AddCell(cell);
                //table.AddCell(cell);
                //cell.Colspan = nbEvalution + 1;
                //table.AddCell(cell);

                cell = new PdfPCell(new Phrase("No", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("MATRICULE", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("NOM", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                int nbColonne = 1; //compte le nbre de colonne crée ds le tableau pour la partie Discipline

                for (int i = 0; i < nbDiscipline; i++)
                {
                    if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                    {
                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + ".inj(" + LDiscipline.ElementAt(i).unite + ")", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        nbColonne++;

                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + ".j(" + LDiscipline.ElementAt(i).unite + ")", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        nbColonne++;
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + "(" + LDiscipline.ElementAt(i).unite + ")", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        nbColonne++;
                    }
                }

                /*cell = new PdfPCell(new Phrase("Abs.inj(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Abs.j(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Retards(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Avert.", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Blâmes", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Exclu.(J)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);*/

                SanctionnerDA sanctionnerDA = new SanctionnerDA();

                //on boucle sur la liste des élèves
                for (int k = 0; k < LEleve.Count; k++)
                {
                    //on appelle la fonction qui défini les avertissements et les blâmes de l'élève
                    GenererBulletinsSequentielBL genererBulletinsSequentielBL = new GenererBulletinsSequentielBL();
                    genererBulletinsSequentielBL.DefinirLesAvertissementsEtBlamesDunEleve(LEleve.ElementAt(k).matricule, codeSequence, annee);

                    //on recher la liste des sanctions séauentielles de l'élèves i
                    //************************ on charge les disciplines de l'élève
                    List<SanctionnerBE> ListSanction = sanctionnerDA.getListSanctionSequentielleEleve(LEleve.ElementAt(k).matricule, annee, codeSequence);

                    //le num de l'élèce
                    cell = new PdfPCell(new Phrase(Convert.ToString(k + 1), times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);

                    //le matricule de l'élèce
                    cell = new PdfPCell(new Phrase(LEleve.ElementAt(k).matricule, times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);

                    //le nom de l'élèce
                    cell = new PdfPCell(new Phrase(LEleve.ElementAt(k).nom, times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    table.AddCell(cell);

                    if (ListSanction != null)
                    {
                        int i = 0;
                        while (i < nbDiscipline)
                        {
                            //représente le code de sanction avec 'inj' (non justifié)
                            //String sanctionCourante = LDiscipline.ElementAt(i).codeSanction + ".inj(" + LDiscipline.ElementAt(i).unite + ")";

                            //représente le code de sanction avec 'j' (jusrifié)
                            //String sanctionCouranteSuivante = LDiscipline.ElementAt(i).codeSanction + ".j(" + LDiscipline.ElementAt(i).unite + ")";

                            if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                            {
                                //on cherche pour le code de sanction en cours, le nombre de sanctions non justifiées
                                bool trouve = false;
                                int nbSanctionJustifiee = 0;
                                int nbSanctionNonJustifiee = 0;
                                for (int j = 0; j < ListSanction.Count; j++)
                                {
                                    if (ListSanction.ElementAt(j).codesanction == LDiscipline.ElementAt(i).codeSanction)
                                    {
                                        trouve = true;
                                        if (ListSanction.ElementAt(j).etat.Equals("NON JUSTIFIEE"))
                                        {
                                            nbSanctionNonJustifiee = nbSanctionNonJustifiee + ListSanction.ElementAt(j).quantité;
                                        }
                                        else
                                            nbSanctionJustifiee = nbSanctionJustifiee + ListSanction.ElementAt(j).quantité;
                                    }
                                }

                                //si on a trouver parmis les sanctions de l'élève une correspondant au code de sanction en cours
                                if (trouve == true)
                                {
                                    //le nombre de sanction non justifiées
                                    cell = new PdfPCell(new Phrase(Convert.ToString(nbSanctionNonJustifiee - nbSanctionJustifiee), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);

                                    //le nombre de sanction justifiées
                                    cell = new PdfPCell(new Phrase(Convert.ToString(nbSanctionJustifiee), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);
                                }
                                else
                                {
                                    //le nombre de sanction non justifiées (on le met à 0)
                                    cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);

                                    //le nombre de sanction justifiées (on le met à 0)
                                    cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);
                                }

                                i = i + 1;
                            }
                            else
                            {
                                //on cherche pour le code de sanction en cours, le nombre de sanctions non justifiées
                                bool trouve = false;
                                int nbSanction = 0;
                                // int nbSanctionNonJustifiee = 0;
                                for (int j = 0; j < ListSanction.Count; j++)
                                {
                                    if (ListSanction.ElementAt(j).codesanction == LDiscipline.ElementAt(i).codeSanction)
                                    {
                                        trouve = true;
                                        nbSanction = nbSanction + ListSanction.ElementAt(j).quantité;
                                    }
                                }

                                //si on a trouver parmis les sanctions de l'élève une correspondant au code de sanction en cours
                                if (trouve == true)
                                {
                                    //le nombre de sanction non justifiées
                                    cell = new PdfPCell(new Phrase(Convert.ToString(nbSanction), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);

                                }
                                else
                                {
                                    //le nombre de sanction non justifiées (on le met à 0)
                                    cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);

                                }

                                i = i + 1;
                            }

                        }
                    }
                    else
                    {  //Dans le cas où l'élève n'a aucune sanction, on met 0 partout

                        int i = 0;
                        while (i < nbDiscipline)
                        {
                            //on met 0
                            cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);
                        }
                    }

                }

                //Fin de la partie discipline


                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(tableEntete);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                //doc.Add(table2);
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        //*** Fonction qui génère l'état du bulletin trimestriel d'un élève
        public void etatDesSanctionsTrimestriellesDuneClasse(List<EleveBE> LEleve, String codeClasse, String codeTrimestre, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfTimes2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Font timesMarque = new Font(bfTimes, 10, Font.BOLD);
                Font timeBlock = new Font(bfTimes2, 9);
                var FontColour_header = new BaseColor(134, 181, 232);
                var FontColour_groupe = new BaseColor(200, 221, 226);
                Font TextFont_Header = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontColour_header);
                Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                int nbColonneStatic = 3; //le nbre de colonne static du tableau, à laquelle ont ajoutera des colonnes qui seront crée dynamiquement

                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                //************ DEBUT Partie renseignant les infos d'entete du fichier

                ClasseDA classeDA = new ClasseDA();
                ClasseBE classe = new ClasseBE();
                classe.codeClasse = codeClasse;
                classe = classeDA.rechercher(classe);
                PdfPTable tableEntete = new PdfPTable(1);
                tableEntete.WidthPercentage = 90f;

                if (classe != null)
                {
                    cell = new PdfPCell(new Phrase("Classe : " + classe.codeClasse + " - " + classe.nomClasse + "\n \n Année Académique " + (annee - 1) + "/" + annee, timeheader));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    tableEntete.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", timeheader));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    tableEntete.AddCell(cell);
                }

                //************ FIN Partie renseignant les infos d'entete du fichier


                //on liste les codes des sanctions
                DisciplineDA disciplineDA = new DisciplineDA();
                List<DisciplineBE> LDiscipline = disciplineDA.listerTous();
                int nbDiscipline = LDiscipline.Count;

                int nbColonneDynamique = 0; //compte le nbre de colonne crée ds le tableau pour la partie Discipline

                for (int i = 0; i < nbDiscipline; i++)
                {
                    if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                    {
                        nbColonneDynamique = nbColonneDynamique + 2;
                    }
                    else
                    {
                        nbColonneDynamique = nbColonneDynamique + 1;
                    }
                }

                PdfPTable table = new PdfPTable(nbColonneStatic + nbColonneDynamique);
                int dim = nbColonneStatic + nbColonneDynamique;
                float[] largeur = new float[dim];
                largeur[0] = 0.2f;
                largeur[1] = 0.6f;
                largeur[2] = 1.3f;

                for (int i = 0; i < nbColonneDynamique; i++)
                    largeur[i + 3] = 0.5f;

                table.SetWidths(largeur);
                table.WidthPercentage = 90f;
                //la table possède 6 colonnes
                /*
                 * 0 : année
                 * 1 : matricule
                 * 2 : codematière
                 * 3 : codesequence
                 * 4 : moyenne
                 * 5:  mention
                 */
                //float[] widths = new float[] { 1.5f, 8f, 1f, 1f, 1f, 1f, 1f, 1.5f };
                //table.SetWidths(widths);

                //******************************** Debut de la partie discipline



                //cell = new PdfPCell(new Phrase(" "));
                //cell.Border = Rectangle.NO_BORDER;
                //table.AddCell(cell);

                //table.AddCell(cell);
                //table.AddCell(cell);
                //table.AddCell(cell);
                //table.AddCell(cell);
                //table.AddCell(cell);
                //table.AddCell(cell);
                //table.AddCell(cell);
                //cell.Colspan = nbEvalution + 1;
                //table.AddCell(cell);

                cell = new PdfPCell(new Phrase("No", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("MATRICULE", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("NOM", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                int nbColonne = 1; //compte le nbre de colonne crée ds le tableau pour la partie Discipline

                for (int i = 0; i < nbDiscipline; i++)
                {
                    if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                    {
                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + ".inj(" + LDiscipline.ElementAt(i).unite + ")", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        nbColonne++;

                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + ".j(" + LDiscipline.ElementAt(i).unite + ")", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        nbColonne++;
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + "(" + LDiscipline.ElementAt(i).unite + ")", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        nbColonne++;
                    }
                }

                /*cell = new PdfPCell(new Phrase("Abs.inj(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Abs.j(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Retards(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Avert.", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Blâmes", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Exclu.(J)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);*/

                SanctionnerDA sanctionnerDA = new SanctionnerDA();

                //on boucle sur la liste des élèves
                for (int k = 0; k < LEleve.Count; k++)
                {
                    //on recher la liste des sanctions séauentielles de l'élèves i
                    //************************ on charge les disciplines de l'élève
                    List<SanctionnerBE> ListSanction = sanctionnerDA.getListSanctionTrimestrielleEleve(LEleve.ElementAt(k).matricule, annee, codeTrimestre);

                    //le No de l'élèce
                    cell = new PdfPCell(new Phrase(Convert.ToString(k + 1), times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);

                    //le matricule de l'élèce
                    cell = new PdfPCell(new Phrase(LEleve.ElementAt(k).matricule, times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);

                    //le nom de l'élèce
                    cell = new PdfPCell(new Phrase(LEleve.ElementAt(k).nom, times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);

                    if (ListSanction != null)
                    {
                        int i = 0;
                        while (i < nbDiscipline)
                        {
                            //représente le code de sanction avec 'inj' (non justifié)
                            //String sanctionCourante = LDiscipline.ElementAt(i).codeSanction + ".inj(" + LDiscipline.ElementAt(i).unite + ")";

                            //représente le code de sanction avec 'j' (jusrifié)
                            //String sanctionCouranteSuivante = LDiscipline.ElementAt(i).codeSanction + ".j(" + LDiscipline.ElementAt(i).unite + ")";

                            if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                            {
                                //on cherche pour le code de sanction en cours, le nombre de sanctions non justifiées
                                bool trouve = false;
                                int nbSanctionJustifiee = 0;
                                int nbSanctionNonJustifiee = 0;
                                for (int j = 0; j < ListSanction.Count; j++)
                                {
                                    if (ListSanction.ElementAt(j).codesanction == LDiscipline.ElementAt(i).codeSanction)
                                    {
                                        trouve = true;
                                        if (ListSanction.ElementAt(j).etat.Equals("NON JUSTIFIEE"))
                                        {
                                            nbSanctionNonJustifiee = nbSanctionNonJustifiee + ListSanction.ElementAt(j).quantité;
                                        }
                                        else
                                            nbSanctionJustifiee = nbSanctionJustifiee + ListSanction.ElementAt(j).quantité;
                                    }
                                }

                                //si on a trouver parmis les sanctions de l'élève une correspondant au code de sanction en cours
                                if (trouve == true)
                                {
                                    //le nombre de sanction non justifiées
                                    cell = new PdfPCell(new Phrase(Convert.ToString(nbSanctionNonJustifiee - nbSanctionJustifiee), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);

                                    //le nombre de sanction justifiées
                                    cell = new PdfPCell(new Phrase(Convert.ToString(nbSanctionJustifiee), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);
                                }
                                else
                                {
                                    //le nombre de sanction non justifiées (on le met à 0)
                                    cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);

                                    //le nombre de sanction justifiées (on le met à 0)
                                    cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);
                                }

                                i = i + 1;
                            }
                            else
                            {
                                //on cherche pour le code de sanction en cours, le nombre de sanctions non justifiées
                                bool trouve = false;
                                int nbSanction = 0;
                                // int nbSanctionNonJustifiee = 0;
                                for (int j = 0; j < ListSanction.Count; j++)
                                {
                                    if (ListSanction.ElementAt(j).codesanction == LDiscipline.ElementAt(i).codeSanction)
                                    {
                                        trouve = true;
                                        nbSanction = nbSanction + ListSanction.ElementAt(j).quantité;
                                    }
                                }

                                //si on a trouver parmis les sanctions de l'élève une correspondant au code de sanction en cours
                                if (trouve == true)
                                {
                                    //le nombre de sanction non justifiées
                                    cell = new PdfPCell(new Phrase(Convert.ToString(nbSanction), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);

                                }
                                else
                                {
                                    //le nombre de sanction non justifiées (on le met à 0)
                                    cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);

                                }

                                i = i + 1;
                            }

                        }
                    }
                    else
                    {  //Dans le cas où l'élève n'a aucune sanction, on met 0 partout

                        int i = 0;
                        while (i < nbDiscipline)
                        {
                            //on met 0
                            cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);
                        }
                    }

                }

                //Fin de la partie discipline


                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(tableEntete);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                //doc.Add(table2);
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        //*** Fonction qui génère l'état du bulletin trimestriel d'un élève
        public void etatDesSanctionsAnnuellesDuneClasse(List<EleveBE> LEleve, String codeClasse, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfTimes2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Font timesMarque = new Font(bfTimes, 10, Font.BOLD);
                Font timeBlock = new Font(bfTimes2, 9);
                var FontColour_header = new BaseColor(134, 181, 232);
                var FontColour_groupe = new BaseColor(200, 221, 226);
                Font TextFont_Header = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontColour_header);
                Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                int nbColonneStatic = 3; //le nbre de colonne static du tableau, à laquelle ont ajoutera des colonnes qui seront crée dynamiquement

                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                //************ DEBUT Partie renseignant les infos d'entete du fichier

                ClasseDA classeDA = new ClasseDA();
                ClasseBE classe = new ClasseBE();
                classe.codeClasse = codeClasse;
                classe = classeDA.rechercher(classe);
                PdfPTable tableEntete = new PdfPTable(1);
                tableEntete.WidthPercentage = 90f;

                if (classe != null)
                {
                    cell = new PdfPCell(new Phrase("Classe : " + classe.codeClasse + " - " + classe.nomClasse + "\n \n Année Académique " + (annee - 1) + "/" + annee, timeheader));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    tableEntete.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", timeheader));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    tableEntete.AddCell(cell);
                }

                //************ FIN Partie renseignant les infos d'entete du fichier

                //on liste les codes des sanctions
                DisciplineDA disciplineDA = new DisciplineDA();
                List<DisciplineBE> LDiscipline = disciplineDA.listerTous();

                int nbDiscipline = LDiscipline.Count;

                int nbColonneDynamique = 0; //compte le nbre de colonne crée ds le tableau pour la partie Discipline

                for (int i = 0; i < nbDiscipline; i++)
                {
                    if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                    {
                        nbColonneDynamique = nbColonneDynamique + 2;
                    }
                    else
                    {
                        nbColonneDynamique = nbColonneDynamique + 1;
                    }
                }

                PdfPTable table = new PdfPTable(nbColonneStatic + nbColonneDynamique);
                int dim = nbColonneStatic + nbColonneDynamique;
                float[] largeur = new float[dim];
                largeur[0] = 0.2f;
                largeur[1] = 0.6f;
                largeur[2] = 1.3f;

                for (int i = 0; i < nbColonneDynamique; i++)
                    largeur[i + 3] = 0.5f;

                table.SetWidths(largeur);
                table.WidthPercentage = 90f;
                //la table possède 6 colonnes
                /*
                 * 0 : année
                 * 1 : matricule
                 * 2 : codematière
                 * 3 : codesequence
                 * 4 : moyenne
                 * 5:  mention
                 */
                //float[] widths = new float[] { 1.5f, 8f, 1f, 1f, 1f, 1f, 1f, 1.5f };
                //table.SetWidths(widths);

                //******************************** Debut de la partie discipline



                //cell = new PdfPCell(new Phrase(" "));
                //cell.Border = Rectangle.NO_BORDER;
                //table.AddCell(cell);

                //table.AddCell(cell);
                //table.AddCell(cell);
                //table.AddCell(cell);
                //table.AddCell(cell);
                //table.AddCell(cell);
                //table.AddCell(cell);
                //table.AddCell(cell);
                //cell.Colspan = nbEvalution + 1;
                //table.AddCell(cell);

                cell = new PdfPCell(new Phrase("No", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("MATRICULE", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("NOM", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                int nbColonne = 1; //compte le nbre de colonne crée ds le tableau pour la partie Discipline

                for (int i = 0; i < nbDiscipline; i++)
                {
                    if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                    {
                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + ".inj(" + LDiscipline.ElementAt(i).unite + ")", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        nbColonne++;

                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + ".j(" + LDiscipline.ElementAt(i).unite + ")", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        nbColonne++;
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase(LDiscipline.ElementAt(i).codeSanction + "(" + LDiscipline.ElementAt(i).unite + ")", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        nbColonne++;
                    }
                }

                /*cell = new PdfPCell(new Phrase("Abs.inj(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Abs.j(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Retards(H)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Avert.", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Blâmes", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Exclu.(J)", timeBlock));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);*/

                SanctionnerDA sanctionnerDA = new SanctionnerDA();

                //on boucle sur la liste des élèves
                for (int k = 0; k < LEleve.Count; k++)
                {
                    //on recher la liste des sanctions séauentielles de l'élèves i
                    //************************ on charge les disciplines de l'élève
                    List<SanctionnerBE> ListSanction = sanctionnerDA.getListSanctionAnuelleEleve(LEleve.ElementAt(k).matricule, annee);

                    //le No de l'élève
                    cell = new PdfPCell(new Phrase(Convert.ToString(k + 1), times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);

                    //le matricule de l'élèce
                    cell = new PdfPCell(new Phrase(LEleve.ElementAt(k).matricule, times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);

                    //le nom de l'élèce
                    cell = new PdfPCell(new Phrase(LEleve.ElementAt(k).nom, times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);



                    if (ListSanction != null)
                    {
                        int i = 0;
                        while (i < nbDiscipline)
                        {
                            //représente le code de sanction avec 'inj' (non justifié)
                            //String sanctionCourante = LDiscipline.ElementAt(i).codeSanction + ".inj(" + LDiscipline.ElementAt(i).unite + ")";

                            //représente le code de sanction avec 'j' (jusrifié)
                            //String sanctionCouranteSuivante = LDiscipline.ElementAt(i).codeSanction + ".j(" + LDiscipline.ElementAt(i).unite + ")";

                            if (LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("absence") || LDiscipline.ElementAt(i).nomSanction.ToLower().Contains("retard"))
                            {
                                //on cherche pour le code de sanction en cours, le nombre de sanctions non justifiées
                                bool trouve = false;
                                int nbSanctionJustifiee = 0;
                                int nbSanctionNonJustifiee = 0;
                                for (int j = 0; j < ListSanction.Count; j++)
                                {
                                    if (ListSanction.ElementAt(j).codesanction == LDiscipline.ElementAt(i).codeSanction)
                                    {
                                        trouve = true;
                                        if (ListSanction.ElementAt(j).etat.Equals("NON JUSTIFIEE"))
                                        {
                                            nbSanctionNonJustifiee = nbSanctionNonJustifiee + ListSanction.ElementAt(j).quantité;
                                        }
                                        else
                                            nbSanctionJustifiee = nbSanctionJustifiee + ListSanction.ElementAt(j).quantité;
                                    }
                                }

                                //si on a trouver parmis les sanctions de l'élève une correspondant au code de sanction en cours
                                if (trouve == true)
                                {
                                    //le nombre de sanction non justifiées
                                    cell = new PdfPCell(new Phrase(Convert.ToString(nbSanctionNonJustifiee - nbSanctionJustifiee), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);

                                    //le nombre de sanction justifiées
                                    cell = new PdfPCell(new Phrase(Convert.ToString(nbSanctionJustifiee), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);
                                }
                                else
                                {
                                    //le nombre de sanction non justifiées (on le met à 0)
                                    cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);

                                    //le nombre de sanction justifiées (on le met à 0)
                                    cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);
                                }

                                i = i + 1;
                            }
                            else
                            {
                                //on cherche pour le code de sanction en cours, le nombre de sanctions non justifiées
                                bool trouve = false;
                                int nbSanction = 0;
                                // int nbSanctionNonJustifiee = 0;
                                for (int j = 0; j < ListSanction.Count; j++)
                                {
                                    if (ListSanction.ElementAt(j).codesanction == LDiscipline.ElementAt(i).codeSanction)
                                    {
                                        trouve = true;
                                        nbSanction = nbSanction + ListSanction.ElementAt(j).quantité;
                                    }
                                }

                                //si on a trouver parmis les sanctions de l'élève une correspondant au code de sanction en cours
                                if (trouve == true)
                                {
                                    //le nombre de sanction non justifiées
                                    cell = new PdfPCell(new Phrase(Convert.ToString(nbSanction), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);

                                }
                                else
                                {
                                    //le nombre de sanction non justifiées (on le met à 0)
                                    cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                    table.AddCell(cell);

                                }

                                i = i + 1;
                            }

                        }
                    }
                    else
                    {  //Dans le cas où l'élève n'a aucune sanction, on met 0 partout

                        int i = 0;
                        while (i < nbDiscipline)
                        {
                            //on met 0
                            cell = new PdfPCell(new Phrase(Convert.ToString(0), times));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);
                        }
                    }

                }

                //Fin de la partie discipline


                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(tableEntete);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                //doc.Add(table2);
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void etatCertificatScolarite(int annee, EleveBE eleve, ClasseBE classe, InscrireBE inscription, ParametresBE parametre)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfTimes2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Font timesMarque = new Font(bfTimes, 10, Font.BOLD);
                Font timeBlock = new Font(bfTimes, 10, Font.BOLD);
                var FontColour_header = new BaseColor(134, 181, 232);
                var FontColour_groupe = new BaseColor(200, 221, 226);
                Font TextFont_Header = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontColour_header);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                int nbColonneStatic = 4; //le nbre de colonne static du tableau, à laquelle ont ajoutera des colonnes qui seront crée dynamiquement

                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                PdfPTable table = new PdfPTable(nbColonneStatic);
                int dim = nbColonneStatic;
                float[] largeur = new float[dim];
                largeur[0] = 1.5f;
                largeur[1] = 2f;
                largeur[2] = 0.5f;
                largeur[3] = 2f;

                table.SetWidths(largeur);
                table.WidthPercentage = 90f;
                //la table possède 6 colonnes
                /*
                 * 0 : année
                 * 1 : matricule
                 * 2 : codematière
                 * 3 : codesequence
                 * 4 : moyenne
                 * 5:  mention
                 */
                //float[] widths = new float[] { 1.5f, 8f, 1f, 1f, 1f, 1f, 1f, 1.5f };
                //table.SetWidths(widths);
                //table.WidthPercentage = 100f;

                table.HeaderRows = 1;


                // début une ligne vide
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table.AddCell(cell);
                // fin une ligne vide

                //***********Debut de la partie bilan travail sequentiel

                //******************* 1ere ligne
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("No", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                string initialEtablissement = "";
                if (!parametre.nomEcole.Equals(""))
                {
                    for (int i = 0; i < parametre.nomEcole.Split(' ').Length; i++)
                    {
                        initialEtablissement += parametre.nomEcole.Split(' ')[i].ElementAt(0).ToString().ToUpper();
                    }
                }

                string initialEleve = "";
                if (!eleve.nom.Equals(""))
                {
                    for (int i = 0; i < eleve.nom.Split(' ').Length; i++)
                    {
                        initialEleve += eleve.nom.Split(' ')[i].ElementAt(0).ToString().ToUpper();
                    }
                }

                string Num = parametre.annee + "/" + eleve.matricule + "/" + initialEleve + "/" + classe.codeClasse + "/" + initialEtablissement;
                cell = new PdfPCell(new Phrase(Num, timeheader));
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                // début une ligne vide
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table.AddCell(cell);
                // fin une ligne vide

                //******************* 2ème ligne

                cell = new PdfPCell(new Phrase("ANNEE SCOLAIRE", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase((annee - 1) + " / " + annee, timeheader));
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                //****** 3ème ligne

                cell = new PdfPCell(new Phrase("SCHOOL YEAR", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                // début une ligne vide
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table.AddCell(cell);
                // fin une ligne vide

                //****** 4ème ligne

                cell = new PdfPCell(new Phrase("Le " + parametre.titreDuChef + " du " + parametre.nomEcole + ", soussigné, atteste que : ", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Colspan = 3;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                //****** 5ème ligne

                cell = new PdfPCell(new Phrase("Le Head of " + parametre.nomEcole + ", undersigned certifies that : ", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Colspan = 3;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                // début une ligne vide
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table.AddCell(cell);
                // fin une ligne vide

                //****** 6ème ligne

                cell = new PdfPCell(new Phrase("M. / Mlle", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(eleve.nom.ToUpper(), timeheader));
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Colspan = 3;
                table.AddCell(cell);

                //****** 7ème ligne

                cell = new PdfPCell(new Phrase("Mr. / Miss", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Colspan = 3;
                table.AddCell(cell);

                // début une ligne vide
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table.AddCell(cell);
                // fin une ligne vide

                //****** 8ème ligne

                cell = new PdfPCell(new Phrase("Né(e) le", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(eleve.dateNaissance.ToShortDateString(), timeheader));
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("à", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(eleve.lieuNaissance.ToUpper(), timeheader));
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                //****** 9ème ligne

                cell = new PdfPCell(new Phrase("Born at", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("at", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                // début une ligne vide
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table.AddCell(cell);
                // fin une ligne vide

                //****** 10ème ligne

                cell = new PdfPCell(new Phrase("Est inscrit(e) au " + parametre.nomEcole + " sous le matricule", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Mle : ", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(eleve.matricule, timeheader));
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                //****** 11ème ligne

                cell = new PdfPCell(new Phrase("Is registered in " + parametre.nomEcole + " under de registration number", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Reg No", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                // début une ligne vide
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table.AddCell(cell);
                // fin une ligne vide

                //****** 12ème ligne

                cell = new PdfPCell(new Phrase("Classe : ", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(inscription.codeClasse, timeheader));
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Série : ", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(classe.codeSerie, timeheader));
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                //****** 13ème ligne

                cell = new PdfPCell(new Phrase("Class", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Serie ", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                // début une ligne vide
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table.AddCell(cell);
                // fin une ligne vide



                // début une ligne vide
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table.AddCell(cell);
                // fin une ligne vide

                // début une ligne vide
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table.AddCell(cell);
                // fin une ligne vide

                //****** 16ème ligne

                cell = new PdfPCell(new Phrase("En foi de quoi le présent certificat est établi et lui est délivré pour servir et valoir ce que de droit. ", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 4;
                table.AddCell(cell);

                //****** 17ème ligne

                cell = new PdfPCell(new Phrase("In witness whereof the present certificate is establish ", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 4;
                table.AddCell(cell);

                // début une ligne vide
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table.AddCell(cell);
                // fin une ligne vide

                // début une ligne vide
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table.AddCell(cell);
                // fin une ligne vide

                // début une ligne vide
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table.AddCell(cell);
                // fin une ligne vide

                //****** 18ème ligne

                cell = new PdfPCell(new Phrase(parametre.ville + ", le/the ", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                cell.Colspan = 3;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                // début une ligne vide
                cell = new PdfPCell(new Phrase(" "));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table.AddCell(cell);
                // fin une ligne vide

                //****** 19ème ligne

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Le " + parametre.titreDuChef, timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                //****** 20ème ligne

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("The head of school ", timeBlock));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);



                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                //doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void etatProfilAcademiqueDunEleve(List<LigneProfilAcademique> ListLigneProfilAcademique, EleveBE eleve, ClasseBE classe)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfTimes2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Font timesMarque = new Font(bfTimes, 10, Font.BOLD);
                Font timeBlock = new Font(bfTimes2, 9);
                var FontColour_header = new BaseColor(134, 181, 232);
                var FontColour_groupe = new BaseColor(200, 221, 226);
                Font TextFont_Header = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontColour_header);
                Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region contenu concernant les grid

                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                int nbColonneStatic = 9;

                PdfPTable table = new PdfPTable(nbColonneStatic);
                int dim = nbColonneStatic;
                float[] largeur = new float[dim];
                largeur[0] = 4.9f;

                for (int i = 0; i < 9; i++)
                    largeur[i] = 1.5f;


                table.SetWidths(largeur);
                table.WidthPercentage = 90f;
                //la table possède 9 colonnes
                /*
                 * 0 : année
                 * 1 : codeClasse
                 * 2 : codematière
                 * 3 : codesequence
                 * 4 : codeTrimestre
                 * 5:  moyenne
                 * 6: mention
                 * 7 : coef
                 * 8 : rang
                 */
                //float[] widths = new float[] { 1.5f, 8f, 1f, 1f, 1f, 1f, 1f, 1.5f };
                //table.SetWidths(widths);
                cell = new PdfPCell(new Phrase("Année", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Classe", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Matière", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);
                
                cell = new PdfPCell(new Phrase("Trimestre", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Séquence", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moyenne", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Mention", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Coef", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rang", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);


                table.HeaderRows = 1;

                if (ListLigneProfilAcademique != null && ListLigneProfilAcademique.Count != 0)
                {

                    for (int i = 0; i < ListLigneProfilAcademique.Count; i++)
                    {
                        if (i == 0)
                        {
                            //l'année
                            cell = new PdfPCell(new Phrase(Convert.ToString(ListLigneProfilAcademique.ElementAt(i).annee), times));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);

                            //la classe
                            cell = new PdfPCell(new Phrase(ListLigneProfilAcademique.ElementAt(i).codeClasse, times));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);

                            //la matière
                            cell = new PdfPCell(new Phrase(ListLigneProfilAcademique.ElementAt(i).codeMatiere, times));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);

                            //le trimestre
                            cell = new PdfPCell(new Phrase(ListLigneProfilAcademique.ElementAt(i).codeTrimestre, times));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);

                            //la séquence
                            cell = new PdfPCell(new Phrase(ListLigneProfilAcademique.ElementAt(i).codeSequence, times));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);

                            //la moyenne
                            cell = new PdfPCell(new Phrase(Convert.ToString(ListLigneProfilAcademique.ElementAt(i).moyenne), times));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);

                            //la mention
                            cell = new PdfPCell(new Phrase(ListLigneProfilAcademique.ElementAt(i).mention, times));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);

                            //le coeficient
                            cell = new PdfPCell(new Phrase(Convert.ToString(ListLigneProfilAcademique.ElementAt(i).coef), times));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);

                            //le rang
                            cell = new PdfPCell(new Phrase(Convert.ToString(ListLigneProfilAcademique.ElementAt(i).rang), times));
                            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            table.AddCell(cell);
                        }
                        else
                        {
                            if (ListLigneProfilAcademique.ElementAt(i).annee != ListLigneProfilAcademique.ElementAt(i - 1).annee)
                            {
                                //on met une grande ligne désignant le résultat annuel de l'élève pour l'année en cours

                                cell = new PdfPCell(new Phrase("Total des Points : " + ListLigneProfilAcademique.ElementAt(i - 1).totalPoint + "    Moyenne : " + ListLigneProfilAcademique.ElementAt(i - 1).moyenneAnnuelle + "    Mention : " + ListLigneProfilAcademique.ElementAt(i - 1).mentionAnnuelle + "     Rang : " + ListLigneProfilAcademique.ElementAt(i - 1).rangAnnuel, timeheader));
                                cell.BackgroundColor = FontColour_groupe;
                                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                                table.AddCell(cell);
                            }
                            else
                            {
                                //l'année
                                cell = new PdfPCell(new Phrase(Convert.ToString(ListLigneProfilAcademique.ElementAt(i).annee), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //la classe
                                cell = new PdfPCell(new Phrase(ListLigneProfilAcademique.ElementAt(i).codeClasse, times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //la matière
                                cell = new PdfPCell(new Phrase(ListLigneProfilAcademique.ElementAt(i).codeMatiere, times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //la séquence
                                cell = new PdfPCell(new Phrase(ListLigneProfilAcademique.ElementAt(i).codeSequence, times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le trimestre
                                cell = new PdfPCell(new Phrase(ListLigneProfilAcademique.ElementAt(i).codeTrimestre, times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //la moyenne
                                cell = new PdfPCell(new Phrase(Convert.ToString(ListLigneProfilAcademique.ElementAt(i).moyenne), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //la mention
                                cell = new PdfPCell(new Phrase(ListLigneProfilAcademique.ElementAt(i).mention, times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le coeficient
                                cell = new PdfPCell(new Phrase(Convert.ToString(ListLigneProfilAcademique.ElementAt(i).coef), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //le rang
                                cell = new PdfPCell(new Phrase(Convert.ToString(ListLigneProfilAcademique.ElementAt(i).rang), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);
                            }

                        }

                    }

                    int taille = ListLigneProfilAcademique.Count;
                    //on met un grand ligne pou
                    cell = new PdfPCell(new Phrase("Total des Points : " + ListLigneProfilAcademique.ElementAt(taille - 1).totalPoint + "    Moyenne : " + ListLigneProfilAcademique.ElementAt(taille - 1).moyenneAnnuelle + "    Mention : " + ListLigneProfilAcademique.ElementAt(taille - 1).mentionAnnuelle + "     Rang : " + ListLigneProfilAcademique.ElementAt(taille - 1).rangAnnuel, timeheader));
                    cell.BackgroundColor = FontColour_groupe;
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    cell.Colspan = 9;
                    table.AddCell(cell);

                }

                #endregion


                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                //doc.Add(Chunk.NEWLINE);
                doc.Add(creerEnteteProfilAcademique(eleve, classe));
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                //doc.Add(table2);
                doc.Add(Chunk.NEWLINE);
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname)); 
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        //------- Fonction qui génère la situation financière des élèves d'une classe---------------------------------
        public void etatSituationFinanciereDuneClasse(List<EleveBE> listEleve, String codeClasse, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfTimes2 = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Font timesMarque = new Font(bfTimes, 10, Font.BOLD);
                Font timeBlock = new Font(bfTimes2, 9);
                var FontColour_header = new BaseColor(134, 181, 232);
                var FontColour_groupe = new BaseColor(200, 221, 226);
                Font TextFont_Header = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontColour_header);
                Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                int nbColonneStatic = 3; //le nbre de colonne static du tableau, à laquelle ont ajoutera des colonnes qui seront crée dynamiquement

                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                //************ DEBUT Partie renseignant les infos d'entete de l'état
                ClasseDA classeDA = new ClasseDA();
                ClasseBE classe = new ClasseBE();
                classe.codeClasse = codeClasse;
                classe = classeDA.rechercher(classe);
                PdfPTable tableEntete = new PdfPTable(2);
                float[] largeurEntete = new float[2];
                largeurEntete[0] = 0.3f;
                largeurEntete[1] = 0.8f;

                tableEntete.SetWidths(largeurEntete);

                tableEntete.WidthPercentage = 80f;

                if (classe != null)
                {
                    cell = new PdfPCell(new Phrase("Classe : ", times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    tableEntete.AddCell(cell);

                    cell = new PdfPCell(new Phrase(classe.codeClasse + " - " + classe.nomClasse, timeheader));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    tableEntete.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Année Académique : ", times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    tableEntete.AddCell(cell);

                    cell = new PdfPCell(new Phrase((annee - 1) + "/" + annee, timeheader));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    tableEntete.AddCell(cell);
                }

                cell = new PdfPCell(new Phrase(" ", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 2;
                tableEntete.AddCell(cell);

                //************ FIN Partie renseignant les infos d'entete du fichier


                //on liste les codes des prestations
                PrestationDA prestationDA = new PrestationDA();
                List<PrestationBE> listPrestation = prestationDA.listerTous();
                // ajouter les autres colones à la liste ------
                PrestationBE unePrestation = new PrestationBE();
                //--la colonne total à verser
                unePrestation.codePrestation = "A Payer";
                listPrestation.Add(unePrestation);

                //--la colonne remise
                unePrestation = new PrestationBE();
                unePrestation.codePrestation = "Remise";
                listPrestation.Add(unePrestation);

                //--la colonne Payé
                unePrestation = new PrestationBE();
                unePrestation.codePrestation = "Payé";
                listPrestation.Add(unePrestation);

                //--la colonne reste à verser
                unePrestation = new PrestationBE();
                unePrestation.codePrestation = "Reste";
                listPrestation.Add(unePrestation);

                int nbPrestation = listPrestation.Count;

                int nbColonneDynamique = 0; //compte le nbre de colonne crée ds le tableau pour la partie prestation

                for (int i = 0; i < nbPrestation; i++)
                {
                    nbColonneDynamique = nbColonneDynamique + 1;
                }

                PdfPTable table = new PdfPTable(nbColonneStatic + nbColonneDynamique);
                int dim = nbColonneStatic + nbColonneDynamique;
                float[] largeur = new float[dim];
                largeur[0] = 0.2f;
                largeur[1] = 1.4f;
                largeur[2] = 0.5f;

                for (int i = 0; i < nbColonneDynamique; i++)
                    largeur[i + 3] = 0.5f;

                table.SetWidths(largeur);
                table.WidthPercentage = 90f;


                //******************************** Debut de la partie discipline


                cell = new PdfPCell(new Phrase("No", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Noms et prénoms", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Matricule", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table.AddCell(cell);

                int nbColonne = 0; //compte le nbre de colonnes créées ds le tableau pour la partie prestation

                for (int i = 0; i < nbPrestation; i++)
                {

                    cell = new PdfPCell(new Phrase(listPrestation.ElementAt(i).codePrestation, timeheader));
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);

                    nbColonne++;
                }

                EleveDA eleveDA = new EleveDA();

                //on boucle sur la liste des élèves
                for (int k = 0; k < listEleve.Count; k++)
                {
                    int nbCaseVide = 0;
                    double aPayer = 0;
                    double remise = 0;
                    double totalPaye = 0;
                    double resteApayer = 0;


                    //on recher la liste des sanctions séquentielles de l'élèves i
                    //************************ on charge les disciplines de l'élève
                    List<LigneSituationFinanciere> listLigneSituation = eleveDA.getSituationFinanciereEleve(listEleve.ElementAt(k).matricule, annee);

                    //le num de l'élèce
                    cell = new PdfPCell(new Phrase(Convert.ToString(k + 1), times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    table.AddCell(cell);

                    //le matricule de l'élèce
                    cell = new PdfPCell(new Phrase(listEleve.ElementAt(k).nom.ToUpper(), times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    table.AddCell(cell);

                    //le nom de l'élèce
                    cell = new PdfPCell(new Phrase(listEleve.ElementAt(k).matricule, times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);

                    if (listLigneSituation != null)
                    {
                        int i = 0;
                        //aPayer = 0;
                        //remise = 0;
                        //totalPaye = 0;
                        //resteApayer = 0;
                        //----calcul des totaux---------------------
                        for (int j = 0; j < listLigneSituation.Count; j++)
                        {


                            aPayer = aPayer + listLigneSituation.ElementAt(j).aPayer;
                            remise = remise + listLigneSituation.ElementAt(j).remise;
                            totalPaye = totalPaye + listLigneSituation.ElementAt(j).paye;
                            resteApayer = resteApayer + listLigneSituation.ElementAt(j).resteApayer;

                        }


                        while (i < nbPrestation)
                        {

                            //on cherche pour le code de prestation en cours, le nombre de sanctions non justifiées
                            bool trouve = false;
                            int pos = 0;
                            double paye = 0;

                            //  int nbPrestation = 0;
                            for (int j = 0; j < listLigneSituation.Count; j++)
                            {


                                if (listLigneSituation.ElementAt(j).prestation == listPrestation.ElementAt(i).codePrestation)
                                {
                                    trouve = true;
                                    pos = j + 1;
                                    paye = listLigneSituation.ElementAt(j).paye;

                                }

                            }

                            //si on a trouver parmis les prestations de l'élève une correspondant au code de prestation en cours
                            if (trouve == true)
                            {
                                //on insère ce qui a été versé pour cette prestation
                                cell = new PdfPCell(new Phrase(Convert.ToString(paye), times));
                                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                                table.AddCell(cell);

                                //nombre de cellule à sauter avant insertion
                                nbCaseVide = nbPrestation - pos - 1 - 4;
                            }


                            i = i + 1;
                        }

                    }


                    //on insère dans l'état
                    for (int p = 0; p < nbCaseVide; p++)
                    {
                        cell = new PdfPCell(new Phrase(Convert.ToString("-"), times));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                    }

                    //insérer le montant à verser
                    cell = new PdfPCell(new Phrase(Convert.ToString(aPayer), times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);

                    //insérer le montant de la remise
                    cell = new PdfPCell(new Phrase(Convert.ToString(remise), times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);

                    //insérer le montant déjà versé
                    cell = new PdfPCell(new Phrase(Convert.ToString(totalPaye), times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);

                    //insérer le montant restant à verser
                    cell = new PdfPCell(new Phrase(Convert.ToString(resteApayer), times));
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table.AddCell(cell);

                    //------------------------------------------------------------------------------------



                }

                //Fin de la partie discipline


                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(tableEntete);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }

        }


        //--------------------------MOI--------------------------------------------------------------

        /**
 * fonction qui génère l'état des insolvables
*/
        public void etatInsolvable(DataGrid grid, string classe, int annee, string date, string observation)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfHelvetic = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Font timesValeur = new Font(bfHelvetic, 10, Font.BOLD);
                //Document document = new Document(PageSize.A4.rotate()); changer lorientation des pages= paysage ou portrait
                Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                // Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title + DateTime.Today.ToShortDateString(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;
                titre.SpacingAfter = 1f;
                //Phrase user = new Phrase("Utilisateur concerné : ", new Font(bfTimes,12,Font.UNDERLINE)) ; 
                Phrase user = new Phrase();
                user.Add(Chunk.NEWLINE);
                user.Add(new Chunk("Généré le : " + DateTime.Today.ToShortDateString(), new Font(bfTimes, 10, Font.ITALIC)));
                titre.Add(user);

                #region informations d'entete
                PdfPTable infos = new PdfPTable(6);
                float[] widths1 = new float[] { 1f, 1f, 1.5f, 2f, 1f, 2.5f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 80f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;

                cell.Phrase = new iTextSharp.text.Phrase("Classe: ", timeheader);
                infos.AddCell(cell);
                if (classe != null)
                    cell.Phrase = new iTextSharp.text.Phrase(classe, timesValeur);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Année Scolaire: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase((Convert.ToInt32(annee) - 1) + "/" + annee, timesValeur);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Critère: ", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(observation.ToUpper(), timesValeur);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase(" ", timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Colspan = 6;
                infos.AddCell(cell);

                #endregion


                #region contenu des insolvables
                PdfPTable table = new PdfPTable(grid.Columns.Count);
                float[] widths = new float[] { 0.7f, 4f, 1.25f, 1.20f, 1.20f, 1.20f, 1.20f, 1.5f, 1.20f };
                table.SetWidths(widths);
                table.WidthPercentage = 95f;
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    table.AddCell(new Phrase(grid.Columns[j].Header.ToString().ToUpper(), timeheader));
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt;
                                try
                                {
                                    txt = cell1.Content as TextBlock;
                                    if (txt != null)
                                    {
                                        table.AddCell(new Phrase(txt.Text, times));
                                    }
                                }
                                catch (Exception)
                                {
                                    table.AddCell(new Phrase("", times));
                                }
                            }
                        }
                    }
                }
                #endregion

                Chunk c;
                Paragraph p = new Paragraph();
                Paragraph numPage = new Paragraph();
                //c = new Chunk("Total des entrées :"+entree,timeheader);
                //p.Add(c);
                //numPage.Add(new Chunk("Page " + doc.PageNumber.ToString() + "/", new Font(bfTimes, 10, Font.ITALIC)));
                //c = new Chunk("Fin Journal", new Font(bfTimes, 10, Font.ITALIC));
                //p.Add("---------------------------------------------------------");
                //p.Add(c);


                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.AddCreationDate();
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(p);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds_caisse());
                doc.Add(numPage);

                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        /**
         * Etat permettant de creer les pdf des courbes statistiques
         */
        public void exportGraphesToPDF(Grid gridChart1, Grid gridChart2, ClasseBE classe, int annee, string periode,
            Dictionary<string, string> stat, List<KeyValuePair<string, int>> listes)
        {
            try
            {

                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 10, Font.BOLD);
                Font timestitre = new Font(bfTimes, 11, Font.BOLD);
                Font timeheader = new Font(bfTimes, 10);
                string pdffile = ConnexionUI.DOSSIER_ETATS + "stat.pdf";
                string imgfile = ConnexionUI.DOSSIER_ETATS + "stat.png";
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 15);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(pdffile, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                PdfPTable graphes = new PdfPTable(1);
                graphes.WidthPercentage = 90f;
                graphes.HorizontalAlignment = Element.ALIGN_CENTER;
                PdfPCell imageCell;

                #region information sur la classe et la periode
                PdfPTable infos = new PdfPTable(6);
                float[] widths1 = new float[] { 2f, 4f, 3f, 2f, 2f, 2f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 90f;
                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase(" ");
                infos.AddCell(cell); infos.AddCell(cell);
                infos.AddCell(cell); infos.AddCell(cell);
                infos.AddCell(cell); infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Classe:".ToUpper(), timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                infos.AddCell(cell);
                if(classe != null)
                cell.Phrase = new iTextSharp.text.Phrase(classe.codeClasse + " - " + classe.nomClasse, times);
                else
                    cell.Phrase = new iTextSharp.text.Phrase("Tout l'établissement", times);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Année scolaire:".ToUpper(), timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase((annee - 1) + " / " + annee, times);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);

                cell.Phrase = new iTextSharp.text.Phrase("Période:".ToUpper(), timeheader);
                cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(periode, times);
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(" ");
                infos.AddCell(cell); infos.AddCell(cell);
                infos.AddCell(cell); infos.AddCell(cell);
                infos.AddCell(cell); infos.AddCell(cell);
                #endregion

                #region premier graphe
                RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                    (int)gridChart1.ActualWidth,
                    (int)gridChart1.ActualHeight,
                    96d,
                    96d,
                    PixelFormats.Default);
                gridChart1.Measure(new System.Windows.Size(gridChart1.Width, gridChart1.Height));
                gridChart1.Arrange(new Rect(new System.Windows.Size(gridChart1.Width, gridChart1.Height)));
                gridChart1.UpdateLayout();
                renderBitmap.Render(gridChart1);
                string docname = ConnexionUI.DOSSIER_ETATS + "stat.png";
                using (FileStream outStream = new FileStream(docname, FileMode.Create))
                {
                    // Use png encoder for our data
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    // push the rendered bitmap to it
                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                    // save the data to the stream
                    encoder.Save(outStream);
                }
                gridChart1.Arrange(new Rect(new Point(32, 138), new System.Windows.Size(gridChart2.Width, gridChart2.Height)));
                gridChart1.UpdateLayout();

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgfile);
                img.Alignment = Element.ALIGN_CENTER;

                float pageWidth = doc.PageSize.Width - 10;
                float pageHeight = doc.PageSize.Height;
                img.ScaleToFit(pageWidth, pageHeight);
                imageCell = new PdfPCell(img);
                imageCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                imageCell.VerticalAlignment = Rectangle.ALIGN_CENTER;
                graphes.AddCell(imageCell);
                #endregion

                graphes.AddCell(cell);

                #region deuxieme graphe
                renderBitmap = new RenderTargetBitmap(
                    (int)gridChart2.ActualWidth,
                    (int)gridChart2.ActualHeight,
                    96d,
                    96d,
                    PixelFormats.Default);
                gridChart2.Measure(new System.Windows.Size(gridChart2.Width, gridChart2.Height));
                gridChart2.Arrange(new Rect(new System.Windows.Size(gridChart2.Width, gridChart2.Height)));
                gridChart2.UpdateLayout();
                renderBitmap.Render(gridChart2);
                using (FileStream outStream = new FileStream(docname, FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                    encoder.Save(outStream);
                }
                gridChart2.Arrange(new Rect(new Point(32, gridChart1.Height + 138), new System.Windows.Size(gridChart2.Width, gridChart2.Height)));
                gridChart2.UpdateLayout();

                iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(imgfile);
                img2.Alignment = Element.ALIGN_CENTER;

                pageWidth = doc.PageSize.Width - 10;
                pageHeight = doc.PageSize.Height;
                img2.ScaleToFit(pageWidth, pageHeight);
                imageCell = new PdfPCell(img2);
                imageCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                imageCell.HorizontalAlignment = Element.ALIGN_CENTER;
                imageCell.VerticalAlignment = Rectangle.ALIGN_CENTER;
                graphes.AddCell(imageCell);
                #endregion

                #region synthese de la classe
                PdfPTable synthese = new PdfPTable(2);
                if (stat != null)
                {
                    float[] widths2 = new float[] { 2f, 3f };
                    synthese.HorizontalAlignment = Element.ALIGN_CENTER;
                    synthese.SetWidths(widths2);
                    synthese.WidthPercentage = 50f;
                    PdfPCell cell2 = new iTextSharp.text.pdf.PdfPCell();
                    cell2.HorizontalAlignment = Element.ALIGN_LEFT;

                    cell2.Phrase = new iTextSharp.text.Phrase("Professeur titulaire", timeheader);
                    synthese.AddCell(cell2);
                    cell2.Phrase = new iTextSharp.text.Phrase(stat["prof"], times);
                    synthese.AddCell(cell2);
                    cell2.Phrase = new iTextSharp.text.Phrase("Effectif", timeheader);
                    synthese.AddCell(cell2);
                    cell2.Phrase = new iTextSharp.text.Phrase(stat["effectif"], times);
                    synthese.AddCell(cell2);
                    cell2.Phrase = new iTextSharp.text.Phrase("Moyenne de la classe", timeheader);
                    synthese.AddCell(cell2);
                    cell2.Phrase = new iTextSharp.text.Phrase(stat["moyenne"], times);
                    synthese.AddCell(cell2);
                    cell2.Phrase = new iTextSharp.text.Phrase("Taux de réussite", timeheader);
                    synthese.AddCell(cell2);
                    cell2.Phrase = new iTextSharp.text.Phrase(stat["taux"], times);
                    synthese.AddCell(cell2);
                }
                #endregion

                #region tableau de stats
                PdfPTable stats = new PdfPTable(listes.Count + 1);
                if (listes != null)
                {
                    stats.HorizontalAlignment = Element.ALIGN_CENTER;
                    stats.WidthPercentage = 96f;
                    PdfPCell cellvide = new iTextSharp.text.pdf.PdfPCell();
                    cellvide.Border = Rectangle.NO_BORDER;
                    cellvide.Phrase = new iTextSharp.text.Phrase(" ", timeheader);
                    PdfPCell cell2 = new iTextSharp.text.pdf.PdfPCell();
                    cell2.HorizontalAlignment = Element.ALIGN_LEFT;

                    cell2.Phrase = new iTextSharp.text.Phrase("Matière", timeheader);
                    stats.AddCell(cell2);
                    foreach(KeyValuePair<string, int> key in listes){
                        cell2.Phrase = new iTextSharp.text.Phrase(key.Key, timeheader);
                        stats.AddCell(cell2);
                    }
                    cell2.Phrase = new iTextSharp.text.Phrase("Réussite", timeheader);
                    stats.AddCell(cell2);
                    double val = 0;
                    foreach (KeyValuePair<string, int> key in listes)
                    {
                        val = (key.Value * 100) / Convert.ToInt32(stat["effectif"]);
                        cell2.Phrase = new iTextSharp.text.Phrase(val + "%", timeheader);
                        stats.AddCell(cell2);
                    }
                    for (int i = 0; i <= listes.Count; i++)
                        stats.AddCell(cellvide);
                }
                #endregion


                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(infos);
                doc.Add(graphes);
                doc.Add(Chunk.NEWLINE); 
                if (listes != null)
                    doc.Add(stats);
                if (stat != null)
                    doc.Add(synthese);
                doc.Add(Chunk.NEWLINE); 
                doc.Add(creerPieds());
                doc.Close();

                System.Diagnostics.Process.Start(System.IO.Path.GetFullPath(pdffile));
            }
            catch (Exception e)
            {
                MessageBox.Show("une erreur c'est produite lors de la generation de l'etat"+e.Message, "School brain:Alerte");

            }
        }


        internal void bilanFinancier(DataGrid grid, double totalAPayer, double totalPaye, double totalRemise, double totalReste)
        {
            try
            {

                CultureInfo elGR = CultureInfo.CreateSpecificCulture("el-GR");
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                var FontColour_header = new BaseColor(134, 181, 232);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region contenu concernant les informations du bilan
                PdfPTable table = new PdfPTable(6);
                float[] widths = new float[] { 1f, 4f, 3f, 3f, 3f, 3f };
                table.SetWidths(widths);
                table.WidthPercentage = 90f;

                PdfPCell cell = new PdfPCell();
                table.WidthPercentage = 90f;

                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    cell.Phrase = new Phrase(" ", timeheader);
                    cell.Border = Rectangle.NO_BORDER;
                    table.AddCell(cell);
                }

                cell = new PdfPCell();
                for (int j = 0; j < grid.Columns.Count; j++)
                {
                    cell.Phrase = new Phrase(grid.Columns[j].Header.ToString(), timeheader);
                    cell.BackgroundColor = FontColour_header;
                    table.AddCell(cell);
                }
                table.HeaderRows = 1;
                IEnumerable itemsSource = grid.ItemsSource as IEnumerable;
                if (itemsSource != null)
                {
                    foreach (var item in itemsSource)
                    {
                        DataGridRow row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null)
                        {
                            DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                            for (int i = 0; i < grid.Columns.Count; ++i)
                            {
                                DataGridCell cell1 = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                                TextBlock txt;
                                try
                                {
                                    txt = cell1.Content as TextBlock;
                                    if (txt != null)
                                    {
                                        table.AddCell(new Phrase(txt.Text, times));
                                    }
                                }
                                catch (Exception)
                                {
                                    table.AddCell(new Phrase("", times));
                                }
                            }
                        }
                    }
                }
                #endregion

                Chunk c;
                Paragraph p = new Paragraph();
                c = new Chunk("     Total A Payer              :" + totalAPayer.ToString("0,0", elGR) + " (" + Tools.numberToString(Convert.ToUInt64(totalAPayer)) + ")\n", timeheader);
                p.Add(c);
                c = new Chunk("     Total déjà payer          :" + totalPaye.ToString("0,0", elGR) + " (" + Tools.numberToString(Convert.ToUInt64(totalPaye)) + ")\n", timeheader);
                p.Add(c);
                c = new Chunk("     Total des remises        :" + totalRemise.ToString("0,0", elGR) + " (" + Tools.numberToString(Convert.ToUInt64(totalRemise)) + ")\n", timeheader);
                p.Add(c);
                c = new Chunk("     Total des reste à payer:" + totalReste.ToString("0,0", elGR) + " (" + Tools.numberToString(Convert.ToUInt64(totalReste)) + ")\n", timeheader);
                p.Add(c);

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(p);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds_caisse());
                doc.Close();

                System.Diagnostics.Process.Start(System.IO.Path.GetFullPath(docname));
            }
            catch (Exception e)
            {
                MessageBox.Show("une erreur c'est produite lors de la generation de l'etat" + e.Message, "School brain:Alerte");

            }

        }

        //fonction utile pour recuperer les row de la datagrid
        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        public void etatPourcentageCumuleDeNotesParSequence(ClasseBE classe, String codeSeq, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfTimes2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                Font times = new Font(bfTimes, 10);
                Font timesMarque = new Font(bfTimes, 10, Font.BOLD);
                Font timestitre = new Font(bfTimes, 9, Font.BOLD);
                Font timestitre2 = new Font(bfTimes, 10, Font.BOLD);
                Font timeheader = new Font(bfTimes2, 10, Font.BOLD);
                Font timeBlock = new Font(bfTimes2, 9);
                var FontColour_header = new BaseColor(134, 181, 232);
                var FontColour_groupe = new BaseColor(200, 221, 226);
                Font TextFont_Header = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontColour_header);
                Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region contenu concernant les grid

                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                int nbColonneStatic = 18;

                PdfPTable table = new PdfPTable(nbColonneStatic);
                int dim = nbColonneStatic;
                float[] largeur = new float[dim];
                largeur[0] = 3f;
                largeur[1] = 2f;

                for (int i = 1; i < nbColonneStatic; i++)
                    largeur[i] = 1.5f;


                table.SetWidths(largeur);
                table.WidthPercentage = 90f;
                //la table possède 9 colonnes
                /*
                 * 0 : année
                 * 1 : codeClasse
                 * 2 : codematière
                 * 3 : codesequence
                 * 4 : codeTrimestre
                 * 5:  moyenne
                 * 6: mention
                 * 7 : coef
                 * 8 : rang
                 */
                //float[] widths = new float[] { 1.5f, 8f, 1f, 1f, 1f, 1f, 1f, 1.5f };
                //table.SetWidths(widths);

                //********************* La Classe 
                cell = new PdfPCell(new Phrase(classe.codeClasse, timestitre2));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 18;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("", timestitre));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 18;
                table.AddCell(cell);

                //**************** première ligne
                cell = new PdfPCell(new Phrase("", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Nombre et Pourcentage cumule de notes comprises entre : ", timestitre));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 14;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("", timeheader));
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                //**************** FIN première ligne

                //**************** deuxième ligne
                cell = new PdfPCell(new Phrase("Enseignants", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Disciplines", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("16-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("14-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("12-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("10-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("9-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("7-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("0-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Classe", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Effectifs", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);


                table.HeaderRows = 1;

               //on liste les matière de la classe en question
                ClasseDA classeDA = new ClasseDA();
                List<MatiereBE> listMatiere = classeDA.ListeMatiereDuneClasse(classe, annee);

                PourcentageCumuleDA pourcentageCumuleDA = new PourcentageCumuleDA();
                if (listMatiere != null) {
                    for (int i = 0; i < listMatiere.Count; i++) {
                       
                        //le nom de l'enseignant
                        String nomProf = pourcentageCumuleDA.getNomEnseignantDuneMatiere(classe.codeClasse, listMatiere.ElementAt(i).codeMat, annee);
                        cell = new PdfPCell(new Phrase(nomProf, timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le code de la matière
                        cell = new PdfPCell(new Phrase(listMatiere.ElementAt(i).codeMat, timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //on recherche l'éffectif des élèves ayant composé la matière
                        int effectifTotal = pourcentageCumuleDA.getEffectifPourUneSequence(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeSeq, annee);

                        //le nombre de personne ayant eu une note entre 16 et 20
                        Double nb_16_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUneSequence(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeSeq, annee, 16, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_16_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_16_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 14 et 20
                        Double nb_14_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUneSequence(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeSeq, annee, 14, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_14_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_14_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 12 et 20
                        Double nb_12_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUneSequence(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeSeq, annee, 12, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_12_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_12_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 10 et 20
                        Double nb_10_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUneSequence(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeSeq, annee, 10, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_10_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_10_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 9 et 20
                        Double nb_9_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUneSequence(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeSeq, annee, 9, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_9_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_9_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 7 et 20
                        Double nb_7_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUneSequence(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeSeq, annee, 7, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_7_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_7_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 0 et 20
                        Double nb_0_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUneSequence(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeSeq, annee, 0, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_0_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_0_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //la moyenne de la Classe
                        Double moyenneClasse = pourcentageCumuleDA.getMoyenneDeClassePrUneSequence(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeSeq, annee);
                        cell = new PdfPCell(new Phrase(Convert.ToString(moyenneClasse), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //l'effectif
                        cell = new PdfPCell(new Phrase(Convert.ToString(effectifTotal), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);


                    }
                }

                #endregion


                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                //doc.Add(Chunk.NEWLINE);
                //doc.Add(creerEnteteProfilAcademique(eleve, classe));
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                //doc.Add(table2);
                doc.Add(Chunk.NEWLINE);
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname)); 
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void etatPourcentageCumuleDeNotesParTrimestre(ClasseBE classe, String codeTrimestre, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfTimes2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                Font times = new Font(bfTimes, 10);
                Font timesMarque = new Font(bfTimes, 10, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10, Font.BOLD);
                Font timestitre2 = new Font(bfTimes, 10, Font.BOLD);
                Font timeheader = new Font(bfTimes2, 10, Font.BOLD);
                Font timeBlock = new Font(bfTimes2, 9);
                var FontColour_header = new BaseColor(134, 181, 232);
                var FontColour_groupe = new BaseColor(200, 221, 226);
                Font TextFont_Header = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontColour_header);
                Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region contenu concernant les grid

                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                int nbColonneStatic = 18;

                PdfPTable table = new PdfPTable(nbColonneStatic);
                int dim = nbColonneStatic;
                float[] largeur = new float[dim];
                largeur[0] = 3f;
                largeur[1] = 2f;

                for (int i = 1; i < nbColonneStatic; i++)
                    largeur[i] = 1.5f;


                table.SetWidths(largeur);
                table.WidthPercentage = 90f;
                //la table possède 9 colonnes
                /*
                 * 0 : année
                 * 1 : codeClasse
                 * 2 : codematière
                 * 3 : codesequence
                 * 4 : codeTrimestre
                 * 5:  moyenne
                 * 6: mention
                 * 7 : coef
                 * 8 : rang
                 */
                //float[] widths = new float[] { 1.5f, 8f, 1f, 1f, 1f, 1f, 1f, 1.5f };
                //table.SetWidths(widths);

                //********************* La Classe 
                cell = new PdfPCell(new Phrase(classe.codeClasse, timestitre2));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 18;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("", timestitre));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 18;
                table.AddCell(cell);

                //**************** première ligne
                cell = new PdfPCell(new Phrase("", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Nombre et Pourcentage cumule de notes comprises entre : ", timestitre));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 14;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("", timeheader));
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                //**************** FIN première ligne

                //**************** deuxième ligne
                cell = new PdfPCell(new Phrase("Enseignants", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Disciplines", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("16-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("14-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("12-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("10-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("9-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("7-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("0-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Classe", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Effectifs", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);


                table.HeaderRows = 1;

                //on liste les matière de la classe en question
                ClasseDA classeDA = new ClasseDA();
                List<MatiereBE> listMatiere = classeDA.ListeMatiereDuneClasse(classe, annee);

                PourcentageCumuleDA pourcentageCumuleDA = new PourcentageCumuleDA();
                if (listMatiere != null)
                {
                    for (int i = 0; i < listMatiere.Count; i++)
                    {

                        //le nom de l'enseignant
                        String nomProf = pourcentageCumuleDA.getNomEnseignantDuneMatiere(classe.codeClasse, listMatiere.ElementAt(i).codeMat, annee);
                        cell = new PdfPCell(new Phrase(nomProf, timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le code de la matière
                        cell = new PdfPCell(new Phrase(listMatiere.ElementAt(i).codeMat, timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //on recherche l'éffectif des élèves ayant composé la matière
                        int effectifTotal = pourcentageCumuleDA.getEffectifPourUnTrimestre(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeTrimestre, annee);

                        //le nombre de personne ayant eu une note entre 16 et 20
                        Double nb_16_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUnTrimestre(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeTrimestre, annee, 16, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_16_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_16_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 14 et 20
                        Double nb_14_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUnTrimestre(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeTrimestre, annee, 14, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_14_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_14_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 12 et 20
                        Double nb_12_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUnTrimestre(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeTrimestre, annee, 12, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_12_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_12_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 10 et 20
                        Double nb_10_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUnTrimestre(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeTrimestre, annee, 10, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_10_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_10_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 9 et 20
                        Double nb_9_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUnTrimestre(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeTrimestre, annee, 9, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_9_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_9_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 7 et 20
                        Double nb_7_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUnTrimestre(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeTrimestre, annee, 7, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_7_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_7_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 0 et 20
                        Double nb_0_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUnTrimestre(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeTrimestre, annee, 0, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_0_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_0_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //la moyenne de la Classe
                        Double moyenneClasse = pourcentageCumuleDA.getMoyenneDeClassePrUnTrimestre(classe.codeClasse, listMatiere.ElementAt(i).codeMat, codeTrimestre, annee);
                        cell = new PdfPCell(new Phrase(Convert.ToString(moyenneClasse), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //l'effectif
                        cell = new PdfPCell(new Phrase(Convert.ToString(effectifTotal), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);


                    }
                }

                #endregion


                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                //doc.Add(Chunk.NEWLINE);
                //doc.Add(creerEnteteProfilAcademique(eleve, classe));
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                //doc.Add(table2);
                doc.Add(Chunk.NEWLINE);
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname)); 
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void etatPourcentageCumuleDeNotesParAnnee(ClasseBE classe, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfTimes2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                Font times = new Font(bfTimes, 10);
                Font timesMarque = new Font(bfTimes, 10, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10, Font.BOLD);
                Font timestitre2 = new Font(bfTimes, 10, Font.BOLD);
                Font timeheader = new Font(bfTimes2, 10, Font.BOLD);
                Font timeBlock = new Font(bfTimes2, 9);
                var FontColour_header = new BaseColor(134, 181, 232);
                var FontColour_groupe = new BaseColor(200, 221, 226);
                Font TextFont_Header = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontColour_header);
                Document doc = new Document(iTextSharp.text.PageSize.A4.Rotate(), 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region contenu concernant les grid

                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                int nbColonneStatic = 18;

                PdfPTable table = new PdfPTable(nbColonneStatic);
                int dim = nbColonneStatic;
                float[] largeur = new float[dim];
                largeur[0] = 3f;
                largeur[1] = 2f;

                for (int i = 1; i < nbColonneStatic; i++)
                    largeur[i] = 1.5f;


                table.SetWidths(largeur);
                table.WidthPercentage = 90f;
                //la table possède 9 colonnes
                /*
                 * 0 : année
                 * 1 : codeClasse
                 * 2 : codematière
                 * 3 : codesequence
                 * 4 : codeTrimestre
                 * 5:  moyenne
                 * 6: mention
                 * 7 : coef
                 * 8 : rang
                 */
                //float[] widths = new float[] { 1.5f, 8f, 1f, 1f, 1f, 1f, 1f, 1.5f };
                //table.SetWidths(widths);

                //********************* La Classe 
                cell = new PdfPCell(new Phrase(classe.codeClasse, timestitre2));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 18;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("", timestitre));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 18;
                table.AddCell(cell);

                //**************** première ligne
                cell = new PdfPCell(new Phrase("", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Nombre et Pourcentage cumule de notes comprises entre : ", timestitre));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 14;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("", timeheader));
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                //**************** FIN première ligne

                //**************** deuxième ligne
                cell = new PdfPCell(new Phrase("Enseignants", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Disciplines", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("16-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("14-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("12-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("10-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("9-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("7-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("0-20", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Colspan = 2;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Moy.Classe", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Effectifs", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);


                table.HeaderRows = 1;

                //on liste les matière de la classe en question
                ClasseDA classeDA = new ClasseDA();
                List<MatiereBE> listMatiere = classeDA.ListeMatiereDuneClasse(classe, annee);

                PourcentageCumuleDA pourcentageCumuleDA = new PourcentageCumuleDA();
                if (listMatiere != null)
                {
                    for (int i = 0; i < listMatiere.Count; i++)
                    {

                        //le nom de l'enseignant
                        String nomProf = pourcentageCumuleDA.getNomEnseignantDuneMatiere(classe.codeClasse, listMatiere.ElementAt(i).codeMat, annee);
                        cell = new PdfPCell(new Phrase(nomProf, timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le code de la matière
                        cell = new PdfPCell(new Phrase(listMatiere.ElementAt(i).codeMat, timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //on recherche l'éffectif des élèves ayant composé la matière
                        int effectifTotal = pourcentageCumuleDA.getEffectifPourUneAnnee(classe.codeClasse, listMatiere.ElementAt(i).codeMat, annee);

                        //le nombre de personne ayant eu une note entre 16 et 20
                        Double nb_16_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUneAnnee(classe.codeClasse, listMatiere.ElementAt(i).codeMat, annee, 16, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_16_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_16_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 14 et 20
                        Double nb_14_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUneAnnee(classe.codeClasse, listMatiere.ElementAt(i).codeMat, annee, 14, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_14_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_14_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 12 et 20
                        Double nb_12_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUneAnnee(classe.codeClasse, listMatiere.ElementAt(i).codeMat, annee, 12, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_12_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_12_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 10 et 20
                        Double nb_10_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUneAnnee(classe.codeClasse, listMatiere.ElementAt(i).codeMat, annee, 10, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_10_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_10_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 9 et 20
                        Double nb_9_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUneAnnee(classe.codeClasse, listMatiere.ElementAt(i).codeMat, annee, 9, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_9_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_9_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 7 et 20
                        Double nb_7_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUneAnnee(classe.codeClasse, listMatiere.ElementAt(i).codeMat, annee, 7, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_7_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_7_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nombre de personne ayant eu une note entre 0 et 20
                        Double nb_0_20 = pourcentageCumuleDA.getNbrPersonneAyantUneMoyenneDsUnIntervalPourUneAnnee(classe.codeClasse, listMatiere.ElementAt(i).codeMat, annee, 0, 20);
                        cell = new PdfPCell(new Phrase(Convert.ToString(nb_0_20), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le pourcentage
                        cell = new PdfPCell(new Phrase(Math.Round((nb_0_20 / effectifTotal) * 100, 2).ToString() + "%", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //la moyenne de la Classe
                        Double moyenneClasse = pourcentageCumuleDA.getMoyenneDeClassePrUneAnnee(classe.codeClasse, listMatiere.ElementAt(i).codeMat, annee);
                        cell = new PdfPCell(new Phrase(Convert.ToString(moyenneClasse), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //l'effectif
                        cell = new PdfPCell(new Phrase(Convert.ToString(effectifTotal), timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);


                    }
                }

                #endregion


                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                //doc.Add(Chunk.NEWLINE);
                //doc.Add(creerEnteteProfilAcademique(eleve, classe));
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                //doc.Add(table2);
                doc.Add(Chunk.NEWLINE);
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname)); 
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void etatConseilDeClasse(ClasseBE classe, String codeSequence, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                BaseFont bfTimes2 = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                Font times = new Font(bfTimes, 9);
                Font timestitre = new Font(bfTimes, 10);
                Font timesMarque = new Font(bfTimes, 9, Font.BOLD);
                Font timestitre2 = new Font(bfTimes, 20, Font.BOLD);
                Font timeheader = new Font(bfTimes2, 9, Font.BOLD);
                Font timeBlock = new Font(bfTimes2, 9);
                var FontColour_header = new BaseColor(134, 181, 232);
                var FontColour_groupe = new BaseColor(200, 221, 226);
                Font TextFont_Header = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 12, FontColour_header);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region contenu concernant les grid

                PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;

                //*********************************************** debut du premier tableau

                int nbColonneStaticPremierTableau = 4;

                PdfPTable table1 = new PdfPTable(nbColonneStaticPremierTableau);
                int dim1 = nbColonneStaticPremierTableau;
                float[] largeur1 = new float[dim1];
                largeur1[0] = 3f;
                largeur1[1] = 3f;
                largeur1[2] = 3f;
                largeur1[3] = 3f;

                table1.SetWidths(largeur1);
                table1.WidthPercentage = 90f;
                //la table possède 9 colonnes
                /*
                 * 0 : année
                 * 1 : codeClasse
                 * 2 : codematière
                 * 3 : codesequence
                 * 4 : codeTrimestre
                 * 5:  moyenne
                 * 6: mention
                 * 7 : coef
                 * 8 : rang
                 */
                //float[] widths = new float[] { 1.5f, 8f, 1f, 1f, 1f, 1f, 1f, 1.5f };
                //table.SetWidths(widths);

                //une ligne vide
                cell = new PdfPCell(new Phrase("", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase("DISCIPLINE", timestitre));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase("", timestitre));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase("RESULTAT SCOLAIRE", timestitre));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Colspan = 2;
                table1.AddCell(cell);

                //une ligne vide
                cell = new PdfPCell(new Phrase("", timestitre));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table1.AddCell(cell);

                //l'entête
                cell = new PdfPCell(new Phrase("Dénominations", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Nombres", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Dénominations", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Nombres", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                table1.HeaderRows = 1;

                DisciplineDA disciplineDA = new DisciplineDA();
                List<DisciplineBE> ListDiscipline = disciplineDA.listerTous();

                PourcentageCumuleDA pourcentageCumuleDA = new PourcentageCumuleDA();

                int indice = 1;
                foreach (DisciplineBE discipline in ListDiscipline) { 
                    //on cherche le nombre de sanction non justifiée pour la discipline considéré
                    int quantiteNonJustifiee = pourcentageCumuleDA.getNombreSanctionDuneDiscipline(classe.codeClasse, discipline.codeSanction, codeSequence, annee, "NON JUSTIFIEE");

                    //on cherche le nombre de sanction justifiée pour la discipline considéré
                    int quantiteJustifiee = pourcentageCumuleDA.getNombreSanctionDuneDiscipline(classe.codeClasse, discipline.codeSanction, codeSequence, annee, "JUSTIFIEE");

                    cell = new PdfPCell(new Phrase(discipline.nomSanction, timeheader));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    table1.AddCell(cell);

                    cell = new PdfPCell(new Phrase(Convert.ToString(quantiteNonJustifiee - quantiteJustifiee) + " " + discipline.variable, timeheader));
                    cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                    table1.AddCell(cell);

                    if (indice == 1)
                    {
                        indice = 2;
                    }
                    else indice = 1;

                }

                //une ligne vide

                if (indice == 1)
                {
                    cell = new PdfPCell(new Phrase("", timeheader));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.Colspan = 2;
                    table1.AddCell(cell);
                }
                else {
                    cell = new PdfPCell(new Phrase("", timeheader));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.Colspan = 2;
                    table1.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", timeheader));
                    cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.Colspan = 2;
                    table1.AddCell(cell);
                }

                int moyenneSup10 = pourcentageCumuleDA.getNombreAyantUneMoyenneSupA10(classe.codeClasse, codeSequence, annee);
                int moyenneInf10 = pourcentageCumuleDA.getNombreAyantUneMoyenneInfA10(classe.codeClasse, codeSequence, annee);

                //l'effectif de la classe
                ClasseDA classeDA = new ClasseDA();
                List<EleveBE> ListEleve = classeDA.listeEleves(classe, annee);

                cell = new PdfPCell(new Phrase("Effectif", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase(Convert.ToString(ListEleve.Count), timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                //les non classés
                cell = new PdfPCell(new Phrase("", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 2;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Non Classés", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase(Convert.ToString(ListEleve.Count - (moyenneSup10 + moyenneInf10)), timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                //la moyenne de la classe
                cell = new PdfPCell(new Phrase("Moyenne de la Classe", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase(Convert.ToString(pourcentageCumuleDA.getMoyenneClassePourUnResultatSequentiel(classe.codeClasse, codeSequence, annee)), timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                //les classés
                cell = new PdfPCell(new Phrase("Classés", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase(Convert.ToString(moyenneSup10 + moyenneInf10), timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                //le pourcentage de réussite
                cell = new PdfPCell(new Phrase("Pourcentage de réussite", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table1.AddCell(cell);

                double x = moyenneSup10;
                double y = moyenneSup10 + moyenneInf10;
                cell = new PdfPCell(new Phrase(Math.Round(Convert.ToDouble( x / y) * 100, 2).ToString() + "%", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                //les Moyenne >=10/20
                cell = new PdfPCell(new Phrase("Moyennes(>=10/20)", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table1.AddCell(cell);

                
                cell = new PdfPCell(new Phrase(Convert.ToString(moyenneSup10), timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                //la Moyenne du premier
                cell = new PdfPCell(new Phrase("Moyenne du premier", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase(Convert.ToString(pourcentageCumuleDA.getResultatSequentielMaximal(classe.codeClasse, codeSequence, annee)), timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                //les Sous Moyenne <10/20
                cell = new PdfPCell(new Phrase("Sous moyennes(<10/20)", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table1.AddCell(cell);

                
                cell = new PdfPCell(new Phrase(Convert.ToString(moyenneInf10), timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                //la Moyenne du dernier
                cell = new PdfPCell(new Phrase("Moyenne du dernier", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase(Convert.ToString(pourcentageCumuleDA.getResultatSequentielMinimal(classe.codeClasse, codeSequence, annee)), timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase(Convert.ToString(""), timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 2;
                table1.AddCell(cell);

                int effectifTotal = moyenneInf10 + moyenneSup10;
                //l'écart type
                cell = new PdfPCell(new Phrase("Ecart type", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase(Convert.ToString(pourcentageCumuleDA.getEcartTypePourUneSequence(classe.codeClasse, codeSequence, annee, effectifTotal)), timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase(Convert.ToString(""), timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 2;
                table1.AddCell(cell);

                //la moyenne de réussite
                cell = new PdfPCell(new Phrase("Moyenne de réussite", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase(Convert.ToString(10), timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase(Convert.ToString(""), timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 2;
                table1.AddCell(cell);

                //la réussite
                cell = new PdfPCell(new Phrase("réussite", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase(Convert.ToString(moyenneSup10 + " SUR " +( moyenneSup10 + moyenneInf10)), timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase(Convert.ToString(""), timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 2;
                table1.AddCell(cell);

                //une ligne vide
                cell = new PdfPCell(new Phrase("", timeheader));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 4;
                table1.AddCell(cell);

                

                //la moyenne de la classe
                //int quantite = pourcentageCumuleDA.getMoyenneDeClassePrUneSequence(classe.codeClasse
                //********************************************** Fin du premier tableau

                int nbColonneStatic = 3;

                PdfPTable table = new PdfPTable(nbColonneStatic);
                int dim = nbColonneStatic;
                float[] largeur = new float[dim];
                largeur[0] = 3f;
                largeur[1] = 2f;
                largeur[2] = 5f;

                table.SetWidths(largeur);
                table.WidthPercentage = 90f;
                //la table possède 9 colonnes
                /*
                 * 0 : année
                 * 1 : codeClasse
                 * 2 : codematière
                 * 3 : codesequence
                 * 4 : codeTrimestre
                 * 5:  moyenne
                 * 6: mention
                 * 7 : coef
                 * 8 : rang
                 */
                //float[] widths = new float[] { 1.5f, 8f, 1f, 1f, 1f, 1f, 1f, 1.5f };
                //table.SetWidths(widths);

                //********************* La Classe 
                cell = new PdfPCell(new Phrase("PERSONNEL ENSEIGNANT", timestitre));
                cell.HorizontalAlignment = Rectangle.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("", timestitre));
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                table.AddCell(cell);

                //**************** première ligne
                cell = new PdfPCell(new Phrase("NOMS ET PRENOMS", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("DISCIPLINE", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("EMARGEMENTS", timeheader));
                cell.BackgroundColor = FontColour_header;
                cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                table.AddCell(cell);

                table.HeaderRows = 1;

                //**************** FIN première ligne

                //**************** deuxième ligne
                List<ProgrammerBE> programmes = new List<ProgrammerBE>();
                GestionProgrammeBL programmeBL = new GestionProgrammeBL();
                MatiereBE matiere;
                EnseignantBE enseignant;
                programmes = programmeBL.listerSuivantCritereProgrammer("codeclasse = " + "'" + classe.codeClasse + "' AND annee = " + "'" + annee + "'");
                if (programmes != null)
                {
                    foreach (ProgrammerBE p in programmes)
                    {
                        matiere = new MatiereBE();
                        matiere.codeMat = p.codematiere;
                        matiere = programmeBL.rechercherMatiere(matiere);
                        enseignant = new EnseignantBE();
                        enseignant.codeProf = p.codeprof;
                        enseignant = programmeBL.rechercherEnseignant(enseignant);
                        
                        //le nom et le prénom de l'enseignant
                        cell = new PdfPCell(new Phrase(enseignant.nomProf, timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //le nom de la discipline
                        cell = new PdfPCell(new Phrase(matiere.nomMat, timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);

                        //l'emargement
                        cell = new PdfPCell(new Phrase("", timeheader));
                        cell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                        table.AddCell(cell);
                    }
                }

                #endregion


                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table1);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                //doc.Add(table2);
                doc.Add(Chunk.NEWLINE);
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname)); 
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        public void recapitulatifNotes_new(List<LigneRecapSeq> recapitulatif, ClasseBE classe, StatistiqueClasseBE stat, string nomprof, List<string> codematieres,
               List<string> codesanctions, int annee, double moyenne)
    {
        try
        {
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times = new Font(bfTimes, 9, Font.BOLD);
            Font timestitre = new Font(bfTimes, 9, Font.BOLD);
            Font timeheader = new Font(bfTimes, 7, Font.BOLD);
            var FontColour_header = new BaseColor(134, 181, 232);
            Document doc = new Document(PageSize.A4.Rotate(), 5, 5, 5, 25);
            PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
            writer.PageEvent = new PDFFooter1();
            doc.Open();

            Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
            titre.Alignment = Element.ALIGN_CENTER;
            int nbColonne = codematieres.Count() + codesanctions.Count() + 8;
            List<string> headers = new List<string>();
            PdfPTable table = new PdfPTable(nbColonne);
            table.WidthPercentage = 100f;
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            float[] widths = new float[nbColonne];
            widths[0] = 1f;
            widths[1] = 5f;
            widths[2] = 2f;
            for (int i = 2; i < nbColonne - 1; i++)
            {
                widths[i] = 1.5f;
            }
            widths[codematieres.Count() + 3] = 1.5f;
            widths[nbColonne - 1] = 4f;
            table.SetWidths(widths);

            #region creation de l'entete
            PdfPTable infos = new PdfPTable(2);
            infos.WidthPercentage = 100f;
            PdfPCell cell1 = new PdfPCell((new Phrase(" ", times)));
            cell1.Border = Rectangle.BOTTOM_BORDER;
            cell1.HorizontalAlignment = Element.ALIGN_LEFT;
            PdfPCell cell2 = new PdfPCell((new Phrase(" ", times)));
            cell2.Border = Rectangle.BOTTOM_BORDER;
            cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.Phrase = new Phrase("Classe", times);
            cell2.Phrase = new Phrase(classe.codeClasse, times);
            infos.AddCell(cell1);
            infos.AddCell(cell2);
            cell1.Phrase = new Phrase("Effectif", times);
            cell2.Phrase = new Phrase(stat.effectif + "", times);
            infos.AddCell(cell1);
            infos.AddCell(cell2);
            cell1.Phrase = new Phrase("Moy classe", times);
            cell2.Phrase = new Phrase(moyenne + "", times);
            infos.AddCell(cell1);
            infos.AddCell(cell2);
            cell1.Phrase = new Phrase("Taux réussite", times);
            cell2.Phrase = new Phrase(stat.pourcentageAdmis, times);
            infos.AddCell(cell1);
            infos.AddCell(cell2);

            PdfPTable conclusion = new PdfPTable(1);
            conclusion.WidthPercentage = 100f;
            cell1 = new iTextSharp.text.pdf.PdfPCell();
            cell1.Border = Rectangle.NO_BORDER;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2 = new iTextSharp.text.pdf.PdfPCell();
            cell2.Border = Rectangle.NO_BORDER;
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2.Phrase = new Phrase("SEQUENCE ", times);
            cell2.BackgroundColor = FontColour_header;
            cell1.Phrase = new Phrase("  ");
            conclusion.AddCell(cell1);
            conclusion.AddCell(cell2);
            cell1.Phrase = new Phrase("RQ:REUSSITE SI MOY >= 10/20", timeheader);
            conclusion.AddCell(cell1);
            cell1.Phrase = new Phrase("(" + stat.nbAdmis + " SUR " + stat.effectif + ")", times);
            conclusion.AddCell(cell1);

            PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
            cell.Colspan = 2;
            cell.AddElement(infos);
            table.AddCell(cell);
            cell = new iTextSharp.text.pdf.PdfPCell();
            cell.Rotation = 90;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;
            cell.Phrase = new Phrase("SEXE \n REDOUBLANT", times);
            table.AddCell(cell);
            foreach (string code in codematieres)
            {
                cell.Phrase = new Phrase(code, times);
                table.AddCell(cell);
            }
            cell.Phrase = new Phrase("Total général", times);
            table.AddCell(cell);
            cell.Phrase = new Phrase("Moyenne générale", times);
            table.AddCell(cell);
            cell.Phrase = new Phrase("Rang", times);
            table.AddCell(cell);
            foreach (string code in codesanctions)
            {
                cell.Phrase = new Phrase(code, times);
                table.AddCell(cell);
            }
            cell.Phrase = new Phrase("NB.SOUS.MOY", times);
            table.AddCell(cell);
            cell = new iTextSharp.text.pdf.PdfPCell();
            cell.AddElement(conclusion);
            table.AddCell(cell);
            #endregion

            #region contenu des differentes lignes
            int ligne = 1;
            cell = new iTextSharp.text.pdf.PdfPCell();
            foreach (LigneRecapSeq l in recapitulatif)
            {
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Phrase = new Phrase("" + ligne++, times);
                table.AddCell(cell);
                cell.Phrase = new Phrase(l.nom, timeheader);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);
                cell.Phrase = new Phrase(l.sexe_redoub.ToLower(), times);
                table.AddCell(cell);
                foreach (string code in codematieres)
                {
                    cell = new PdfPCell();
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    try
                    {
                        cell.Phrase = new Phrase(Math.Round(l.moyennesSequentielles[code], 2).ToString(), times);
                        if (l.moyennesSequentielles[code] < 10)
                            cell.BackgroundColor = FontColour_header;
                        table.AddCell(cell);
                    }
                    catch (Exception)
                    {
                        cell.Phrase = new Phrase(" - ", times);
                        cell.BackgroundColor = FontColour_header;
                        table.AddCell(cell);
                    }
                }
                cell = new PdfPCell();
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Phrase = new Phrase(l.total + "", times);
                table.AddCell(cell);
                cell.Phrase = new Phrase(l.moyenne + "", times);
                table.AddCell(cell);
                cell.Phrase = new Phrase(l.rang + "", times);
                table.AddCell(cell);
                foreach (string code in codesanctions)
                {
                    try
                    {
                        cell.Phrase = new Phrase(l.sanctions[code].ToString(), times);
                        table.AddCell(cell);
                    }
                    catch (Exception)
                    {
                        cell.Phrase = new Phrase("  ", times);
                        table.AddCell(cell);
                    }
                }
                cell.Phrase = new Phrase(l.nb_sous_moyenne + "", times);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table.AddCell(cell);
                cell.Phrase = new Phrase(l.mention + "", times);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);
            }

            #endregion

            doc.Add(creerEntete());
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
            doc.Add(table);
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
            doc.Add(creerPieds());
            doc.Close();

            //SendToPrinter(Path.GetFullPath(docname));
            //File.Delete(Path.GetFullPath(docname));
            System.Diagnostics.Process.Start(Path.GetFullPath(docname));
        }
        catch (Exception)
        {
            MessageBox.Show("Une exception s'est produite lors de la création de l'état");
        }
    }

        public void recapitulatifMoyenne(List<LigneRecapSeq> recapitulatif, ClasseBE classe, StatistiqueClasseBE stat, string nomprof,
               List<string> codesanctions, int annee, double moyenne)
    {
        try
        {
            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            Font times = new Font(bfTimes, 7, Font.BOLD);
            Font timestitre = new Font(bfTimes, 7, Font.BOLD);
            Font timeheader = new Font(bfTimes, 5);
            var FontColour_header = new BaseColor(134, 181, 232);
            Document doc = new Document(PageSize.A4, 5, 5, 5, 25);
            PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
            writer.PageEvent = new PDFFooter();
            doc.Open();

            Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
            titre.Alignment = Element.ALIGN_CENTER;
            int nbColonne = codesanctions.Count() + 8;
            List<string> headers = new List<string>();
            PdfPTable table = new PdfPTable(nbColonne);
            table.WidthPercentage = 100f;
            table.HorizontalAlignment = Element.ALIGN_CENTER;
            float[] widths = new float[nbColonne];
            widths[0] = 1f;
            widths[1] = 5f;
            widths[2] = 2f;
            for (int i = 3; i < nbColonne - 1; i++)
            {
                widths[i] = 1f;
            }
            widths[nbColonne - 1] = 4f;
            table.SetWidths(widths);

            #region creation de l'entete
            PdfPTable infos = new PdfPTable(2);
            infos.WidthPercentage = 100f;
            PdfPCell cell1 = new PdfPCell((new Phrase(" ", times)));
            cell1.Border = Rectangle.BOTTOM_BORDER;
            cell1.HorizontalAlignment = Element.ALIGN_LEFT;
            PdfPCell cell2 = new PdfPCell((new Phrase(" ", times)));
            cell2.Border = Rectangle.BOTTOM_BORDER;
            cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell1.Phrase = new Phrase("Classe", times);
            cell2.Phrase = new Phrase(classe.codeClasse, times);
            infos.AddCell(cell1);
            infos.AddCell(cell2);
            cell1.Phrase = new Phrase("Effectif", times);
            cell2.Phrase = new Phrase(stat.effectif + "", times);
            infos.AddCell(cell1);
            infos.AddCell(cell2);
            cell1.Phrase = new Phrase("Moy classe", times);
            cell2.Phrase = new Phrase(moyenne + "", times);
            infos.AddCell(cell1);
            infos.AddCell(cell2);
            cell1.Phrase = new Phrase("Taux réussite", times);
            cell2.Phrase = new Phrase(stat.pourcentageAdmis, times);
            infos.AddCell(cell1);
            infos.AddCell(cell2);

            PdfPTable conclusion = new PdfPTable(1);
            conclusion.WidthPercentage = 100f;
            cell1 = new iTextSharp.text.pdf.PdfPCell();
            cell1.Border = Rectangle.NO_BORDER;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2 = new iTextSharp.text.pdf.PdfPCell();
            cell2.Border = Rectangle.NO_BORDER;
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            cell2.Phrase = new Phrase("SEQUENCE ", times);
            cell2.BackgroundColor = FontColour_header;
            cell1.Phrase = new Phrase("  ");
            conclusion.AddCell(cell1);
            conclusion.AddCell(cell2);
            cell1.Phrase = new Phrase("RQ:REUSSITE SI MOY >= 10/20", timeheader);
            conclusion.AddCell(cell1);
            cell1.Phrase = new Phrase("(" + stat.nbAdmis + " SUR " + stat.effectif + ")", times);
            conclusion.AddCell(cell1);

            PdfPCell cell = new iTextSharp.text.pdf.PdfPCell();
            cell.Colspan = 2;
            cell.AddElement(infos);
            table.AddCell(cell);
            cell = new iTextSharp.text.pdf.PdfPCell();
            cell.Rotation = 90;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_CENTER;
            cell.Phrase = new Phrase("SEXE \n REDOUBLANT", times);
            table.AddCell(cell);
            cell.Phrase = new Phrase("Total général", times);
            table.AddCell(cell);
            cell.Phrase = new Phrase("Moyenne générale", times);
            table.AddCell(cell);
            cell.Phrase = new Phrase("Rang", times);
            table.AddCell(cell);
            foreach (string code in codesanctions)
            {
                cell.Phrase = new Phrase(code, times);
                table.AddCell(cell);
            }
            cell.Phrase = new Phrase("NB.SOUS.MOY", times);
            table.AddCell(cell);
            cell = new iTextSharp.text.pdf.PdfPCell();
            cell.AddElement(conclusion);
            table.AddCell(cell);
            #endregion

            #region contenu des differentes lignes
            int ligne = 1;
            cell = new iTextSharp.text.pdf.PdfPCell();
            foreach (LigneRecapSeq l in recapitulatif)
            {
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Phrase = new Phrase("" + ligne++, times);
                table.AddCell(cell);
                cell.Phrase = new Phrase(l.nom, times);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);
                cell.Phrase = new Phrase(l.sexe_redoub.ToLower(), times);
                table.AddCell(cell);
                cell = new PdfPCell();
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Phrase = new Phrase(l.total + "", times);
                table.AddCell(cell);
                cell.Phrase = new Phrase(l.moyenne + "", times);
                table.AddCell(cell);
                cell.Phrase = new Phrase(l.rang + "", times);
                table.AddCell(cell);
                foreach (string code in codesanctions)
                {
                    try
                    {
                        cell.Phrase = new Phrase(l.sanctions[code].ToString(), timeheader);
                        table.AddCell(cell);
                    }
                    catch (Exception)
                    {
                        cell.Phrase = new Phrase("  ", timeheader);
                        table.AddCell(cell);
                    }
                }
                cell.Phrase = new Phrase(l.nb_sous_moyenne + "", times);
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(cell);
                cell.Phrase = new Phrase(l.mention + "", times);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(cell);
            }

            #endregion

            doc.Add(creerEntete());
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
            doc.Add(table);
            doc.Add(Chunk.NEWLINE);
            doc.Add(Chunk.NEWLINE);
            doc.Add(creerPieds());
            doc.Close();

            //SendToPrinter(Path.GetFullPath(docname));
            //File.Delete(Path.GetFullPath(docname));
            System.Diagnostics.Process.Start(Path.GetFullPath(docname));
        }
        catch (Exception)
        {
            MessageBox.Show("Une exception s'est produite lors de la création de l'état");
        }
    }

        public void synthese_resultat_sequentiel(ClasseBE classe, string sequence, Synthese synthese)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 10, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10, Font.BOLD);
                Font timeheader = new Font(bfTimes, 8);
                var FontColour_header = new BaseColor(134, 181, 232);
                Document doc = new Document(PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;
                PdfPTable table = new PdfPTable(6);
                table.WidthPercentage = 95f;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                float[] widths = new float[6] { 1f, 4f, 2f, 1f, 4f, 2f };
                table.SetWidths(widths);

                PdfPCell cellvide = new PdfPCell(new Phrase(" "));
                cellvide.Border = Rectangle.NO_BORDER;

                PdfPCell cell = new PdfPCell();

                #region creation de l'entete
                table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide);

                table.AddCell(cellvide);
                cell.Phrase = new Phrase("PERIODE", times);
                table.AddCell(cell);
                cell.Phrase = new Phrase("SEQUENCE " + sequence, times);
                table.AddCell(cell);
                table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide);
                table.AddCell(cellvide);
                cell.Phrase = new Phrase("CLASSE", times);
                table.AddCell(cell);
                cell.Phrase = new Phrase(classe.nomClasse, times);
                table.AddCell(cell);
                table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide);

                table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide);

                foreach (KeyValuePair<string, double> couple in synthese.synthese_classe)
                {
                    table.AddCell(cellvide);
                    cell.Phrase = new Phrase(couple.Key, times);
                    table.AddCell(cell);
                    cell.Phrase = new Phrase(couple.Value+" ", times);
                    table.AddCell(cell);
                    table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide);
                }

                table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide);

                for (int i = 0; i < synthese.synthese_garcon.Count(); i++)
                {
                    table.AddCell(cellvide);
                    cell.Phrase = new Phrase(synthese.synthese_garcon.ElementAt(i).Key, times);
                    table.AddCell(cell);
                    cell.Phrase = new Phrase(synthese.synthese_garcon.ElementAt(i).Value + " ", times);
                    table.AddCell(cell);
                    table.AddCell(cellvide);
                    cell.Phrase = new Phrase(synthese.synthese_fille.ElementAt(i).Key, times);
                    table.AddCell(cell);
                    cell.Phrase = new Phrase(synthese.synthese_fille.ElementAt(i).Value + " ", times);
                    table.AddCell(cell);
                }

                table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide);

                cell.Phrase = new Phrase("No", times);
                table.AddCell(cell);
                cell.Phrase = new Phrase("DIX PREMIERS", times);
                table.AddCell(cell);
                cell.Phrase = new Phrase("MOYENNE", times);
                table.AddCell(cell);
                cell.Phrase = new Phrase("No", times);
                table.AddCell(cell);
                cell.Phrase = new Phrase("DIX DERNIERS", times);
                table.AddCell(cell);
                cell.Phrase = new Phrase("MOYENNE", times);
                table.AddCell(cell);
                for (int i = 0; i < 10; i++)
                {
                    cell.Phrase = new Phrase(i + 1 + " ", times);
                    table.AddCell(cell);
                    if (i < synthese.synthese_premiers.Count)
                    {
                        cell.Phrase = new Phrase(synthese.synthese_premiers.ElementAt(i).Key, times);
                        table.AddCell(cell);
                        cell.Phrase = new Phrase(synthese.synthese_premiers.ElementAt(i).Value + " ", times);
                        table.AddCell(cell);
                    }
                    else {
                        cell.Phrase = new Phrase(" ", times);
                        table.AddCell(cell);
                        cell.Phrase = new Phrase(" ", times);
                        table.AddCell(cell);
                    }
                    cell.Phrase = new Phrase(i + 1 + " ", times);
                    table.AddCell(cell);
                    if (i < synthese.synthese_derniers.Count)
                    {
                        cell.Phrase = new Phrase(synthese.synthese_derniers.ElementAt(i).Key, times);
                        table.AddCell(cell);
                        cell.Phrase = new Phrase(synthese.synthese_derniers.ElementAt(i).Value + " ", times);
                        table.AddCell(cell);
                    }
                    else
                    {
                        cell.Phrase = new Phrase(" ", times);
                        table.AddCell(cell);
                        cell.Phrase = new Phrase(" ", times);
                        table.AddCell(cell);
                    }
                }
                table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide); table.AddCell(cellvide);


                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }

        internal void etatChangement(List<LigneChangeClasse> lignes, List<string> headers, string classe, int annee)
        {
            try
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                Font times = new Font(bfTimes, 8, Font.BOLD);
                Font timestitre = new Font(bfTimes, 10);
                Font timeheader = new Font(bfTimes, 9);
                Document doc = new Document(iTextSharp.text.PageSize.A4, 5, 5, 5, 25);
                PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(docname, System.IO.FileMode.Create));
                writer.PageEvent = new PDFFooter();
                doc.Open();

                Paragraph titre = new Paragraph(title.ToUpper(), timestitre);
                titre.Alignment = Element.ALIGN_CENTER;

                #region information sur la classe
                PdfPTable infos = new PdfPTable(4);
                float[] widths1 = new float[] { 1.5f, 4f, 1.5f, 2f };
                infos.HorizontalAlignment = Element.ALIGN_CENTER;
                infos.SetWidths(widths1);
                infos.WidthPercentage = 90f;
                PdfPCell cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Phrase = new iTextSharp.text.Phrase("Classe:".ToUpper(), timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase(classe, times);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase("Année:", timeheader);
                infos.AddCell(cell);
                cell.Phrase = new iTextSharp.text.Phrase((annee - 1) + " / " + annee, times);
                infos.AddCell(cell);
                #endregion

                #region contenu concernant les anonymats
                PdfPTable table = new PdfPTable(headers.Count);
                float[] widths = new float[] { 1f, 6f, 2f, 2f };
                table.SetWidths(widths);
                table.WidthPercentage = 90f;
                for (int j = 0; j < headers.Count; j++)
                {
                    table.AddCell(new Phrase(headers.ElementAt(j), times));
                }
                table.HeaderRows = 1;
                if (lignes != null)
                {
                    foreach (var item in lignes)
                    {
                        table.AddCell(new Phrase(item.numero.ToString(), timeheader));
                        table.AddCell(new Phrase(item.nom, timeheader));
                        table.AddCell(new Phrase(item.matricule, timeheader));
                        table.AddCell(new Phrase(item.codeclasse, timeheader));
                    }
                }
                #endregion

                doc.Add(creerEntete());
                doc.Add(Chunk.NEWLINE);
                doc.Add(titre);
                doc.Add(Chunk.NEWLINE);
                doc.Add(infos);
                doc.Add(Chunk.NEWLINE);
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(creerPieds());
                doc.Close();

                //SendToPrinter(Path.GetFullPath(docname));
                //File.Delete(Path.GetFullPath(docname));
                System.Diagnostics.Process.Start(Path.GetFullPath(docname));
            }
            catch (Exception)
            {
                MessageBox.Show("Une exception s'est produite lors de la création de l'état");
            }
        }
    }

    public class PDFFooter : PdfPageEventHelper
    {
        //template to contain the total number of pages
        PdfTemplate total;

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            total = writer.DirectContent.CreateTemplate(30, 16);
        }

        // write on start of each page
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
        }

        // write on end of each page
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            int pageN = writer.PageNumber;
            String text = "Page. " + pageN.ToString() + "/";

            PdfPTable tabFot = new PdfPTable(new float[] { 2f, 2f, 0.5f, 0.5f });
            tabFot.DefaultCell.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
            tabFot.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell;
            tabFot.TotalWidth = document.Right - document.Left + 10; //600f;
            cell = new PdfPCell(new Phrase("©2016 School Brain - Tel (+237) 679 56 42 58/696 26 94 00", FontFactory.GetFont(FontFactory.TIMES, 6, iTextSharp.text.Font.NORMAL)));
            cell.Border = Rectangle.TOP_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            tabFot.AddCell(cell);
            cell = new PdfPCell(new Phrase("Imprimé le " + DateTime.Today.ToShortDateString() + " à "+DateTime.Now.ToShortTimeString(), FontFactory.GetFont(FontFactory.TIMES, 6, iTextSharp.text.Font.NORMAL)));
            cell.Border = Rectangle.TOP_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabFot.AddCell(cell);
            cell = new PdfPCell(new Phrase(text, FontFactory.GetFont(FontFactory.TIMES, 6, iTextSharp.text.Font.NORMAL)));
            cell.Border = Rectangle.TOP_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabFot.AddCell(cell);
            cell = new PdfPCell(iTextSharp.text.Image.GetInstance(total));
            cell.Border = Rectangle.TOP_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(0, -1, 0, 20, writer.DirectContent);
        }

        //write on close of document
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            ColumnText.ShowTextAligned(total, Element.ALIGN_LEFT,
                    new Phrase(Convert.ToString(writer.PageNumber - 1), FontFactory.GetFont(FontFactory.TIMES, 6, iTextSharp.text.Font.NORMAL)),
                    2, 6, 0);
        }
    }

    public class PDFFooter1 : PdfPageEventHelper
    {
        //template to contain the total number of pages
        PdfTemplate total;
        string watermarkText = string.Empty;

        public PDFFooter1()
        {
        }

        public PDFFooter1(string watermarkText)
        {
            // TODO: Complete member initialization
            this.watermarkText = watermarkText;
        }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            total = writer.DirectContent.CreateTemplate(30, 16);
        }

        // write on start of each page
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
        }

        // write on end of each page
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            int pageN = writer.PageNumber;
            String text = "Page. " + pageN.ToString() + "/";

            PdfPTable tabFot = new PdfPTable(new float[] { 2f, 2f, 0.5f, 0.5f });
            tabFot.DefaultCell.Border = Rectangle.TOP_BORDER | Rectangle.BOTTOM_BORDER;
            tabFot.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell cell;
            tabFot.TotalWidth = document.Right - document.Left + 10; //600f;
            cell = new PdfPCell(new Phrase("©2016 School Brain", FontFactory.GetFont(FontFactory.TIMES, 6, iTextSharp.text.Font.NORMAL)));
            cell.Border = Rectangle.TOP_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            tabFot.AddCell(cell);
            cell = new PdfPCell(new Phrase("Imprimé le " + DateTime.Today.ToShortDateString() + " à " + DateTime.Now.ToShortTimeString(), FontFactory.GetFont(FontFactory.TIMES, 6, iTextSharp.text.Font.NORMAL)));
            cell.Border = Rectangle.TOP_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            tabFot.AddCell(cell);
            cell = new PdfPCell(new Phrase(text, FontFactory.GetFont(FontFactory.TIMES, 6, iTextSharp.text.Font.NORMAL)));
            cell.Border = Rectangle.TOP_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabFot.AddCell(cell);
            cell = new PdfPCell(iTextSharp.text.Image.GetInstance(total));
            cell.Border = Rectangle.TOP_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            tabFot.AddCell(cell);
            tabFot.WriteSelectedRows(0, -1, 0, 20, writer.DirectContent);


            float fontSize = 40;
            float xPosition = 300;
            float yPosition = 400;
            float angle = 45;
            try
            {
                PdfContentByte under = writer.DirectContentUnder;
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);
                under.BeginText();
                under.SetColorFill(BaseColor.LIGHT_GRAY);
                under.SetFontAndSize(baseFont, fontSize);
                under.ShowTextAligned(PdfContentByte.ALIGN_CENTER, watermarkText, xPosition, yPosition, angle);
                under.EndText();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        //write on close of document
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            ColumnText.ShowTextAligned(total, Element.ALIGN_LEFT,
                    new Phrase(Convert.ToString(writer.PageNumber - 1), FontFactory.GetFont(FontFactory.TIMES, 6, iTextSharp.text.Font.NORMAL)),
                    2, 6, 0);
        }
    }
}
