   M S B A M L   `   `   `  ����|  xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting  5   	    School Brain.   (Ecole.UI.StatComparaisonResultatNiveauUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   �chartingToolkit{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit     5   	     � Title$7  1Statistique comparative des resultats d'un niveau��5   	   $	��751��6C   $
��1155��6P    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.   .��5      �� ���	#FFC8DDE2$
��Auto=�6*   $��0,0,0,0q�6M   $	��715��6^   $
��1133��6k   .��5   
   �� -   5        $��
stackpanel��5   A   $��0,2,0,28q�6   $	��712��6'   $
��1114��64   .+�5	      � .+�5
      �� -   5        $��	groupBox1��5
   Y   $-��'Information sur la classe et la periode��6   $	��119��6L   $	��537��6j   $��33,10,10,10q�6v   $
��Left=�6�   .��5      � -   5        $��grid1��5   '   $��89��6   $	��499��64   ���	#FF86B5E8 � RowDefinitions 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5      �� 5      �� 5      .+�5      �� -   5        $��lblAnnee��5   g   $��Année��6    $��1��60   $��28��6@   $
��Left=�6L   $	��Top=�6w   $	��141��6�   $��Center=�6�   $��22,2,0,0q�6�   �� 5      -   5        $��txtAnnee��5   �   {�.  $��1��64   $��1��6D   $��23��6Q   $
��Left=�6]   $��87,2,0,0q�6x   $	��Top=�6�   $��76��6�   �� 5      -   5        $��txtAnneeScolaire��5   [   $��23��6"   $
��Left=�6.   $��23,1,0,0q�6I   $	��Top=�6s   $��60��6�   $��1��6�   $��1��6�   6�   �� 5      -   5        $��
cmdAnnuler��5   z   $��Annuler��6!   $��2��63   $��23��6@   $
��Left=�6L   $��	189,0,0,0q�6g   $	��Top=�6�   $	��120��6�   $��2��6�   �� 5      -   5        $��label1��5   k   $��Période��6    $��28��62   $
��Left=�6>   $��51,2,0,0q�6Y   $	��Top=�6y   �� 5      -	   5        $��	cmbExamen��5   i   $��1��6#   $��23��60   $
��Left=�6<   $��18,0,0,0q�6W   $	��Top=�6z   $	��120��6�   �� 5      -
   5        $��cmdImprimer��5   n   $��Imprimer��6!   $��23��64   $
��Left=�6@   $��	102,0,0,0q�6[   $	��Top=�6�   $	��142��6�   $��1��6�   $��2��6�   6�   �� 5      -   5        $��cmdOK��5   g   $��OK��6!   $��23��6.   $
��Left=�6:   $��16,1,0,0q�6U   $	��Top=�6t   $	��122��6�   $��2��6�   6�   5      5      �� 5      -   5        $��image1��5   $   $��67��6   $
��Fill=�62   $<��6/School%20Brain;component/Images/Logo-Brain-Studio.png��6A   $��954,37,33,59q�6�   5       � 5!      -   5        $��gridChartColumn��5!       $
��1114��6   $	��283��67   .+�5"      >   8System.Windows.Controls.DataVisualization.Charting.Chart  -   5        $��columnChart��5"   P   $	��279��6(   $
��Left=�65      Title$, &Comparaison des résultats des niveaux��6c   $
��1080��6�      Series. 5#      E   ?System.Windows.Controls.DataVisualization.Charting.ColumnSeries  -   5        $��matieres��5#   C   ?   9System.Windows.Controls.DataVisualization.Charting.Series   Title$  %Admis��63   H   BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries   DependentValuePath$ Value��6S   	   IndependentValuePath$		 Key��6n   
   ItemsSource
 6�   �� 5$      5%      � 5&      -   5        $��gridChartLine��5&       $
��1114��6   $	��285��65   .+�5'        -   5        $��	lineChart��5'   P   $	��279��6(   $
��Left=�65   $, &Comparaison des résultats des niveaux��6a   $
��1080��6�   . 5(      C   =System.Windows.Controls.DataVisualization.Charting.LineSeries  -   5        $��matieresLine��5(   A   $  %Admis��61   $ Value��6U   $		 Key��6p   
 6�   �� 5)      5*      5+      5,      5-      