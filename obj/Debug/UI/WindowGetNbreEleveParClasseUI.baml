   M S B A M L   `   `   `  ����  School Brain,    &Ecole.UI.WindowGetNbreEleveParClasseUI   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$8  2School brain : Consulter les Effectifs des classes��5   	   $	��621��6D   $	��610��6Q    � WindowStartupLocation$ CenterScreen=�6]   ���	#FFD0DAE5.��5	      � ���	#FFC8DDE2$��Center=�6"   $��Center=�6?   $	��584��6Z   $	��583��6f   .+�5
   
   �� -   5        $��
rectangle1��5
   O   $	��146��6   $
��Left=�6!   $��	20,16,0,0q�6<   
���   �$	��Top=�6p   $	��334��6�   ���	#FF86B5E86�   �� 5   
   -   5        $��	lblClasse��5   _   $��
Classe (*)��6   $��28��6%   $
��Left=�61   $��	35,31,0,0q�6L   $	��Top=�6p   $��76��6�   �� 5   
   -   5        $��	cmbClasse��5   O   $��27��6   $��Right=�6   $��
0,31,266,0q�6;   $	��Top=�6`   $	��216��6x   �� 5   
   -   5        $��lblAnnee��5   ^   $��
Année (*)��6   $��28��6$   $
��Left=�60   $��	35,73,0,0q�6K   $	��Top=�6n   �� 5   
   -   5        $��txtAnneeScolaire��5   4   $��26��6   $��102,75,420,0q�6   $	��Top=�6L   6�   �� 5   
   -   5        $��label2��5   X   $��/��6   $��26��6   $��Right=�6(   $��
0,78,394,0q�6D   $	��Top=�6f   $��17��6~   �� 5   
   -   5        $��txtAnnee��5   _   {�.  $��26��6$   $
��Left=�60   $��
194,75,0,0q�6K   $	��Top=�6o   $��62��6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5        $��grdListeEffectif��5   }      AutoGenerateColumns .     
IsReadOnly . $	��319��6A   $
��Left=�6N   $��
19,186,0,0q�6i   $	��Top=�6�   $	��553��6�    $� IsVirtualizing .  ��6�   �� *ListeEffectifClasses+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Classe��6D      Width5  /System.Windows.Controls.DataGridLengthConverter$	 265 6T   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *
codeClasse+6T     5      $ Effectif��6B   $	 275 6T    6%   �� *
effectif+6T   5      � 5   
   -	   5        $��grid1��5   I   $��44��6   $
��Left=�6   $��	0,537,0,0q�66   $	��Top=�6V   $	��584��6n   ���	#FF86B5E8.+�5      �� -
   5        $��	cmdFermer��5   a   $��Fermer��6   $��23��6&   $
��Left=�62   $��
459,11,0,0q�6M   $	��Top=�6r   $	��115��6�   6�   �� 5      -   5        $��cmdImprimer��5   b   $��Imprimer��6   $��23��6(   $
��Left=�64   $��	21,11,0,0q�6O   $	��Top=�6u   $	��117��6�   6�   5      �� 5   
   -   5        $��
cmdValider��5   ^   $��Valider��6   $��24��6#   $
��Left=�6/   $��
35,129,0,0q�6J   $	��Top=�6p   $	��125��6�   6�   �� 5   
   -   5        $��
cmdAnnuler��5   _   $��Annuler��6   $��24��6#   $
��Left=�6/   $��191,129,0,0q�6J   $	��Top=�6q   $	��127��6�   6�   �� 5   
   -   5        $��label1��5   ~   $��Total : ��6   $��15(�6#   $
��Bold"�61   $��28��6C   $
��Left=�6O   $��
14,505,0,0q�6j   $	��Top=�6�   �� 5   
   -   5        $��lblTotal��5   w   $��0��6   $��15(�6   $
��Bold"�6*   $��28��6<   $
��Left=�6H   $��
77,506,0,0q�6c   $	��Top=�6�   5      5       