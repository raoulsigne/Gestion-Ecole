   M S B A M L   `   `   `  ����  School Brain*    $Ecole.UI.WindowListeEleveParClasseUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$=  7School brain : Creer / Editer les Effectifs des classes��5   	   $	��621��6I   $	��884��6V    � WindowStartupLocation$ CenterScreen=�6b   ���	#FFD0DAE5 � 
ResizeMode$ CanMinimize=�6�    � 	Resources 5
      :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value ��Name �� Path	 �� StringFormat
 �� RelativeSource �� AncestorType(  "System.Windows.Controls.DatePicker��%�   '        �� 5   
   "  6   .�5      ��  � �� 5      �� 5      .R�5      �� $ PART_TextBox��6"   ��5      �� $ SelectedDate�$	 
dd-MM-yyyy��
 �� "  5      5      5      5      .��5      � ���	#FFC8DDE2$��Center=�6"   $��Center=�6?   $	��572��6Z   $	��868��6g   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��
-2,3,0,414q�6.   .+�5      �� -   5        $��	groupBox1��5   G   $��Informations obligatoires��6   $	��121��6:   $	��380��6X   $��15,15q�6d   .��5      � -   5        $��grid1��5   #   $��68��6   $	��342��60   ���	#FF86B5E8 � RowDefinitions 5      �� 5      �� 5       � ColumnDefinitions 5       �� 5!      $
��124* �6+   �� 5"      $
��218* �6+   .+�5$      �� -   5        $��	lblClasse��5$   i   $��
Classe (*)��6   $��28��61   $
��Left=�6=   $��0,1,0,0q�6X   $	��Top=�6z   $��94��6�   �� 5%      -   5        $��lblAnnee��5%   h   $��
Année (*)��6   $��28��60   $
��Left=�6<   $��0,6,0,0q�6W   $	��Top=�6x   $��1��6�   $��94��6�   �� 5&      -   5        $��	cmbClasse��5&   Y   $��25��6   $��Right=�6+   $��0,4,21,0q�6G   $	��Top=�6j   $	��197��6�   $��1��6�   �� 5'      -   5        $��txtAnneeScolaire��5'   =   $��23��6   $��	0,4,165,0q�6*   $	��Top=�6U   $��1��6�   $��1��6�   6�   �� 5(      -   5        $��label7��5(   c   $��/��6   $��23��6(   $��Right=�64   $��	0,4,142,0q�6P   $	��Top=�6q   $��17��6�   $��1��6�   $��1��6�   �� 5)      -   5        $��txtAnnee��5)   O   {�.  $��22��60   $��	82,6,76,0q�6<   $	��Top=�6_   $��1��6w   $��1��6�   6�   5*      5+      � 5,      -	   5        $��grid2��5,       $	��106��6   $	��118��6-   $��10q�69   ���	#FF86B5E8 5-      �� 5.      �� 5/       51      �� 52      $	��36* �6'   �� 53      $��8* �6'   �� 54      $	��74* �6'   .+�56      �� -
   5        $��
cmdValider��56   d   $��Valider��6   $��31��6+   $
��Left=�67   $��6,14,0,0q�6R   $	��Top=�6v   $	��106��6�   $��3��6�   �� 57      -   5        $��
cmdAnnuler��57   +   $��Annuler��6   $	��106��6=   $��3��6b   $��	6,11,6,14q�6v   $��1��6�   58      59      &   System.Windows.Controls.DataGrid  5:   
   -   5        $��grdListeEffectif��5:   }      AutoGenerateColumns .     
IsReadOnly . $	��330��6A   $
��Left=�6N   $��
12,172,0,0q�6i   $	��Top=�6�   $	��844��6�    $� IsVirtualizing .  ��6�   �� *ListeEleves+   Columns 5;      0  *System.Windows.Controls.DataGridTextColumn  5<      ,  &System.Windows.Controls.DataGridColumn   Header$ No��6@      Width5  /System.Windows.Controls.DataGridLengthConverter$ 70 6L   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *numero+6L     5=      $	 Nom��6=   $	 200 6J    6%   �� *nom+6J     5>      $ 	Matricule��6C   $	 100 6V    6%   �� *	matricule+6V     5?      $ 
Catégorie��6C   $ 70 6V    6%   �� *	categorie+6V     5@      $
 Sexe��6>   $ 80 6L    6%   �� *sexe+6L     5A      $
 Etat��6>   $ 80 6L    6%   �� *etat+6L     5B      $ date De Naissance��6M   $	 100 6h    6%   �� *dateNaissanceString+6h     5C      $ Téléphone��6C   $	 100 6V    6%   �� *	telephone+6V     5D      $ Téléphone du père��6C   $	 100 6^    6%   �� *	telParent+6^     5E      $ Email��6?   $	 150 6N    6%   �� *email+6N     5F      $ Adresse��6A   $	 100 6R    6%   �� *	adresse+6R   5H      � 5I   
   -   5        $
��grid��5I      $��40��6   $��	0,532,0,0q�6'   ���	#FF86B5E8 5J      �� 5K      $	��20* �6     5M      �� 5N      $
��182* �6#   �� 5O      $	��88* �6#   �� 5P      $	��95* �6#   �� 5Q      $
��175* �6#   .+�5S      �� -   5        $��	cmdFermer��5S   _   $��Fermer��6   $��29��6&   $
��Left=�62   $��70,5,0,0q�6M   $	��Top=�6p   $	��152��6�   $��3��6�   �� 5T      -   5        $��cmdImprimer��5T   a   $��Imprimer��6   $��28��6(   $
��Left=�64   $��12,6,0,0q�6O   $	��Top=�6t   $	��142��6�   6�   5U      �� 5V   
   -   5        $��label1��5V   ~   $��Total : ��6   $��15(�6#   $
��Bold"�61   $��28��6C   $
��Left=�6O   $��
10,500,0,0q�6j   $	��Top=�6�   �� 5W   
   -   5        $��lblTotal��5W   w   $��0��6   $��15(�6   $
��Bold"�6*   $��28��6<   $
��Left=�6H   $��
73,501,0,0q�6c   $	��Top=�6�   5X      5Y      