   M S B A M L   `   `   `  ����|  xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting  5   	    School Brain    Ecole.UI.BilanSequentielUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   �chartingToolkit{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit     5   	     � Title$#  Bilan Sequentiel d'une classe��5   	   $	��751��6/   $
��1155��6<    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.   .��5      �� ���	#FFC8DDE2$
��Auto=�6*   $��0,0,0,0q�6M   $	��715��6^   $
��1133��6k   .��5   
   �� -   5        $��
stackpanel��5   A   $��0,2,0,28q�6   $	��712��6'   $
��1114��64   .+�5	      � .+�5
      �� -   5        $��	groupBox1��5
   Y   $-��'Information sur la classe et la periode��6   $	��119��6L   $	��615��6j   $��33,10,10,10q�6v   $
��Left=�6�   .��5      � -   5        $��grid1��5   '   $��89��6   $	��572��64   ���	#FF86B5E8 � RowDefinitions 5      �� 5      �� 5      �� 5       � ColumnDefinitions 5      �� 5      �� 5      �� 5      .+�5      �� -   5        $��lblAnnee��5   g   $��Année��6    $��2��60   $��28��6@   $
��Left=�6L   $	��Top=�6w   $	��141��6�   $��Center=�6�   $��25,2,0,0q�6�   �� 5      -   5        $��txtAnnee��5   �   {�.  $��2��64   $��1��6D   $��23��6Q   $
��Left=�6]   $��90,2,0,0q�6x   $	��Top=�6�   $��76��6�   �� 5      -   5        $��txtAnneeScolaire��5   [   $��23��6"   $
��Left=�6.   $��26,1,0,0q�6I   $	��Top=�6s   $��60��6�   $��2��6�   $��1��6�   6�   �� 5      -   5        $��	lblClasse��5   j   $��Classe��6    $��28��61   $
��Left=�6=   $��76,2,0,0q�6X   $	��Top=�6{   �� 5      -   5        $��	cmbClasse��5   i   $��1��6#   $��23��60   $
��Left=�6<   $��35,0,0,0q�6W   $	��Top=�6z   $	��122��6�   �� 5      -	   5        $��cmdOK��5   t   $��OK��6!   $��2��6.   $��23��6;   $
��Left=�6G   $��35,5,0,0q�6b   $	��Top=�6�   $	��122��6�   6�   �� 5      -
   5        $��
cmdAnnuler��5   �   $��Annuler��6!   $��1��63   $��2��6C   $��23��6P   $
��Left=�6\   $��28,4,0,0q�6w   $	��Top=�6�   $	��120��6�   6�   �� 5      -   5        $��label1��5   {   $��Période��6    $��1��62   $��28��6B   $
��Left=�6N   $��62,2,0,0q�6i   $	��Top=�6�   �� 5      -   5        $��	cmbExamen��5   y   $��1��6#   $��1��63   $��23��6@   $
��Left=�6L   $��29,0,0,0q�6g   $	��Top=�6�   $	��120��6�   �� 5      -   5        $��cmdImprimer��5   m   $��Imprimer��6!   $��23��64   $
��Left=�6@   $��25,3,0,0q�6[   $	��Top=�6�   $	��142��6�   $��2��6�   $��2��6�   6�   5       5!      �� 5"      -   5        $��image1��5"   $   $��67��6   $
��Fill=�62   $<��6/School%20Brain;component/Images/Logo-Brain-Studio.png��6A   $��911,37,33,59q�6�   5#      � 5$      -   5        $��gridChartColumn��5$       $
��1114��6   $	��283��67   .+�5%      >   8System.Windows.Controls.DataVisualization.Charting.Chart  -   5        $��columnChart��5%   P   $	��279��6(   $
��Left=�65      Title$( "Variation des moyennes par matiere��6c   $
��1080��6�      Series. 5&      E   ?System.Windows.Controls.DataVisualization.Charting.ColumnSeries  -   5        $��matieres��5&   A   ?   9System.Windows.Controls.DataVisualization.Charting.Series   Title$ Admis��63   H   BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries   DependentValuePath$ Value��6Q   	   IndependentValuePath$		 Key��6l   
   ItemsSource
 6�   �� 5'      5(      � 5)      -   5        $��gridChartLine��5)       $
��1114��6   $	��285��65   .+�5*        -   5        $��	lineChart��5*   P   $	��279��6(   $
��Left=�65   $( "Variation des moyennes par matiere��6a   $
��1080��6�   . 5+      C   =System.Windows.Controls.DataVisualization.Charting.LineSeries  -   5        $��matieresLine��5+   ?   $ Admis��61   $ Value��6S   $		 Key��6n   
 6�   �� 5,      5-      5.      5/      50      